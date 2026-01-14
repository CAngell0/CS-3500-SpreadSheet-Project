// <copyright file="FormulaSyntaxTests.cs" company="UofU-CS3500">
//   Copyright 2024 UofU-CS3500. All rights reserved.
// </copyright>
// <authors> [Insert Your Name] </authors>
// <date> [Insert the Date] </date>

namespace FormulaTests;

using System.Text;
using CS3500.Formula3; // Change this using statement to use different formula implementations.

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
    // --- Tests for One Token Rule --- DONE

    /// <summary>
    ///     <para> Tests the formula constructor with an empty string </para>
    ///     <remarks>
    ///         Inputs string.Empty into the formula constructor.
    ///         <list type="bullet">
    ///             <item> Input: string.Empty </item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_NoTokens_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula(string.Empty));
    }

    /// <summary>
    ///     <para> This test makes sure the constructor can handle single integer tokens. </para>
    ///     <remarks> 
    ///         Short integers, long integers and inegers with leading zeroes arae tested. 
    ///         <list type="bullet">
    ///             <item> Input: 5, 598582, 00028532, etc. </item>
    ///             <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
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
    ///     <remarks> 
    ///         Small decimals, long decimals and decimal numbers with leading zeroes are tested. 
    ///         <list type="bullet">
    ///             <item> Input: 7.1, 545.02744192, 0072.2861, etc. </item>
    ///             <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
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
    ///     <remarks> 
    ///         Decimals with two decimal points are inputted. Leading zeroes are also tested. 
    ///         <list type="bullet">
    ///             <item> Input: 5721.59572.39481, 08812.456.1 </item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_IncorrectSingleDecimalToken_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("5721.59572.39481"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("008812.456.1"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("008812 . 456"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("008812. 456"));
    }

    /// <summary>
    ///     <para> Makes sure the constructor can handle numbers with scientific notation. </para>
    ///     <remarks> 
    ///         Tests numbers with capital notation (3E10) and lowercase notation (3e10). Numbers with leading zeroes are also tested. 
    ///         <list type="bullet">
    ///             <item> Input: 8E10, 166e100000, 8721E-10, 00386e100 </item>
    ///            <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_SingleScientificNotationToken_Valid() {
        _ = new Formula("8E10");
        _ = new Formula("8e10"); //! Assertion Fails
        _ = new Formula("166e100000"); //! Assertion Fails
        _ = new Formula("8721E-10");
        _ = new Formula("00386e100"); //! Assertion Fails
    }

    /// <summary>
    ///     <para> Makes sure the constructor can handle decimal numbers that have scientific notation. </para>
    ///     <remarks> 
    ///         Tests numbers with capital notation (3.7E10) and lowercase notation (3.7e10). Numbers with leading zeroes, and negative exponents are also tested. 
    ///         <list type="bullet">
    ///             <item> Input: 7.42e10, 961.9572E1004550, 21146.88321E-20, 00942.4551e1000 </item>
    ///             <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_SingleDecimalScientificNotationToken_Valid() {
        _ = new Formula("7.42e10");
        _ = new Formula("961.9572E1004550");
        _ = new Formula("21146.88321E-20");
        _ = new Formula("00942.4551e1000");

        _ = new Formula("8.31e76.7");
        _ = new Formula("28571.48421E855631");
        _ = new Formula("495782.45822e-43");
        _ = new Formula("002143.9482E100");
    }


    /// <summary>
    ///     <para> Tests malformed scientific notation numbers in the formula constructor. </para>
    ///     <remarks> 
    ///         Tests numbers with multiple exponent terms. Tests numbers with capital notation (3e10E20) and lowercase notation (3E10e20). 
    ///         Decimal and numbers with leading zeroes are tested. 
    ///         <list type="bullet">
    ///             <item> Input: 8E10E10, 8e10e10, 102.44E362310, 0000481e7E20E428 </item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
///         </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_IncorrectSingleScientificNotationToken_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("8E10E10"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("102e362E10"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("8e10e10"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("102.44E362310"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("0000481e7E20E428"));
    }

    /// <summary>
    ///     <para> Tests single variable tokens in the formula constructor </para>
    ///     <remarks> 
    ///         Tests short and long variable names. With both lowercase, and uppercase letters. 
    ///         <list type="bullet">
    ///             <item> Input: a5, axs55976478, KDBFYUE100, KDBFHkebsis81264671246781729, etc. </item>
    ///             <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
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
    ///     <remarks> 
    ///         Variables that don't match the expected naming scheme are tests. Captital and lowercase letters are tested in the variable names. 
    ///         <list type="bullet">
    ///             <item> Input: a, auegfiuwo, a8i, J, 9K4, 83926jagdws, 7u </item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_IncorrectSingleVariableToken_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("a")); //! Assertion Fails
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("auegfiuwo")); //! Assertion Fails
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("a8i")); //! Assertion Fails
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("cugqiuwd5371haefudaw")); //! Assertion Fails
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("7u"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("83926jagdwsc"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("J")); //! Assertion Fails
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("9K4"));
    }

    /// <summary>
    ///     <para> Tests arithmetic tokens (+, -, *, /) as the only tokens in a formula provided to the constructor. </par>
    ///     <remarks> 
    ///         The four basic arithmetic operators are tested on their own (+, -, *, /) 
    ///         <list type="bullet">
    ///             <item> Input: +, -, *, / </item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
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
    ///         <list type="bullet">
    ///             <item> Input: +, A, &, #, etc. </item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///      </remarks>
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





    // --- Tests for Valid Token Rule --- DONE

    /// <summary>
    ///     <para> Tests integer equations with two terms (pairwise) with the basic arithmetic operators (+, -, *, /) </para>
    ///     <remarks> 
    ///         This test inputs integers into two term equations with a single basic operator seperating them (+, -, *, /).
    ///         Also tests with and without spaces between the operators and integers. Integers with leading zeroes are tested.
    ///         <list type="bullet">
    ///             <item> Input: 8 +10, 346 - 219, 556428*05895318, 00271468716349821 / 195476561281</item>
    ///             <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_ArithmeticTokensInTwoTermEquation_Valid() {
        _ = new Formula("8 +10");
        _ = new Formula("346 - 219");
        _ = new Formula("556428*05895318");
        _ = new Formula("00271468716349821 / 195476561281");
    }

    /// <summary>
    ///     <para> Tests decimal equations with two terms (pairwise) with the basic arithmetic operators (+, -, *, /) </para>
    ///     <remarks>
    ///         This test inputs decimals into two term equations with a single basic operator seperating them (+, -, *, /).
    ///         Also tests with and without spaces between the operators and numbers. Decimals with leading zeroes are tested.
    ///         <list type="bullet">
    ///             <item> Input: 8.2 + 19.53, 588.1235 -964.1335, 5698214.731893 * 06123764.435941, etc.</item>
    ///             <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_ArithmeticTokensInTwoTermDecimalEquation_Valid() {
        _ = new Formula("8.2 + 19.53");
        _ = new Formula("588.1235 -964.1335");
        _ = new Formula("5698214.731893 * 06123764.435941");
        _ = new Formula("45619823791581.325698271 / 91238645791.54186931");
    }

    /// <summary>
    ///     <para> Tests scientific notation equations with two terms (pairwise) with the basic arithmetic operators (+, -, *, /) </para>
    ///     <remarks>
    ///         This test inputs scientific notation integers into two term equations with a single basic operator seperating them (+, -, *, /).
    ///         Also tests with and without spaces between the operators and numbers. Leading zeroes are tested.
    ///         Captial exponent notation (3E10) and lowercase notation (3e10) is tested, along with negative exponents.
    ///         <list type="bullet">
    ///             <item> Input: 85E2 + 96E10, 718E651-0835e927, 718e651 * 0835e927, etc.</item>
    ///             <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_ArithmeticTokensInTwoTermScientificEquation_Valid() {
        _ = new Formula("85E2 + 96E10");
        _ = new Formula("718E651-0835e927");
        _ = new Formula("718e651 * 0835e927");
        _ = new Formula("4645783E241734* 5655713E-025461");
        _ = new Formula("3453687123e564739014 / 87464731245E5864371985");
    }

    /// <summary>
    ///     <para> Tests equations with variables in two term (pairwise) equations using the basic arithmetic operators (+, -, *, /) </para>
    ///     <remarks>
    ///         This test inputs variables into two term equations with a single basic operator seperating them (+, -, *, /).
    ///         Also tests with and without spaces between the operators and numbers.
    ///         <list type="bullet">
    ///             <item> Input: g6 + J9, fhd631-KDBE83472, sjbdEIEDN9331 * dwmwn4783317, JSJiensn20491* eJ9, etc.</item>
    ///             <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_ArithmeticTokensInTwoTermVariableEquation_Valid() {
        _ = new Formula("g6 + J9");
        _ = new Formula("fhd631-KDBE83472");
        _ = new Formula("sjbdEIEDN9331 * dwmwn4783317");
        _ = new Formula("JSJiensn20491* eJ9");
        _ = new Formula("KDND9383 / k483883");
    }

    /// <summary>
    ///     <para> Tests two term equations with the other common arithmetic operators besides the basic ones </para>
    ///     <remarks> 
    ///         Tests the modulus, exponent and factorial operators (%, ^, !) in two term equations. 
    ///         <list type="bullet">
    ///             <item> Input: 5% 2, 5^2, 5! * 6!</item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_OtherCommonArithmeticOperatorTokensInTwoTermEquations_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("5% 2"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("5^2"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("5! * 6!"));
    }


    /// <summary>
    ///     <para> Tests longer equations that have multiple terms, with the basic operators seperating them (+, -, *, /) </para>
    ///     <remarks>
    ///         The longer formulas contain multiple terms of random tokens. Integers, decimals, scientific notation, and variables.
    ///         Containing leading zeroes, negative exponents, and capital/lowercase exponent notatiosn. Along with varying spaces between the operators and terms.
    ///         <list type="bullet">
    ///             <item> Input: 7 + 28 -64 * 253/80, 4981.3848 * kjvcSzh623478 + 592.6653 - 812.6621/451.2514, etc.</item>
    ///             <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_ArithmeticTokensInLongerFormula_Valid() { //- Test marked for check
        _ = new Formula("7 + 28 -64 * 253/80");
        _ = new Formula("581-498+j7 / 2543 / 532 * 478");
        _ = new Formula("5739010 * 00946317 / 474631 * 4536821 - 00466743");
        _ = new Formula("83725748192971 * 64372981* awy4362 *436189092");

        _ = new Formula("2.7 + 68.63 - 72.25 * 34.63 / 53.12");
        _ = new Formula("4981.3848 * kjvcSzh623478 + 592.6653 - 812.6621/451.2514");
        _ = new Formula("4792123.48314 /00547381.0057481 - 003435617.54315 + 3875843.43124 / 4567831.5764536");
        _ = new Formula("461907651024.547185 / 8748381245.56487125/ 563712455.6546372/ 000000045367814.0000005463712");

        _ = new Formula("78E56 + 97e13 -OPJDUY3245878 / 561E-284 * 12e-0042");
        _ = new Formula("482E1234 - 3984.53E-0004881/00673E2140");
        _ = new Formula("43914E9182* 49582E-48731 + 374831.004318E-5");
        _ = new Formula("3453687123E564739014 / 7464731245E5864371985 + 5748391097E18295841 *00000567182.5743516E-0047561");
    }

    /// <summary>
    ///     <para> Tests longer formulas with multiple terms seperated by basic operators (+, -, *, /). Except it includes a random special character in the formula.</para>
    ///     <remarks> 
    ///         The tokens used are a random combination of integers, decimals, scientific notation, and variables. 
    ///         With negative exponents, and capital/lowercase notations for scientific notation.
    ///         <list type="bullet">
    ///             <item> Input: 7 + 28 -64 * 253/80, 4981.3848 * kjvcSzh623478 + 592.6653 - 812.6621/451.2514, etc.</item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_SpecialCharsInLongerFormula_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("fugbw57+541&*45E-1024*3"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("0.31/4521*12-32.54+2465;1"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("38#4.42-1244.21*KWsw54e-12"));
    }
    
    /// <summary>
    ///     <para> Tests all ASCII characters, minus the basic operators and parenthesis (+, -, /, *, (, )) between the terms of a two term equation </para>
    ///     <remarks>
    ///         This test iterates through the ASCII table and puts each character in between the two terms of an equation. Making it act like an operator.
    ///         It also tests spaces between the two operator and the terms. It tests one space on the left, one space on the right and spaces on both sides.
    ///         <list type="bullet">
    ///             <item> Input: 5 & 2, 5^ 2, 5 #2, etc.</item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_ASCIITokensExcludingArithmeticTokensInTwoTermEquation_Invalid() {
        char[] excludedChars = ['+', '-', '*', '/', ')', '('];

        for (int i = 0; i <= 127; i++) {
            
            byte[] charBytes = BitConverter.GetBytes(i);
            char asciiChar = Encoding.ASCII.GetChars(charBytes)[0];

            if (!excludedChars.Contains(asciiChar)) {
                System.Diagnostics.Trace.WriteLine($"Tested Character (ASCII #{i}) : '{asciiChar}'");
                System.Diagnostics.Trace.WriteLine($"Raw Tested String : \"{$"5 {asciiChar} 2"}\"");

                Assert.Throws<FormulaFormatException>(() => _ = new Formula($"5 {asciiChar} 2"));
                Assert.Throws<FormulaFormatException>(() => _ = new Formula($"5{asciiChar} 2"));
                Assert.Throws<FormulaFormatException>(() => _ = new Formula($"5 {asciiChar}2"));
            }
        }
    }





    // --- Tests for Closing Parenthesis Rule ---

    [TestMethod]
    public void FormulaConstructor_SingleTokenInRightHeavyParens_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(65))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(65.742))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(4751E482.47)))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((wadDJG5653))))))))))"));
    }

    [TestMethod]
    public void FormulaConstructor_RightHeavyParensInTwoTermEquation_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(8 + 4))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(742434678 + 34.8)))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((2E8+djAnw54))))"));
    }

    [TestMethod]
    public void FormulaConstructor_RightHeeavyParensWithIncreasingDifference_Invalid() {
        int maxDifference = 10;
        int maxNumberOfParens = 20;
        string equation = "2341 + 84614";
        StringBuilder builder = new();

        for (int i = 1; i <= maxDifference; i++) {
            for (int j = 1; j <= maxNumberOfParens; j++) {
                builder.Append(new string('(', j));
                builder.Append(equation);
                builder.Append(new string(')', j + i));

                Assert.Throws<FormulaFormatException>(() => _ = new Formula(builder.ToString()));
                builder.Clear();
            }
        }
    }

    [TestMethod]
    public void FormulaConstructor_RightHeeavyParensWithMultipleTerms_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((541 +398) / (221- 4566)))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(2451 - 9)) / (78 * 661E-2198)"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((12342E-83+ 0047.5721) / (885-45)) * ((6735 /fijwaiSJowmdE00291)))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(28.293 / 0582.43725E10)) / (6 - 903) * aHvw287 * 10))+ ((9.9)))"));
    }


    // --- Tests for Balanced Parentheses Rule ---

    [TestMethod]
    public void FormulaConstructor_BalancedParensWithSingleToken_Valid() {
        _ = new Formula("(ajwE643)");
        _ = new Formula("((((45772.4322))))");
        _ = new Formula("((2918))");
        _ = new Formula("(53E20)");
        _ = new Formula("(ajhEjb291E-00482)");
        _ = new Formula("((((((2))))))");
        _ = new Formula("((361.5))");
    }


    [TestMethod]
    public void FormulaConstructor_ManyBalancedParenLayersWithSingleToken_Valid() {
        int maxParenCount = 100;
        string insideToken = "ajdbEjd03846278E-00047261";
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
    public void FormulaConstructor_BalancedParensInTwoTermEquation_Valid() {
        _ = new Formula("(473 + 76E10)");
        _ = new Formula("(ajsn36281 + 482E-75)");
        _ = new Formula("((297.53 + 6E82.6))"); //! Assertion Fails
        _ = new Formula("((5782 + 137))");
        _ = new Formula("(((krSJNei3957 + 144617.341)))");
        _ = new Formula("((((JBEF347 + 1E10))))"); //! Assertion Fails
        _ = new Formula("(((58941 + 58931.44)))");
        _ = new Formula("(((((((0.551 + 0001)))))))");
    }

    [TestMethod]
    public void FormulaConstructor_BalancedParensWithMultipleTerms_Valid() {
        _ = new Formula("(((541 + 398) / (221 -4566)))");
        _ = new Formula("((2451 - 9))/ (78 * 661E210)");
        _ = new Formula("((12342E-83+0047.5721) / ((885 - 45)) * ((6735 /fijwaiSJowmdE00291)))");
        _ = new Formula("(((28.293 / 0582.43725E10) / (6 - 903) *aHvw287*10)) + (9.9)");
    }

    [TestMethod]
    public void FormulaConstructor_ManyBalancedParensInTwoTermEquation_Valid() {
        StringBuilder builder = new();
        char operatorToken = '*';
        string termOne = "5653184.42";
        string termTwo = "dsugau34735";
        int parenthesis = 100;

        for (int i = 1; i <= parenthesis; i++) {
            for (int j = 0; j < 3; j++) {
                builder.Append(new string('(', i));
                builder.Append(termOne);
                if (j == 0 || j == 1) builder.Append(' ');
                builder.Append(operatorToken);
                if (j == 1 || j == 2) builder.Append(' ');
                builder.Append(termTwo);
                builder.Append(new string(')', i));

                _ = new Formula(builder.ToString());
                builder.Clear();
            }
        }
    }

    [TestMethod]
    public void FormulaConstructor_SingleTokenInLeftHeavyParens_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((54135)"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(((894.24))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(((((((((5642356e10))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((((((EBSXG47462)"));
    }

    [TestMethod]
    public void FormulaConstructor_LeftHeavyParensWithTwoTermEquation_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((63132 * 437631)"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((((63132.7664/437631e10)"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(((((((((((((((DUGajw546737 +afjkewh47631))))))))))))))"));
    }

    [TestMethod]
    public void FormulaConstructor_LeftHeavyParensWithMultiTermEquation_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((4673871-84 + 0047.5721) / ((885 - 45)) * ((6735 / fijwaiSJowmdE00291.36)))"));
    }

    [TestMethod]
    public void FormulaConstructor_LeftHeeavyParensWithIncreasingDifference_Invalid() {
        int maxDifference = 10;
        int maxNumberOfParens = 20;
        string equation = "aihjnd3672 +578367E104";
        StringBuilder builder = new();

        for (int i = 1; i <= maxDifference; i++) {
            for (int j = 1; j <= maxNumberOfParens; j++) {
                builder.Append(new string('(', j + i));
                builder.Append(equation);
                builder.Append(new string(')', j));

                Assert.Throws<FormulaFormatException>(() => _ = new Formula(builder.ToString()));
                builder.Clear();
            }
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
    public void FormulaConstructor_FirstTokenNumber_Valid() {
        _ = new Formula("1+1");
        _ = new Formula("1413 +0038631");
        _ = new Formula("000001*43732.5442 / 43135");
    }

    [TestMethod]
    public void FormulaConstructor_FirstTokenVariable_Valid() {
        _ = new Formula("a6 *387");
        _ = new Formula("SAOKDNMFsdk6536 + 387.542");
        _ = new Formula("skojefo6/dsaf331");
        _ = new Formula("jKj431 - D3");
    }

    [TestMethod]
    public void FormulaConstructor_FirstTokenScientificNotation_Valid() {
        _ = new Formula("54737E10 -75342");
        _ = new Formula("5483.64e3 + as3731");
        _ = new Formula("00010E010/4637.432E030");
        _ = new Formula("1e1 - 1E1");
    }

    [TestMethod]
    public void FormulaConstructor_FirstTokenOpenParen_Valid() {
        _ = new Formula("(6 + 7)");
        _ = new Formula("(((((jahwed3768)))))");
        _ = new Formula("(((437.3E10)))");
    }
    [TestMethod]
    public void FormulaConstructor_FirstTokenCloseParen_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula(")654 * 12)"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula(") ((((asdgfa1243)))))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula(")((4541.87E1)))"));
    }

    [TestMethod]
    public void FormulaConstructor_FirstTokenOperator_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("+(731)"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("-86E11"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("* JD583"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("/(676.32)"));
    }

    [TestMethod]
    public void FormulaConstructor_FirstTokenSpecialChar_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("&(56)"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("^8E10"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("# asd467"));
    }

    // --- Tests for Last Token Rule ---

    [TestMethod]
    public void FormulaConstructor_LastTokenNumber_Valid() {
        _ = new Formula("1+1");
        _ = new Formula("63124 +00436271");
        _ = new Formula("0002*1241.0 / 0000043652.46");
    }

    [TestMethod]
    public void FormulaConstructor_LastTokenVariable_Valid() {
        _ = new Formula("7523.452 /asf435");
        _ = new Formula("42E10+ GEDF3");
        _ = new Formula("dcjh34/a7");
        _ = new Formula("1E1 - J2143");
    }

    [TestMethod]
    public void FormulaConstructor_LastTokenScientificNotation_Valid() {
        _ = new Formula("6452 +677212E12");
        _ = new Formula("652.1E1 + 6e2");
        _ = new Formula("saeghfsae3241/46876.431E10");
        _ = new Formula("1E3 - 1e1");
    }

    [TestMethod]
    public void FormulaConstructor_LastTokenCloseParen_Valid() {
        _ = new Formula("(a2 + 16E12)");
        _ = new Formula("(((125.53)))");
        _ = new Formula("((((((((((JSHSGH7835271)))))))))");
    }
    [TestMethod]
    public void FormulaConstructor_LastTokenOpenParen_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(837.34 * 43)  ("));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((((4431e123)))))("));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(((ajhkdgwihu2312))) ("));
    }

    [TestMethod]
    public void FormulaConstructor_LastTokenOperator_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(5432)*"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("J4+"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("53E10-"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(000.0021)/"));
    }

    [TestMethod]
    public void FormulaConstructor_LastTokenSpecialChar_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(74)&"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("90E101^"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("asd467 #"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("493.453 @"));
    }

    // --- Tests for Parentheses/Operator Following Rule ---






    // --- Tests for Extra Following Rule ---
}
