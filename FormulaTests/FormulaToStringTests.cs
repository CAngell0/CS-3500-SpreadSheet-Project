namespace FormulaTests;

using Formula;
[TestClass]
public class FormulaToStringTests {

    // --- Tests for single integer tokens ---
    [TestMethod]
    public void FormulaToString_SingleIntegerToken_ReturnsUnchanged() {
        Assert.AreEqual("56", new Formula("56").ToString());
    }

    [TestMethod]
    public void FormulaToString_SingleIntegerTokenWithLeadingZeroes_ReturnsCanonicalForm() {
        Assert.AreEqual("547", new Formula("000547").ToString());
        Assert.AreEqual("547000", new Formula("0547000").ToString());
    }

    [TestMethod]
    public void FormulaToString_SingleDecimalToken_ReturnsUnchanged() {
        Assert.AreEqual("8.2", new Formula("8.2").ToString());
    }

    [TestMethod]
    public void FormulaToString_SingleDecimalTokenWithNoExtraZero_ReturnsCanonicalForm() {
        Assert.AreEqual("0.75", new Formula(".75").ToString());
    }

    [TestMethod]
    public void FormulaToString_SingleDecimalTokenWithLeadingZeroes_ReturnsCanonicalForm() {
        Assert.AreEqual("63.21", new Formula("00063.21").ToString());
        Assert.AreEqual("63.21", new Formula("63.210000").ToString());
        Assert.AreEqual("63.00021", new Formula("63.00021").ToString());
    }

    [TestMethod]
    public void FormulaToString_SingleScientificToken_ReturnsCanonicalForm() {
        Assert.AreEqual("500", new Formula("5e2").ToString());
        Assert.AreEqual("500", new Formula("5E2").ToString());
    }

    [TestMethod]
    public void FormulaToString_SingleScientificTokenWithLeadingZeroes_ReturnsCanonicalForm() {
        Assert.AreEqual("500", new Formula("0005e2").ToString());
        Assert.AreEqual("500", new Formula("5E002").ToString());
        Assert.AreEqual("500", new Formula("0005E002").ToString());
    }



    // --- Tests for single variable tokens ---
    [TestMethod]
    public void FormulaToString_SingleVariableToken_ReturnsUnchanged() {
        Assert.AreEqual("ABC123", new Formula("ABC123").ToString());
    }

    [TestMethod]
    public void FormulaToString_SingleVariableToken_ReturnsCanonicalForm() {
        Assert.AreEqual("AGD478", new Formula("agd478").ToString());
        Assert.AreEqual("ASBHJS9", new Formula("aSbhJs9").ToString());
    }
    
    /// <summary>
    ///     Since variable names are really column-row coordinate for the spreadshett, that would mean
    ///     the number part of the variable name should be treated as a pure number. Meaning any
    ///     leading zeroes would be removed in canonical form. This tests that.
    ///         Ex:  "a05" -> "A5"
    /// </summary>
    [TestMethod]
    public void FormulaToString_SingleVariableTokenWithLeadingZeroes_ReturnsCanonicalForm() {
        Assert.AreEqual("ABC123", new Formula("abc00123").ToString());
    }



    // --- Tests with longer equations ---
    [TestMethod]
    public void FormulaToString_TwoTermEquation_ReturnsCanonicalForm() {
        Assert.AreEqual("45.32*200000", new Formula("45.3200 * 2e5").ToString());
        Assert.AreEqual("90/JED1", new Formula("090 / jEd1").ToString());
    }

    [TestMethod]
    public void FormulaToString_TwoTermEquationWithParentheses_ReturnsCanonicalForm() {
        Assert.AreEqual("((45.32*200000))", new Formula("((45.3200 * 2e5))").ToString());
        Assert.AreEqual("(90)/JED1", new Formula("(090) / jEd1").ToString());
        Assert.AreEqual("500+((((((((((((K9))))))))))))", new Formula("5E02 + ((((((((((((k9))))))))))))").ToString());
    }

    [TestMethod]
    public void FormulaToString_EquationsInsideParentheses_ReturnsCanonicalForm() {
        Assert.AreEqual("28+(ABD34*4000)", new Formula("28 + (abd34 * 4e00003)").ToString());
        Assert.AreEqual("(0.56-23)*(((AL2/10)))", new Formula("(.5600-23) * (((al2/1e1)))").ToString());
    }
}
