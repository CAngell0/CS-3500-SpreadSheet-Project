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

    
    // --- Tests evaluating single token formulas ---
    [TestMethod]
    public void FormulaEvaluateMethod_SingleIntegerToken_NumberValue() {
        object result = new Formula("136").Evaluate(lookup);
        Assert.IsInstanceOfType<double>(result);

        double value = (double) result;
        Assert.IsTrue(value.Equals(136));
    }

    [TestMethod]
    public void FormulaEvaluateMethod_SingleDecimalToken_NumberValue() {
        object result = new Formula("45.67").Evaluate(lookup);
        Assert.IsInstanceOfType<double>(result);

        double value = (double) result;
        Assert.IsTrue(value.Equals(45.67));
    }

    [TestMethod]
    public void FormulaEvaluateMethod_SingleScientificToken_NumberValue() {
        object result = new Formula("2e3").Evaluate(lookup);
        Assert.IsInstanceOfType<double>(result);

        double value = (double) result;
        Assert.IsTrue(value.Equals(2000));
    }

    [TestMethod]
    public void FormulaEvaluateMethod_SingleVariableToken_NumberValue() {
        object result = new Formula("a1").Evaluate(lookup);
        Assert.IsInstanceOfType<double>(result);

        double value = (double) result;
        Assert.IsTrue(value.Equals(10));
    }
}
