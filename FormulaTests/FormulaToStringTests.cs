namespace FormulaTests;

using Formula;
[TestClass]
public class FormulaToStringTests {
    [TestMethod]
    public void FormulaToString_SingleIntegerToken_ReturnsCanonicalForm() {
        Assert.AreEqual("56", new Formula("56").ToString());
    }

    [TestMethod]
    public void FormulaToString_SingleIntegerTokenWithLeadingZeroes_ReturnsCanonicalForm() {
        Assert.AreEqual("547", new Formula("000547").ToString());
        Assert.AreEqual("547000", new Formula("0547000").ToString());
    }

    [TestMethod]
    public void FormulaToString_SingleDecimalToken_ReturnsCanonicalForm() {
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
}
