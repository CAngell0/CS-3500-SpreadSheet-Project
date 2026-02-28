// <author> Carson Angell </author>
// <date> 2/27/2026 </date>

namespace SpreadsheetTests;

using Spreadsheet;
using System.Text;

/// <summary>
///     Holds a stress test that creates a long, single dependency chain. Then tests how long it takes to construct the chain evaluate
///     the whole thing in one go.
/// </summary>
[TestClass]
public class LongDependencyChainStressTest {
    /// <summary> How many cells are in the spreadsheet </summary>
    private static readonly int CELL_COUNT = 750;
    /// <summary> Used to add in the cell formulas to the spreadsheet in reverse order so that evaluate as intended </summary>
    private readonly Stack<string> _cellContents = new();
    /// <summary> Tracks the final expected output when constructing the spreadsheet </summary>
    private double _expectedFinalValue;

    /// <summary>
    ///     This stress test adds a very long chain of formulas that depend on the last created cell. Then evaluates the cells all at once.
    ///     Here are the steps the algorithm takes. Steps 1-2 are performed in the SetUp method before the test begins. This is so it only
    ///     tests how long it takes to construct and evaluate the spreadsheet. Not generating the random formulas.
    ///     <list type="number">
    ///         <item>
    ///             <b>Create a long chain of formulas that would depend on the last created cell.</b>
    /// 
    ///             <para>
    ///                In a nutshell, it puts a bunch of formulas in a single row
    ///                in a spreadsheet, with each cell depending on the cell to its left.
    ///                This is what the dependency graph would look like:   A1 &lt;- A2 &lt;- A3 &lt;- ... &lt;- A499 &lt;- A500.
    ///                Each formula follows the same structure: =[DependeeCell][RandomOperator][RandomNumber]. For example: A3+6.23 or A236/47.23.
    ///                Every random number is number ranging from 0.00 to 50.00. It's a random decimal rounded to two decimal spots.
    ///             </para>
    ///             <para><i>
    ///                 Note: the first cell in the chain is a random 0.00 - 50.00 double. It is not a formula
    ///             </i></para>
    ///             <para>| </para>
    ///         </item>
    ///         <item>
    ///             <b>Calculates an expected final value along with way.</b> 
    /// 
    ///             <para>
    ///                 Since the last formula will depend on the result of the first cell.
    ///                 That means its value will be the final value after calculating all the cells. While creating each of the formulas,
    ///                 the test keeps track of what that final value would be by applying the operation to a tracking local variable.
    ///                 </para>
    ///             <para>| </para>
    ///         </item>
    ///         <item>
    ///             <b>Call SetContentsOfCell in reverse order of the dependency graph.</b>
    ///             
    ///             <para>
    ///                 By loading the formulas into the sheet in reverse order
    ///                 (i.e. A500 formula then A499 formula then A498 formula). Each cell will have a FormulaError value since it depends on cells
    ///                 that aren't added yet. For example, A500 depends on A499. But since A500 gets added before A499, it doesn't have a value and 
    ///                 will be a FormulaError. But once the first cell in the chain is added (which is a double and not a formula), the dependency graph 
    ///                 will be able to be evaluated, so it will traverse through all the cells added and evaluate them one by one, all in one go. 
    ///                 <i>That's what I'm stress testing.</i>
    ///             </para>
    ///         </item>
    ///     </list>
    /// </summary>
    [TestMethod]
    [Timeout(8000, CooperativeCancellation = true)]
    public void Spreadsheet_StressTestOnLongDependencyChain_Success() {
        int size = _cellContents.Count;
        Spreadsheet sheet = new();
        // Adds the created formulas to the sheet in reverse order (A500 -> A1)
        for (int i = size; i > 0; i--) sheet.SetContentsOfCell($"A{i}", _cellContents.Pop());
        Assert.AreEqual(_expectedFinalValue, sheet.GetCellValue($"A{size}"));
    }


    /// <summary>
    ///     Performs steps 1-2 as documented in <see cref="Spreadsheet_StressTestOnLongDependencyChain_Success"/> 
    /// </summary>
    [TestInitialize]
    public void SetUp() {
        const string OPERATORS = "+-*/";
        Random random = new();
        StringBuilder builder = new();

        // Creates a random value of A1 and uses it to create a final expected value
        _expectedFinalValue = Math.Round(random.NextDouble() * 5000) / 100;
        _cellContents.Push($"{_expectedFinalValue}");

        // The for loop variable keeps track of which cell the next formula will depend on (i.e. A{i} -> A23)
        for (int i = 1; i <= CELL_COUNT; i++) {
            // Generates a random decimal from 0.00 - 50.00 and picks a random operation to perform
            double operand = Math.Round(random.NextDouble() * 5000) / 100;
            char oper = OPERATORS.ElementAt((int)random.NextInt64(0, 4));

            // Updates the expected value based on the random operation that was chosen
            switch (oper) {
                case '+': _expectedFinalValue += operand; break;
                case '-': _expectedFinalValue -= operand; break;
                case '*': _expectedFinalValue *= operand; break;
                case '/': _expectedFinalValue /= operand; break;
            }

            // Builds the formula string under the syntax '=A{i} [+-*/] {operand}'  (i.e. A4*32.56)
            builder.Append('=');
            builder.Append('A');
            builder.Append(i);
            builder.Append(oper);
            builder.Append(operand);

            // Puts the formula on a stack that will later be added to the sheet
            _cellContents.Push(builder.ToString());
            builder.Clear();
        }
    }
}







/// <summary>
///     Holds a stress test that creates a randomly generated spreadsheet with a more complex dependency graph.
///     Dependencies are not long in a straight line like the other test. But in a more traditional graph
///     structure that's random. It tests how long it takes to construct and evaluate the spreadsheet
///     with that structure and formulas.
/// </summary>
[TestClass]
public class LongDependencyGraphStressTest { 
    /// <summary> How many cells there generated in the spreadsheet </summary>
    private static readonly int CELL_COUNT = 10000;
    /// <summary> Stores the randomly generated formula before they are added to the sheet </summary>
    private readonly Dictionary<string, string> cellContents = new();
    /// <summary> Expected values of the cells are calculated on the fly while the formulas are generated </summary>
    private readonly Dictionary<string, double> expectedValues = new();
    /// <summary> 
    ///     Generated formulas pick a cell from this list at random to be its dependee. With the way this list is built,
    ///     iterating through it in reverse is equivalent to performing a DFS traversal on the dependency graph. 
    ///     That is used when constructing the spreadsheet the same way as the previous test. So the when the last
    ///     cell is added, the whole sheet gets evaluated in one go. See <see cref="Spreadsheet_HeavyEvaluateStressTestInLargeDependencyGraph_Success"/>
    ///     for more details.
    /// </summary>
    private readonly List<string> cellSelecting = [ "A1" ];

    /// <summary>
    ///     Randomly generates a spreadsheet with a more complex dependency graph. Dependencies aren't in a straight line like
    ///     the last stress test. The structure and layout of the graph is random. Along with its contents and values. The
    ///     test follows these steps. Steps 1-2 are performed in a setup method before the test is ran. That way the generation
    ///     of the random formulas does not take up timeout budget.
    ///     <list type="number">
    ///         <item>
    ///             <b> Randomly generate a complete, acyclic spreadsheet graph </b>
    ///             
    ///             <para>
    ///                 Starts with a root node 'A1' that gets assigned a random double ranging from 0.00 to 49.99. Then it generates
    ///                 a random cell name ranging from A1 -> ZZ500. The cell name gets checked to make sure it's unique (this avoids cycles).
    ///                 Then it will select a random cell that has already been defined. For example, at the first iteration it will select A1.
    ///                 On the second iteration will will select either A1 or the newly made cell from last iteration. All cells are kept track of.
    ///             </para>
    /// 
    ///             <para>
    ///                 After that it will randomly generate a formula with the same syntax as the last test; with the selected cell followed by
    ///                 a random operator and a random number from 0.00 - 49.99 (i.e. CK173+32.89). Cells being generated in this way will create
    ///                 a complete acyclic graph with nothing but formulas.
    ///             </para>
    ///             <para>| </para>
    ///         </item>
    ///         <item>
    ///             <b> Calculate what the expected values of the cells </b>
    /// 
    ///             <para>
    ///                 After the cell and formula have been generated, it will apply the operation to the selected cell's estimated value and create
    ///                 its own estimated value for the new cell. This happens iteratively as it generates new cells. By the end of the algorithm, we have
    ///                 a dictionary with every generated cell's predicted value. Which is referenced in assertions.
    ///             </para>
    ///             <para>| </para>
    ///         </item>
    ///         <item>
    ///             <b> Evaluate and construct spreadsheet and check expected values </b>
    ///             
    ///             <para>
    ///                 Newly generated formulas choose their dependee cell by simply pulling a random index on a List<string> of already made cells.
    ///                 Iterating through this list in reverse is the equivalent of performing a DFS traversal on the dependency graph. This is the order
    ///                 the cells get added to the testing spreadsheet. That way when the root node is added to the spreadsheet (A1), all the formulas
    ///                 get evaluated in one go. This construction and evaluation is what's timed in the test.
    ///             </para>
    ///         </item>
    ///     </list>
    /// </summary>
    [TestMethod]
    [Timeout(3000, CooperativeCancellation = true)]
    public void Spreadsheet_HeavyEvaluateStressTestInLargeDependencyGraph_Success() {
        // Constructs and evaluates the random spreadsheet
        Spreadsheet sheet = new();
        for (int i = cellSelecting.Count - 1; i >= 0; i--) sheet.SetContentsOfCell(cellSelecting[i], cellContents.GetValueOrDefault(cellSelecting[i]) ?? "");
        // Asserts that all the values match what's to be expected
        foreach (string key in expectedValues.Keys) Assert.AreEqual(expectedValues.GetValueOrDefault(key), sheet.GetCellValue(key));
    }

    [TestInitialize]
    public void SetUp() {
        // How many cells in the sheet and dependency graph
        const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string OPERATORS = "+-*/";

        Random random = new();
        StringBuilder builder = new();
        HashSet<string> cellChecking = new(); // Used to make sure we don't get duplicate cell names and cause cyclical exceptions

        // Assigns the root of the dependency graph to a random value
        expectedValues.Add("A1", Math.Round(random.NextDouble() * 5000) / 100);
        cellContents.Add("A1", $"{expectedValues.GetValueOrDefault("A1")}");

        // Small helper method used to generate a random cell name ranging from A1 -> ZZ500
        string GenerateRandomCellName() => $"{ALPHABET[random.Next(0, 26)]}{ALPHABET[random.Next(0, 26)]}{random.Next(1,500)}";

        for (int i = 0; i < CELL_COUNT; i++) {
            // Generates a new cell name and makes sure its unique
            string newCellName = GenerateRandomCellName();
            while (cellChecking.Contains(newCellName)) newCellName = GenerateRandomCellName();

            // Select a random cell as the formula dependee and add the new cell to the selection and checking pool
            string selectedCell = cellSelecting[random.Next(0, cellSelecting.Count)];
            cellSelecting.Add(newCellName);
            cellChecking.Add(newCellName);

            // Select a random operator and generate a random operand to make the formula
            double operand = Math.Round(random.NextDouble() * 5000) / 100;
            char oper = OPERATORS.ElementAt((int)random.NextInt64(0, 4));

            // Applies the generated operation to the dependee's value
            // This is what calculates the expected values of every cell on the fly
            switch (oper) {
                case '+': 
                    expectedValues.Add(newCellName,
                        expectedValues.GetValueOrDefault(selectedCell) + operand);
                    break;
                case '-': 
                    expectedValues.Add(newCellName,
                        expectedValues.GetValueOrDefault(selectedCell) - operand);
                    break;
                case '*': 
                    expectedValues.Add(newCellName,
                        expectedValues.GetValueOrDefault(selectedCell) * operand);
                    break;
                case '/': 
                    expectedValues.Add(newCellName,
                        expectedValues.GetValueOrDefault(selectedCell) / operand);
                    break;
            }

            // Create the formula string
            builder.Append('=');
            builder.Append(selectedCell);
            builder.Append(oper);
            builder.Append(operand);

            // Add the formula string to the cell contents and clear the string builder
            cellContents.Add(newCellName, builder.ToString());
            builder.Clear();
        }
    }
}
