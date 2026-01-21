// <summary>
//   <para>
//     This code is provided to start your assignment.  It was written
//     by Profs Joe, Danny, Jim, and Travis.  You should keep this attribution
//     at the top of your code where you have your header comment, along
//     with any other required information.
//   </para>
//   <para>
//     You should remove/add/adjust comments in your file as appropriate
//     to represent your work and any changes you make.
//   </para>
// </summary>

namespace Formula;

using System.Text.RegularExpressions;

/// <summary>
///   <para>
///     This class represents formulas written in standard infix notation using standard precedence
///     rules.  The allowed symbols are non-negative numbers written using double-precision
///     floating-point syntax; variables that consist of one or more letters followed by
///     one or more numbers; parentheses; and the four operator symbols +, -, *, and /.
///   </para>
///   <para>
///     Spaces are significant only insofar that they delimit tokens.  For example, "xy" is
///     a single variable, "x y" consists of two variables "x" and y; "x23" is a single variable;
///     and "x 23" consists of a variable "x" and a number "23".  Otherwise, spaces are to be removed.
///   </para>
///   <para>
///     For Assignment Two, you are to implement the following functionality:
///   </para>
///   <list type="bullet">
///     <item>
///        Formula Constructor which checks the syntax of a formula.
///     </item>
///     <item>
///        Get Variables
///     </item>
///     <item>
///        ToString
///     </item>
///   </list>
/// </summary>
public partial class Formula {
    /// <summary>
    ///   All variables are letters followed by numbers.  This pattern
    ///   represents valid variable name strings.
    /// </summary>
    private const string VariableRegExPattern = @"[a-zA-Z]+\d+";
    private const string ValidCharsRegExPattern = @"[\+\-\*/\(\)a-zA-Z\d]+";
    private const string OperatorRegExPattern = @"[\+\-\*/]";

    private List<string> _tokens;

    private readonly Regex _variableRegex;
    private readonly Regex _validCharsRegex;
    private readonly Regex _operatorRegex;

    /// <summary>
    ///   Initializes a new instance of the <see cref="Formula"/> class.
    ///   <para>
    ///     Creates a Formula from a string that consists of an infix expression written as
    ///     described in the class comment.  If the expression is syntactically incorrect,
    ///     throws a FormulaFormatException with an explanatory Message.  See the assignment
    ///     specifications for the syntax rules you are to implement.
    ///   </para>
    ///   <para>
    ///     Non-Exhaustive Example Errors:
    ///   </para>
    ///   <list type="bullet">
    ///     <item>
    ///        Invalid variable name, e.g., x, x1x  (Note: x1 is valid, but would be normalized to X1)
    ///     </item>
    ///     <item>
    ///        Empty formula, e.g., string.Empty
    ///     </item>
    ///     <item>
    ///        Mismatched Parentheses, e.g., "(("
    ///     </item>
    ///     <item>
    ///        Invalid Following Rule, e.g., "2x+5"
    ///     </item>
    ///   </list>
    /// </summary>
    /// <param name="formula"> The string representation of the formula to be created.</param>
    public Formula(string formula) {
        _tokens = GetTokens(formula);
        int openParenCount = 0;
        int closeParenCount = 0;

        _variableRegex = new Regex(VariableRegExPattern); //TODO - Check to see if generated RegEx is better
        _validCharsRegex = new Regex(ValidCharsRegExPattern);
        _operatorRegex = new Regex(OperatorRegExPattern);


        // Checks the one token rule (Rule #1)
        if (_tokens.Count == 0) throw new FormulaFormatException("There must be at least one token in the formula.");


        // Checks the first token rule (Rule #5)
        string firstToken = _tokens.First();
        if (!(TokenIsNumber(firstToken) || TokenIsVariable(firstToken) || firstToken == "(")) throw new FormulaFormatException("The first token must be either a number, variable or opening parenthesis");


        // Checks the last token rule (Rule #6)
        string lastToken = _tokens.Last();
        if (!(TokenIsNumber(lastToken) || TokenIsVariable(lastToken) || lastToken == ")")) throw new FormulaFormatException("The last token must be either a number, variable or closing parenthesis");


        for (int i = 0; i < _tokens.Count; i++) {
            string token = _tokens.ElementAt(i);
            string? nextToken = (i != _tokens.Count - 1) ? _tokens.ElementAt(i + 1) : null;

            // Checks the valid tokens rule (Rule #2)
            if (!TokenHasValidChars(token)) throw new FormulaFormatException("Formula must contain valid tokens, no special characters allowed.");

            if (token == "(") {
                openParenCount++;
                if (nextToken == "(") continue;

                // Checks the parenthesis following rule (Rule #7). Only checks for operators following parens, not operators
                if (!(TokenIsNumber(nextToken) || TokenIsVariable(nextToken) || TokenIsOperator(nextToken))) throw new FormulaFormatException("Invalid token following open parenthesis.");
            }
            else if (token == ")") {
                closeParenCount++;
                if (nextToken == null || nextToken == ")") continue;

                // Checks the closing parentheses rule (Rule #3)
                if (closeParenCount > openParenCount) throw new FormulaFormatException("Number of closing parenthesis has exceeded the number of open parenthesis."); //TODO - Check if this is a good error message
                //Checks for extra following rule for parentheses (Rule #8)
                if (!TokenIsOperator(nextToken)) throw new FormulaFormatException("Invalid token following closed parenthesis.");
            }
            // else if (TokenIsOperator(token)) {
                // TODO - Where I left off...
                // - TokenIsVariable helper is getting null value from nextoken
                // - This is puzzling since it should not be null if the last token is an operator (last token check should have caught it)
                // - Kind of working on rules 7-8
            //     if (!(TokenIsNumber(nextToken) || TokenIsVariable(nextToken) || nextToken == "(")) throw new FormulaFormatException("Invalid token following operator.");
            // }
            // else {
            //     if (nextToken == null) continue;

            //     if (!(TokenIsOperator(nextToken) || nextToken == ")")) throw new FormulaFormatException("Invalid token following a number or variable.");
            // }


            Console.WriteLine(token); //TODO - remove this
        }

        // Checks the balanced parentheses rule (Rule #4)
        if (openParenCount != closeParenCount) throw new FormulaFormatException("Parentheses are not balanced in the formula.");
    }

    private bool TokenIsVariable(string token) => _variableRegex.IsMatch(token);
    private static bool TokenIsNumber(string token) => Double.TryParse(token, out _);
    private bool TokenHasValidChars(string token) => _validCharsRegex.IsMatch(token);
    private bool TokenIsOperator(string token) => _operatorRegex.IsMatch(token);

    /// <summary>
    ///   <para>
    ///     Returns a set of all the variables in the formula.
    ///   </para>
    ///   <remarks>
    ///     Important: no variable may appear more than once in the returned set, even
    ///     if it is used more than once in the Formula.
	///     Variables should be returned in canonical form, having all letters converted
	///     to uppercase.
    ///   </remarks>
    ///   <list type="bullet">
    ///     <item>new("x1+y1*z1").GetVariables() should return a set containing "X1", "Y1", and "Z1".</item>
    ///     <item>new("x1+X1"   ).GetVariables() should return a set containing "X1".</item>
    ///   </list>
    /// </summary>
    /// <returns> the set of variables (string names) representing the variables referenced by the formula. </returns>
    public ISet<string> GetVariables() {
        // FIXME: implement your code here
        return new HashSet<string>();
    }

    /// <summary>
    ///   <para>
    ///     Returns a string representation of a canonical form of the formula.
    ///   </para>
    ///   <para>
    ///     The string will contain no spaces.
    ///   </para>
    ///   <para>
    ///     If the string is passed to the Formula constructor, the new Formula f
    ///     will be such that this.ToString() == f.ToString().
    ///   </para>
    ///   <para>
    ///     All the variable and number tokens in the string will be normalized.
    ///     For numbers, this means that the original string token is converted to
    ///     a number using double.Parse or double.TryParse, then converted back to a
    ///     string using double.ToString.
    ///     For variables, this means all letters are uppercased.
    ///   </para>
    ///   <para>
    ///       For example:
    ///   </para>
    ///   <code>
    ///       new("x1 + Y1").ToString() should return "X1+Y1"
    ///       new("x1 + 5.0000").ToString() should return "X1+5".
    ///   </code>
    ///   <para>
    ///     This method should execute in O(1) time.
    ///   </para>
    /// </summary>
    /// <returns>
    ///   A canonical version (string) of the formula. All "equal" formulas
    ///   should have the same value here.
    /// </returns>
    public override string ToString() {
        // FIXME: add your code here.
        return string.Empty;
    }

    /// <summary>
    ///   Reports whether "token" is a variable.  It must be one or more letters
    ///   followed by one or more numbers.
    /// </summary>
    /// <param name="token"> A token that may be a variable. </param>
    /// <returns> true if the string matches the requirements, e.g., A1 or a1. </returns>
    private static bool IsVar(string token) {
        // notice the use of ^ and $ to denote that the entire string being matched is just the variable
        string standaloneVarPattern = $"^{VariableRegExPattern}$";
        return Regex.IsMatch(token, standaloneVarPattern);
    }

    /// <summary>
    ///   <para>
    ///     Given an expression, enumerates the tokens that compose it.
    ///   </para>
    ///   <para>
    ///     Tokens returned are:
    ///   </para>
    ///   <list type="bullet">
    ///     <item>left paren</item>
    ///     <item>right paren</item>
    ///     <item>one of the four operator symbols</item>
    ///     <item>a string consisting of one or more letters followed by one or more numbers</item>
    ///     <item>a double literal</item>
    ///     <item>and anything that doesn't match one of the above patterns</item>
    ///   </list>
    ///   <para>
    ///     There are no empty tokens; white space is ignored (except to separate other tokens).
    ///   </para>
    /// </summary>
    /// <param name="formula"> A string representing an infix formula such as 1*B1/3.0. </param>
    /// <returns> The ordered list of tokens in the formula. </returns>
    private static List<string> GetTokens(string formula) {
        List<string> results = [];

        string lpPattern = @"\(";
        string rpPattern = @"\)";
        string opPattern = @"[\+\-*/]";
        string doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: [eE][\+-]?\d+)?";
        string spacePattern = @"\s+";

        // Overall pattern
        string pattern = string.Format(
                                        "({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})",
                                        lpPattern,
                                        rpPattern,
                                        opPattern,
                                        VariableRegExPattern,
                                        doublePattern,
                                        spacePattern);

        // Enumerate matching tokens that don't consist solely of white space.
        foreach (string s in Regex.Split(formula, pattern, RegexOptions.IgnorePatternWhitespace)) {
            if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline)) {
                results.Add(s);
            }
        }

        return results;
    }
}


/// <summary>
///   Used to report syntax errors in the argument to the Formula constructor.
/// </summary>
public class FormulaFormatException : Exception {
    /// <summary>
    ///   Initializes a new instance of the <see cref="FormulaFormatException"/> class.
    ///   <para>
    ///      Constructs a FormulaFormatException containing the explanatory message.
    ///   </para>
    /// </summary>
    /// <param name="message"> A developer defined message describing why the exception occured.</param>
    public FormulaFormatException(string message)
        : base(message) {
        // All this does is call the base constructor. No extra code needed.
    }
}
