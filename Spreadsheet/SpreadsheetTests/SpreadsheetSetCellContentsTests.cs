namespace SpreadsheetTests;

using Spreadsheet;
using Formula;

[TestClass]
public class SpreadsheetSetCellContentsTests {

    // --- TESTS ON AN EMPTY SPREADSHEET INSTANCE ---
    // - Tests that throw an exception -
    [TestMethod]
    public void SpreadsheetSetCellContents_EmptyStringNameOnEmptySheet_InvalidNameException() {
        Spreadsheet spreadsheet = new();
        Assert.Throws<InvalidNameException>(() => spreadsheet.SetCellContents("", 5));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_InvalidNameOnEmptySheet_InvalidNameException() {
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
        Assert.IsTrue(110.0.Equals((double)result, 0.000001));
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
    public void SpreadsheetSetCellContents_EmptyStringNameOnSingleCellSheet_InvalidNameException() {
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
    public void SpreadsheetSetCellContents_NewDoubleCellOnSingleCellSheet_SuccessfullyAdds() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("B7", 20);
        spreadsheet.SetCellContents("A5", 110);

        object result = spreadsheet.GetCellContents("A5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.IsTrue(110.0.Equals((double)result, 0.000001));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_NewStringCellOnSingleCellSheet_SuccessfullyAdds() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("B7", 20);
        spreadsheet.SetCellContents("A5", "Test");

        object result = spreadsheet.GetCellContents("A5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("Test", (string)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_NewFormulaCellOnSingleCellSheet_SuccessfullyAdds() {
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
    public void SpreadsheetSetCellContents_OverwriteDoubleCellSameValueOnSingleCellSheet_NoChange() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", 5);
        spreadsheet.SetCellContents("A5", 5);

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.IsTrue(5.0.Equals((double)result, 0.000001));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteDoubleCellWithDoubleOnSingleCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", 5);
        spreadsheet.SetCellContents("A5", 59);

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.IsTrue(59.0.Equals((double)result, 0.000001));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteStringCellWithStringOnSingleCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", "Hello");
        spreadsheet.SetCellContents("A5", "World");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("World", (string)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteFormulaCellWithFormulaOnSingleCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", new Formula("3 * 80"));
        spreadsheet.SetCellContents("A5", new Formula("2e2 / 5"));

        object result = spreadsheet.GetCellContents("A5");

        Formula expected = new("2e2/5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual(expected, (Formula)result);
    }


    // - Tests on overwriting existing cell with different type of value -
    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteDoubleCellWithStringOnSingleCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", 5);
        spreadsheet.SetCellContents("A5", "Hello");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("Hello", (string)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteDoubleCellWithFormulaOnSingleCellSheet_SuccessfullyOverwrites() {
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
    public void SpreadsheetSetCellContents_OverwriteStringCellWithDoubleOnSingleCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", "Hello World");
        spreadsheet.SetCellContents("A5", 56.7);

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.IsTrue(56.7.Equals((double)result, 0.000001));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteStringCellWithFormulaOnSingleCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", "Test");
        spreadsheet.SetCellContents("A5", new Formula("2 * 6 + 5"));

        object result = spreadsheet.GetCellContents("A5");

        Formula expected = new("2*6+5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Formula>(result);
        Assert.AreEqual(expected, (Formula)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteFormulaCellWithDoubleOnSingleCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", new Formula("3 * 80"));
        spreadsheet.SetCellContents("A5", 80);

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.IsTrue(80.0.Equals((double)result, 0.000001));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteFormulaCellWithStringOnSingleCellSheet_SuccessfullyOverwrites() {
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
    public void SpreadhseetSetCellContents_DeletingExistingDoubleCellOnSingleCellSheet_OverwritesToEmptyString() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", 5);
        spreadsheet.SetCellContents("A5", ""); // Should remove the cell from the backend data structure...

        object result = spreadsheet.GetCellContents("A5"); // but it should return a "" because that denotes an empty cell

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("", (string)result);
    }

    [TestMethod]
    public void SpreadhseetSetCellContents_DeletingExistingStringCellOnSingleCellSheet_OverwritesToEmptyString() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", "Tester");
        spreadsheet.SetCellContents("A5", ""); // Should remove the cell from the backend data structure...

        object result = spreadsheet.GetCellContents("A5"); // but it should return a "" because that denotes an empty cell

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("", (string)result);
    }

    [TestMethod]
    public void SpreadhseetSetCellContents_DeletingExistingFormulaCellOnSingleCellSheet_OverwritesToEmptyString() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", new Formula("2+2"));
        spreadsheet.SetCellContents("A5", ""); // Should remove the cell from the backend data structure...

        object result = spreadsheet.GetCellContents("A5"); // but it should return a "" because that denotes an empty cell

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("", (string)result);
    }
}
