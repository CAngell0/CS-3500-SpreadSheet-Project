// <authors> Carson Angell </authors>
// <date> 1/23/2026 </date>
namespace FormulaTests;

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
    public void FormulaGetVariables_SingleVariableToken_OneCanonicalVariable() {
        ISet<string> vars = new Formula("ab12").GetVariables();

        Assert.IsNotNull(vars);
        Assert.HasCount(1, vars);
        Assert.Contains("AB12", vars);
    }



    // --- Two term equation tests ---
    [TestMethod]
    public void FormulaGetVariables_TwoTermEquationWithIntegersOnly_Empty() {
        ISet<string> vars = new Formula("5464*80").GetVariables();

        Assert.IsNotNull(vars);
        Assert.IsEmpty(vars);
    }

    [TestMethod]
    public void FormulaGetVariables_TwoTermEquationWithDecimalsOnly_Empty() {
        ISet<string> vars = new Formula("5.3*.900").GetVariables();

        Assert.IsNotNull(vars);
        Assert.IsEmpty(vars);
    }

    [TestMethod]
    public void FormulaGetVariables_TwoTermEquationWithScientificOnly_Empty() {
        ISet<string> vars = new Formula("5.5E6*3e2").GetVariables();

        Assert.IsNotNull(vars);
        Assert.IsEmpty(vars);
    }

    [TestMethod]
    public void FormulaGetVariables_TwoTermEquationWithVariablesOnly_TwoCanonicalVariables() {
        ISet<string> vars = new Formula("abc123*Jei67").GetVariables();

        Assert.IsNotNull(vars);
        Assert.HasCount(2, vars);
        Assert.Contains("ABC123", vars);
        Assert.Contains("JEI67", vars);
    }

    [TestMethod]
    public void FormulaGetVariables_TwoTermEquationWithVariableAndInteger_OneCanonicalVariable() {
        ISet<string> vars = new Formula("5787 / ag0060").GetVariables();

        Assert.IsNotNull(vars);
        Assert.HasCount(1, vars);
        Assert.Contains("AG60", vars);
    }

    [TestMethod]
    public void FormulaGetVariables_TwoTermEquationWithVariableAndDecimal_OneCanonicalVariable() {
        ISet<string> vars = new Formula("jKn87*.6734").GetVariables();

        Assert.IsNotNull(vars);
        Assert.HasCount(1, vars);
        Assert.Contains("JKN87", vars);
    }

    [TestMethod]
    public void FormulaGetVariables_TwoTermEquationWithVariableAndScientific_OneCanonicalVariable() {
        ISet<string> vars = new Formula("Kje23*5e-3").GetVariables();

        Assert.IsNotNull(vars);
        Assert.HasCount(1, vars);
        Assert.Contains("KJE23", vars);
    }



    // --- Tests with equations that have parentheses ---
    [TestMethod]
    public void FormulaGetVariables_IntegerOnlyEquationWithParenthesis_Empty() {
        ISet<string> vars = new Formula("((56 + 20)) / (2) * 600").GetVariables();

        Assert.IsNotNull(vars);
        Assert.IsEmpty(vars);
    }

    [TestMethod]
    public void FormulaGetVariables_DecimalOnlyEquationWithParenthesis_Empty() {
        ISet<string> vars = new Formula(".5-.9 * (45.7) / (((((30.5 + (6.344))))))").GetVariables();

        Assert.IsNotNull(vars);
        Assert.IsEmpty(vars);
    }

    [TestMethod]
    public void FormulaGetVariables_ScientificOnlyEquationWithParenthesis_Empty() {
        ISet<string> vars = new Formula("(((1e3)))/ 4E2 * (1e2 + 6E0)/3e-2").GetVariables();

        Assert.IsNotNull(vars);
        Assert.IsEmpty(vars);
    }

    [TestMethod]
    public void FormulaGetVariables_VariableOnlyEquationWithParenthesis_FiveCanonicalVariables() {
        ISet<string> vars = new Formula("((ab34)) / k9 *(((JHeD90 - ASD21)* h0006))").GetVariables();

        Assert.IsNotNull(vars);
        Assert.HasCount(5, vars);
        Assert.Contains("AB34", vars);
        Assert.Contains("K9", vars);
        Assert.Contains("JHED90", vars);
        Assert.Contains("ASD21", vars);
        Assert.Contains("H6", vars);
    }

    [TestMethod]
    public void FormulaGetVariables_MixedTokenEquationWithParenthesis_FourCanonicalVariables() {
        ISet<string> vars = new Formula(".675 + (a4 / ((56 * 2e4))) / (anj23) + ((NejD003 - Kol34))").GetVariables();

        Assert.IsNotNull(vars);
        Assert.HasCount(4, vars);
        Assert.Contains("A4", vars);
        Assert.Contains("ANJ23", vars);
        Assert.Contains("NEJD3", vars);
        Assert.Contains("KOL34", vars);
    }
}
