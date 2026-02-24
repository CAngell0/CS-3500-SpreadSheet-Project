// <author> Carson Angell </author>
// <date> 2/11/2026 </date>

namespace SpreadsheetTests;

using Spreadsheet;
using Formula;

[TestClass]
public class SpreadsheetSetCellContentsTests {
    /// <summary> Helper method that creates a spreadsheet with only one cell. </summary>
    /// <returns> 
    ///     Newly initialized spreadshet with the following cells:
    ///     <list type="bullet">
    ///         <item>Name: "B7"  |  Value: (double) 20</item>
    ///     </list> 
    /// </returns>
    private static Spreadsheet CreateSingleCellSheet() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("B7", "20");
        return sheet;
    }

    /// <summary> 
    ///     Helper method that creates a spreadsheet with only one cell. The cell that's in
    ///     the spreadsheet is made with the two parameters. Only that cell will be inside 
    ///     the spreadsheet. Assumes that the cell contents is either a string, double, or Formula.
    /// </summary>
    /// <param name="cellName"> The name of the cell </param>
    /// <param name="contents"> The contents of the cell </param>
    /// <returns> Newly initialized spreadshet with the provided cell </returns>
    private static Spreadsheet CreateSingleCellSheet(string cellName, string contents) {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell(cellName, contents);
        return sheet;
    }

    /// <summary> Helper method that creates a spreadsheet with multiple cells inside it. </summary>
    /// <returns>
    ///     Newly initialized spreadshet with the following cells:
    ///     <list type="bullet">
    ///         <item>Name: "B7"  |  Value: (double) 20</item>
    ///         <item>Name: "C5"  |  Value: (string) "Hello World"</item>
    ///         <item>Name: "D9"  |  Value: (Formula) "56 * 70.3 + d6"</item>
    ///     </list> 
    /// </returns>
    private static Spreadsheet CreateMultiCellSheet() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("B7", "20");
        sheet.SetContentsOfCell("C5", "Hello World");
        sheet.SetContentsOfCell("D9", "=56 * 70.3 + d6");
        return sheet;
    }

    /// <summary>
    ///     A helper method that creates a spreadsheet with multiple cells. Plus one provided
    ///     in the parameters. Assumes that the cell contents is either a string, double, or Formula.
    /// </summary>
    /// <param name="additionalCellName"> Name of the additional cell that's added to the sheet </param>
    /// <param name="contents"> Contents of the cell </param>
    /// <returns>
    ///     Newly initialized spreadshet with the following cells:
    ///     <list type="bullet">
    ///         <item>Name: "B7"  |  Value: (double) 20</item>
    ///         <item>Name: "C5"  |  Value: (string) "Hello World"</item>
    ///         <item>Name: "D9"  |  Value: (Formula) "56 * 70.3 + d6"</item>
    ///         <item>Name: *additionalCellName*  |  Value: (*typeof(contents)*) *contents*</item>
    ///     </list> 
    /// </returns>
    private static Spreadsheet CreateMultiCellSheet(string additionalCellName, string contents) {
        Spreadsheet sheet = CreateMultiCellSheet();
        sheet.SetContentsOfCell(additionalCellName, contents);
        return sheet;
    }




    // --- TESTS ON AN EMPTY SPREADSHEET INSTANCE ---
    // - Tests that throw an exception -
    [TestMethod]
    public void SpreadsheetSetCellContents_EmptyStringNameOnEmptySheet_InvalidNameException() {
        Spreadsheet spreadsheet = new();
        Assert.Throws<InvalidNameException>(() => spreadsheet.SetContentsOfCell("", "5"));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_InvalidNameOnEmptySheet_InvalidNameException() {
        Spreadsheet spreadsheet = new();
        Assert.Throws<InvalidNameException>(() => spreadsheet.SetContentsOfCell("H", "5"));
        Assert.Throws<InvalidNameException>(() => spreadsheet.SetContentsOfCell("8", "5"));
    }


    // - Tests on creating new cells -

    public void SpreadsheetSetCellContents_SetCellWithEmptyString_EmptySheet() {
        Spreadsheet sheet = new Spreadsheet();
        sheet.SetContentsOfCell("B1", "");
        Assert.IsFalse(sheet.GetNamesOfAllNonemptyCells().Any());
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_NewDoubleCellOnEmptySheet_ReturnsDouble() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetContentsOfCell("A5", "110");

        object result = spreadsheet.GetCellContents("A5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.IsTrue(110.0.Equals((double)result, 0.000001));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_NewStringCellOnEmptySheet_ReturnsString() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetContentsOfCell("A5", "Test");

        object result = spreadsheet.GetCellContents("A5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("Test", (string)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_NewFormulaCellOnEmptySheet_ReturnsFormula() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetContentsOfCell("A5", "=80 + d3");

        object result = spreadsheet.GetCellContents("A5");
        Formula expected = new("80+D3");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Formula>(result);
        Assert.AreEqual(expected, (Formula)result);
    }




    // --- TESTS ON A SPREADSHEET INSTANCE WITH ONE CELL ---
    // - Tests that throw a invalid name exception -
    [TestMethod]
    public void SpreadsheetSetCellContents_EmptyStringNameOnSingleCellSheet_InvalidNameException() {
        Spreadsheet spreadsheet = CreateSingleCellSheet();
        Assert.Throws<InvalidNameException>(() => spreadsheet.SetContentsOfCell("", "5"));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_InvalidCellNameOnSingleCellSheet_InvalidNameException() {
        Spreadsheet spreadsheet = CreateSingleCellSheet();
        Assert.Throws<InvalidNameException>(() => spreadsheet.SetContentsOfCell("H", "5"));
        Assert.Throws<InvalidNameException>(() => spreadsheet.SetContentsOfCell("8", "5"));
    }


    // - Tests that throw a circular exception
    [TestMethod]
    public void SpreadsheetSetCellContents_AddFormulaThatDependsOnItself_CircularException() {
        Spreadsheet spreadsheet = new();
        Assert.Throws<CircularException>(() => spreadsheet.SetContentsOfCell("A1", "=A1 + 2"));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_AddTwoFormulasThatDependOnEachOther_CircularException() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetContentsOfCell("A1", "=B2 * 8");
        Assert.Throws<CircularException>(() => spreadsheet.SetContentsOfCell("B2", "=A1 + 2"));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteExistingDoubleCellWithFormulaThatDependsOnItself_CircularException() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetContentsOfCell("A1", "100.56");
        Assert.Throws<CircularException>(() => spreadsheet.SetContentsOfCell("A1", "=A1 + 2"));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteExistingStringCellWithFormulaThatDependsOnItself_CircularException() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetContentsOfCell("A1", "Hello World");
        Assert.Throws<CircularException>(() => spreadsheet.SetContentsOfCell("A1", "=A1 + 2"));
    }


    // - Tests on creating new cells -
    [TestMethod]
    public void SpreadsheetSetCellContents_NewDoubleCellOnSingleCellSheet_SuccessfullyAdds() {
        Spreadsheet spreadsheet = CreateSingleCellSheet();
        spreadsheet.SetContentsOfCell("A5", "110");

        object result = spreadsheet.GetCellContents("A5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.IsTrue(110.0.Equals((double)result, 0.000001));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_NewStringCellOnSingleCellSheet_SuccessfullyAdds() {
        Spreadsheet spreadsheet = CreateSingleCellSheet();
        spreadsheet.SetContentsOfCell("A5", "Test");

        object result = spreadsheet.GetCellContents("A5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("Test", (string)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_NewFormulaCellOnSingleCellSheet_SuccessfullyAdds() {
        Spreadsheet spreadsheet = CreateSingleCellSheet();
        spreadsheet.SetContentsOfCell("A5", "=80 + d3");

        object result = spreadsheet.GetCellContents("A5");
        Formula expected = new("80+D3");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Formula>(result);
        Assert.AreEqual(expected, (Formula)result);
    }


    // - Tests on overwriting existing cell with the same type of value -
    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteDoubleCellSameValueOnSingleCellSheet_NoChange() {
        Spreadsheet spreadsheet = CreateSingleCellSheet("A5", "5");
        spreadsheet.SetContentsOfCell("A5", "5");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.IsTrue(5.0.Equals((double)result, 0.000001));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteDoubleCellWithDoubleOnSingleCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateSingleCellSheet("A5", "5");
        spreadsheet.SetContentsOfCell("A5", "59");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.IsTrue(59.0.Equals((double)result, 0.000001));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteStringCellWithStringOnSingleCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateSingleCellSheet("A5", "Hello");
        spreadsheet.SetContentsOfCell("A5", "World");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("World", (string)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteFormulaCellWithFormulaOnSingleCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateSingleCellSheet("A5", "=3 * 80");
        spreadsheet.SetContentsOfCell("A5", "=2e2 / 5");

        object result = spreadsheet.GetCellContents("A5");

        Formula expected = new("2e2/5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Formula>(result);
        Assert.AreEqual(expected, (Formula)result);
    }


    // - Tests on overwriting existing cell with different type of value -
    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteDoubleCellWithStringOnSingleCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateSingleCellSheet("A5", "5");
        spreadsheet.SetContentsOfCell("A5", "Hello");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("Hello", (string)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteDoubleCellWithFormulaOnSingleCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateSingleCellSheet("A5", "90");
        spreadsheet.SetContentsOfCell("A5", "=6 / 2.78");

        object result = spreadsheet.GetCellContents("A5");

        Formula expected = new("6/2.78");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Formula>(result);
        Assert.AreEqual(expected, (Formula)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteStringCellWithDoubleOnSingleCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateSingleCellSheet("A5", "Hello World");
        spreadsheet.SetContentsOfCell("A5", "56.7");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.IsTrue(56.7.Equals((double)result, 0.000001));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteStringCellWithFormulaOnSingleCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateSingleCellSheet("A5", "Test");
        spreadsheet.SetContentsOfCell("A5", "=2 * 6 + 5");

        object result = spreadsheet.GetCellContents("A5");

        Formula expected = new("2*6+5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Formula>(result);
        Assert.AreEqual(expected, (Formula)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteFormulaCellWithDoubleOnSingleCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateSingleCellSheet("A5", "=3 * 80");
        spreadsheet.SetContentsOfCell("A5", "80");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.IsTrue(80.0.Equals((double)result, 0.000001));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteFormulaCellWithStringOnSingleCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateSingleCellSheet("A5", "=3 * 80");
        spreadsheet.SetContentsOfCell("A5", "Tester");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("Tester", (string)result);
    }


    // - Tests on deleting existing cell -
    [TestMethod]
    public void SpreadhseetSetCellContents_DeletingExistingDoubleCellOnSingleCellSheet_OverwritesToEmptyString() {
        Spreadsheet spreadsheet = CreateSingleCellSheet("A5", "5");
        spreadsheet.SetContentsOfCell("A5", ""); // Should remove the cell from the backend data structure...

        object result = spreadsheet.GetCellContents("A5"); // but it should return a "" because that denotes an empty cell

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("", (string)result);
    }

    [TestMethod]
    public void SpreadhseetSetCellContents_DeletingExistingStringCellOnSingleCellSheet_OverwritesToEmptyString() {
        Spreadsheet spreadsheet = CreateSingleCellSheet("A5", "Tester");
        spreadsheet.SetContentsOfCell("A5", ""); // Should remove the cell from the backend data structure...

        object result = spreadsheet.GetCellContents("A5"); // but it should return a "" because that denotes an empty cell

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("", (string)result);
    }

    [TestMethod]
    public void SpreadhseetSetCellContents_DeletingExistingFormulaCellOnSingleCellSheet_OverwritesToEmptyString() {
        Spreadsheet spreadsheet = CreateSingleCellSheet("A5", "=2+2");
        spreadsheet.SetContentsOfCell("A5", ""); // Should remove the cell from the backend data structure...

        object result = spreadsheet.GetCellContents("A5"); // but it should return a "" because that denotes an empty cell

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("", (string)result);
    }




    // --- TESTS ON A SPREADSHEET INSTANCE WITH MULTIPLE CELLS ---
    // - Tests that throw a invalid name exception -
    [TestMethod]
    public void SpreadsheetSetCellContents_EmptyStringNameOnMultiCellSheet_InvalidNameException() {
        Spreadsheet spreadsheet = CreateMultiCellSheet();
        Assert.Throws<InvalidNameException>(() => spreadsheet.SetContentsOfCell("", "5"));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_InvalidCellNameOnMultiCellSheet_InvalidNameException() {
        Spreadsheet spreadsheet = CreateMultiCellSheet();
        Assert.Throws<InvalidNameException>(() => spreadsheet.SetContentsOfCell("H", "5"));
        Assert.Throws<InvalidNameException>(() => spreadsheet.SetContentsOfCell("8", "Test"));
        Assert.Throws<InvalidNameException>(() => spreadsheet.SetContentsOfCell("h5", "=2+2"));
    }


    // - Tests that throw a circular exception -
    [TestMethod]
    public void SpreadSheetSetCellContents_NewFormulaDependsOnADependeeFormula_CircularException() {
        Spreadsheet spreadsheet = CreateMultiCellSheet(); // Should contain the formula that depends on D6
        Assert.Throws<CircularException>(() => spreadsheet.SetContentsOfCell("D6", "=D9 - 28"));
    }

    [TestMethod]
    public void SpreadSheetSetCellContents_BigDependencyCircleWithFormulas_CircularException() {
        Spreadsheet spreadsheet = CreateMultiCellSheet();
        spreadsheet.SetContentsOfCell("A1", "=B2 + 56");
        spreadsheet.SetContentsOfCell("B2", "=C3 + 2e3");
        spreadsheet.SetContentsOfCell("C3", "=D4 * 6");
        Assert.Throws<CircularException>(() => spreadsheet.SetContentsOfCell("D4", "=A1 - 9"));
    }

    [TestMethod]
    public void SpreadSheetSetCellContents_ImperfectDependencyCircleWithFormulas_CircularException() {
        Spreadsheet spreadsheet = CreateMultiCellSheet();
        spreadsheet.SetContentsOfCell("A1", "=B2 + 56");
        spreadsheet.SetContentsOfCell("B2", "=C3 + 2e3");
        spreadsheet.SetContentsOfCell("C3", "=D4 * 6");
        spreadsheet.SetContentsOfCell("D4", "=E5 / 7");
        Assert.Throws<CircularException>(() => spreadsheet.SetContentsOfCell("E5", "=C3 - 9"));
    }

    [TestMethod]
    public void SpreadSheetSetCellContents_IndirectDependencyCircleWithFormulas_CircularException() {
        Spreadsheet spreadsheet = CreateMultiCellSheet();
        spreadsheet.SetContentsOfCell("A1", "=B2 + 56");
        spreadsheet.SetContentsOfCell("B2", "=C3 + 2e3");
        spreadsheet.SetContentsOfCell("C3", "=F6 * 6 - D4");
        spreadsheet.SetContentsOfCell("D4", "=E5 / 7");
        Assert.Throws<CircularException>(() => spreadsheet.SetContentsOfCell("E5", "=C3 - 9"));
    }


    // - Tests on creating new cells -
    [TestMethod]
    public void SpreadsheetSetCellContents_NewDoubleCellOnMultiCellSheet_SuccessfullyAdds() {
        Spreadsheet spreadsheet = CreateMultiCellSheet();
        spreadsheet.SetContentsOfCell("A5", "110.4");

        object result = spreadsheet.GetCellContents("A5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.IsTrue(110.4.Equals((double)result, 0.000001));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_NewStringCellOnMultiCellSheet_SuccessfullyAdds() {
        Spreadsheet spreadsheet = CreateMultiCellSheet();
        spreadsheet.SetContentsOfCell("A5", "Test");

        object result = spreadsheet.GetCellContents("A5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("Test", (string)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_NewFormulaCellOnMultiCellSheet_SuccessfullyAdds() {
        Spreadsheet spreadsheet = CreateMultiCellSheet();
        spreadsheet.SetContentsOfCell("A5", "=80 + d3");

        object result = spreadsheet.GetCellContents("A5");
        Formula expected = new("80+D3");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Formula>(result);
        Assert.AreEqual(expected, (Formula)result);
    }


    // - Tests on overwriting existing cell with the same type of value -
    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteDoubleCellSameValueOnMultiCellSheet_NoChange() {
        Spreadsheet spreadsheet = CreateMultiCellSheet("A5", "5");
        spreadsheet.SetContentsOfCell("A5", "5");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.IsTrue(5.0.Equals((double)result, 0.000001));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteDoubleCellWithDoubleOnMultiCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateMultiCellSheet("A5", "5");
        spreadsheet.SetContentsOfCell("A5", "59");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.IsTrue(59.0.Equals((double)result, 0.000001));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteStringCellWithStringOnMultiCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateMultiCellSheet("A5", "Hello");
        spreadsheet.SetContentsOfCell("A5", "World");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("World", (string)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteFormulaCellWithFormulaOnMultiCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateMultiCellSheet("A5", "=3 * 80");
        spreadsheet.SetContentsOfCell("A5", "=2e2 / 5");

        object result = spreadsheet.GetCellContents("A5");

        Formula expected = new("2e2/5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Formula>(result);
        Assert.AreEqual(expected, (Formula)result);
    }


    // - Tests on overwriting existing cell with different type of value -
    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteDoubleCellWithStringOnMultiCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateMultiCellSheet("A5", "5");
        spreadsheet.SetContentsOfCell("A5", "Hello");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("Hello", (string)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteDoubleCellWithFormulaOnMultiCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateMultiCellSheet("A5", "90");
        spreadsheet.SetContentsOfCell("A5", "=6 / 2.78");

        object result = spreadsheet.GetCellContents("A5");

        Formula expected = new("6/2.78");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Formula>(result);
        Assert.AreEqual(expected, (Formula)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteStringCellWithDoubleOnMultiCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateMultiCellSheet("A5", "Hello World");
        spreadsheet.SetContentsOfCell("A5", "56.7");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.IsTrue(56.7.Equals((double)result, 0.000001));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteStringCellWithFormulaOnMultiCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateMultiCellSheet("A5", "Test");
        spreadsheet.SetContentsOfCell("A5", "=2 * 6 + 5");

        object result = spreadsheet.GetCellContents("A5");

        Formula expected = new("2*6+5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Formula>(result);
        Assert.AreEqual(expected, (Formula)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteFormulaCellWithDoubleOnMultiCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateMultiCellSheet("A5", "=3 * 80");
        spreadsheet.SetContentsOfCell("A5", "80");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.IsTrue(80.0.Equals((double)result, 0.000001));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteFormulaCellWithStringOnMultiCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateMultiCellSheet("A5", "=3 * 80");
        spreadsheet.SetContentsOfCell("A5", "Tester");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("Tester", (string)result);
    }


    // - Tests on deleting existing cell -
    [TestMethod]
    public void SpreadhseetSetCellContents_DeletingExistingDoubleCellOnMultiCellSheet_OverwritesToEmptyString() {
        Spreadsheet spreadsheet = CreateMultiCellSheet("A5", "5");
        spreadsheet.SetContentsOfCell("A5", ""); // Should remove the cell from the backend data structure...

        object result = spreadsheet.GetCellContents("A5"); // but it should return a "" because that denotes an empty cell

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("", (string)result);
    }

    [TestMethod]
    public void SpreadhseetSetCellContents_DeletingExistingStringCellOnMultiCellSheet_OverwritesToEmptyString() {
        Spreadsheet spreadsheet = CreateMultiCellSheet("A5", "Tester");
        spreadsheet.SetContentsOfCell("A5", ""); // Should remove the cell from the backend data structure...

        object result = spreadsheet.GetCellContents("A5"); // but it should return a "" because that denotes an empty cell

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("", (string)result);
    }

    [TestMethod]
    public void SpreadhseetSetCellContents_DeletingExistingFormulaCellOnMultiCellSheet_OverwritesToEmptyString() {
        Spreadsheet spreadsheet = CreateMultiCellSheet("A5", "=2+2");
        spreadsheet.SetContentsOfCell("A5", ""); // Should remove the cell from the backend data structure...

        object result = spreadsheet.GetCellContents("A5"); // but it should return a "" because that denotes an empty cell

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("", (string)result);
    }
}
