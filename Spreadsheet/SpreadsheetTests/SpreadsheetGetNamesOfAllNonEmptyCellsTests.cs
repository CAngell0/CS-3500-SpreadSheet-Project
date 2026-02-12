// <author> Carson Angell </author>
// <date> 2/11/2026 </date>

namespace SpreadsheetTests;

using Spreadsheet;
using Formula;

[TestClass]
public class SpreadsheetGetNamesOfAllNonemptyCellsTests {
    [TestMethod]
    public void SpreadsheetGetNamesOfAllNonemptyCells_GetCellsOfEmptySheet_EmptySet() {
        Spreadsheet spreadsheet = new();
        ISet<string> result = spreadsheet.GetNamesOfAllNonemptyCells();

        Assert.IsNotNull(result);
        Assert.IsEmpty(result);
    }

    [TestMethod]
    public void SpreadsheetGetNamesOfAllNonemptyCells_GetCellsOfOneDoubleCellSheet_OneNameReturned() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A1", 50.6);
        ISet<string> result = spreadsheet.GetNamesOfAllNonemptyCells();

        Assert.IsNotNull(result);
        Assert.HasCount(1, result);
        Assert.AreEqual("A1", result.First());
    }

    [TestMethod]
    public void SpreadsheetGetNamesOfAllNonemptyCells_GetCellsOfOneStringCellSheet_OneNameReturned() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A1", "Hello World");
        ISet<string> result = spreadsheet.GetNamesOfAllNonemptyCells();

        Assert.IsNotNull(result);
        Assert.HasCount(1, result);
        Assert.AreEqual("A1", result.First());
    }

    [TestMethod]
    public void SpreadsheetGetNamesOfAllNonemptyCells_GetCellsOfOneFormulaCellSheet_OneNameReturned() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A1", new Formula("2 + 2"));
        ISet<string> result = spreadsheet.GetNamesOfAllNonemptyCells();

        Assert.IsNotNull(result);
        Assert.HasCount(1, result);
        Assert.AreEqual("A1", result.First());
    }

    [TestMethod]
    public void SpreadsheetGetNamesOfAllNonemptyCells_GetCellsOfMultiCellSheet_ThreeNameReturned() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("B7", 20);
        spreadsheet.SetCellContents("C5", "Hello World");
        spreadsheet.SetCellContents("D9", new Formula("56 * 70.3 + d6"));
        ISet<string> result = spreadsheet.GetNamesOfAllNonemptyCells();

        HashSet<string> expected = ["B7", "C5", "D9"];
        Assert.IsNotNull(result);
        Assert.HasCount(3, result);
        Assert.IsEmpty(expected.Except(result));
    }
}
