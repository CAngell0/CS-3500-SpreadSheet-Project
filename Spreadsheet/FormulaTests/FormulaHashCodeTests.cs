namespace FormulaTests;

using Formula;

[TestClass]
public class FormulaHashCodeTests {

    // --- Testing GetHashCode method with slightly different formulas ---
    // - Tests with slighly different single token formulas -

    [TestMethod]
    public void FormulaGetHashCode_SlightlyDifferentSingleIntegerTokens_DifferentHashCodes() {
        Formula[] formulas = [
            new("100"),
            new("101"),
            new("201"),
            new("100.1")
        ];

        int len = formulas.Length;
        for (int i = 0; i < formulas.Length; i++) {
            Assert.AreNotEqual(formulas[i].GetHashCode(), formulas[(i + 1) % len].GetHashCode());
        }
    }

    [TestMethod]
    public void FormulaGetHashCode_SlightlyDifferentSingleDecimalTokens_DifferentHashCodes() {
        Formula[] formulas = [
            new("12.34"),
            new("12.35"),
            new("1234e1"),
            new("1.234")
        ];

        int len = formulas.Length;
        for (int i = 0; i < formulas.Length; i++) {
            Assert.AreNotEqual(formulas[i].GetHashCode(), formulas[(i + 1) % len].GetHashCode());
        }
    }

    [TestMethod]
    public void FormulaGetHashCode_SlightlyDifferentSingleScientificTokens_DifferentHashCodes() {
        Formula[] formulas = [
            new("2e3"),
            new("2E2"),
            new("3e3"),
            new("2001")
        ];

        int len = formulas.Length;
        for (int i = 0; i < formulas.Length; i++) {
            Assert.AreNotEqual(formulas[i].GetHashCode(), formulas[(i + 1) % len].GetHashCode());
        }
    }
    
    [TestMethod]
    public void FormulaGetHashCode_SlightlyDifferentSingleVariableTokens_DifferentHashCodes() {
        Formula[] formulas = [
            new("abc5"),
            new("abc6"),
            new("ab5")
        ];

        int len = formulas.Length;
        for (int i = 0; i < formulas.Length; i++) {
            Assert.AreNotEqual(formulas[i].GetHashCode(), formulas[(i + 1) % len].GetHashCode());
        }
    }
}
