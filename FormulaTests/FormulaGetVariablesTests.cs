// <authors> Carson Angell </authors>
// <date> 1/23/2026 </date>
namespace Company.TestProject1;

using Formula;

/// <summary>
///     Unit tests for the Formula class GetVariables method.
/// </summary>
[TestClass]
public class FormulaGetVariablesTests {

    // --- Single token tests ---
    [TestMethod]
    public void FormulaGetVariables_SingleIntegerToken_Empty() {
        ISet<string> vars = new Formula("800").GetVariables();

        Assert.IsNotNull(vars);
        Assert.IsEmpty(vars);
    }

    [TestMethod]
    public void FormulaGetVariables_SingleDecimalToken_Empty() {
        ISet<string> vars = new Formula("5.6").GetVariables();

        Assert.IsNotNull(vars);
        Assert.IsEmpty(vars);
    }

    [TestMethod]
    public void FormulaGetVariables_SingleScientificToken_Empty() {
        ISet<string> vars = new Formula("3e4").GetVariables();

        Assert.IsNotNull(vars);
        Assert.IsEmpty(vars);
    }

    [TestMethod]
    public void FormulaGetVariables_SingleVariableToken_Empty() {
        ISet<string> vars = new Formula("ab12").GetVariables();

        Assert.IsNotNull(vars);
        Assert.HasCount(1, vars);
        Assert.Contains("AB12", vars);
    }



    // --- Two term equation tests ---
    [TestMethod]
    public void FormulaGetVariables_TwoTermEquationWithIntegers_Empty() {
        ISet<string> vars = new Formula("5464*80").GetVariables();

        Assert.IsNotNull(vars);
        Assert.IsEmpty(vars);
    }

    [TestMethod]
    public void FormulaGetVariables_TwoTermEquationWithDecimals_Empty() {
        ISet<string> vars = new Formula("5.3*.900").GetVariables();

        Assert.IsNotNull(vars);
        Assert.IsEmpty(vars);
    }

    [TestMethod]
    public void FormulaGetVariables_TwoTermEquationWithScientific_Empty() {
        ISet<string> vars = new Formula("5.5E6*3e2").GetVariables();

        Assert.IsNotNull(vars);
        Assert.IsEmpty(vars);
    }

    [TestMethod]
    public void FormulaGetVariables_TwoTermEquationWithVariables_Empty() {
        ISet<string> vars = new Formula("abc123*Jei67").GetVariables();

        Assert.IsNotNull(vars);
        Assert.HasCount(2, vars);
        Assert.Contains("ABC123", vars);
        Assert.Contains("JEI67", vars);
    }
}
