namespace SpreadsheetTests;

using Spreadsheet;
using Formula;

[TestClass]
public class SpreadsheetConstructorTests {
    private readonly string CorrectSpreadsheetPath = "./json/CorrectSpreadsheet.json";
    private readonly string CyclicalSpreadsheetPath = "./json/CorrectSpreadsheet.json";
    private readonly string InvalidNameSpreadsheetPath = "./json/CorrectSpreadsheet.json";

    [TestMethod]
    public void SpreadsheetEmptyConstructor_InitializingSpreadsheet_IsEmpty() {
        Spreadsheet sheet = new();

        Assert.IsNotNull(sheet);
        Assert.IsFalse(sheet.Changed);
    }

    [TestMethod]
    public void SpreadsheetFileConstructor_ReadingCorrectFile_DataRetrieved() {
        Spreadsheet sheet = new(CorrectSpreadsheetPath);

        Dictionary<string, object> expectedContents = [];
        expectedContents["A1"] = 5.0;
        expectedContents["B2"] = new Formula("A1 + 2");
        expectedContents["C3"] = "hello";
        expectedContents["D4"] = 56.23;
        expectedContents["E5"] = 500.0;

        Assert.IsNotNull(sheet);
        foreach (string key in expectedContents.Keys) {
            Assert.AreEqual(expectedContents[key], sheet.GetCellContents(key));
        }
    }

    [TestMethod]
    public void SpreadsheetFileConstructor_ReadingCyclicalFile_SpreadsheetReadWriteException() {
        Spreadsheet sheet;
        Assert.Throws<SpreadsheetReadWriteException>(() => sheet = new(CyclicalSpreadsheetPath));
    }

    [TestMethod]
    public void SpreadsheetFileConstructor_ReadingInvalidNameFile_SpreadsheetReadWriteException() {
        Spreadsheet sheet;
        Assert.Throws<SpreadsheetReadWriteException>(() => sheet = new(InvalidNameSpreadsheetPath));
    }

    [TestMethod]
    public void SpreadsheetFileConstructor_ReadingNonExistentFile_SpreadsheetReadWriteException() {
        Spreadsheet sheet;
        Assert.Throws<SpreadsheetReadWriteException>(() => sheet = new("./this/file/does/not/exist.json"));
    }
}
