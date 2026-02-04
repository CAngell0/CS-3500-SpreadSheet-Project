namespace FormulaTests;

using Formula;

[TestClass]
public class FormulaEqualsTests {

    // --- TESTS USING THE .Equals METHOD ---
    // - Tests with single token formulas that are equal -

    [TestMethod]
    public void FormulaEqualsMethod_EqualSingleIntegerTokenFormulas_Equal() {
        Formula f1 = new("89");
        Formula f2 = new("89");

        Assert.IsTrue(f1.Equals(f2));
    }

    [TestMethod]
    public void FormulaEqualsMethod_EqualSingleDecimalTokenFormulas_Equal() {
        Formula f1 = new("32.45");
        Formula f2 = new("32.45");

        Assert.IsTrue(f1.Equals(f2));
    }

    [TestMethod]
    public void FormulaEqualsMethod_EqualSingleScientificTokenFormulas_Equal() {
        Formula f1 = new("5e2");
        Formula f2 = new("500");
        Formula f3 = new("5E2");

        Assert.IsTrue(f1.Equals(f2));
        Assert.IsTrue(f2.Equals(f3));
        Assert.IsTrue(f3.Equals(f1));
    }

    [TestMethod]
    public void FormulaEqualsMethod_EqualSingleVariableTokenFormulas_Equal() {
        Formula f1 = new("a15");
        Formula f2 = new("A15");
        Formula f3 = new("A15");

        Assert.IsTrue(f1.Equals(f2));
        Assert.IsTrue(f2.Equals(f3));
        Assert.IsTrue(f3.Equals(f1));
    }
}
