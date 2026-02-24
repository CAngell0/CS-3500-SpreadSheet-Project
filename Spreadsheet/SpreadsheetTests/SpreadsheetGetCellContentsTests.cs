// <author> Carson Angell </author>
// <date> 2/11/2026 </date>

// Unit tests for the GetCellContents method in the Spreadsheet class.

namespace SpreadsheetTests;

using Spreadsheet;
using Formula;

[TestClass]
public class SpreadsheetGetCellContentsTests {

    // --- TESTS ON AN EMPTY SPREADSHEET INSTANCE ---
    // - Tests that throw an invalid name exception -
    [TestMethod]
    public void SpreadsheetGetCellContents_EmptyStringNameFromEmptySheet_InvalidNameException() {
        Spreadsheet spreadsheet = new();
        Assert.Throws<InvalidNameException>(() => spreadsheet.GetCellContents(""));
    }

    [TestMethod]
    public void SpreadsheetGetCellContents_InvalidNameFromEmptySheet_InvalidNameException() {
        Spreadsheet spreadsheet = new();
        Assert.Throws<InvalidNameException>(() => spreadsheet.GetCellContents("kb"));
    }


    // - Tests that don't throw an exception -
    [TestMethod]
    public void SpreadsheetGetCellContents_CorrectNameFromEmptySheet_EmptyString() {
        Spreadsheet spreadsheet = new();
        Assert.AreEqual("", spreadsheet.GetCellContents("B5"));
    }

    [TestMethod]
    public void SpreadsheetGetCellContents_IncorrectNameFromSingleCellSheet_EmptyString() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A5", 20);

        Assert.AreEqual("", spreadsheet.GetCellContents("B5"));
    }




    // --- TESTS ON A SPREADSHEET INSTANCE WITH ONE CELL ---
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
        spreadsheet.SetCellContents("A5", new Formula("63 + 2e3"));
        object result = spreadsheet.GetCellContents("A5");

        Formula expected = new("63+2000");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Formula>(result);
        Assert.AreEqual(expected, (Formula) result);
    }
}
