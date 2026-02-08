// <author> Carson ANgell </author>
// <date> 2/6/2026 </date>
namespace FormulaTests;

using Formula;

[TestClass]
public class FormulaEvaluateTests {
    private Lookup lookup = (variable) => 0;

    [TestInitialize]
    public void DefineLookupMethod() {
        lookup = (variable) => {
            return variable switch {
                "A1" => 10,
                "B1" => 20,
                "C1" => 100,
                "D1" => 30.6,
                _ => throw new ArgumentException(),
            };
        };
    }



    
    // --- Tests evaluating formulas without parentheses ---
    // - Tests with single token formulas -

    [TestMethod]
    public void FormulaEvaluateMethod_SingleIntegerToken_NumberValue() {
        object result = new Formula("136").Evaluate(lookup);
        Assert.IsInstanceOfType<double>(result);

        double value = (double) result;
        Assert.IsTrue(value.IsEqualTo(136));
    }

    [TestMethod]
    public void FormulaEvaluateMethod_SingleDecimalToken_NumberValue() {
        object result = new Formula("45.67").Evaluate(lookup);
        Assert.IsInstanceOfType<double>(result);

        double value = (double) result;
        Assert.IsTrue(value.IsEqualTo(45.67));
    }

    [TestMethod]
    public void FormulaEvaluateMethod_SingleScientificToken_NumberValue() {
        object result = new Formula("2e3").Evaluate(lookup);
        Assert.IsInstanceOfType<double>(result);

        double value = (double) result;
        Assert.IsTrue(value.IsEqualTo(2000));
    }

    [TestMethod]
    public void FormulaEvaluateMethod_SingleVariableToken_NumberValue() {
        object result = new Formula("a1").Evaluate(lookup);
        Assert.IsInstanceOfType<double>(result);

        double value = (double) result;
        Assert.IsTrue(value.IsEqualTo(10));
    }




    // - Tests evaluating two token formulas -
    [TestMethod]
    public void FormulaEvaluateMethod_DualIntegerTokens_NumberValue() {
        object result = new Formula("89 * 3").Evaluate(lookup);
        Assert.IsInstanceOfType<double>(result);

        double value = (double) result;
        Assert.IsTrue(value.IsEqualTo(89 * 3));
    }

    [TestMethod]
    public void FormulaEvaluateMethod_DualDecimalTokens_NumberValue() {
        object result = new Formula(".3 - .88").Evaluate(lookup);
        Assert.IsInstanceOfType<double>(result);

        double value = (double) result;
        Assert.IsTrue(value.IsEqualTo(0.3 - 0.88));
    }

    [TestMethod]
    public void FormulaEvaluateMethod_DualScientificTokens_NumberValue() {
        object result = new Formula("2e3 / 1e2").Evaluate(lookup);
        Assert.IsInstanceOfType<double>(result);

        double value = (double) result;
        Assert.IsTrue(value.IsEqualTo(2e3 / 1e2));
    }

    [TestMethod]
    public void FormulaEvaluateMethod_DualVariableTokens_NumberValue() {
        object result = new Formula("a1 - b1").Evaluate(lookup);
        Assert.IsInstanceOfType<double>(result);

        double value = (double) result;
        Assert.IsTrue(value.IsEqualTo(10 - 20));
    }

    [TestMethod]
    public void FormulaEvaluateMethod_DualMixedTokens_NumberValue() {
        object result = new Formula("1e2 - D1").Evaluate(lookup);
        Assert.IsInstanceOfType<double>(result);

        double value = (double) result;
        Assert.IsTrue(value.IsEqualTo(100 - 30.6));
    }




    // - Tests evaluating two token formulas -
    [TestMethod]
    public void FormulaEvaluateMethod_MultipleIntegerTokens_NumberValue() {
        object result = new Formula("89 * 3 - 20").Evaluate(lookup);
        Assert.IsInstanceOfType<double>(result);

        double value = (double) result;
        Assert.IsTrue(value.IsEqualTo(89 * 3 - 20));
    }

    [TestMethod]
    public void FormulaEvaluateMethod_MultipleDecimalTokens_NumberValue() {
        object result = new Formula(".3 - .88 / 0.5").Evaluate(lookup);
        Assert.IsInstanceOfType<double>(result);

        double value = (double) result;
        Assert.IsTrue(value.IsEqualTo(0.3 - 0.88 / 0.5));
    }

    [TestMethod]
    public void FormulaEvaluateMethod_MultipleScientificTokens_NumberValue() {
        object result = new Formula("2e3 / 1e2 * 2e-1 - 1E1").Evaluate(lookup);
        Assert.IsInstanceOfType<double>(result);

        double value = (double) result;
        Assert.IsTrue(value.IsEqualTo(2e3 / 1e2 * 2e-1 - 1E1));
    }

    [TestMethod]
    public void FormulaEvaluateMethod_MultipleVariableTokens_NumberValue() {
        object result = new Formula("a1 - b1 * c1").Evaluate(lookup);
        Assert.IsInstanceOfType<double>(result);

        double value = (double) result;
        Assert.IsTrue(value.IsEqualTo(10 - 20 * 100));
    }

    [TestMethod]
    public void FormulaEvaluateMethod_MultipleMixedTokens_NumberValue() {
        object result = new Formula("1e2 - D1 / 10.6 + 3.14 * B1").Evaluate(lookup);
        Assert.IsInstanceOfType<double>(result);

        double value = (double) result;
        Assert.IsTrue(value.IsEqualTo(100 - 30.6 / 10.6 + 3.14 * 20));
    }




    // --- Tests evaluating formulas with parentheses ---
    // - Tests for single token formulas with parentheses -
    [TestMethod]
    public void FormulaEvaluateMethod_SingleIntegerTokenWithParens_NumberValue() {
        object result = new Formula("((136))").Evaluate(lookup);
        Assert.IsInstanceOfType<double>(result);

        double value = (double) result;
        Assert.IsTrue(value.IsEqualTo(136));
    }

    [TestMethod]
    public void FormulaEvaluateMethod_SingleDecimalTokenWithParens_NumberValue() {
        object result = new Formula("(45.67)").Evaluate(lookup);
        Assert.IsInstanceOfType<double>(result);

        double value = (double) result;
        Assert.IsTrue(value.IsEqualTo(45.67));
    }

    [TestMethod]
    public void FormulaEvaluateMethod_SingleScientificTokenWithParens_NumberValue() {
        object result = new Formula("((((((2e3))))))").Evaluate(lookup);
        Assert.IsInstanceOfType<double>(result);

        double value = (double) result;
        Assert.IsTrue(value.IsEqualTo(2000));
    }

    [TestMethod]
    public void FormulaEvaluateMethod_SingleVariableTokenWithParens_NumberValue() {
        object result = new Formula("(((a1)))").Evaluate(lookup);
        Assert.IsInstanceOfType<double>(result);

        double value = (double) result;
        Assert.IsTrue(value.IsEqualTo(10));
    }
}



/// <summary>
///     An extension class for doubles.
/// </summary>
public static class DoubleExtension {
    /// <summary>
    ///     Compares two double to see if they are approximately equal.
    /// </summary>
    /// <param name="d1"> First double to compare with </param>
    /// <param name="num"> Double to compare against </param>
    /// <returns> True if the values are within 0.0000001 of each other, false if not. </returns>
    public static bool IsEqualTo(this double d1, double num) => Math.Abs(d1 - num) <= 0.0000001;
}
