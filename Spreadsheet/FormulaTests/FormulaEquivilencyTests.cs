namespace FormulaTests;

using Formula;

[TestClass]
public class FormulaEqualsTests {

    [TestMethod]
    public void FormulaEquivilency_SameReferenceFormula_Equal() {
        Formula f1 = new("(34 -  a2)/4e5 - ((.5 * 2.5))");
        Formula f2 = f1;

        Assert.IsTrue(f1.Equals(f2));
        Assert.IsTrue(f1 == f2);
        Assert.IsFalse(f1 != f2);
    }




    // --- TESTS WITH EQUAL FORMULAS ---
    // - Tests with single token formulas that are equal -

    [TestMethod]
    public void FormulaEquivilency_EqualSingleIntegerTokenFormulas_Equal() {
        Formula f1 = new("89");
        Formula f2 = new("89");

        Assert.IsTrue(f1.Equals(f2));
        Assert.IsTrue(f1 == f2);
        Assert.IsFalse(f1 != f2);
    }

    [TestMethod]
    public void FormulaEquivilency_EqualSingleDecimalTokenFormulas_Equal() {
        Formula f1 = new("32.45");
        Formula f2 = new("32.45");

        Assert.IsTrue(f1.Equals(f2));
        Assert.IsTrue(f1 == f2);
        Assert.IsFalse(f1 != f2);
    }

    [TestMethod]
    public void FormulaEquivilency_EqualSingleScientificTokenFormulas_Equal() {
        Formula[] testedFormulas = [new("5e2"), new("500"), new("5E2")];

        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsTrue(testedFormulas[i].Equals(testedFormulas[(i + 1) % 3]));
            Assert.IsTrue(testedFormulas[i] == testedFormulas[(i + 1) % 3]);
            Assert.IsFalse(testedFormulas[i] != testedFormulas[(i + 1) % 3]);
        }
    }

    [TestMethod]
    public void FormulaEquivilency_EqualSingleVariableTokenFormulas_Equal() {
        Formula[] testedFormulas = [new("a15"), new("A15"), new("A15")];

        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsTrue(testedFormulas[i].Equals(testedFormulas[(i + 1) % 3]));
            Assert.IsTrue(testedFormulas[i] == testedFormulas[(i + 1) % 3]);
            Assert.IsFalse(testedFormulas[i] != testedFormulas[(i + 1) % 3]);
        }
    }




    // - Tests with single token formulas with parentheses that are equal -

    [TestMethod]
    public void FormulaEquivilency_EqualSingleIntegerTokenFormulasWithParens_Equal() {
        Formula f1 = new("(((89)))");
        Formula f2 = new("(((89)))");

        Assert.IsTrue(f1.Equals(f2));
        Assert.IsTrue(f1 == f2);
        Assert.IsFalse(f1 != f2);
    }

    [TestMethod]
    public void FormulaEquivilency_EqualSingleDecimalTokenFormulasWithParens_Equal() {
        Formula f1 = new("(  (32.45) )");
        Formula f2 = new("((  32.45)   )");

        Assert.IsTrue(f1.Equals(f2));
        Assert.IsTrue(f1 == f2);
        Assert.IsFalse(f1 != f2);
    }

    [TestMethod]
    public void FormulaEquivilency_EqualSingleScientificTokenFormulasWithParens_Equal() {
        Formula[] testedFormulas = [new("(((5e2))   )"), new("(  ( (500) ) )"), new("(((5E2)))")];

        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsTrue(testedFormulas[i].Equals(testedFormulas[(i + 1) % 3]));
            Assert.IsTrue(testedFormulas[i] == testedFormulas[(i + 1) % 3]);
            Assert.IsFalse(testedFormulas[i] != testedFormulas[(i + 1) % 3]);
        }
    }

    [TestMethod]
    public void FormulaEquivilency_EqualSingleVariableTokenFormulasWithParens_Equal() {
        Formula[] testedFormulas = [new("((a15))"), new("( (A15   ))"), new("(   ( A15 )   )")];

        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsTrue(testedFormulas[i].Equals(testedFormulas[(i + 1) % 3]));
            Assert.IsTrue(testedFormulas[i] == testedFormulas[(i + 1) % 3]);
            Assert.IsFalse(testedFormulas[i] != testedFormulas[(i + 1) % 3]);
        }
    }




    // - Tests with two token formulas that are equal -

    [TestMethod]
    public void FormulaEquivilency_EqualDualIntegerTokenFormulas_Equal() {
        Formula f1 = new("89 + 32");
        Formula f2 = new("89 + 32");

        Assert.IsTrue(f1.Equals(f2));
        Assert.IsTrue(f1 == f2);
        Assert.IsFalse(f1 != f2);
    }

    [TestMethod]
    public void FormulaEquivilency_EqualDualDecimalTokenFormulas_Equal() {
        Formula f1 = new("32.45 - .2");
        Formula f2 = new("32.45 - .2");

        Assert.IsTrue(f1.Equals(f2));
        Assert.IsTrue(f1 == f2);
        Assert.IsFalse(f1 != f2);
    }

    [TestMethod]
    public void FormulaEquivilency_EqualDualScientificTokenFormulas_Equal() {
        Formula[] testedFormulas = [new("5e2 / 3e1"), new("500 / 30"), new("5E2/3E1")];

        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsTrue(testedFormulas[i].Equals(testedFormulas[(i + 1) % 3]));
            Assert.IsTrue(testedFormulas[i] == testedFormulas[(i + 1) % 3]);
            Assert.IsFalse(testedFormulas[i] != testedFormulas[(i + 1) % 3]);
        }
    }

    [TestMethod]
    public void FormulaEquivilency_EqualDualVariableTokenFormulas_Equal() {
        Formula[] testedFormulas = [new("a15 - zc29"), new("A15-Zc29"), new("A15-zC29"), new("a15-ZC29")];

        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsTrue(testedFormulas[i].Equals(testedFormulas[(i + 1) % 3]));
            Assert.IsTrue(testedFormulas[i] == testedFormulas[(i + 1) % 3]);
            Assert.IsFalse(testedFormulas[i] != testedFormulas[(i + 1) % 3]);
        }
    }




    // - Tests with two term formulas with parentheses that are equal -

    [TestMethod]
    public void FormulaEquivilency_EqualDualIntegerTokenFormulasWithParens_Equal() {
        Formula f1 = new("(((89)) + 32)");
        Formula f2 = new("( ((89) ) + 32  )");

        Assert.IsTrue(f1.Equals(f2));
        Assert.IsTrue(f1 == f2);
        Assert.IsFalse(f1 != f2);
    }

    [TestMethod]
    public void FormulaEquivilency_EqualDualDecimalTokenFormulasWithParens_Equal() {
        Formula f1 = new("( ((  (( (32.45))) ))) - ( ((( ((.2))  )) ))");
        Formula f2 = new("((((((32.45))  )))) -((((((.2)))) ))");

        Assert.IsTrue(f1.Equals(f2));
        Assert.IsTrue(f1 == f2);
        Assert.IsFalse(f1 != f2);
    }

    [TestMethod]
    public void FormulaEquivilency_EqualDualScientificTokenFormulasWithParens_Equal() {
        Formula[] testedFormulas = [new("(5e2) / 3e1"), new("(  500) / 30"), new("(5E2   )/3E1")];

        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsTrue(testedFormulas[i].Equals(testedFormulas[(i + 1) % 3]));
            Assert.IsTrue(testedFormulas[i] == testedFormulas[(i + 1) % 3]);
            Assert.IsFalse(testedFormulas[i] != testedFormulas[(i + 1) % 3]);
        }
    }

    [TestMethod]
    public void FormulaEquivilency_EqualDualVariableTokenFormulasWithParens_Equal() {
        Formula[] testedFormulas = [new("(( (a15 - zc29)  ))"), new("(( (A15-Zc29)  ))"), new("(((A15-zC29)))"), new("( ( (a15-ZC29)  ))")];

        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsTrue(testedFormulas[i].Equals(testedFormulas[(i + 1) % 3]));
            Assert.IsTrue(testedFormulas[i] == testedFormulas[(i + 1) % 3]);
            Assert.IsFalse(testedFormulas[i] != testedFormulas[(i + 1) % 3]);
        }
    }




    // - Tests with multi term formulas with and without parentheses that are equal -

    [TestMethod]
    public void FormulaEquivilency_EqualMultiTermFormulas_Equal() {
        Formula[] testedFormulas = [
            new("45 / 6e2 * ab123 - .4 - 4e-2 + 60"),
            new("45 /6E2 * aB123 - 0.4 - 0.04 + 060"),
            new("45.0/ 600 * Ab123 - .4 - 4E-2 + 60"),
            new("45 / 6e2 * AB0123 - 000.4 - 4E-2 + 0060.0")
        ];

        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsTrue(testedFormulas[i].Equals(testedFormulas[(i + 1) % 3]));
            Assert.IsTrue(testedFormulas[i] == testedFormulas[(i + 1) % 3]);
            Assert.IsFalse(testedFormulas[i] != testedFormulas[(i + 1) % 3]);
        }
    }

    [TestMethod]
    public void FormulaEquivilency_EqualMultiTermFormulasWithParens_Equal() {
        Formula[] testedFormulas = [
            new("((45 / 6e2) ) * ab123 - (.4 - ((    4e-2 + (60) )))"),
            new("(  (45 /6E2)) * aB123 - (0.4 - ((0.04 + (060))  ))"),
            new("((45.0/ 600)) * Ab123 - (.4 - ( (4E-2 + (60 )) ) )"),
            new("( (45 / 6e2 )) * AB0123 - (   000.4 - ((4E-2 + (0060.0) )))")
        ];

        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsTrue(testedFormulas[i].Equals(testedFormulas[(i + 1) % 3]));
            Assert.IsTrue(testedFormulas[i] == testedFormulas[(i + 1) % 3]);
            Assert.IsFalse(testedFormulas[i] != testedFormulas[(i + 1) % 3]);
        }
    }
}
