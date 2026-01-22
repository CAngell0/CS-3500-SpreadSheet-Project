// <copyright file="FormulaSyntaxTests.cs" company="UofU-CS3500">
//   Copyright 2024 UofU-CS3500. All rights reserved.
// </copyright>
// <authors> Carson Angell </authors>
// <date> 1/10/2025 </date>

//TODO - Add tests for decimals with no leading zeroes (eg .5)

namespace FormulaTests;

using System.Text;
using Formula;

/// <summary>
///   <para>
///     Unit test cases for the Formula class constructor. Used to test the different implementations made by past students and the instructor/TAs
///   </para>
/// </summary>
[TestClass]
public sealed class FormulaConstructorTests {
    // --- Tests for One Token Rule ---

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
    ///         Short integers, long integers and integers with leading zeroes are tested. 
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
    ///         Decimals with two decimal points are inputted. Along with decimals that have spaces between the period symbol.
    ///         Leading zeroes are also tested
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
    ///             <item> Input: 8E10, 166e10, 8721E-10, 00386e10 </item>
    ///            <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_SingleScientificNotationToken_Valid() {
        _ = new Formula("8E10");
        _ = new Formula("8e10");
        _ = new Formula("166e10");
        _ = new Formula("8721E-10");
        _ = new Formula("00386e10");
    }


    /// <summary>
    ///     <para> Makes sure the constructor can handle decimal numbers that have scientific notation. </para>
    ///     <remarks> 
    ///         Tests numbers with capital notation (3.7E10) and lowercase notation (3.7e10). Numbers with leading zeroes, and negative exponents are also tested. 
    ///         <list type="bullet">
    ///             <item> Input: 7.42e10, 961.9572E10, 21146.88321E-20, 00942.4551e10 </item>
    ///             <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_SingleDecimalScientificNotationToken_Valid() {
        _ = new Formula("7.42e10");
        _ = new Formula("961.9572E10");
        _ = new Formula("21146.88321E-20");
        _ = new Formula("00942.4551e10");

        _ = new Formula("8.31e76");
        _ = new Formula("28571.48421E35");
        _ = new Formula("495782.45822e-43");
        _ = new Formula("002143.9482E10");
    }


    /// <summary>
    ///     <para> Tests malformed scientific notation numbers in the formula constructor. </para>
    ///     <remarks> 
    ///         Tests numbers with multiple exponent terms. Tests numbers with capital notation (3e10E20) and lowercase notation (3E10e20). 
    ///         Decimal and numbers with leading zeroes are tested. 
    ///         <list type="bullet">
    ///             <item> Input: 8E10E10, 8e10e10, 102.44E36, 0000481e7E20E428 </item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///         </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_IncorrectSingleScientificNotationToken_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("8E10E10"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("102e362E10"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("8e10e10"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("102.44E36E10"));
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
    ///         Variables that don't match the expected naming scheme are tests. Capital and lowercase letters are tested in the variable names. 
    ///         <list type="bullet">
    ///             <item> Input: a, auegfiuwo, a8i, J, 9K4, 83926jagdws, 7u </item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_IncorrectSingleVariableToken_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("a"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("a8i"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("7u"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("J"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("9K4"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("auegfiuwo"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("cugqiuwd5371haefudaw"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("83926jagdwsc"));
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
    ///     <para> Tests simple special characters as single tokens inside a string</par>
    ///     <remarks> 
    ///         Four special characters are tests on their own, "&", "#", "$", "!"
    ///         <list type="bullet">
    ///             <item> Input: &, #, $, ! </item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_SingleSpecialCharacterTokens_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("&"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("#"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("$"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("!"));
    }


    /// <summary>
    ///     <para> Tests every ASCII character as single tokens in the formula constructor. Excluding valid tokens like number characters. </para>
    ///     <remarks> 
    ///         This test iterates through the ASCII table and tests every character as a single token in the formula constructor.
    ///         Skips the characters 0 through 9, since they are valid integer tokens when used in the constructor in this way.
    ///         <list type="bullet">
    ///             <item> Input: +, A, &, #, etc. </item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///      </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_ASCIISingleCharacterTokensExcludingNumbers_Invalid() {
        // Characters that are excluded from the test
        char[] excludedTokens = [ '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' ];

        // Iterates through the integers on the ASCII table
        for (int i = 32; i <= 127; i++) {
            // Gets the ASCII character from that integer
            byte[] charBytes = BitConverter.GetBytes(i);
            char asciiChar = Encoding.ASCII.GetChars(charBytes)[0];

            // If the character is not excluded, document and run the test
            if (!excludedTokens.Contains(asciiChar)) {
                System.Diagnostics.Trace.WriteLine($"Tested Character (ASCII #{i}) : '{asciiChar}'");
                System.Diagnostics.Trace.WriteLine($"Raw Tested String : \"{new string(asciiChar, 1)}\"");
                Assert.Throws<FormulaFormatException>(() => _ = new Formula(new string(asciiChar, 1)));
            }
        }
    }





    // --- Tests for Valid Token Rule ---

    /// <summary>
    ///     <para> Tests integer equations with two terms (pairwise) with the basic arithmetic operators (+, -, *, /) </para>
    ///     <remarks> 
    ///         This test inputs integers into two term equations with a single basic operator separating them (+, -, *, /).
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
    ///         This test inputs decimals into two term equations with a single basic operator separating them (+, -, *, /).
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
    ///         This test inputs scientific notation integers into two term equations with a single basic operator separating them (+, -, *, /).
    ///         Also tests with and without spaces between the operators and numbers. Leading zeroes are tested.
    ///         Capital exponent notation (3E10) and lowercase notation (3e10) is tested, along with negative exponents.
    ///         <list type="bullet">
    ///             <item> Input: 85E2 + 96E10, 718E6-0835e92, 718e6 * 0835e92, etc.</item>
    ///             <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_ArithmeticTokensInTwoTermScientificEquation_Valid() {
        _ = new Formula("85E2 + 96E10");
        _ = new Formula("4.54E-10-2124");
        _ = new Formula("718E6-0835e7");
        _ = new Formula("718e6 * 0835e7");
        _ = new Formula("4645783E24* 5655713E-021");
        _ = new Formula("3453687123e5 / 87464731245E9");
    }


    /// <summary>
    ///     <para> Tests equations with variables in two term (pairwise) equations using the basic arithmetic operators (+, -, *, /) </para>
    ///     <remarks>
    ///         This test inputs variables into two term equations with a single basic operator separating them (+, -, *, /).
    ///         Also tests with and without spaces between the operators and numbers.
    ///         <list type="bullet">
    ///             <item> Input: g6 + J9, fhd631-KDBE83472, sjbdEIEDN9331 * dwmwn4783317, JSJiensn20491* eJ9, etc.</item>
    ///             <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_ArithmeticTokensInTwoTermVariableEquation_Valid() {//- Left off here
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
    ///     <para> Tests longer integer equations that have multiple terms, with the basic operators separating them (+, -, *, /) </para>
    ///     <remarks>
    ///         The longer formulas contain multiple terms of integer tokens with basic operators separating them.
    ///         Containing leading zeroes, along with varying spaces between the operators and terms.
    ///         Parenthesis are not used in the equations. Variables are also thrown into the equations at random points.
    ///         <list type="bullet">
    ///             <item> Input: 7 + 28 -64 * 253/80, 5739010 * 00946317 / 474631 * 4536821 - 00466743, etc.</item>
    ///             <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_IntegersWithArithmeticTokensInLongerEquation_Valid() {
        _ = new Formula("7 + 28 -64 * 253/80");
        _ = new Formula("581-498+j7 / 2543 / 532 * 478");
        _ = new Formula("5739010 * 00946317 / 474631 * 4536821 - 00466743");
        _ = new Formula("83725748192971 * 64372981* awy4362 *436189092");
    }


    /// <summary>
    ///     <para> Tests longer decimal equations that have multiple terms, with the basic operators separating them (+, -, *, /) </para>
    ///     <remarks>
    ///         The longer formulas contain multiple terms of decimal tokens with basic operators separating them.
    ///         Containing leading zeroes, along with varying spaces between the operators and terms.
    ///         Parenthesis are not used in the equations. Variables are also thrown into the equations at random points.
    ///         <list type="bullet">
    ///             <item> Input: 2.7 + 68.63 - 72.25 * 34.63 / 53.12, 4981.3848 * kjvcSzh623478 + 592.6653 - 812.6621/451.2514, etc.</item>
    ///             <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_DecimalsWithArithmeticTokensInLongerEquation_Valid() {
        _ = new Formula("2.7 + 68.63 - 72.25 * 34.63 / 53.12");
        _ = new Formula("4981.3848 * kjvcSzh623478 + 592.6653 - 812.6621/451.2514");
        _ = new Formula("4792123.48314 /00547381.0057481 - 003435617.54315 + 3875843.43124 / 4567831.5764536");
        _ = new Formula("461907651024.547185 / 8748381245.56487125/ 563712455.6546372/ 000000045367814.0000005463712");
    }


    /// <summary>
    ///     <para> Tests longer scientific notation equations that have multiple terms, with the basic operators separating them (+, -, *, /) </para>
    ///     <remarks>
    ///         The longer formulas contain multiple terms of scientific notation number tokens with basic operators separating them.
    ///         Containing leading zeroes, along with varying spaces between the operators and terms.
    ///         Capital and lowercase scientific notation is included. Along with negative exponents.
    ///         Parenthesis are not used in the equations. Variables are also thrown into the equations at random points.
    ///         <list type="bullet">
    ///             <item> Input: 78E56 + 97e13 -OPJDUY3245878 / 561E-28 * 12e-42, 482E12 - 3984.53E-28/00673E21, etc.</item>
    ///             <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_ScientificNotationWithArithmeticTokensInLongerEquation_Valid() {
        _ = new Formula("78E56 + 97e13 -OPJDUY3245878 / 561E-28 * 12e-42");
        _ = new Formula("482E12 - 3984.53E-48/00673E21");
        _ = new Formula("43914E41* 49582E-28 + 374831.004318E-5");
        _ = new Formula("3453687123E56 / 7464731245E58 + 5748391097E18 *00000567182.5743516E-4");
    }


    /// <summary>
    ///     <para> Tests longer equations with only variables. Terms are separated with the basic operators separating them (+, -, *, /) </para>
    ///     <remarks>
    ///         The longer formulas contain multiple terms of variable tokens with basic operators separating them.
    ///         Parenthesis are not used in the equations.
    ///         <list type="bullet">
    ///             <item> Input: gfasd5543 + esa35 -OPJDUY3245878 / F4 * dsfFSAFD3543, FAEF34 - GHGefsger3565/FGD0064</item>
    ///             <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_VariablesWithArithmeticTokensInLongerEquation_Valid() {
        _ = new Formula("gfasd5543 + esa35 -OPJDUY3245878 / F4 * dsfFSAFD3543");
        _ = new Formula("FAEF34 - GHGefsger3565/FGD0064");
    }


    /// <summary>
    ///     <para> Tests longer formulas with multiple terms separated by basic operators (+, -, *, /). Except it includes a random special character in the formula.</para>
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
    public void FormulaConstructor_SpecialCharsInLongerEquation_Invalid() {
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
        // Chars to not test for
        char[] excludedChars = ['+', '-', '*', '/', ')', '('];

        // Iterates through the integer numbers of the ASCII table
        for (int i = 32; i <= 127; i++) {
            
            // Gets the ASCII character
            byte[] charBytes = BitConverter.GetBytes(i);
            char asciiChar = Encoding.ASCII.GetChars(charBytes)[0];

            // If the character is not excluded, document and run the test with that character.
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

    /// <summary>
    ///     <para> Tests single tokens wrapped in parenthesis that are right heavy. Meaning that there are more closing parens than opening ones. </para>
    ///     <remarks>
    ///         Integers, decimals, scientific notation, and variable tokens are tested inside the parenthesis.
    ///         The difference in the amount of opening parens vs closing parens also varies.
    ///         <list type="bullet">
    ///             <item> Input: 6522), (65)), (65.742)), (4751E48))), ((wadDJG5653))))))))))</item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_SingleTokenInRightHeavyParens_Invalid() {
        // Assert.Throws<FormulaFormatException>(() => _ = new Formula("6522)"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(65))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(65.742))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(4751E48)))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((wadDJG5653))))))))))"));
    }


    /// <summary>
    ///     <para> Tests two term equations wrapped in right heavy parenthesis. </para>
    ///     <remarks>
    ///         Integers, decimals, scientific notation, and variable tokens are tested inside the parenthesis.
    ///         The difference in the amount of opening parens vs closing parens also varies.
    ///         <list type="bullet">
    ///             <item> Input: 84e31 + a64), (8 + 4)), (742434678 + 34.8))), ((2E8+djAnw54))))</item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_RightHeavyParensInTwoTermEquation_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("84e31 + a64)"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(8 + 4))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(742434678 + 34.8)))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((2E8+djAnw54))))"));
    }


    /// <summary>
    ///     <para> Tests a two term equation inside right heavy parens with increasing amount of difference between the opening and closing parens </para>
    ///     <remarks>
    ///         Puts this two term equation in parenthesis, "2341 + 84614". Then iterates and puts opening and closing parenthesis on either side of the equation in different amount of differences.
    ///         For example, it'll start with a difference of one, "(2341 + 84614))", then a difference of two, "(2341 + 84614)))", and three "(2341 + 84614))))", etc. until it reaches a max difference of 10.
    ///         It will also do it with more opening parens in the varying differences. For example, it will test a difference of 5 with 10 parens "((((((((((2341 + 84614)))))))))))))))".
    ///         It goes to a maximum of 20 opening parens. It tests every one of these combinations.
    ///         <list type="bullet">
    ///             <item> Input: (2341 + 84614)), (2341 + 84614))), (2341 + 84614)))), ((((((((((2341 + 84614))))))))))))))), etc.</item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_RightHeavyParensWithIncreasingDifference_Invalid() {
        // Set some constants for the test and initialize a string builder
        int maxDifference = 10;
        int maxNumberOfParens = 20;
        string equation = "2341 + 84614";
        StringBuilder builder = new();

        // Iterate through the difference values and the number of opening parens to use.
        for (int i = 1; i <= maxDifference; i++) {
            for (int j = 0; j <= maxNumberOfParens; j++) {
                //Add the opening parens, the two term equation, and the closing parens
                builder.Append(new string('(', j));
                builder.Append(equation);
                builder.Append(new string(')', j + i));

                // Run the test and clear the builder
                Assert.Throws<FormulaFormatException>(() => _ = new Formula(builder.ToString()));
                builder.Clear();
            }
        }
    }


    /// <summary>
    ///     <para> Tests more complicated formulas with right heavy parenthesis sets hidden in the formula. </para>
    ///     <remarks>
    ///         The equations are not nested around one set of parenthesis. The formulas contain multiple sets of parenthesis, some balanced, and some right heavy.
    ///         This tests whether the constructor can detect those right heavy sets that are hard to spot.
    ///         Uses all kinds of tokens like integers, decimals, scientific notation, variables and operators.
    ///         <list type="bullet">
    ///             <item> Input: ((541 +398) / (221- 4566))), (28.293 / 0582.43725E10)) / (6 - 903) * aHvw287 * 10))+ ((9.9))), etc.</item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_RightHeavyParensWithMultipleTerms_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((541 +398) / (221- 4566)))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(2451 - 9)) / (78 * 661E-2)"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((12342E-33+ 0047.5721) / (885-45)) * ((6735 /fijwaiSJowmdE00291)))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(28.293 / 0582.43725E10)) / (6 - 903) * aHvw287 * 10))+ ((9.9)))"));
    }





    // --- Tests for Balanced Parentheses Rule ---

    /// <summary>
    ///     <para> Tests single tokens inside of balanced parenthesis. </para>
    ///     <remarks>
    ///         The amount of balanced paren layers varies. Spaces are also put in random spots between parens and between the token and parens.
    ///         Tokens used inside the parens are random and include integers, decimals, scientific notation, and variables
    ///         <list type="bullet">
    ///             <item> Input: (ajwE643), ((( (45772.4322))  )), ( 53E20), etc.</item>
    ///             <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_BalancedParensWithSingleToken_Valid() {
        _ = new Formula("(ajwE643)");
        _ = new Formula("((361.5))");
        _ = new Formula("((( (45772.4322))  ))");
        _ = new Formula("(( 2918 ))");
        _ = new Formula("( 53E20)");
        _ = new Formula("(( ( (((2)) ))))");
    }


    /// <summary>
    ///     <para> Tests several layers of balanced parens around a single token </para>
    ///     <remarks>
    ///         This test iterates and adds different amounts of balanced parenthesis around a single token.
    ///         Starting off with no parenthesis, all the way up to 100 layers of balanced parens.
    ///         The token inside the parens is constant, with a value of "120.54E-00047".
    ///         <list type="bullet">
    ///             <item> Input: (120.54E-00047), (((120.54E-00047))), (((((((120.54E-00047))))))), etc.</item>
    ///             <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_ManyBalancedParenLayersWithSingleToken_Valid() {
        // Sets some constants for the test and initializes a string builder
        int maxParenCount = 100;
        string insideToken = "120.54E-00047";
        StringBuilder builder = new(); //!

        // Iterates through the parenthesis amounts
        for (int i = 0; i <= maxParenCount; i++) {
            System.Diagnostics.Trace.WriteLine($"Parenthesis Count : {i}");

            // Adds the parenthesis along with the constant token in the middle
            builder.Append(new string('(', i));
            builder.Append(insideToken);
            builder.Append(new string(')', i));

            System.Diagnostics.Trace.WriteLine($"Raw Tested String : \"{builder.ToString()}\"");

            // Tests and clears the builder
            _ = new Formula(builder.ToString());
            builder.Clear();
        }
    }


    /// <summary>
    ///     <para> Puts two term equations inside pairs of balanced parenthesis </para>
    ///     <remarks>
    ///         The two term equations use a mix of different tokens like integers, decimals, scientific notation, and variables.
    ///         Operators are also alternated and there is a mix of spaces added between the operators and terms. Along with 
    ///         in between the parenthesis inside the equation.
    ///         <list type="bullet">
    ///             <item> Input: (473+ 76E10), (((   krSJNei3957 +144617.341)) ), (((58941+58931.44))), etc.</item>
    ///             <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_BalancedParensInTwoTermEquation_Valid() {
        _ = new Formula("(473+ 76E10)");
        _ = new Formula("(ajsn36281 / 482E-7  )");
        _ = new Formula("((297.53 + 6E2))");
        _ = new Formula("((5782 * 137))");
        _ = new Formula("(((   krSJNei3957 +144617.341)) )");
        _ = new Formula("(( ((JBEF347 - 1e10))))");
        _ = new Formula("(((58941+58931.44)))");
        _ = new Formula("(( (((((0.551 *001)))  ))) )");
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


    /// <summary>
    ///     <para> Puts multiple terms of multi-layer balanced parenthesis in a longer equation and tests it. </para>
    ///     <remarks>
    ///         The test puts multiple terms of parenthesized single token or two term equations in one longer formula (separated by operators).
    ///         Tokens used are a mix of integers, decimals, scientific notation, and variables. Spaces are mixed into the equation between tokens,
    ///         parenthesis and other spots.
    ///         <list type="bullet">
    ///             <item> Input: ((541 + 398) / (221 -4566))  ), ((12342E-53+0047.5721) / ((885 - 45)) * ((6735 /fijwaiSJowmdE00291)) ), etc.</item>
    ///             <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_BalancedParensWithLongerEquation_Valid() {
        _ = new Formula("(((541 + 398) / (221 -4566))  )");
        _ = new Formula("((2451-9))/ (78 * 661E21)");
        _ = new Formula("((12342E-53+0047.5721) / ((885 - 45)) * ((6735 /fijwaiSJowmdE00291)) )");
        _ = new Formula("(( (28.293 / 0582.43725E10) / (6  - 903) *aHvw287*10)) + (9.9)");
    }

    
    /// <summary>
    ///     <para> Puts singular tokens into parenthesis that are left heavy and unbalanced </para>
    ///     <remarks>
    ///         Tests all four kinds of tokens, integer, decimal, scientific notation and variables.
    ///         Along with varying amounts of parenthesis and difference in balance.
    ///         <list type="bullet">
    ///             <item> Input: ((54135), (((894.24)), (((((((((5642356e10)), ((((((EBSXG47462)</item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_SingleTokenInLeftHeavyParens_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((54135)"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(((894.24))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(((((((((5642356e10))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((((((EBSXG47462)"));
    }

    
    /// <summary>
    ///     <para> Tests two term equations in a left heavy set of parens. </para>
    ///     <remarks>
    ///         Tests all four kinds of tokens, integers, decimals, scientific notation and variables.
    ///         Along with varying amounts of parenthesis and difference in balance.
    ///         Equation terms are separated by basic operators and varying amounts of spaces.
    ///         <list type="bullet">
    ///             <item> Input: ((63132 * 437631), ((((63132.7664/437631e10), (((((((((((((((DUGajw546737 +afjkewh47631))))))))))))))</item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_LeftHeavyParensWithTwoTermEquation_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((63132 * 437631)"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((((63132.7664/437631e10)"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(((((((((((((((DUGajw546737 +afjkewh47631))))))))))))))"));
    }


    /// <summary>
    ///     <para> Tests longer, multi term equations in left heavy sets of parens. </para>
    ///     <remarks>
    ///         The tokens used for the equation terms are random and cover all four types, integer, decimals, scientific notation, and variables.
    ///         The left-heavy parens are snuck into the equation, not every set of parentheses inside the equation is left heavy (but at least one).
    ///         Spaces are also varyingly put between the operators and terms.
    ///         <list type="bullet">
    ///             <item> Input: ((4673871-84 +0047.5721) /((885 - 45)) * ((6735/ fijwaiSJowmdE00291.36))), etc.</item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_LeftHeavyParensWithMultiTermEquation_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((4673871-84 +0047.5721) /(((885 - 45)) * ((6735/ fijwaiSJowmdE00291.36)))"));
    }


    /// <summary>
    ///     <para> Tests a two term equation inside left heavy parens with increasing amount of difference between the opening and closing parens </para>
    ///     <remarks>
    ///         Puts this two term equation in parenthesis, "aihjnd3672 +578367E7". Then iterates and puts opening and closing parenthesis on either side of the equation in different amount of differences.
    ///         For example, it'll start with a difference of one, "((aihjnd3672 +578367E7)", then a difference of two, "(((aihjnd3672 +578367E7)", and three "((((aihjnd3672 +578367E7)", etc. until it reaches a max difference of 10.
    ///         It will also do it with more closing parens in the varying differences. For example, it will test a difference of 5 with 10 parens "(((((((((((((((aihjnd3672 +578367E7))))))))))".
    ///         It goes to a maximum of 20 closing parens. It tests every one of these combinations.
    ///         <list type="bullet">
    ///             <item> Input: ((aihjnd3672 +578367E7), (((aihjnd3672 +578367E7), ((((aihjnd3672 +578367E7), (((((((((((((((aihjnd3672 +578367E7)))))))))), etc.</item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_LeftHeeavyParensWithIncreasingDifference_Invalid() {
        int maxDifference = 10;
        int maxNumberOfParens = 20;
        string equation = "aihjnd3672 +578367E7";
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
    ///     <para> Tests variables as the first token in a two term equation </para>
    ///     <remarks>
    ///         Puts a variable at the beginning of a two term equation. Tests all four of the basic operators (+, -, *, /).
    ///         Along with varying spaces between the operators and the terms.
    ///         Tests with the second term as another token of all types, integer, decimal, scientific notation, and a variable.
    ///         <list type="bullet">
    ///             <item> Input: a6 *387, SAOKDNMFsdk6536 + 387.542, skojefo6/dsaf331, etc.</item>
    ///             <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_FirstTokenVariable_Valid() {
        _ = new Formula("a6 *387");
        _ = new Formula("SAOKDNMFsdk6536 + 387.542");
        _ = new Formula("skojefo6/dsaf331");
        _ = new Formula("jKj431 - D3");
    }

    /// <summary>
    ///     <para> Tests scientific notation numbers as the first token in a two term equation </para>
    ///     <remarks>
    ///         Puts a scientific notation number at the beginning of a two term equation. Tests all four of the basic operators (+, -, *, /).
    ///         Along with varying spaces between the operators and the terms.
    ///         Tests with the second term as another token of all types, integer, decimal, scientific notation, and a variable.
    ///         <list type="bullet">
    ///             <item> Input: a54737E10 -75342, 5483.64e3 + as3731, 574.53e-3 + 02 etc.</item>
    ///             <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_FirstTokenScientificNotation_Valid() {
        _ = new Formula("54737E10 -75342");
        _ = new Formula("5483.64e3 + as3731");
        _ = new Formula("574.53e-3 + 02");
        _ = new Formula("00010E10/4637.432E30");
        _ = new Formula("1e1 - 1E1");
    }
    
    /// <summary>
    ///     <para> Tests a closed paren as the first token in a formula </para>
    ///     <remarks>
    ///         Puts a closed paren in the first character of the string, followed by either an equation or a set of parentheses.
    ///         Spaces are also tested between the parens/equation and the closed paren.
    ///         <list type="bullet">
    ///             <item> Input: )654 * 12), ) ((((asdgfa1243))))), )((4541.87E1)))</item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_FirstTokenCloseParen_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula(")654 * 12)"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula(") ((((asdgfa1243)))))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula(")((4541.87E1)))"));
    }

    /// <summary>
    ///     <para> Tests an operator as the first token in a formula </para>
    ///     <remarks>
    ///         Puts an operator token in the first character of the string, followed by either an equation or a set of parentheses.
    ///         Spaces are also tested between the parens/equation and the operator.
    ///         <list type="bullet">
    ///             <item> Input: +( 731), -86E11, * JD583, /(676.32)</item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_FirstTokenOperator_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("+( 731)"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("-86E11"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("* JD583"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("/(676.32)"));
    }

    /// <summary>
    ///     <para> Tests an operator as the first token in a formula </para>
    ///     <remarks>
    ///         Puts a special character in the first character of the string, followed by either an equation or a set of parentheses.
    ///         Spaces are also tested between the parens/equation and the special character.
    ///         <list type="bullet">
    ///             <item> Input: &(56)), ^8E10 + 20, # (asd467*10)</item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_FirstTokenSpecialChar_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("&(56)"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("^8E10 + 20"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("# (asd467*10)"));
    }





    // --- Tests for Last Token Rule ---

    /// <summary>
    ///     <para> Takes a two term equation and puts a number as the last token in it. </para>
    ///     <remarks>
    ///         The last token number includes integers, scientific notation, and decimals.
    ///         While the first token is any one of this plus variables.
    ///         Spaces are also varied between the operator and the equation terms.
    ///         Leading zeroes are also added in places.
    ///         <list type="bullet">
    ///             <item> Input: 1+1, 63124 +00436271, 7646.57 +587E10, etc. </item>
    ///             <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_LastTokenNumber_Valid() {
        _ = new Formula("1+1");
        _ = new Formula("63124 +00436271");
        _ = new Formula("7646.57 +587E10");
        _ = new Formula("asf36 +587e10");
        _ = new Formula("0002*1241.0 / 0000043652.46");
    }

    /// <summary>
    ///     <para> Takes a two term equation and puts a variable as the last token in it. </para>
    ///     <remarks>
    ///         The first token is any kind of token, integer, decimals, scientific notation and variables.
    ///         Spaces are also varied between the operator and the equation terms.
    ///         Leading zeroes are also added in places.
    ///         <list type="bullet">
    ///             <item> Input: 007523.452 /asf435, 42E10+ GEDF3, dcjh34/a7, etc. </item>
    ///             <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_LastTokenVariable_Valid() {
        _ = new Formula("007523.452 /asf435");
        _ = new Formula("42E10+ GEDF3");
        _ = new Formula("dcjh34/a7");
        _ = new Formula("154732 - J2143");
    }

    /// <summary>
    ///     <para> Tests a open paren as the last token in a formula </para>
    ///     <remarks>
    ///         Puts a open paren as the last character of the string, with either an equation or a set of parentheses before it.
    ///         Spaces are also tested between the parens/equation and the open paren.
    ///         <list type="bullet">
    ///             <item> Input: 578e10 * a5(, (837.34 * 43)  (, ((((4431e3)))))(, (((ajhkdgwihu2312))) (</item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_LastTokenOpenParen_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("578e10 * a5("));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(837.34 * 43)  ("));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((((4431e3)))))("));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(((ajhkdgwihu2312))) ("));
    }

    /// <summary>
    ///     <para> Tests an operator as the last token in a formula </para>
    ///     <remarks>
    ///         Puts an operator as the last character of the string, with either an equation or a set of parentheses before it.
    ///         Spaces are also tested between the parens/equation and the operator.
    ///         <list type="bullet">
    ///             <item> Input: (5432 + 230)* (, 65764 *10 +, J4+, 53E10-, (000.0021)/</item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_LastTokenOperator_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(5432 + 230)*"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("65764 *10 +"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("J4+"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("53E10-"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(000.0021)/"));
    }

    /// <summary>
    ///     <para> Tests a special character as the last token in a formula </para>
    ///     <remarks>
    ///         Puts an special character as the last character of the string, with either an equation or a set of parentheses before it.
    ///         Spaces are also tested between the parens/equation and the operator.
    ///         <list type="bullet">
    ///             <item> Input: 20 + 55&, (74)&, 90E101^, (asd467 * 21e10) #, 493.453 </item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_LastTokenSpecialChar_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("20 + 55&"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(74)&"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("90E101^"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(asd467 * 21e10) #"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("493.453 @"));
    }





    // --- Tests for Parentheses/Operator Following Rule ---

    /// <summary>
    ///     <para> Puts an operator as the first character after an open parenthesis </para>
    ///     <remarks>
    ///         The parentheses are balanced and wrap around a single token inside (after the operator).
    ///         The token is ether a number or a variable. Spaces are tested in between the operator
    ///         and the token as well.
    ///         An operator is also tested on its own as the only token inside the parentheses.
    ///         <list type="bullet">
    ///             <item> Input: (+), ((* 654)), (((/ sdf34))), (-54E10)</item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_OperatorFollowingOpenParen_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(+)"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((* 654))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((( /sdf34)))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(-54E10)"));
    }

    /// <summary>
    ///     <para> Puts a special character as the first character after an open parenthesis </para>
    ///     <remarks>
    ///         The parentheses are balanced and wrap around a single token inside (after the operator).
    ///         The token is ether a number or a variable. Spaces are tested in between the special character
    ///         and the token as well.
    ///         A special character is also tested on its own as the only token inside the parentheses.
    ///         <list type="bullet">
    ///             <item> Input: (&), ((^5431)), ((($ a7))), ( #6j14)</item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_SpecialCharFollowingOpenParen_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(&)"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((^5431))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((($ a7)))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("( #6j14)"));
    }

    /// <summary>
    ///     <para> Puts a closed paren as the first character after an open parenthesis. Essentially making an empty set of parens. </para>
    ///     <remarks>
    ///         Tests both balanced and unbalanced parens. No token is put inside the parens.
    ///         Multiply sets of empty parens put side by side are also tested.
    ///         Spaces between parens and paren sets are also tested.
    ///         <list type="bullet">
    ///             <item> Input: (), ( ), ((()) ), ()(), (( ()) ))  (())</item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_ClosingParenFollowingOpenParen_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("()"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("( )"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("((()) )"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("()()"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(( ()) ))  (())"));
    }

    /// <summary>
    ///     <para> Iterates through the ASCII table and tests putting special characters in front of open parens </para>
    ///     <remarks>
    ///         Puts the special character in a balanced set of parens '($)' and test that for each ASCII character
    ///         except numbers and the open parenthesis.
    ///         <list type="bullet">
    ///             <item> Input: (^), (*), (|), etc. </item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_ASCIICharsFollowingOpenParen_Invalid() {
        // Characters that are excluded from the test
        char[] excludedTokens = [ '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '(' ];

        // Iterates through the integers on the ASCII table
        for (int i = 32; i <= 127; i++) {
            // Gets the ASCII character from that integer
            byte[] charBytes = BitConverter.GetBytes(i);
            char asciiChar = Encoding.ASCII.GetChars(charBytes)[0];

            string formula = $"(${asciiChar})";

            // If the character is not excluded, document and run the test
            if (!excludedTokens.Contains(asciiChar)) {
                System.Diagnostics.Trace.WriteLine($"Tested Character (ASCII #{i}) : '{asciiChar}'");
                System.Diagnostics.Trace.WriteLine($"Raw Tested String : \"{formula}\"");
                Assert.Throws<FormulaFormatException>(() => _ = new Formula(formula));
            }
        }
    }

    /// <summary>
    ///     <para> Tests two term equations that have a set pr parenthesis directly after the operator </para>
    ///     <remarks>
    ///         The parenthesis are balanced and contain a single token.
    ///         The tokens in each term or pair of parens are either numbers orm variables.
    ///         spaces are put between parens/terms and the operator, along with in between paren characters.
    ///         <list type="bullet">
    ///             <item> Input: 64 + (541), (((6234.65)))* (agfe354), 6587.531E-21-((( (( (aiewhd67387  )))) )), etc. </item>
    ///             <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_OpenParenFollowingOperator_Valid() {
        _ = new Formula("64 + (541)");
        _ = new Formula("(((6234.65)))* (agfe354)");
        _ = new Formula("984/(534.54e10)");
        _ = new Formula("anfd7346-(((431.453)))");
        _ = new Formula("6587.531E-21-((( (( (aiewhd67387  )))) ))");
    }

    /// <summary>
    ///     <para> Takes a two term equation and puts two operators between the terms, side by side. </para>
    ///     <remarks>
    ///         The tokens used for the equation terms are either numbers or variables. Spaces
    ///         are varied between operators and terms. Balanced parentheses are also wrapped
    ///         around some of the equations.
    ///         <list type="bullet">
    ///             <item> Input: 54 +/ afd32, 4782E-3--566, (48632.4673** 123), (((djd6347--3932))), etc. </item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_OperatorFollowingOperator_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("54 +/ afd32"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("4782E-3--566"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(48632.4673** 123)"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(((djd6347--3932)))"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(5421E10*//3412)"));
    }

    /// <summary>
    ///     <para> Takes a two term equation and puts an operator followed by a special character between the terms, side by side. </para>
    ///     <remarks>
    ///         The tokens used for the equation terms are either numbers or variables. Spaces
    ///         are varied between operators/special characters and terms. Balanced parentheses are also wrapped
    ///         around some of the equations.
    ///         <list type="bullet">
    ///             <item> Input: 6542 +& af324, (64321.654 -^453245E12), ajisdh3341+= 565.432 etc. </item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_SpecialCharFollowingOperator_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("6542 +& af324"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(64321.654 -^453245E12)"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("ajisdh3341+= 565.432"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("(((a442+$af324)))"));
    }

    /// <summary>
    ///     <para> Takes a two term equation and tests every ASCII character to go directly after the operator in that equation, side by side. </para>
    ///     <remarks>
    ///         Takes the following two term equation, "456736 + 31E10" and puts a special character directly after the + operator.
    ///         Tests for every ASCII character, except for numbers and the open parenthesis.
    ///         <list type="bullet">
    ///             <item> Input: 456736 +^ 31E10, 456736 +| 31E10, 456736 +% 31E10 </item>
    ///             <item> Expected Output: FormulaFormatException </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_ASCIICharsFollowingOperator_Invalid() {
        // Characters that are excluded from the test
        char[] excludedTokens = [ '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '(' ];

        // Iterates through the integers on the ASCII table
        for (int i = 32; i <= 127; i++) {
            // Gets the ASCII character from that integer
            byte[] charBytes = BitConverter.GetBytes(i);
            char asciiChar = Encoding.ASCII.GetChars(charBytes)[0];

            string formula = $"456736 +${asciiChar} 31E10";

            // If the character is not excluded, document and run the test
            if (!excludedTokens.Contains(asciiChar)) {
                System.Diagnostics.Trace.WriteLine($"Tested Character (ASCII #{i}) : '{asciiChar}'");
                System.Diagnostics.Trace.WriteLine($"Raw Tested String : \"{formula}\"");
                Assert.Throws<FormulaFormatException>(() => _ = new Formula(formula));
            }
        }
    }



    // --- Tests for Extra Following Rule ---
    
    /// <summary>
    ///     <para> Takes a two term equation and makes the first term wrapped in parens. Then follows the closed paren with an operator and the second term </para>
    ///     <remarks>
    ///         The two terms used are either a number or variable. The parentheses are balanced and come in varying layers.
    ///         Spaces are varied between the operator and the two terms (and parens).
    ///         <list type="bullet">
    ///             <item> Input: (572) + 563891, (((584E1))) -483E10, (572)/ 5748.543e20, (af325)*6542.641, etc. </item>
    ///             <item> Expected Output: None </item>
    ///         </list>
    ///     </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_OperatorFollowingClosedParen_Valid() {
        _ = new Formula("(572) + 563891");
        _ = new Formula("(af325)*6542.641");
        _ = new Formula("(((584E1))) -483E10");
        _ = new Formula("(572)/ 5748.543e20");
        _ = new Formula("((572))-5848E-10");
    }
}
