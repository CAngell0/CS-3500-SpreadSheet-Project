// <copyright file="FormulaSyntaxTests.cs" company="UofU-CS3500">
//   Copyright 2024 UofU-CS3500. All rights reserved.
// </copyright>
// <authors> [Insert Your Name] </authors>
// <date> [Insert the Date] </date>

namespace FormulaTests;

using System.Text;
using CS3500.Formula1; // Change this using statement to use different formula implementations.

/// <summary>
///   <para>
///     The following class shows the basics of how to use the MSTest framework,
///     including:
///   </para>
///   <list type="number">
///     <item> How to catch exceptions. </item>
///     <item> How a test of valid code should look. </item>
///   </list>
/// </summary>
[TestClass]
public class FormulaSyntaxTests {
    // --- Tests for One Token Rule ---

    /// <summary>
    ///   <para>
    ///     This test makes sure the right kind of exception is thrown
    ///     when trying to create a formula with no tokens.
    ///   </para>
    ///   <remarks>
    ///     <list type="bullet">
    ///       <item>
    ///         We use the _ (discard) notation because the formula object
    ///         is not used after that point in the method.  Note: you can also
    ///         use _ when a method must match an interface but does not use
    ///         some of the required arguments to that method.
    ///       </item>
    ///       <item>
    ///         string.Empty is often considered best practice (rather than using "") because it
    ///         is explicit in intent (e.g., perhaps the coder forgot to but something in "").
    ///       </item>
    ///       <item>
    ///         The name of a test method should follow the MS standard:
    ///         https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices
    ///       </item>
    ///       <item>
    ///         All methods should be documented, but perhaps not to the same extent
    ///         as this one.  The remarks here are for your educational
    ///         purposes (i.e., a developer would assume another developer would know these
    ///         items) and would be superfluous in your code.
    ///       </item>
    ///       <item>
    ///         Notice the use of the attribute tag [ExpectedException] which tells the test
    ///         that the code should throw an exception, and if it doesn't an error has occurred;
    ///         i.e., the correct implementation of the constructor should result
    ///         in this exception being thrown based on the given poorly formed formula.
    ///       </item>
    ///     </list>
    ///   </remarks>
    ///   <example>
    ///     <code>
    ///        // here is how we call the formula constructor with a string representing the formula
    ///        _ = new Formula( "5+5" );
    ///     </code>
    ///   </example>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_NoTokens_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula(string.Empty));
    }

    /// <summary>
    ///     <para> This test makes sure the constructor can handle single integer tokens. </para>
    ///     <remarks> Short integers, long integers and inegers with leading zeroes arae tested. </remarks>
    ///     <list type="bullet">
    ///         <item> Input: 5, 598582, 00028532, etc. </item>
    ///         <item> Expected Output: None </item>
    ///     </list>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_SingleIntegerToken_Valid() {
        _ = new Formula("5");
        _ = new Formula("598582");
        _ = new Formula("5372548726489124712847");
        _ = new Formula("00028532");
    }

    /// <summary>
    ///     <para> This test makes sure teh constructor can handle single decimal tokens. </para>
    ///     <remarks> Small decimals, long decimals and decimal numbers with leading zeroes are tested. </remarks>
    ///     <list type="bullet">
    ///         <item> Input: 7.1, 545.02744192, 0072.2861, etc. </item>
    ///         <item> Expected Output: None </item>
    ///     </list>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_SingleDecimalToken_Valid() {
        _ = new Formula("7.1");
        _ = new Formula("545.9281");
        _ = new Formula("5.02744192");
        _ = new Formula("0072.2861");
    }

    /// <summary>
    ///     <para> This test makes sure that malformed decimal numbers throw an exception. </para>
    ///     <remarks> Decimals with two decimal points are inputted. Leading zeroes are also tested. </remarks>
    ///     <list type="bullet">
    ///         <item> Input: 5721.59572.39481, 08812.456.1 </item>
    ///         <item> Expected Output: FormulaFormatException </item>
    ///     </list>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_IncorrectSingleDecimalToken_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("5721.59572.39481"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("008812.456.1"));
    }

    /// <summary>
    ///     <para> Makes sure the constructor can handle numbers with scientific notation. </para>
    ///     <remarks> Tests numbers with capital notation (3E10) and lowercase notation (3e10). Numbers with leading zeroes are also tested. </para>
    ///     <list type="bullet">
    ///         <item> Input: 8E10, 166e100000, 8721E-10, 00386e100 </item>
    ///        <item> Expected Output: None </item>
    ///     </list>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_SingleScientificNotationToken_Valid() {
        _ = new Formula("8E10");
        _ = new Formula("166e100000");
        _ = new Formula("8721E-10");
        _ = new Formula("00386e100");
    }

    /// <summary>
    ///     <para> Makes sure the constructor can handle decimal numbers that have scientific notation. </para>
    ///     <remarks> Tests numbers with capital notation (3.7E10) and lowercase notation (3.7e10). Numbers with leading zeroes, and negative exponents are also tested. </remarks>
    ///     <list type="bullet">
    ///         <item> Input: 7.42e10, 961.9572E1004550, 21146.88321E-20, 00942.4551e1000 </item>
    ///         <item> Expected Output: None </item>
    ///     </list>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_SingleDecimalScientificNotationToken_Valid() {
        _ = new Formula("7.42e10");
        _ = new Formula("961.9572E1004550");
        _ = new Formula("21146.88321E-20");
        _ = new Formula("00942.4551e1000");
    }

    /// <summary>
    ///     <para> Tests malformed scientific notation numbers in the formula constructor. </para>
    ///     <remarks> Tests numbers with multiple exponent terms. Tests numbers with capital notation (3e10E20) and lowercase notation (3E10e20). Decimal and numbers with leading zeroes are tested. </remarks>
    ///     <list type="bullet">
    ///         <item> Input: 8E10E10, 8e10e10, 102.44E362310, 0000481e7E20E428 </item>
    ///         <item> Expected Output: FormulaFormatException </item>
    ///     </list>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_IncorrectSingleScientificNotationToken_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("8E10E10"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("8e10e10"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("102.44E362310"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("0000481e7E20E428"));
    }

    /// <summary>
    ///     <para> Tests single variable tokens in the formula constructor </para>
    ///     <remarks> Tests short and long variable names. With both lowercase, and uppercase letters. </remarks>
    ///     <list type="bullet">
    ///         <item> Input: a5, axs55976478, KDBFYUE100, KDBFHkebsis81264671246781729, etc. </item>
    ///         <item> Expected Output: None </item>
    ///     </list>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_SingleVariableToken_Valid() {
        _ = new Formula("a5");
        _ = new Formula("ajhs521");
        _ = new Formula("axs55976478");
        _ = new Formula("asVibFTH5");
        _ = new Formula("KDBFYUE100");
        _ = new Formula("KDBFHkebsis81264671246781729");
        _ = new Formula("uwgudiheOBVUE8");
    }

    /// <summary>
    ///     <para> Tests malformed single variable tokens in the formula constructor. </para>
    ///     <remarks> Variables that don't match the expected naming scheme are tests. Captital and lowercase letters are tested in the variable names. </remarks>
    ///     <list type="bullet">
    ///         <item> Input: a, auegfiuwo, a8i, J, 9K4, 83926jagdws, 7u </item>
    ///         <item> Expected Output: FormulaFormatException </item>
    ///     </list>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_IncorrectSingleVariableToken_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("a"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("auegfiuwo"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("a8i"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("cugqiuwd5371haefudaw"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("7u"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("83926jagdwsc"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("J"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("9K4"));
    }

    /// <summary>
    ///     <para> Tests arithmetic tokens (+, -, *, /) as the only tokens in a formula provided to the constructor. </par>
    ///     <remarks> The four basic arithmetic operators are tested on their own (+, -, *, /) </remarks>
    ///     <list type="bullet">
    ///         <item> Input: +, -, *, / </item>
    ///         <item> Expected Output: FormulaFormatException </item>
    ///     </list>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_SingleArithmeticTokens_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("+"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("-"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("*"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("/"));
    }

    /// <summary>
    ///     <para> Tests every ASCII character as single tokens in the formula constructor. Excluding valid tokens like number characters. </para>
    ///     <remarks> 
    ///         This test iterates through the ASCII table and tests every character as a single token in the formula constructor.
    ///         Skips the characters 0 throguh 9, since they are valid integer tokens when used in the constructor in this way.
    ///      </remarks>
    ///     <list type="bullet">
    ///         <item> Input: +, A, &, #, etc. </item>
    ///         <item> Expected Output: FormulaFormatException </item>
    ///     </list>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_ASCIISingleCharacterTokensExcludingNumbers_Invalid() {
        char[] excludedTokens = [ '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' ];

        for (int i = 66; i <= 127; i++) {
            byte[] charBytes = BitConverter.GetBytes(i);
            char asciiChar = Encoding.ASCII.GetChars(charBytes)[0];

            if (!excludedTokens.Contains(asciiChar)) {
                System.Diagnostics.Trace.WriteLine($"Tested Character (ASCII #{i}) : '{asciiChar}'");
                System.Diagnostics.Trace.WriteLine($"Raw Tested String : \"{new string(asciiChar, 1)}\"");
                Assert.Throws<FormulaFormatException>(() => _ = new Formula(new string(asciiChar, 1)));
            }
        }
    }





    // --- Tests for Valid Token Rule ---

    /// <summary>
    ///     <para> Tests equations with two terms (pairwise) with the basic arithmetic operators (+, -, *, /) </para>
    ///     <remarks> This test inputs integers, decimals, and scientific notation numbers into pairwise equations </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_ArithmeticTokensInPairwiseFormula_Valid() {
        _ = new Formula("8 + 10");
        _ = new Formula("346 - 219");
        _ = new Formula("556428 * 05895318");
        _ = new Formula("00271468716349821 / 195476561281");

        _ = new Formula("8.2 + 19.53");
        _ = new Formula("588.1235 - 964.1335");
        _ = new Formula("5698214.731893 * 06123764.435941");
        _ = new Formula("45619823791581.325698271 / 91238645791.54186931");

        _ = new Formula("85E2 + 96E10");
        _ = new Formula("718E651 - 0835E927");
        _ = new Formula("4645783E241734 * 5655713E025461");
        _ = new Formula("3453687123E564739014 / 87464731245E5864371985");
    }

    [TestMethod]
    public void FormulaConstructor_ModulusAndExponentTokenInPairwiseFormula_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("5 % 2"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("5 ^ 2"));
    }

    public void FormulaConstructor_ArithmeticTokensInLongerFormula_Valid() {
        _ = new Formula("7 + 28 - 64 * 253 / 80");
        _ = new Formula("581 - 498 + j7 / 2543 / 532 * 478");
        _ = new Formula("5739010 * 00946317 / 474631 * 4536821 - 00466743");
        _ = new Formula("83725748192971 * 64372981 * awy4362 * 436189092");

        _ = new Formula("2.7 + 68.63 - 72.25 * 34.63 / 53.12");
        _ = new Formula("4981.3848 * kjvcSzh623478 + 592.6653 - 812.6621 / 451.2514");
        _ = new Formula("4792123.48314 / 00547381.0057481 - 003435617.54315 + 3875843.43124 / 4567831.5764536");
        _ = new Formula("461907651024.547185 / 8748381245.56487125 / 563712455.6546372 / 000000045367814.0000005463712");

        _ = new Formula("78E56 + 97E13 - OPJDUY3245878 / 561E-284 * 12E-0042");
        _ = new Formula("482E1234 - 3984.53E-0004881 / 00673E2140");
        _ = new Formula("43914E9182 * 49582E-48731.5643 + 374831.004318E-000.01256");
        _ = new Formula("3453687123E564739014 / 7464731245E5864371985 + 5748391097E18295841 * 00000567182.5743516E-0047561");
    }

    [TestMethod]
    public void FormulaConstructor_SpecialCharsInLongerFormula_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("fugbw57+541&*45E-1024*3"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("0.31/4521*12Ea32.54+2465;1"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("38#4.42-1244.21*KWsw54E-k12"));
    }
    
    [TestMethod]
    public void FormulaConstructor_ASCIITokensExcludingArithmeticTokensInPairwiseFormula_Invalid() {
        char[] excludedChars = ['+', '-', '*', '/', ')', '('];

        for (int i = 0; i <= 127; i++) {
            
            byte[] charBytes = BitConverter.GetBytes(i);
            char asciiChar = Encoding.ASCII.GetChars(charBytes)[0];

            if (!excludedChars.Contains(asciiChar)) {
                Assert.Throws<FormulaFormatException>(() => _ = new Formula($"5 {asciiChar} 2"));
            }
        }
    }





    // --- Tests for Closing Parenthesis Rule ---

    [TestMethod]
    public void FormulaConstructor_RightHeavyParensInPairwiseFormula_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(8 + 4))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(742434678 + 34.8)))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((2E8 + djAnw54))))"));
    }

    [TestMethod]
    public void FormulaConstructor_RightHeeavyParensWithIncreasingDifference_Invalid() {
        int maxDifference = 10;
        int maxNumberOfParens = 20;

        StringBuilder builder = new();

        for (int i = 1; i <= maxDifference; i++) {
            for (int j = 1; j <= maxNumberOfParens; j++) {
                builder.Append(new string('(', j));
                builder.Append("2341 + 84614");
                builder.Append(new string(')', j + i));

                Assert.Throws<FormulaFormatException>(() => _ = new Formula(builder.ToString()));
                builder.Clear();
            }
        }
    }

    [TestMethod]
    public void FormulaConstructor_RightHeeavyParensWithMultipleTerms_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((541 + 398) / (221 - 4566)))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(2451 - 9)) / (78 * 661E-2198.43)"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((12342E-83 + 0047.5721) / (885 - 45)) * ((6735 / fijwaiSJowmdE00291.54)))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(28.293 / 0582.43725E10)) / (6 - 903) * aHvw287 * 10)) + ((9.9)))"));
    }


    // --- Tests for Balanced Parentheses Rule ---

    [TestMethod]
    public void FormulaConstructor_BalancedParensWithSingleToken_Valid() {
        _ = new Formula("(ajwE643)");
        _ = new Formula("((((45772.4322))))");
        _ = new Formula("((2918))");
        _ = new Formula("(53E20)");
        _ = new Formula("(ajhEjb291E-00482.271)");
        _ = new Formula("((((((2))))))");
        _ = new Formula("((361.5))");
    }


    [TestMethod]
    public void FormulaConstructor_ManyBalancedParensLayersWithSingleToken_Valid() {
        int maxParenCount = 100;
        string insideToken = "ajdbEjd03846278E-00047261.3821";
        StringBuilder builder = new();

        for (int i = 1; i <= maxParenCount; i++) {
            builder.Clear();
            builder.Append(new string('(', i));
            builder.Append(insideToken);
            builder.Append(new string(')', i));

            _ = new Formula(builder.ToString());
        }
    }


    [TestMethod]
    public void FormulaConstructor_BalancedParensInPairwiseFormula_Valid() {
        _ = new Formula("(ajsn36281 + 482E-75.6)");
        _ = new Formula("((297.53 + 6E82.6))");
        _ = new Formula("((5782 + 137))");
        _ = new Formula("(((krSJNei3957 + 144617.341)))");
        _ = new Formula("((((JBEF347 + 1E10))))");
        _ = new Formula("(((58941 + 58931.44)))");
        _ = new Formula("(((((((0.551 + 0001)))))))");
    }

    [TestMethod]
    public void FormulaConstructor_TestWithManyBalancedParenthesis_WithNumbers_InPairwiseFormula_Valid() {
        StringBuilder builder = new();
        int parenthesis = 50;

        for (int i = 1; i <= parenthesis; i++) {
            builder.Append(new string('(', i));
            builder.Append("5653184 * 3824156");
            builder.Append(new string(')', i));

            _ = new Formula(builder.ToString());
            builder.Clear();
        }
    }

    [TestMethod]
    public void FormulaConstructor_TestBalancedParenthesis_WithVariables_InPairwiseFormula_Valid() {
        _ = new Formula("(1 + a2)");
        _ = new Formula("(((b6 + 8)))");
        _ = new Formula("(((zk98 + arw26)))");
    }

    [TestMethod]
    public void FormulaConstructor_TestWithManyBalancedParenthesis_WithVariables_InPairwiseFormula_Valid() {
        StringBuilder builder = new();
        int parenthesis = 50;

        for (int i = 1; i <= parenthesis; i++) {
            builder.Append(new string('(', i));
            builder.Append("sguya6584 * awfs3841");
            builder.Append(new string(')', i));

            _ = new Formula(builder.ToString());
            builder.Clear();
        }
    }


    // --- Tests for First Token Rule

    /// <summary>
    ///   <para>
    ///     Make sure a simple well-formed formula is accepted by the constructor (the constructor
    ///     should not throw an exception).
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is not expected to throw an exception, i.e., it succeeds.
    ///     In other words, the formula "1+1" is a valid formula which should not cause any errors.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestFirstTokenNumber_Valid() {
        _ = new Formula("1+1");
    }

    // --- Tests for  Last Token Rule ---

    // --- Tests for Parentheses/Operator Following Rule ---

    // --- Tests for Extra Following Rule ---
}
