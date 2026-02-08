// <author> Carson ANgell </author>
// <date> 2/6/2026 </date>

namespace FormulaTests;

using Formula;

[TestClass]
public class FormulaEquivalencyTests {

    [TestMethod]
    public void FormulaEquivalency_SameReferenceFormula_Equal() {
        Formula f1 = new("(34 -  a2)/4e5 - ((.5 * 2.5))");
        Formula f2 = f1;

        Assert.IsTrue(f1.Equals(f2));
        Assert.IsTrue(f1 == f2);
        Assert.IsFalse(f1 != f2);
    }




    // --- TESTS WITH EQUAL FORMULAS ---
    // - Tests with single token formulas that are equal -

    [TestMethod]
    public void FormulaEquivalency_EqualSingleIntegerTokenFormulas_Equal() {
        Formula f1 = new("89");
        Formula f2 = new("89");

        Assert.IsTrue(f1.Equals(f2));
        Assert.IsTrue(f1 == f2);
        Assert.IsFalse(f1 != f2);
    }

    [TestMethod]
    public void FormulaEquivalency_EqualSingleDecimalTokenFormulas_Equal() {
        Formula f1 = new("32.45");
        Formula f2 = new("32.45");

        Assert.IsTrue(f1.Equals(f2));
        Assert.IsTrue(f1 == f2);
        Assert.IsFalse(f1 != f2);
    }

    [TestMethod]
    public void FormulaEquivalency_EqualSingleScientificTokenFormulas_Equal() {
        Formula[] testedFormulas = [new("5e2"), new("500"), new("5E2")];

        int len = testedFormulas.Length;
        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsTrue(testedFormulas[i].Equals(testedFormulas[(i + 1) % len]));
            Assert.IsTrue(testedFormulas[i] == testedFormulas[(i + 1) % len]);
            Assert.IsFalse(testedFormulas[i] != testedFormulas[(i + 1) % len]);
        }
    }

    [TestMethod]
    public void FormulaEquivalency_EqualSingleVariableTokenFormulas_Equal() {
        Formula[] testedFormulas = [new("a15"), new("A15"), new("A15")];

        int len = testedFormulas.Length;
        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsTrue(testedFormulas[i].Equals(testedFormulas[(i + 1) % len]));
            Assert.IsTrue(testedFormulas[i] == testedFormulas[(i + 1) % len]);
            Assert.IsFalse(testedFormulas[i] != testedFormulas[(i + 1) % len]);
        }
    }




    // - Tests with single token formulas with parentheses that are equal -

    [TestMethod]
    public void FormulaEquivalency_EqualSingleIntegerTokenFormulasWithParens_Equal() {
        Formula f1 = new("(((89)))");
        Formula f2 = new("(((89)))");

        Assert.IsTrue(f1.Equals(f2));
        Assert.IsTrue(f1 == f2);
        Assert.IsFalse(f1 != f2);
    }

    [TestMethod]
    public void FormulaEquivalency_EqualSingleDecimalTokenFormulasWithParens_Equal() {
        Formula f1 = new("(  (32.45) )");
        Formula f2 = new("((  32.45)   )");

        Assert.IsTrue(f1.Equals(f2));
        Assert.IsTrue(f1 == f2);
        Assert.IsFalse(f1 != f2);
    }

    [TestMethod]
    public void FormulaEquivalency_EqualSingleScientificTokenFormulasWithParens_Equal() {
        Formula[] testedFormulas = [new("(((5e2))   )"), new("(  ( (500) ) )"), new("(((5E2)))")];

        int len = testedFormulas.Length;
        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsTrue(testedFormulas[i].Equals(testedFormulas[(i + 1) % len]));
            Assert.IsTrue(testedFormulas[i] == testedFormulas[(i + 1) % len]);
            Assert.IsFalse(testedFormulas[i] != testedFormulas[(i + 1) % len]);
        }
    }

    [TestMethod]
    public void FormulaEquivalency_EqualSingleVariableTokenFormulasWithParens_Equal() {
        Formula[] testedFormulas = [new("((a15))"), new("( (A15   ))"), new("(   ( A15 )   )")];

        int len = testedFormulas.Length;
        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsTrue(testedFormulas[i].Equals(testedFormulas[(i + 1) % len]));
            Assert.IsTrue(testedFormulas[i] == testedFormulas[(i + 1) % len]);
            Assert.IsFalse(testedFormulas[i] != testedFormulas[(i + 1) % len]);
        }
    }




    // - Tests with two token formulas that are equal -

    [TestMethod]
    public void FormulaEquivalency_EqualDualIntegerTokenFormulas_Equal() {
        Formula f1 = new("89 + 32");
        Formula f2 = new("89 + 32");

        Assert.IsTrue(f1.Equals(f2));
        Assert.IsTrue(f1 == f2);
        Assert.IsFalse(f1 != f2);
    }

    [TestMethod]
    public void FormulaEquivalency_EqualDualDecimalTokenFormulas_Equal() {
        Formula f1 = new("32.45 - .2");
        Formula f2 = new("32.45 - .2");

        Assert.IsTrue(f1.Equals(f2));
        Assert.IsTrue(f1 == f2);
        Assert.IsFalse(f1 != f2);
    }

    [TestMethod]
    public void FormulaEquivalency_EqualDualScientificTokenFormulas_Equal() {
        Formula[] testedFormulas = [new("5e2 / 3e1"), new("500 / 30"), new("5E2/3E1")];

        int len = testedFormulas.Length;
        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsTrue(testedFormulas[i].Equals(testedFormulas[(i + 1) % len]));
            Assert.IsTrue(testedFormulas[i] == testedFormulas[(i + 1) % len]);
            Assert.IsFalse(testedFormulas[i] != testedFormulas[(i + 1) % len]);
        }
    }

    [TestMethod]
    public void FormulaEquivalency_EqualDualVariableTokenFormulas_Equal() {
        Formula[] testedFormulas = [new("a15 - zc29"), new("A15-Zc29"), new("A15-zC29"), new("a15-ZC29")];

        int len = testedFormulas.Length;
        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsTrue(testedFormulas[i].Equals(testedFormulas[(i + 1) % len]));
            Assert.IsTrue(testedFormulas[i] == testedFormulas[(i + 1) % len]);
            Assert.IsFalse(testedFormulas[i] != testedFormulas[(i + 1) % len]);
        }
    }

    [TestMethod]
    public void FormulaEquivalency_EqualMixedDualTokenFormulas_Equal() {
        Formula[] testedFormulas = [new("ab12 / .54"), new("AB012/ 0.540"), new("aB12 / 5.4e-1")];

        int len = testedFormulas.Length;
        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsTrue(testedFormulas[i].Equals(testedFormulas[(i + 1) % len]));
            Assert.IsTrue(testedFormulas[i] == testedFormulas[(i + 1) % len]);
            Assert.IsFalse(testedFormulas[i] != testedFormulas[(i + 1) % len]);
        }
    }




    // - Tests with two term formulas with parentheses that are equal -

    [TestMethod]
    public void FormulaEquivalency_EqualDualIntegerTokenFormulasWithParens_Equal() {
        Formula f1 = new("(((89)) + 32)");
        Formula f2 = new("( ((89) ) + 32  )");

        Assert.IsTrue(f1.Equals(f2));
        Assert.IsTrue(f1 == f2);
        Assert.IsFalse(f1 != f2);
    }

    [TestMethod]
    public void FormulaEquivalency_EqualDualDecimalTokenFormulasWithParens_Equal() {
        Formula f1 = new("( ((  (( (32.45))) ))) - ( ((( ((.2))  )) ))");
        Formula f2 = new("((((((32.45))  )))) -((((((.2)))) ))");

        Assert.IsTrue(f1.Equals(f2));
        Assert.IsTrue(f1 == f2);
        Assert.IsFalse(f1 != f2);
    }

    [TestMethod]
    public void FormulaEquivalency_EqualDualScientificTokenFormulasWithParens_Equal() {
        Formula[] testedFormulas = [new("(5e2) / 3e1"), new("(  500) / 30"), new("(5E2   )/3E1")];

        int len = testedFormulas.Length;
        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsTrue(testedFormulas[i].Equals(testedFormulas[(i + 1) % len]));
            Assert.IsTrue(testedFormulas[i] == testedFormulas[(i + 1) % len]);
            Assert.IsFalse(testedFormulas[i] != testedFormulas[(i + 1) % len]);
        }
    }

    [TestMethod]
    public void FormulaEquivalency_EqualDualVariableTokenFormulasWithParens_Equal() {
        Formula[] testedFormulas = [new("(( (a15 - zc29)  ))"), new("(( (A15-Zc29)  ))"), new("(((A15-zC29)))"), new("( ( (a15-ZC29)  ))")];

        int len = testedFormulas.Length;
        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsTrue(testedFormulas[i].Equals(testedFormulas[(i + 1) % len]));
            Assert.IsTrue(testedFormulas[i] == testedFormulas[(i + 1) % len]);
            Assert.IsFalse(testedFormulas[i] != testedFormulas[(i + 1) % len]);
        }
    }

    [TestMethod]
    public void FormulaEquivalency_EqualMixedDualTokenFormulasWithParens_Equal() {
        Formula[] testedFormulas = [
            new("( ((5632.0) / 2e3  ))"), 
            new("(((56.32e2)/ 2000) )"), 
            new("( ( (5632000e-3) / 20E2) )"),
            new("(((5632) / 02000))")
        ];

        int len = testedFormulas.Length;
        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsTrue(testedFormulas[i].Equals(testedFormulas[(i + 1) % len]));
            Assert.IsTrue(testedFormulas[i] == testedFormulas[(i + 1) % len]);
            Assert.IsFalse(testedFormulas[i] != testedFormulas[(i + 1) % len]);
        }
    }




    // - Tests with multi term formulas with and without parentheses that are equal -

    [TestMethod]
    public void FormulaEquivalency_EqualMultiTermFormulas_Equal() {
        Formula[] testedFormulas = [
            new("45 / 6e2 * ab123 - .4 - 4e-2 + 60"),
            new("45 /6E2 * aB123 - 0.4 - 0.04 + 060"),
            new("45.0/ 600 * Ab123 - .4 - 4E-2 + 60"),
            new("45 / 6e2 * AB0123 - 000.4 - 4E-2 + 0060.0")
        ];

        int len = testedFormulas.Length;
        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsTrue(testedFormulas[i].Equals(testedFormulas[(i + 1) % len]));
            Assert.IsTrue(testedFormulas[i] == testedFormulas[(i + 1) % len]);
            Assert.IsFalse(testedFormulas[i] != testedFormulas[(i + 1) % len]);
        }
    }

    [TestMethod]
    public void FormulaEquivalency_EqualMultiTermFormulasWithParens_Equal() {
        Formula[] testedFormulas = [
            new("((45 / 6e2) ) * ab123 - (.4 - ((    4e-2 + (60) )))"),
            new("(  (45 /6E2)) * aB123 - (0.4 - ((0.04 + (060))  ))"),
            new("((45.0/ 600)) * Ab123 - (.4 - ( (4E-2 + (60 )) ) )"),
            new("( (45 / 6e2 )) * AB0123 - (   000.4 - ((4E-2 + (0060.0) )))")
        ];

        int len = testedFormulas.Length;
        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsTrue(testedFormulas[i].Equals(testedFormulas[(i + 1) % len]));
            Assert.IsTrue(testedFormulas[i] == testedFormulas[(i + 1) % len]);
            Assert.IsFalse(testedFormulas[i] != testedFormulas[(i + 1) % len]);
        }
    }





    // --- TESTS WITH UNEQUAL FORMULAS ---
    // - Tests with single token formulas that are not equal -

    [TestMethod]
    public void FormulaEquivalency_UnequalSingleIntegerTokenFormulas_Unequal() {
        Formula f1 = new("89");
        Formula f2 = new("88");

        Assert.IsFalse(f1.Equals(f2));
        Assert.IsFalse(f1 == f2);
        Assert.IsTrue(f1 != f2);
    }

    [TestMethod]
    public void FormulaEquivalency_UnequalSingleDecimalTokenFormulas_Unequal() {
        Formula f1 = new("32.45");
        Formula f2 = new("45.63");

        Assert.IsFalse(f1.Equals(f2));
        Assert.IsFalse(f1 == f2);
        Assert.IsTrue(f1 != f2);
    }

    [TestMethod]
    public void FormulaEquivalency_UnequalSingleScientificTokenFormulas_Unequal() {
        Formula[] testedFormulas = [new("5e2"), new("5000"), new("5E1")];

        int len = testedFormulas.Length;
        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsFalse(testedFormulas[i].Equals(testedFormulas[(i + 1) % len]));
            Assert.IsFalse(testedFormulas[i] == testedFormulas[(i + 1) % len]);
            Assert.IsTrue(testedFormulas[i] != testedFormulas[(i + 1) % len]);
        }
    }

    [TestMethod]
    public void FormulaEquivalency_UnequalSingleVariableTokenFormulas_Unequal() {
        Formula[] testedFormulas = [new("a115"), new("A515"), new("A150")];

        int len = testedFormulas.Length;
        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsFalse(testedFormulas[i].Equals(testedFormulas[(i + 1) % len]));
            Assert.IsFalse(testedFormulas[i] == testedFormulas[(i + 1) % len]);
            Assert.IsTrue(testedFormulas[i] != testedFormulas[(i + 1) % len]);
        }
    }




    // - Tests with single token formulas with parentheses that are equal -

    [TestMethod]
    public void FormulaEquivalency_UnequalSingleIntegerTokenFormulasWithParens_Unequal() {
        Formula f1 = new("(((89)))");
        Formula f2 = new("((89))");

        Assert.IsFalse(f1.Equals(f2));
        Assert.IsFalse(f1 == f2);
        Assert.IsTrue(f1 != f2);
    }

    [TestMethod]
    public void FormulaEquivalency_UnequalSingleDecimalTokenFormulasWithParens_Unequal() {
        Formula f1 = new("(  (32.45) )");
        Formula f2 = new("((  32.145)   )");

        Assert.IsFalse(f1.Equals(f2));
        Assert.IsFalse(f1 == f2);
        Assert.IsTrue(f1 != f2);
    }

    [TestMethod]
    public void FormulaEquivalency_UnequalSingleScientificTokenFormulasWithParens_Unequal() {
        Formula[] testedFormulas = [new("((5e2))   "), new("(  ( (600) ) )"), new("(((5E3)))")];

        int len = testedFormulas.Length;
        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsFalse(testedFormulas[i].Equals(testedFormulas[(i + 1) % len]));
            Assert.IsFalse(testedFormulas[i] == testedFormulas[(i + 1) % len]);
            Assert.IsTrue(testedFormulas[i] != testedFormulas[(i + 1) % len]);
        }
    }

    [TestMethod]
    public void FormulaEquivalency_UnequalSingleVariableTokenFormulasWithParens_Unequal() {
        Formula[] testedFormulas = [new("((a152))"), new("( A15   )"), new("(   ( A15 )   )")];

        int len = testedFormulas.Length;
        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsFalse(testedFormulas[i].Equals(testedFormulas[(i + 1) % len]));
            Assert.IsFalse(testedFormulas[i] == testedFormulas[(i + 1) % len]);
            Assert.IsTrue(testedFormulas[i] != testedFormulas[(i + 1) % len]);
        }
    }




    // - Tests with two token formulas that are not equal -

    [TestMethod]
    public void FormulaEquivalency_UnequalDualIntegerTokenFormulas_Unequal() {
        Formula f1 = new("89 + 321");
        Formula f2 = new("89 + 32");

        Assert.IsFalse(f1.Equals(f2));
        Assert.IsFalse(f1 == f2);
        Assert.IsTrue(f1 != f2);
    }

    [TestMethod]
    public void FormulaEquivalency_UnequalDualDecimalTokenFormulas_Unequal() {
        Formula f1 = new("32.45 - .2e1");
        Formula f2 = new("32.045 - .2");

        Assert.IsFalse(f1.Equals(f2));
        Assert.IsFalse(f1 == f2);
        Assert.IsTrue(f1 != f2);
    }

    [TestMethod]
    public void FormulaEquivalency_UnequalDualScientificTokenFormulas_Unequal() {
        Formula[] testedFormulas = [new("5e2 / .3e1"), new("500 / 30"), new("5E2/3E2")];

        int len = testedFormulas.Length;
        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsFalse(testedFormulas[i].Equals(testedFormulas[(i + 1) % len]));
            Assert.IsFalse(testedFormulas[i] == testedFormulas[(i + 1) % len]);
            Assert.IsTrue(testedFormulas[i] != testedFormulas[(i + 1) % len]);
        }
    }

    [TestMethod]
    public void FormulaEquivalency_UnequalDualVariableTokenFormulas_Unequal() {
        Formula[] testedFormulas = [new("a0151 - zc29"), new("A15*Zc29"), new("A15-zC29"), new("ac15/ZC29")];

        int len = testedFormulas.Length;
        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsFalse(testedFormulas[i].Equals(testedFormulas[(i + 1) % len]));
            Assert.IsFalse(testedFormulas[i] == testedFormulas[(i + 1) % len]);
            Assert.IsTrue(testedFormulas[i] != testedFormulas[(i + 1) % len]);
        }
    }

    [TestMethod]
    public void FormulaEquivalency_UnequalMixedDualTokenFormulas_Unequal() {
        Formula[] testedFormulas = [new("ab12 - .54"), new("12-d3"), new("aB12 / 5.4e-2")];

        int len = testedFormulas.Length;
        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsFalse(testedFormulas[i].Equals(testedFormulas[(i + 1) % len]));
            Assert.IsFalse(testedFormulas[i] == testedFormulas[(i + 1) % len]);
            Assert.IsTrue(testedFormulas[i] != testedFormulas[(i + 1) % len]);
        }
    }




    // - Tests with two term formulas with parentheses that are not equal -

    [TestMethod]
    public void FormulaEquivalency_UnequalDualIntegerTokenFormulasWithParens_Unequal() {
        Formula f1 = new("(((89)) + 32)");
        Formula f2 = new("( (89)  + 32  )");

        Assert.IsFalse(f1.Equals(f2));
        Assert.IsFalse(f1 == f2);
        Assert.IsTrue(f1 != f2);
    }

    [TestMethod]
    public void FormulaEquivalency_UnequalDualDecimalTokenFormulasWithParens_Unequal() {
        Formula f1 = new("( ((  (( (32.451))) ))) - ( ((( ((.2))  )) ))");
        Formula f2 = new("((((((32.45))  )))) -((((.2))) )");

        Assert.IsFalse(f1.Equals(f2));
        Assert.IsFalse(f1 == f2);
        Assert.IsTrue(f1 != f2);
    }

    [TestMethod]
    public void FormulaEquivalency_UnequalDualScientificTokenFormulasWithParens_Unequal() {
        Formula[] testedFormulas = [new("89/23"), new("  500 / 30"), new("(5E2   )-3E1")];

        int len = testedFormulas.Length;
        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsFalse(testedFormulas[i].Equals(testedFormulas[(i + 1) % len]));
            Assert.IsFalse(testedFormulas[i] == testedFormulas[(i + 1) % len]);
            Assert.IsTrue(testedFormulas[i] != testedFormulas[(i + 1) % len]);
        }
    }

    [TestMethod]
    public void FormulaEquivalency_UnequalDualVariableTokenFormulasWithParens_Unequal() {
        Formula[] testedFormulas = [new("( (a15 - zc29)  )"), new("A15-Zc29"), new("(((A15-zCf29)))"), new("( ( (a15-ZC29)  ))")];

        int len = testedFormulas.Length;
        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsFalse(testedFormulas[i].Equals(testedFormulas[(i + 1) % len]));
            Assert.IsFalse(testedFormulas[i] == testedFormulas[(i + 1) % len]);
            Assert.IsTrue(testedFormulas[i] != testedFormulas[(i + 1) % len]);
        }
    }

    [TestMethod]
    public void FormulaEquivalency_UnequalMixedDualTokenFormulasWithParens_Unequal() {
        Formula[] testedFormulas = [
            new("( ((5632.0) / 2e4  ))"), 
            new("(((ad34)/ 2000) )"), 
            new("( ( (5632000e-3) / 20E2) )"),
            new("5632 / 02000")
        ];

        int len = testedFormulas.Length;
        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsFalse(testedFormulas[i].Equals(testedFormulas[(i + 1) % len]));
            Assert.IsFalse(testedFormulas[i] == testedFormulas[(i + 1) % len]);
            Assert.IsTrue(testedFormulas[i] != testedFormulas[(i + 1) % len]);
        }
    }




    // - Tests with multi term formulas with and without parentheses that are equal -

    [TestMethod]
    public void FormulaEquivalency_UnequalMultiTermFormulas_Unequal() {
        Formula[] testedFormulas = [
            new("89 - 23 * a1"),
            new("45 /6E2 * aB123 - 0.4 - 0.04 + 050"),
            new("45.0/ 600 * Ab123 - .4 - 4E-2 + 60"),
            new("45 / 6e2 * Ac0123 - 000.4 - 0.4 + 0060.0")
        ];

        int len = testedFormulas.Length;
        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsFalse(testedFormulas[i].Equals(testedFormulas[(i + 1) % len]));
            Assert.IsFalse(testedFormulas[i] == testedFormulas[(i + 1) % len]);
            Assert.IsTrue(testedFormulas[i] != testedFormulas[(i + 1) % len]);
        }
    }

    [TestMethod]
    public void FormulaEquivalency_UnequalMultiTermFormulasWithParens_Unequal() {
        Formula[] testedFormulas = [
            new("((45 / 6e2) ) * ab123 - (.4 - ((    4e-2 + (60) )))"),
            new("  (45 /6E2) * aB123 - 0.4 - ((0.04 + (060))  )"),
            new("45.0/ 600 * Ab123 - .4 - 4E-2 + 60 "),
            new("833- ab12 / 6e3"),
            new("( (45 / 6e2 )) * AB0123 - (   000.4 - ((4E-2 + (0060.1) )))")
        ];

        int len = testedFormulas.Length;
        for (int i = 0; i < testedFormulas.Length; i++) {
            Assert.IsFalse(testedFormulas[i].Equals(testedFormulas[(i + 1) % len]));
            Assert.IsFalse(testedFormulas[i] == testedFormulas[(i + 1) % len]);
            Assert.IsTrue(testedFormulas[i] != testedFormulas[(i + 1) % len]);
        }
    }
}
