//TODO - Delete or remake this test file after implementation is finished
// This is a temporary test case file that doesn't follow any particular organization

namespace SpreadsheetTests;

using Spreadsheet;
using Formula;

[TestClass]
public sealed class SpreadSheetGeneralTests {
    // --- SetCellContents method tests
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

    [TestMethod]
    public void SpreadsheetSetCellContents_SettingExistingDoubleCell_InvalidNameException() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", 5);
        spreadsheet.SetCellContents("A5", 59);

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.AreEqual(59, (double) result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_SettingExistingStringCell_InvalidNameException() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", "Hello");
        spreadsheet.SetCellContents("A5", "World");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("World", (string) result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_SettingExistingFormulaCell_InvalidNameException() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", "3 * 80");
        spreadsheet.SetCellContents("A5", "2e2 / 5");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.IsTrue(((Formula) result).Equals(new Formula("200/5")));
    }





    // --- GetCellContents method tests
    [TestMethod]
    public void SpreadsheetGetCellContents_OfEmptyString_InvalidNameException() {
        Spreadsheet spreadsheet = new();
        Assert.Throws<InvalidNameException>(() => spreadsheet.GetCellContents(""));
    }

    [TestMethod]
    public void SpreadsheetGetCellContents_OfIncorrectNameFromEmptySheet_InvalidNameException() {
        Spreadsheet spreadsheet = new();
        Assert.Throws<InvalidNameException>(() => spreadsheet.GetCellContents("B5"));
    }

    [TestMethod]
    public void SpreadsheetGetCellContents_OfIncorrectNameFromSingleCellSheet_InvalidNameException() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", 20);

        Assert.Throws<InvalidNameException>(() => spreadsheet.GetCellContents("B5"));
    }

    [TestMethod]
    public void SpreadsheetGetCellContents_DoubleCellFromSingleCellSheet_CorrectValue() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", 20);
        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.AreEqual(20, (double) result);
    }

    [TestMethod]
    public void SpreadsheetGetCellContents_StringCellFromSingleCellSheet_CorrectValue() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", "Hello World");
        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("Hello World", (string) result);
    }

    [TestMethod]
    public void SpreadsheetGetCellContents_FormulaCellFromSingleCellSheet_CorrectValue() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", "63 + 2e3");
        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Formula>(result);
        Assert.IsTrue(((Formula) result).Equals(new Formula("63+2e3")));
    }
}
