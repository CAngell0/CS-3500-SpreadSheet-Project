namespace SpreadsheetTests;

using Spreadsheet;
using Formula;

[TestClass]
public class SpreadsheetSetCellContentsTests {

    // --- TESTS ON AN EMPTY SPREADSHEET INSTANCE ---
    // - Tests that throw an exception -
    [TestMethod]
    public void SpreadsheetSetCellContents_OfEmptyStringOnEmptySheet_InvalidNameException() {
        Spreadsheet spreadsheet = new();
        Assert.Throws<InvalidNameException>(() => spreadsheet.SetCellContents("", 5));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_InvalidCellNameOnEmptySheet_InvalidNameException() {
        Spreadsheet spreadsheet = new();
        Assert.Throws<InvalidNameException>(() => spreadsheet.SetCellContents("H", 5));
        Assert.Throws<InvalidNameException>(() => spreadsheet.SetCellContents("8", 5));
    }

    // - Tests on creating new cells -
    [TestMethod]
    public void SpreadsheetSetCellContents_NewDoubleCellOnEmptySheet_ReturnsDouble() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", 110);

        object result = spreadsheet.GetCellContents("A5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.AreEqual(110, (double)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_NewStringCellOnEmptySheet_ReturnsString() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", "Test");

        object result = spreadsheet.GetCellContents("A5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("Test", (string)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_NewFormulaCellOnEmptySheet_ReturnsFormula() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", new Formula("80 + d3"));

        object result = spreadsheet.GetCellContents("A5");
        Formula expected = new("80+D3");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Formula>(result);
        Assert.AreEqual(expected, (Formula)result);
    }




    // --- TESTS ON A SPREADSHEET INSTANCE WITH ONE CELL ---
    // - Tests that throw an exception -
    [TestMethod]
    public void SpreadsheetSetCellContents_OfEmptyStringOnSingleCellSheet_InvalidNameException() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("B7", 20);
        Assert.Throws<InvalidNameException>(() => spreadsheet.SetCellContents("", 5));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_InvalidCellNameOnSingleCellSheet_InvalidNameException() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("B7", 20);
        Assert.Throws<InvalidNameException>(() => spreadsheet.SetCellContents("H", 5));
        Assert.Throws<InvalidNameException>(() => spreadsheet.SetCellContents("8", 5));
    }

    // - Tests on creating new cells -
    [TestMethod]
    public void SpreadsheetSetCellContents_NewDoubleCellOnSingleCellSheet_ReturnsDouble() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("B7", 20);
        spreadsheet.SetCellContents("A5", 110);

        object result = spreadsheet.GetCellContents("A5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.AreEqual(110, (double)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_NewStringCellOnSingleCellSheet_ReturnsString() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("B7", 20);
        spreadsheet.SetCellContents("A5", "Test");

        object result = spreadsheet.GetCellContents("A5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("Test", (string)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_NewFormulaCellOnSingleCellSheet_ReturnsFormula() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("B7", 20);
        spreadsheet.SetCellContents("A5", new Formula("80 + d3"));

        object result = spreadsheet.GetCellContents("A5");
        Formula expected = new("80+D3");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Formula>(result);
        Assert.AreEqual(expected, (Formula)result);
    }

    // - Tests on overwriting existing cell with the same type of value -
    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteDoubleCellWithDoubleOnSingleCellSheet_ReturnsDouble() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", 5);
        spreadsheet.SetCellContents("A5", 59);

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.AreEqual(59, (double)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteStringCellWithStringOnSingleCellSheet_ReturnsString() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", "Hello");
        spreadsheet.SetCellContents("A5", "World");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("World", (string)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteFormulaCellWithFormulaOnSingleCellSheet_ReturnsFormula() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", new Formula("3 * 80"));
        spreadsheet.SetCellContents("A5", new Formula("2e2 / 5"));

        object result = spreadsheet.GetCellContents("A5");

        Formula expected = new("2e2/5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual(expected, (Formula) result);
    }

    // - Tests on overwriting existing cell with different type of value -
    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteDoubleCellWithStringOnSingleCellSheet_ReturnsString() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", 5);
        spreadsheet.SetCellContents("A5", "Hello");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("Hello", (string)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteDoubleCellWithFormulaOnSingleCellSheet_ReturnsFormula() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", 90);
        spreadsheet.SetCellContents("A5", new Formula("6 / 2.78"));

        object result = spreadsheet.GetCellContents("A5");

        Formula expected = new("6/2.78");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Formula>(result);
        Assert.AreEqual(expected, (Formula)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteStringCellWithDoubleOnSingleCellSheet_ReturnsDouble() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", "Hello World");
        spreadsheet.SetCellContents("A5", 56.7);

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.AreEqual(56.7, (double)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteStringCellWithFormulaOnSingleCellSheet_ReturnsFormula() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", "Test");
        spreadsheet.SetCellContents("A5", new Formula("2 * 6 + 5"));

        object result = spreadsheet.GetCellContents("A5");

        Formula expected = new("2*6+5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Formula>(result);
        Assert.AreEqual(expected, (Formula) result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteFormulaCellWithDoubleOnSingleCellSheet_ReturnsDouble() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", new Formula("3 * 80"));
        spreadsheet.SetCellContents("A5", 80);

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.AreEqual(80, (double)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteFormulaCellWithStringOnSingleCellSheet_ReturnsString() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", new Formula("3 * 80"));
        spreadsheet.SetCellContents("A5", "Tester");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("Tester", (string)result);
    }

    // - Tests on deleting existing cell -
    [TestMethod]
    public void SpreadhseetSetCellContents_DeletingExistingDoubleCellOnSingleCellSheet_ReturnsEmptyString() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", 5);
        spreadsheet.SetCellContents("A5", ""); // Should remove the cell from the backend data structure...

        object result = spreadsheet.GetCellContents("A5"); // but it should return a "" because that denotes an empty cell

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("", (string)result);
    }

    [TestMethod]
    public void SpreadhseetSetCellContents_DeletingExistingStringCellOnSingleCellSheet_ReturnsEmptyString() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", "Tester");
        spreadsheet.SetCellContents("A5", ""); // Should remove the cell from the backend data structure...

        object result = spreadsheet.GetCellContents("A5"); // but it should return a "" because that denotes an empty cell

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("", (string)result);
    }

    [TestMethod]
    public void SpreadhseetSetCellContents_DeletingExistingFormulaCellOnSingleCellSheet_ReturnsEmptyString() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", new Formula("2+2"));
        spreadsheet.SetCellContents("A5", ""); // Should remove the cell from the backend data structure...

        object result = spreadsheet.GetCellContents("A5"); // but it should return a "" because that denotes an empty cell

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("", (string)result);
    }
}
