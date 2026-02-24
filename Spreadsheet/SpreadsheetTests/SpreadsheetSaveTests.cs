namespace SpreadsheetTests;

using Spreadsheet;
using Formula;

using System.IO;

[TestClass]
public class SpreadsheetSaveTests {
    private readonly string TestingFilePath = "./json/Output.json";

    [TestMethod]
    public void SpreadsheetSaveMethod_BuildAndSaveSpreadsheet_SavesCorrectly() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "56");
        sheet.SetContentsOfCell("B2", "=A1 * 2");
        sheet.SetContentsOfCell("C3", "Hello World!");

        sheet.Save(TestingFilePath);
        Assert.IsTrue(File.Exists(TestingFilePath));

        Spreadsheet retrievedSheet = new(TestingFilePath);
        Assert.IsNotNull(retrievedSheet);
        Assert.AreEqual(56, (double) retrievedSheet.GetCellContents("A1"));
        Assert.AreEqual(new Formula("A1 * 2"), (Formula) retrievedSheet.GetCellContents("B2"));
        Assert.AreEqual("Hello World!", (string) retrievedSheet.GetCellContents("C3"));

        File.Delete(TestingFilePath);
    }

    [TestMethod]
    public void SpreadsheetSaveMethod_SaveEmptySpreadsheet_SavesCorrectly() {
        Spreadsheet sheet = new();
        sheet.Save(TestingFilePath);

        Assert.IsTrue(File.Exists(TestingFilePath));

        string expectedJSON = "{\"Cells\":{}}";
        string retrievedJSON = File.ReadAllText(TestingFilePath);
        Assert.AreEqual(expectedJSON, retrievedJSON);

        File.Delete(TestingFilePath);
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
