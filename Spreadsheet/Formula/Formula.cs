// <authors> Carson Angell </authors>
// <date> 1/19/2026 </date>

namespace Formula;

using System.Text;
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
///     </item> (?<=[a-zA-Z]0+)
///   </list>
/// </summary>
public partial class Formula {
    /// <summary>
    ///   All variables tokens are letters followed by numbers. This pattern
    ///   represents valid variable name tokens.
    /// </summary>
    private const string VariableRegExPattern = @"^[a-zA-Z]+\d+$";
    /// <summary>
    ///     All operator tokens are single character strings with one of the four main basic operators (+, -, *, /).
    ///     This pattern represents valid operator tokens.
    /// </summary>
    private const string OperatorRegExPattern = @"^[\+\-\*/]$";
    /// <summary>
    ///     Some variable may contain leading zeroes in front of their row coordinate (e.g. abc0123 instead of abc123).
    ///     This pattern detects that case in a variable.
    /// </summary>
    private const string LeadingZeroInVariableRegExPattern = @"[a-zA-Z]0+";
    
    private readonly List<string> _tokens;
    private readonly HashSet<string> _variables;

    // These Regex member objects correspond to the patterns above
    private readonly Regex _variableRegex;
    private readonly Regex _operatorRegex;
    private readonly Regex _leadingZeroVarRegex;

    // Holds the canonical string version of the formula
    private string _stringifiedFormula;

    /// <summary>
    ///   Initializes a new instance of the <see cref="Formula"/> class.
    ///   <para>
    ///     Creates a Formula from a string that consists of an infix expression written as
    ///     described in the class comment.  If the expression is syntactically incorrect,
    ///     throws a FormulaFormatException with an explanatory Message.
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
        _variables = new HashSet<string>();
        _variableRegex = new Regex(VariableRegExPattern);
        _operatorRegex = new Regex(OperatorRegExPattern);
        _leadingZeroVarRegex = new Regex(LeadingZeroInVariableRegExPattern);
        _stringifiedFormula = ""; // The CheckFormulaSyntaxOnEveryToken method will set this to the correct value

        // The following tests only check for the surface level rules (Rules #1, #5, and #6) where iterating is not needed.

        // Checks the one token rule (Rule #1)
        if (_tokens.Count == 0) throw new FormulaFormatException("There must be at least one token in the formula. Rule #1 (One Token Rule) Violated.");


        // Checks the first token rule (Rule #5)
        string firstToken = _tokens.First();
        if (!TokenIsNumber(firstToken) && !TokenIsVariable(firstToken) && firstToken != "(") throw new FormulaFormatException($"Invalid first token: \"{firstToken}\". Must be either a number, variable or opening parenthesis. Rule #5 (First Token Rule) Violated.");


        // Checks the last token rule (Rule #6)
        string lastToken = _tokens.Last();
        if (!TokenIsNumber(lastToken) && !TokenIsVariable(lastToken) && lastToken != ")") throw new FormulaFormatException($"Invalid last token: \"{lastToken}\". Must be either a number, variable or closing parenthesis. Rule #6 (Last Token Rule) Violated.");


        // Iterates through the tokens and checks each one
        // I decided to put this code into a separate method so make things look less messy and hard to read.
        CheckFormulaSyntaxOnEveryToken();
    }

    /// <summary>
    ///     Iterates through the current list of formula tokens and checks each one for syntax errors.
    ///     <para><remarks>
    ///        Some things to note about this method:
    ///        <list type="bullet">
    ///            <item>
    ///                Checks rules #2, #3, #4, #7, and #8. Assumes that rules #1, #5, and #6 are already checked.
    ///                Rule #2 is checked implicitly due to how the Regex patterns are constructed (they will not match with special characters).
    ///            </item>
    ///            <item>
    ///                Method constructs the canonical version of the formula and stores it in the _stringifiedFormula field.
    ///            </item>
    ///            <item>
    ///                Method will take any variables it finds and store its canonical version in the _variables field.
    ///            </item>
    ///            <item>
    ///                Complexity is O(n) where n is the number of tokens in the formula.
    ///            </item>
    ///        </list>
    ///     </remarks></para>
    /// </summary>
    /// <exception cref="FormulaFormatException">Exception detailing the problem with the formula's syntax</exception>
    private void CheckFormulaSyntaxOnEveryToken() {
        // Builder used to make the string returned by the ToString method.
        // The canonical forms of the tokens are appended one by one during syntax checking.
        StringBuilder builder = new();
        int openParenCount = 0; 
        int closeParenCount = 0;

        // Iterates through the formula tokens...
        for (int i = 0; i < _tokens.Count; i++) {
            string token = _tokens[i];

            // If there is no token after the current one, the value is null (meaning we reached the end of the list)
            // I did this to avoid IndexOutOfBounds exceptions when referencing tokens in front of the current one. It makes it a bit cleaner than index checking.
            string? nextToken = (i != _tokens.Count - 1) ? _tokens[i + 1] : null;

            if (TokenIsOperator(token)) {
                // Checks the operator following rule (Rule #7)
                // Null checking for nextToken is not needed here since an operator cannot be the last token (earlier checks made sure of that).
                if (!TokenIsNumber(nextToken) && !TokenIsVariable(nextToken) && nextToken != "(") throw new FormulaFormatException($"Invalid token following operator: \"{nextToken}\". Rule #7 (Parenthesis/Operator Following Rule) Violated.");
            }

            else if (token == "(") {
                openParenCount++;
                // Checks the open parenthesis following rule (Rule #7)
                // Null checking for nextToken is not needed here since an opening parenthesis cannot be the last token (earlier checks made sure of that).
                if (!TokenIsNumber(nextToken) && !TokenIsVariable(nextToken) && nextToken != "(") throw new FormulaFormatException($"Invalid token following open parenthesis: \"{nextToken}\". Rule #7 (Parenthesis/Operator Following Rule) Violated.");
            }

            else if (token == ")") {
                closeParenCount++;
                // Checks the closing parentheses rule (Rule #3)
                if (closeParenCount > openParenCount) throw new FormulaFormatException("Cannot have more closing parentheses than opening parentheses. Rule #3 (Closing Parentheses Rule) Violated.");

                // Checks the extra following rule (Rule #8)
                if (nextToken != null && nextToken != ")" && !TokenIsOperator(nextToken)) throw new FormulaFormatException($"Invalid token following closed parenthesis: \"{nextToken}\". Rule #8 (Extra Following Rule) Violated.");   
            }

            // Executes if the current token is a number or variable
            else {
                token = UpdateTokenToCanonicalForm(tokenIndex: i);
                if (TokenIsVariable(token)) _variables.Add(token);
                // Checks the extra following rule (Rule #8)
                if (nextToken != null && nextToken != ")" && !TokenIsOperator(nextToken)) throw new FormulaFormatException($"Invalid token following a number or variable: \"{nextToken}\". Rule #8 (Extra Following Rule) Violated.");
            }

            builder.Append(token);
        }

        // Checks the balanced parentheses rule (Rule #4)
        if (openParenCount != closeParenCount) throw new FormulaFormatException("Parentheses are not balanced in the formula. Rule #4 (Balanced Parentheses Rule) Violated.");

        _stringifiedFormula = builder.ToString();
    }



    /// <summary>
    ///     Converts the token at the given index into its canonical form.
    ///     <remarks>
    ///         Overwrites the old token in the _tokens list member variable.
    ///         Only converts numbers or variables. Operators and parens don't have canonical forms.
    ///     </remarks>
    ///     <para>
    ///         Non-Exhaustive Example Conversions:
    ///     </para>
    ///     <list type="bullet">
    ///         <item>
    ///             "a1" -> "A1"
    ///         </item>
    ///         <item>
    ///            "003.5300" -> "3.53"
    ///         </item>
    ///         <item>
    ///            "3e5" -> "300000"
    ///         </item>
    ///     </list>
    /// </summary>
    /// <param name="tokenIndex"></param>
    /// <returns> New version of token after it has been updated to canonical form </returns>
    private string UpdateTokenToCanonicalForm(int tokenIndex) {
        string token = _tokens[tokenIndex];
        // Convert a variable token to canonical form...
        if (TokenIsVariable(token)) {
            // If there are leading tokens in a variable, remove them with the regex below.
            if (_leadingZeroVarRegex.IsMatch(token)) token = Regex.Replace(token, @"(?<=[a-zA-Z])0+", "");

            _tokens[tokenIndex] = token.ToUpper();
        }
        // Convert a number token to canonical form...
        else if (TokenIsNumber(token)) {
            _ = Double.TryParse(token, out double convertedToken);
            _tokens[tokenIndex] = $"{convertedToken}";
        }
        return _tokens[tokenIndex];
    }



    /// <summary>
    ///     Reports when the token is a valid variable. It must be one or more letters
    ///     followed by one or more numbers.
    /// </summary>
    /// <param name="token">A token that may be a variable. </param>
    /// <returns> 
    ///     true if the string matches the requirements, e.g., A1 or a1. 
    ///     Will return false if token string is null.
    /// </returns>
    private bool TokenIsVariable(string? token) => token != null && _variableRegex.IsMatch(token);

    /// <summary>
    ///     Reports whether a token is a valid operator. Token must be a basic
    ///     arithmetic operator.
    /// </summary>
    /// <param name="token"> A token that may be an operator </param>
    /// <returns> 
    ///     true if the string matches the requirements. e.g. +, -, *, or /.
    ///     Will return false if token string is null.
    /// </returns>
    private bool TokenIsOperator(string? token) => token != null && _operatorRegex.IsMatch(token);

    /// <summary>
    ///     Reports whether a token is a valid number. Accepts integers,
    ///     decimals and numbers with scientific notation.
    /// </summary>
    /// <param name="token"> A token that may be a number </param>
    /// <returns>
    ///     true if the string matches the requirements.
    ///     Will return false if token is null.
    /// </returns>
    private static bool TokenIsNumber(string? token) => token != null && Double.TryParse(token, out _);


    /// <summary>
    ///   <para>
    ///     Returns a set of all the variables in the formula.
    ///   </para>
    ///   <remarks>
    ///     No variable may appear more than once in the returned set, even
    ///     if it is used more than once in the Formula.
    ///   </remarks>
    ///   <list type="bullet">
    ///     <item>new("x1+y1*z1").GetVariables() should return a set containing "X1", "Y1", and "Z1".</item>
    ///     <item>new("x1+X1"   ).GetVariables() should return a set containing "X1".</item>
    ///   </list>
    /// </summary>
    /// <returns> the set of variables (string names) representing the variables referenced by the formula in canonical form (all letters converted to uppercase). </returns>
    public ISet<string> GetVariables() {
        HashSet<string> returnVariables = [];
        foreach (string variable in _variables) returnVariables.Add(variable);
        return returnVariables;
    }

    /// <summary>
    ///     Returns a string representation of a canonical form of the formula.
    ///     <remarks>
    ///         The string will contain no spaces. All the variable and number tokens in the string will be normalized.
    ///         For numbers, this means that the original string token is converted to
    ///         a number using double.Parse or double.TryParse, then converted back to a
    ///         string using double.ToString. For variables, this means all letters are uppercase.
    ///         <para> For example: </para>
    ///         <code>
    ///             new("x1 + Y1").ToString() should return "X1+Y1"
    ///             new("x1 + 5.0000").ToString() should return "X1+5".
    ///         </code>
    ///     </remarks>
    /// </summary>
    /// <returns>
    ///     A canonical version (string) of the formula. All "equal" formulas should have the same value here.
    /// </returns>
    public override string ToString() {
        return _stringifiedFormula;
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
        string varPattern = @"[a-zA-Z]+\d+";
        string doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: [eE][\+-]?\d+)?";
        string spacePattern = @"\s+";

        // Overall pattern
        string pattern = string.Format(
                                        "({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})",
                                        lpPattern,
                                        rpPattern,
                                        opPattern,
                                        varPattern,
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
    /// <param name="message"> A developer defined message describing why the exception occurred.</param>
    public FormulaFormatException(string message)
        : base(message) {
        // All this does is call the base constructor. No extra code needed.
    }
}
