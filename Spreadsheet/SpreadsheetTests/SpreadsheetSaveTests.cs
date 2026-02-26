namespace SpreadsheetTests;

using Spreadsheet;
using Formula;

using System.IO;
using Spreadsheet.Model;

using System.Text.Json;

[TestClass]
public class SpreadsheetSaveTests {
    private static readonly string TestingFileName = "testSheet.json";

    [TestMethod]
    [DoNotParallelize]
    public void SpreadsheetSaveMethod_BuildAndSaveSpreadsheet_SavesCorrectly() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "56");
        sheet.SetContentsOfCell("B2", "=A1 * 2");
        sheet.SetContentsOfCell("C3", "Hello World!");

        sheet.Save(TestingFileName);
        Assert.IsTrue(File.Exists(TestingFileName));

        Spreadsheet retrievedSheet = new(TestingFileName);
        Assert.IsNotNull(retrievedSheet);
        Assert.AreEqual(56, (double) retrievedSheet.GetCellContents("A1"));
        Assert.AreEqual(new Formula("A1 * 2"), (Formula) retrievedSheet.GetCellContents("B2"));
        Assert.AreEqual("Hello World!", (string) retrievedSheet.GetCellContents("C3"));
    }

    [TestMethod]
    [DoNotParallelize]
    public void SpreadsheetSaveMethod_SaveEmptySpreadsheet_SavesCorrectly() {
        Spreadsheet sheet = new();
        sheet.Save(TestingFileName);

        Assert.IsTrue(File.Exists(TestingFileName));

        string retrievedJSONString = File.ReadAllText(TestingFileName);
        SheetJSON? retrievedJSON = JsonSerializer.Deserialize<SheetJSON>(retrievedJSONString);

        Assert.IsNotNull(retrievedJSON);
        Assert.IsNotNull(retrievedJSON.Cells);
        Assert.IsEmpty(retrievedJSON.Cells);
    }

    [TestMethod]
    public void SpreadsheetSaveMethod_SavesToFileWithIncorrectExtension_SpreadsheetReadWriteException() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "56");

        Assert.Throws<SpreadsheetReadWriteException>(() => sheet.Save("./path/to/incorrect/file/type.txt"));
    }

    [TestMethod]
    public void SpreadsheetSaveMethod_SavesToFolderInsteadOfFile_SpreadsheetReadWriteException() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "56");

        Assert.Throws<SpreadsheetReadWriteException>(() => sheet.Save("./path/to/dummy/folder"));
    }
}
