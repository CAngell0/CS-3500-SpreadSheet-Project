namespace FormulaTests;

using Formula;

[TestClass]
public class FormulaEqualsTests {

    // --- TESTS WITH EQUAL FORMULAS ---
    // - Tests with single token formulas that are equal -

    [TestMethod]
    public void FormulaEqualsMethod_EqualSingleIntegerTokenFormulas_Equal() {
        Formula f1 = new("89");
        Formula f2 = new("89");

        Assert.IsTrue(f1.Equals(f2));
        Assert.IsTrue(f1 == f2);
        Assert.IsFalse(f1 != f2);
    }

    [TestMethod]
    public void FormulaEqualsMethod_EqualSingleDecimalTokenFormulas_Equal() {
        Formula f1 = new("32.45");
        Formula f2 = new("32.45");

        Assert.IsTrue(f1.Equals(f2));
        Assert.IsTrue(f1 == f2);
        Assert.IsFalse(f1 != f2);
    }

    [TestMethod]
    public void FormulaEqualsMethod_EqualSingleScientificTokenFormulas_Equal() {
        Formula[] testedFormulas = [new("5e2"), new("500"), new("5E2")];

        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsTrue(testedFormulas[i].Equals(testedFormulas[(i + 1) % 3]));
            Assert.IsTrue(testedFormulas[i] == testedFormulas[(i + 1) % 3]);
            Assert.IsFalse(testedFormulas[i] != testedFormulas[(i + 1) % 3]);
        }
    }

    [TestMethod]
    public void FormulaEqualsMethod_EqualSingleVariableTokenFormulas_Equal() {
        Formula[] testedFormulas = [new("a15"), new("A15"), new("A15")];

        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsTrue(testedFormulas[i].Equals(testedFormulas[(i + 1) % 3]));
            Assert.IsTrue(testedFormulas[i] == testedFormulas[(i + 1) % 3]);
            Assert.IsFalse(testedFormulas[i] != testedFormulas[(i + 1) % 3]);
        }
    }
}
