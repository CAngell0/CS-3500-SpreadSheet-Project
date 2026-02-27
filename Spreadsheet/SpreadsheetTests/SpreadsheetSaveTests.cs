namespace SpreadsheetTests;

using Spreadsheet;
using Formula;

using System.IO;
using Spreadsheet.Model;

using System.Text.Json;

[TestClass]
public class SpreadsheetSaveTests {
    /// <summary> Name of the JSON file to test saving to in tests </summary>
    private static readonly string TestingFileNameJSON = "testSheet.json";
    /// <summary> Name of the TXT file to test saving to in tests </summary>
    private static readonly string TestingFileNameTXT = "testSheet.txt";


    [TestMethod]
    public void SpreadsheetSaveMethod_SavesToNonexistentDirectory_SpreadsheetReadWriteException() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "56");

        Assert.Throws<SpreadsheetReadWriteException>(() => sheet.Save("./path/to/missing/folder"));
    }


    // --- TESTS SAVING TO A JSON FILE ---

    [TestMethod]
    [DoNotParallelize]
    public void SpreadsheetSaveMethod_SaveFullSpreadsheetToExistingJSONFile_SavesCorrectly() {
        File.Delete(TestingFileNameJSON);
        File.WriteAllText(TestingFileNameJSON, string.Empty);

        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "56");
        sheet.SetContentsOfCell("B2", "=A1 * 2");
        sheet.SetContentsOfCell("C3", "Hello World!");
        sheet.SetContentsOfCell("D4", "=16 + 28 / 2");

        sheet.Save(TestingFileNameJSON);
        Assert.IsTrue(File.Exists(TestingFileNameJSON));

        Spreadsheet retrievedSheet = new(TestingFileNameJSON);
        Assert.IsNotNull(retrievedSheet);
        Assert.AreEqual(56, (double) retrievedSheet.GetCellContents("A1"));
        Assert.AreEqual(new Formula("A1 * 2"), (Formula) retrievedSheet.GetCellContents("B2"));
        Assert.AreEqual("Hello World!", (string) retrievedSheet.GetCellContents("C3"));
        Assert.AreEqual(new Formula("16 + 28 / 2"), (Formula) retrievedSheet.GetCellContents("D4"));
    }

    [TestMethod]
    [DoNotParallelize]
    public void SpreadsheetSaveMethod_SaveFullSpreadsheetToMissingJSONFile_CreatesFileAndSavesCorrectly() {
        File.Delete(TestingFileNameJSON);

        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "56");
        sheet.SetContentsOfCell("B2", "=A1 * 2");
        sheet.SetContentsOfCell("C3", "Hello World!");
        sheet.SetContentsOfCell("D4", "=16 + 28 / 2");

        sheet.Save(TestingFileNameJSON);
        Assert.IsTrue(File.Exists(TestingFileNameJSON));

        Spreadsheet retrievedSheet = new(TestingFileNameJSON);
        Assert.IsNotNull(retrievedSheet);
        Assert.AreEqual(56, (double) retrievedSheet.GetCellContents("A1"));
        Assert.AreEqual(new Formula("A1 * 2"), (Formula) retrievedSheet.GetCellContents("B2"));
        Assert.AreEqual("Hello World!", (string) retrievedSheet.GetCellContents("C3"));
        Assert.AreEqual(new Formula("16 + 28 / 2"), (Formula) retrievedSheet.GetCellContents("D4"));
    }

    [TestMethod]
    [DoNotParallelize]
    public void SpreadsheetSaveMethod_SaveEmptySpreadsheetToJSON_SavesCorrectly() {
        Spreadsheet sheet = new();
        sheet.Save(TestingFileNameJSON);

        Assert.IsTrue(File.Exists(TestingFileNameJSON));

        string retrievedJSONString = File.ReadAllText(TestingFileNameJSON);
        SheetJSON? retrievedJSON = JsonSerializer.Deserialize<SheetJSON>(retrievedJSONString);

        Assert.IsNotNull(retrievedJSON);
        Assert.IsNotNull(retrievedJSON.Cells);
        Assert.IsEmpty(retrievedJSON.Cells);
    }

    [TestMethod]
    [DoNotParallelize]
    public void SpreadsheetSaveMethod_SaveSheetWithExplicitEmptyCellToJSON_SavesCorrectly() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "");
        sheet.Save(TestingFileNameJSON);

        Assert.IsTrue(File.Exists(TestingFileNameJSON));

        string retrievedJSONString = File.ReadAllText(TestingFileNameJSON);
        SheetJSON? retrievedJSON = JsonSerializer.Deserialize<SheetJSON>(retrievedJSONString);

        Assert.IsNotNull(retrievedJSON);
        Assert.IsNotNull(retrievedJSON.Cells);
        Assert.IsEmpty(retrievedJSON.Cells);
    }

    [TestMethod]
    public void SpreadsheetSaveMethod_SavesToNonexistentJSONFileInMissingFolder_SpreadsheetReadWriteException() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "56");

        Assert.Throws<SpreadsheetReadWriteException>(() => sheet.Save("./path/to/missing/folder/file.json"));
    }




    // --- TESTS SAVING TO A TXT FILE ---

    [TestMethod]
    [DoNotParallelize]
    public void SpreadsheetSaveMethod_SaveFullSpreadsheetToExistingTXTFile_SavesCorrectly() {
        File.Delete(TestingFileNameTXT);
        File.WriteAllText(TestingFileNameTXT, string.Empty);

        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "56");
        sheet.SetContentsOfCell("B2", "=A1 * 2");
        sheet.SetContentsOfCell("C3", "Hello World!");
        sheet.SetContentsOfCell("D4", "=16 + 28 / 2");

        sheet.Save(TestingFileNameTXT);
        Assert.IsTrue(File.Exists(TestingFileNameTXT));

        Spreadsheet retrievedSheet = new(TestingFileNameTXT);
        Assert.IsNotNull(retrievedSheet);
        Assert.AreEqual(56, (double) retrievedSheet.GetCellContents("A1"));
        Assert.AreEqual(new Formula("A1 * 2"), (Formula) retrievedSheet.GetCellContents("B2"));
        Assert.AreEqual("Hello World!", (string) retrievedSheet.GetCellContents("C3"));
        Assert.AreEqual(new Formula("16 + 28 / 2"), (Formula) retrievedSheet.GetCellContents("D4"));
    }

    [TestMethod]
    [DoNotParallelize]
    public void SpreadsheetSaveMethod_SaveFullSpreadsheetToMissingTXTFile_CreatesFileAndSavesCorrectly() {
        File.Delete(TestingFileNameTXT);

        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "56");
        sheet.SetContentsOfCell("B2", "=A1 * 2");
        sheet.SetContentsOfCell("C3", "Hello World!");
        sheet.SetContentsOfCell("D4", "=16 + 28 / 2");

        sheet.Save(TestingFileNameTXT);
        Assert.IsTrue(File.Exists(TestingFileNameTXT));

        Spreadsheet retrievedSheet = new(TestingFileNameTXT);
        Assert.IsNotNull(retrievedSheet);
        Assert.AreEqual(56, (double) retrievedSheet.GetCellContents("A1"));
        Assert.AreEqual(new Formula("A1 * 2"), (Formula) retrievedSheet.GetCellContents("B2"));
        Assert.AreEqual("Hello World!", (string) retrievedSheet.GetCellContents("C3"));
        Assert.AreEqual(new Formula("16 + 28 / 2"), (Formula) retrievedSheet.GetCellContents("D4"));
    }

    [TestMethod]
    [DoNotParallelize]
    public void SpreadsheetSaveMethod_SaveEmptySpreadsheetToTXT_SavesCorrectly() {
        Spreadsheet sheet = new();
        sheet.Save(TestingFileNameTXT);

        Assert.IsTrue(File.Exists(TestingFileNameTXT));

        string retrievedJSONString = File.ReadAllText(TestingFileNameTXT);
        SheetJSON? retrievedJSON = JsonSerializer.Deserialize<SheetJSON>(retrievedJSONString);

        Assert.IsNotNull(retrievedJSON);
        Assert.IsNotNull(retrievedJSON.Cells);
        Assert.IsEmpty(retrievedJSON.Cells);
    }

    [TestMethod]
    [DoNotParallelize]
    public void SpreadsheetSaveMethod_SaveSheetWithExplicitEmptyCellToTXT_SavesCorrectly() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "");
        sheet.Save(TestingFileNameTXT);

        Assert.IsTrue(File.Exists(TestingFileNameTXT));

        string retrievedJSONString = File.ReadAllText(TestingFileNameTXT);
        SheetJSON? retrievedJSON = JsonSerializer.Deserialize<SheetJSON>(retrievedJSONString);

        Assert.IsNotNull(retrievedJSON);
        Assert.IsNotNull(retrievedJSON.Cells);
        Assert.IsEmpty(retrievedJSON.Cells);
    }

    [TestMethod]
    public void SpreadsheetSaveMethod_SavesToNonexistentTXTFileInMissingFolder_SpreadsheetReadWriteException() {
        Spreadsheet sheet = new();
        sheet.SetContentsOfCell("A1", "56");

        Assert.Throws<SpreadsheetReadWriteException>(() => sheet.Save("./path/to/missing/folder/file.txt"));
    }
}
