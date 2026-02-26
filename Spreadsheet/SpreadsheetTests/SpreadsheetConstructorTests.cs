namespace SpreadsheetTests;

using Spreadsheet;
using Formula;

//TODO - Add stress test or two

[TestClass]
public class SpreadsheetConstructorTests {
    private static readonly string CorrectSheetJSON = "{\"Cells\":{\"A1\":{\"StringForm\":\"5\"},\"B2\":{\"StringForm\":\"=A1+2\"},\"C3\":{\"StringForm\":\"hello\"},\"D4\":{\"StringForm\":\"56.23\"},\"E5\":{\"StringForm\":\"5e2\"}}}";
    private static readonly string CyclicalSheetJSON = "{\"Cells\":{\"A1\":{\"StringForm\":\"5\"},\"B2\":{\"StringForm\":\"=A1+2\"},\"D4\":{\"StringForm\":\"56.23\"},\"E5\":{\"StringForm\":\"=B2 * 2 + G7\"},\"F6\":{\"StringForm\":\"=E5 - 2\"},\"G7\":{\"StringForm\":\"=F6 / 10\"}}}";
    private static readonly string InvalidNameSheetJSON = "{\"Cells\":{\"8\":{\"StringForm\":\"=5 * 8\"},\"A1\":{\"StringForm\":\"5\"},\"B2\":{\"StringForm\":\"=A1+2\"},\"C3\":{\"StringForm\":\"hello\"},\"D4\":{\"StringForm\":\"56.23\"},\"E5\":{\"StringForm\":\"5e2\"},\"G\":{\"StringForm\":\"62\"}}}";
    private static readonly string TestingFileName = "testSheet.json";

    [TestMethod]
    public void SpreadsheetEmptyConstructor_InitializingSpreadsheet_IsEmpty() {
        Spreadsheet sheet = new();

        Assert.IsNotNull(sheet);
        Assert.IsFalse(sheet.Changed);
    }

    [TestMethod]
    [DoNotParallelize]
    public void SpreadsheetFileConstructor_ReadingCorrectFile_DataRetrieved() {
        File.Delete(TestingFileName);
        File.WriteAllText(TestingFileName, CorrectSheetJSON);
        Spreadsheet sheet = new(TestingFileName);

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
    [DoNotParallelize]
    public void SpreadsheetFileConstructor_ReadingCyclicalFile_SpreadsheetReadWriteException() {
        Spreadsheet sheet;
        File.Delete(TestingFileName);
        File.WriteAllText(TestingFileName, CyclicalSheetJSON);
        Assert.Throws<SpreadsheetReadWriteException>(() => sheet = new(TestingFileName));
    }

    [TestMethod]
    [DoNotParallelize]
    public void SpreadsheetFileConstructor_ReadingInvalidNameFile_SpreadsheetReadWriteException() {
        Spreadsheet sheet;
        File.Delete(TestingFileName);
        File.WriteAllText(TestingFileName, InvalidNameSheetJSON);
        Assert.Throws<SpreadsheetReadWriteException>(() => sheet = new(TestingFileName));
    }

    [TestMethod]
    public void SpreadsheetFileConstructor_ReadingNonExistentFile_SpreadsheetReadWriteException() {
        Spreadsheet sheet;
        Assert.Throws<SpreadsheetReadWriteException>(() => sheet = new("/this/file/does/not/exist.json"));
    }
}
