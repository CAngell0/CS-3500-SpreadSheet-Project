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




    // - Tests with slighly different dual token formulas -

    [TestMethod]
    public void FormulaGetHashCode_SlightlyDifferentDualIntegerTokens_DifferentHashCodes() {
        Formula[] formulas = [
            new("123 + 456"),
            new("123 - 456"),
            new("124 + 456"),
            new("123 + 457")
        ];

        int len = formulas.Length;
        for (int i = 0; i < formulas.Length; i++) {
            Assert.AreNotEqual(formulas[i].GetHashCode(), formulas[(i + 1) % len].GetHashCode());
        }
    }

    [TestMethod]
    public void FormulaGetHashCode_SlightlyDifferentDualDecimalTokens_DifferentHashCodes() {
        Formula[] formulas = [
            new("46.32 * 46.33"),
            new("46.32e1 * 463.3"),
            new("46.32 * 46.34"),
            new("46.32 / 46.33")
        ];

        int len = formulas.Length;
        for (int i = 0; i < formulas.Length; i++) {
            Assert.AreNotEqual(formulas[i].GetHashCode(), formulas[(i + 1) % len].GetHashCode());
        }
    }

    [TestMethod]
    public void FormulaGetHashCode_SlightlyDifferentDualScientificTokens_DifferentHashCodes() {
        Formula[] formulas = [
            new("2e3 / 10"),
            new("2E3 / 11"),
            new("2e2 / 10"),
            new("2001 / 10")
        ];

        int len = formulas.Length;
        for (int i = 0; i < formulas.Length; i++) {
            Assert.AreNotEqual(formulas[i].GetHashCode(), formulas[(i + 1) % len].GetHashCode());
        }
    }
    
    [TestMethod]
    public void FormulaGetHashCode_SlightlyDifferentDualVariableTokens_DifferentHashCodes() {
        Formula[] formulas = [
            new("abc5 + def2"),
            new("abc6 + def3"),
            new("ab5 + def2")
        ];

        int len = formulas.Length;
        for (int i = 0; i < formulas.Length; i++) {
            Assert.AreNotEqual(formulas[i].GetHashCode(), formulas[(i + 1) % len].GetHashCode());
        }
    }
}
