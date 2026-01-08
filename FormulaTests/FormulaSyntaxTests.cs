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
    public void FormulaConstructor_TestNoTokens_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula(string.Empty));
    }

    [TestMethod]
    public void FormulaConstructor_TestSingleNumberToken_Valid() {
        _ = new Formula("5");
    }

    [TestMethod]
    public void FormulaConstructor_TestSingleVariableToken_Valid() {
        _ = new Formula("a5");
    }

    [TestMethod]
    public void FormulaConstructor_TestSingleArithmeticTokens_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("+"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("-"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("*"));
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("/"));
    }

    // --- Tests for Valid Token Rule ---
    [TestMethod]
    public void FormulaConstructor_TestBasicArithmeticTokensInPairwiseFormula_Valid() {
        _ = new Formula("1 + 1");
        _ = new Formula("1 - 1");
        _ = new Formula("1 * 1");
        _ = new Formula("1 / 1");
    }

    [TestMethod]
    public void FormulaConstructor_TestModulusToken_Invalid() {
        Assert.Throws<FormulaFormatException>(() => _ = new Formula("5 % 2"));
    }
    
    [TestMethod]
    public void FormulaConstructor_TestAllASCIITokensExcludingArithmetic_Invalid() {
        char[] validCharacters = ['+', '-', '*', '/', ')', '('];

        for (int i = 0; i <= 127; i++) {
            
            byte[] charBytes = BitConverter.GetBytes(i);
            char ch = Encoding.ASCII.GetChars(charBytes)[0];

            if (!validCharacters.Contains(ch)) {
                Assert.Throws<FormulaFormatException>(() => _ = new Formula($"5 {ch} 2"));
            }
        }
    }


    // --- Tests for Closing Parenthesis Rule

    // --- Tests for Balanced Parentheses Rule

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
