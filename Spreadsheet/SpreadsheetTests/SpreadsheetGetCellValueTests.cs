namespace SpreadsheetTests;

using Formula;
using Spreadsheet;

using System.Text;

//TODO - Add stress test or two (if this method involves evaluating)
// - Remember: A formula error can be cause by a divide by zero or *when the sheet does not have data for that cell*

[TestClass]
public class SpreadsheetGetCellValueTests {
    [TestMethod]
    public void SpreadsheetGetCellValue_FromEmptySheet_EmptyStringReturned() {
        Spreadsheet sheet = new();
        Assert.AreEqual("", (string) sheet.GetCellValue("A1"));
    }

    [TestMethod]
    public void SpreadsheetGetCellValue_FromExplicitEmptyCell_EmptyStringReturned() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "");
        Assert.AreEqual("", (string) sheet.GetCellValue("A1"));
    }




    // --- TESTS WITH CELLS THAT HAVE DOUBLE VALUES FROM A FORMULA ---
    [TestMethod]
    public void SpreadsheetGetCellValue_TwoTermDoubleFormula_CorrectCalculationReturned() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "=10 * 3");
        Assert.AreEqual(30, (double) sheet.GetCellValue("A1"));
    }

    [TestMethod]
    public void SpreadsheetGetCellValue_MultiTermDoubleFormulaWithParens_CorrectCalculationReturned() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "=(10 * 3) / 2 + 8");
        Assert.AreEqual(23, (double) sheet.GetCellValue("A1"));
    }

    [TestMethod]
    public void SpreadsheetGetCellValue_FormulaDependingOnSingleCell_CorrectCalculationReturned() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "=10 * 3");
        sheet.SetContentsOfCell("B2", "=A1 - 10");
        Assert.AreEqual(20, (double) sheet.GetCellValue("B2"));
    }

    [TestMethod]
    public void SpreadsheetGetCellValue_FormulaDependingOnMultipleCells_CorrectCalculationReturned() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "=10 * 3");
        sheet.SetContentsOfCell("B2", "=20 / 2");
        sheet.SetContentsOfCell("C3", "=3 + 7");
        sheet.SetContentsOfCell("D4", "=A1 + B2 - C3");
        Assert.AreEqual(30, (double) sheet.GetCellValue("D4"));
    }

    [TestMethod]
    public void SpreadsheetGetCellValue_OnLongDependencyChain_CorrectCalculationReturned() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "=10 * 3");
        sheet.SetContentsOfCell("B2", "=A1 - 10");
        sheet.SetContentsOfCell("C3", "=B2 - 10");
        sheet.SetContentsOfCell("D4", "=C3 - 10");

        Assert.AreEqual(30, (double) sheet.GetCellValue("A1"));
        Assert.AreEqual(20, (double) sheet.GetCellValue("B2"));
        Assert.AreEqual(10, (double) sheet.GetCellValue("C3"));
        Assert.AreEqual(0, (double) sheet.GetCellValue("D4"));
    }

    [TestMethod]
    public void SpreadsheetGetCellValue_OnComplexDependencyChain_CorrectCalculationReturned() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "=10 * 3");
        sheet.SetContentsOfCell("B2", "=A1 - 10");
        sheet.SetContentsOfCell("C3", "=B2 - 10 * D4");

        sheet.SetContentsOfCell("D4", "=A1 + 5");
        sheet.SetContentsOfCell("F5", "=D4 / 2");

        Assert.AreEqual(30, (double) sheet.GetCellValue("A1"));
        Assert.AreEqual(20, (double) sheet.GetCellValue("B2"));
        Assert.AreEqual(35, (double) sheet.GetCellValue("D4"));
        Assert.AreEqual(-330, (double) sheet.GetCellValue("C3"));
        Assert.AreEqual(17.5, (double) sheet.GetCellValue("F5"));
    }




    // --- TESTS WITH CELLS THAT HAVE STATIC DOUBLE VALUES ---
    [TestMethod]
    public void SpreadsheetGetCellValue_CellWithDoubleIntegerValue_StringReturned() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "93");
        Assert.AreEqual(93, (double) sheet.GetCellValue("A1"));
    }

    [TestMethod]
    public void SpreadsheetGetCellValue_CellWithDoubleValue_StringReturned() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "5.3");
        Assert.AreEqual(5.3, (double) sheet.GetCellValue("A1"));
    }

    [TestMethod]
    public void SpreadsheetGetCellValue_CellWithDoubleValueThatHasLongDecimal_StringReturned() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "5.3567687931");
        Assert.AreEqual(5.3567687931, (double) sheet.GetCellValue("A1"));
    }

    [TestMethod]
    public void SpreadsheetGetCellValue_CellWithScientificDoubleValue_StringReturned() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "2e3");
        Assert.AreEqual(2000, (double) sheet.GetCellValue("A1"));
    }



    // --- TESTS WITH CELLS THAT HAVE STRING VALUES ---

    [TestMethod]
    public void SpreadsheetGetCellValue_CellWithStringValue_StringReturned() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "Hello World!");
        Assert.AreEqual("Hello World!", (string) sheet.GetCellValue("A1"));
    }




    // --- TESTS WITH CELLS THAT HAVE FORMULAERROR VALUES ---
    [TestMethod]
    public void SpreadsheetGetCellValue_DivideByZeroValue_FormualaErrorReturned() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "=56 / 0");
        Assert.IsInstanceOfType<FormulaError>(sheet.GetCellValue("A1"));
    }

    [TestMethod]
    public void SpreadsheetGetCellValue_DivideByZeroValueInDependencyChain_FormualaErrorReturned() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "=20 * 3");
        sheet.SetContentsOfCell("B2", "=A1 + 5 - 10");
        sheet.SetContentsOfCell("C3", "=56 / (B2 - B2)");
        sheet.SetContentsOfCell("D4", "=C3 - 40");
        Assert.IsInstanceOfType<FormulaError>(sheet.GetCellValue("D4"));
    }

    [TestMethod]
    public void SpreadsheetGetCellValue_FormulaDependsOnNonExistentCell_FormualaErrorReturned() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "=B6 + 7");
        Assert.IsInstanceOfType<FormulaError>(sheet.GetCellValue("A1"));
    }

    [TestMethod]
    public void SpreadsheetGetCellValue_FormulaDependsOnNonExistentCellOnLongDependencyChain_FormualaErrorReturned() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "=12 + 7");
        sheet.SetContentsOfCell("B2", "=A1*6 / C9");
        sheet.SetContentsOfCell("C3", "=(B2 + 5) - 2");
        sheet.SetContentsOfCell("D4", "=C3 - 1");
        Assert.IsInstanceOfType<FormulaError>(sheet.GetCellValue("D4"));
    }

    [TestMethod]
    public void SpreadsheetGetCellValue_FormulaDependentOnStringCell_FormulaErrorReturned() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "Hello World!");
        sheet.SetContentsOfCell("B2", "=A1 + 60");
        Assert.IsInstanceOfType<FormulaError>(sheet.GetCellValue("B2"));
    }

    [TestMethod]
    public void SpreadsheetGetCellValue_FormulaDependentOnOnlyStringCells_FormulaErrorReturned() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "Hello World!");
        sheet.SetContentsOfCell("B2", "This is a spreadsheet!");
        sheet.SetContentsOfCell("C3", "=A1 + B2");
        Assert.IsInstanceOfType<FormulaError>(sheet.GetCellValue("C3"));
    }

    [TestMethod]
    public void SpreadsheetGetCellValue_FormulaDependentOnFormulaErrorCell_FormulaErrorReturned() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "Hello World!");
        sheet.SetContentsOfCell("B2", "=A1 + 60");
        sheet.SetContentsOfCell("C3", "=B2 * 80");
        Assert.IsInstanceOfType<FormulaError>(sheet.GetCellValue("B2"));
        Assert.IsInstanceOfType<FormulaError>(sheet.GetCellValue("C3"));
    }




    // --- TESTS WITH CELLS THAT THROW EXCEPTIONS ---

    [TestMethod]
    public void SpreadsheetGetCellValue_InvalidNameOnEmptySheet_FormualaErrorReturned() {
        Spreadsheet sheet = new();
        Assert.Throws<InvalidNameException>(() => sheet.GetCellValue("1"));
    }

    [TestMethod]
    public void SpreadsheetGetCellValue_InvalidNameOnSingleCellSheet_FormualaErrorReturned() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "56");
        Assert.Throws<InvalidNameException>(() => sheet.GetCellValue("ab"));
    }

    [TestMethod]
    public void SpreadsheetGetCellValue_InvalidNameOnMultiCellSheet_FormualaErrorReturned() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "56");
        sheet.SetContentsOfCell("B6", "=48-1");
        sheet.SetContentsOfCell("C3", "Hello");
        Assert.Throws<InvalidNameException>(() => sheet.GetCellValue("23"));
    }
}
