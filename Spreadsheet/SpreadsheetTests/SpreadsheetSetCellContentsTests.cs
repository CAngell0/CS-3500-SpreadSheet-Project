namespace SpreadsheetTests;

using Spreadsheet;
using Formula;

[TestClass]
public class SpreadsheetSetCellContentsTests {
    private static Spreadsheet CreateSingleCellSheet() {
        Spreadsheet sheet = new();
        sheet.SetCellContents("B7", 20);
        return sheet;
    }

    private static Spreadsheet CreateSingleCellSheet(string cellName, object contents) {
        Spreadsheet sheet = new();
        if (contents is double doub) sheet.SetCellContents(cellName, doub);
        else if (contents is string str) sheet.SetCellContents(cellName, str); 
        else if (contents is Formula formula) sheet.SetCellContents(cellName, formula); 
        return sheet;
    }

    private static Spreadsheet CreateMultiCellSheet() {
        Spreadsheet sheet = new();
        sheet.SetCellContents("B7", 20);
        sheet.SetCellContents("C5", "Hello World");
        sheet.SetCellContents("D9", new Formula("56 * 70.3 + d6"));
        return sheet;
    }

    private static Spreadsheet CreateMultiCellSheet(string additionalCellName, object contents) {
        Spreadsheet sheet = CreateMultiCellSheet();
        if (contents is double doub) sheet.SetCellContents(additionalCellName, doub);
        else if (contents is string str) sheet.SetCellContents(additionalCellName, str); 
        else if (contents is Formula formula) sheet.SetCellContents(additionalCellName, formula); 
        return sheet;
    }

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
    // - Tests that throw a invalid name exception -
    [TestMethod]
    public void SpreadsheetSetCellContents_EmptyStringNameOnSingleCellSheet_InvalidNameException() {
        Spreadsheet spreadsheet = CreateSingleCellSheet();
        Assert.Throws<InvalidNameException>(() => spreadsheet.SetCellContents("", 5));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_InvalidCellNameOnSingleCellSheet_InvalidNameException() {
        Spreadsheet spreadsheet = CreateSingleCellSheet();
        Assert.Throws<InvalidNameException>(() => spreadsheet.SetCellContents("H", 5));
        Assert.Throws<InvalidNameException>(() => spreadsheet.SetCellContents("8", 5));
    }


    // - Tests that throw a circular exception
    [TestMethod]
    public void SpreadsheetSetCellContents_AddFormulaThatDependsOnItself_CircularException() {
        Spreadsheet spreadsheet = new();
        Assert.Throws<CircularException>(() => spreadsheet.SetCellContents("A1", new Formula("A1 + 2")));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_AddTwoFormulasThatDependOnEachOther_CircularException() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A1", new Formula("B2 * 8"));
        Assert.Throws<CircularException>(() => spreadsheet.SetCellContents("B2", new Formula("A1 + 2")));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteExistingDoubleCellWithFormulaThatDependsOnItself_CircularException() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A1", 100.56);
        Assert.Throws<CircularException>(() => spreadsheet.SetCellContents("A1", new Formula("A1 + 2")));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteExistingStringCellWithFormulaThatDependsOnItself_CircularException() {
        Spreadsheet spreadsheet = new();
        spreadsheet.SetCellContents("A1", "Hello World");
        Assert.Throws<CircularException>(() => spreadsheet.SetCellContents("A1", new Formula("A1 + 2")));
    }


    // - Tests on creating new cells -
    [TestMethod]
    public void SpreadsheetSetCellContents_NewDoubleCellOnSingleCellSheet_SuccessfullyAdds() {
        Spreadsheet spreadsheet = CreateSingleCellSheet();
        spreadsheet.SetCellContents("A5", 110);

        object result = spreadsheet.GetCellContents("A5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.IsTrue(110.0.Equals((double)result, 0.000001));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_NewStringCellOnSingleCellSheet_SuccessfullyAdds() {
        Spreadsheet spreadsheet = CreateSingleCellSheet();
        spreadsheet.SetCellContents("A5", "Test");

        object result = spreadsheet.GetCellContents("A5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("Test", (string)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_NewFormulaCellOnSingleCellSheet_SuccessfullyAdds() {
        Spreadsheet spreadsheet = CreateSingleCellSheet();
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
        Spreadsheet spreadsheet = CreateSingleCellSheet("A5", 5);
        spreadsheet.SetCellContents("A5", 5);

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.IsTrue(5.0.Equals((double)result, 0.000001));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteDoubleCellWithDoubleOnSingleCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateSingleCellSheet("A5", 5);
        spreadsheet.SetCellContents("A5", 59);

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.IsTrue(59.0.Equals((double)result, 0.000001));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteStringCellWithStringOnSingleCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateSingleCellSheet("A5", "Hello");
        spreadsheet.SetCellContents("A5", "World");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("World", (string)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteFormulaCellWithFormulaOnSingleCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateSingleCellSheet("A5", new Formula("3 * 80"));
        spreadsheet.SetCellContents("A5", new Formula("2e2 / 5"));

        object result = spreadsheet.GetCellContents("A5");

        Formula expected = new("2e2/5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Formula>(result);
        Assert.AreEqual(expected, (Formula)result);
    }


    // - Tests on overwriting existing cell with different type of value -
    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteDoubleCellWithStringOnSingleCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateSingleCellSheet("A5", 5);
        spreadsheet.SetCellContents("A5", "Hello");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("Hello", (string)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteDoubleCellWithFormulaOnSingleCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateSingleCellSheet("A5", 90);
        spreadsheet.SetCellContents("A5", new Formula("6 / 2.78"));

        object result = spreadsheet.GetCellContents("A5");

        Formula expected = new("6/2.78");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Formula>(result);
        Assert.AreEqual(expected, (Formula)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteStringCellWithDoubleOnSingleCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateSingleCellSheet("A5", "Hello World");
        spreadsheet.SetCellContents("A5", 56.7);

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.IsTrue(56.7.Equals((double)result, 0.000001));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteStringCellWithFormulaOnSingleCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateSingleCellSheet("A5", "Test");
        spreadsheet.SetCellContents("A5", new Formula("2 * 6 + 5"));

        object result = spreadsheet.GetCellContents("A5");

        Formula expected = new("2*6+5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Formula>(result);
        Assert.AreEqual(expected, (Formula)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteFormulaCellWithDoubleOnSingleCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateSingleCellSheet("A5", new Formula("3 * 80"));
        spreadsheet.SetCellContents("A5", 80);

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.IsTrue(80.0.Equals((double)result, 0.000001));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteFormulaCellWithStringOnSingleCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateSingleCellSheet("A5", new Formula("3 * 80"));
        spreadsheet.SetCellContents("A5", "Tester");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("Tester", (string)result);
    }


    // - Tests on deleting existing cell -
    [TestMethod]
    public void SpreadhseetSetCellContents_DeletingExistingDoubleCellOnSingleCellSheet_OverwritesToEmptyString() {
        Spreadsheet spreadsheet = CreateSingleCellSheet("A5", 5);
        spreadsheet.SetCellContents("A5", ""); // Should remove the cell from the backend data structure...

        object result = spreadsheet.GetCellContents("A5"); // but it should return a "" because that denotes an empty cell

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("", (string)result);
    }

    [TestMethod]
    public void SpreadhseetSetCellContents_DeletingExistingStringCellOnSingleCellSheet_OverwritesToEmptyString() {
        Spreadsheet spreadsheet = CreateSingleCellSheet("A5", "Tester");
        spreadsheet.SetCellContents("A5", ""); // Should remove the cell from the backend data structure...

        object result = spreadsheet.GetCellContents("A5"); // but it should return a "" because that denotes an empty cell

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("", (string)result);
    }

    [TestMethod]
    public void SpreadhseetSetCellContents_DeletingExistingFormulaCellOnSingleCellSheet_OverwritesToEmptyString() {
        Spreadsheet spreadsheet = CreateSingleCellSheet("A5", new Formula("2+2"));
        spreadsheet.SetCellContents("A5", ""); // Should remove the cell from the backend data structure...

        object result = spreadsheet.GetCellContents("A5"); // but it should return a "" because that denotes an empty cell

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("", (string)result);
    }




    // --- TESTS ON A SPREADSHEET INSTANCE WITH MULTIPLE CELLS ---
    // - Tests that throw a invalid name exception -
    [TestMethod]
    public void SpreadsheetSetCellContents_EmptyStringNameOnMultiCellSheet_InvalidNameException() {
        Spreadsheet spreadsheet = CreateMultiCellSheet();
        Assert.Throws<InvalidNameException>(() => spreadsheet.SetCellContents("", 5));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_InvalidCellNameOnMultiCellSheet_InvalidNameException() {
        Spreadsheet spreadsheet = CreateMultiCellSheet();
        Assert.Throws<InvalidNameException>(() => spreadsheet.SetCellContents("H", 5));
        Assert.Throws<InvalidNameException>(() => spreadsheet.SetCellContents("8", "Test")); 
        Assert.Throws<InvalidNameException>(() => spreadsheet.SetCellContents("h5", new Formula("2+2"))); 
    }


    // - Tests that throw a circular exception -
    [TestMethod]
    public void SpreadSheetSetCellContents_NewFormulaDependsOnADependeeFormula_CircularException() {
        Spreadsheet spreadsheet = CreateMultiCellSheet(); // Should contain the formula that depends on D6
        Assert.Throws<CircularException>(() => spreadsheet.SetCellContents("D6", new Formula("D9 - 28")));
    }

    [TestMethod]
    public void SpreadSheetSetCellContents_BigDependencyCircleWithFormulas_CircularException() {
        Spreadsheet spreadsheet = CreateMultiCellSheet();
        spreadsheet.SetCellContents("A1", new Formula("B2 + 56"));
        spreadsheet.SetCellContents("B2", new Formula("C3 + 2e3"));
        spreadsheet.SetCellContents("C3", new Formula("D4 * 6"));
        Assert.Throws<CircularException>(() => spreadsheet.SetCellContents("D4", new Formula("A1 - 9")));
    }

    [TestMethod]
    public void SpreadSheetSetCellContents_ImperfectDependencyCircleWithFormulas_CircularException() {
        Spreadsheet spreadsheet = CreateMultiCellSheet();
        spreadsheet.SetCellContents("A1", new Formula("B2 + 56"));
        spreadsheet.SetCellContents("B2", new Formula("C3 + 2e3"));
        spreadsheet.SetCellContents("C3", new Formula("D4 * 6"));
        spreadsheet.SetCellContents("D4", new Formula("E5 / 7"));
        Assert.Throws<CircularException>(() => spreadsheet.SetCellContents("E5", new Formula("C3 - 9")));
    }

    [TestMethod]
    public void SpreadSheetSetCellContents_IndirectDependencyCircleWithFormulas_CircularException() {
        Spreadsheet spreadsheet = CreateMultiCellSheet();
        spreadsheet.SetCellContents("A1", new Formula("B2 + 56"));
        spreadsheet.SetCellContents("B2", new Formula("C3 + 2e3"));
        spreadsheet.SetCellContents("C3", new Formula("F6 * 6 - D4"));
        spreadsheet.SetCellContents("D4", new Formula("E5 / 7"));
        Assert.Throws<CircularException>(() => spreadsheet.SetCellContents("E5", new Formula("C3 - 9")));
    }


    // - Tests on creating new cells -
    [TestMethod]
    public void SpreadsheetSetCellContents_NewDoubleCellOnMultiCellSheet_SuccessfullyAdds() {
        Spreadsheet spreadsheet = CreateMultiCellSheet();
        spreadsheet.SetCellContents("A5", 110.4);

        object result = spreadsheet.GetCellContents("A5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.IsTrue(110.4.Equals((double)result, 0.000001));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_NewStringCellOnMultiCellSheet_SuccessfullyAdds() {
        Spreadsheet spreadsheet = CreateMultiCellSheet();
        spreadsheet.SetCellContents("A5", "Test");

        object result = spreadsheet.GetCellContents("A5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("Test", (string)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_NewFormulaCellOnMultiCellSheet_SuccessfullyAdds() {
        Spreadsheet spreadsheet = CreateMultiCellSheet();
        spreadsheet.SetCellContents("A5", new Formula("80 + d3"));

        object result = spreadsheet.GetCellContents("A5");
        Formula expected = new("80+D3");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Formula>(result);
        Assert.AreEqual(expected, (Formula)result);
    }


    // - Tests on overwriting existing cell with the same type of value -
    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteDoubleCellSameValueOnMultiCellSheet_NoChange() {
        Spreadsheet spreadsheet = CreateMultiCellSheet("A5", 5);
        spreadsheet.SetCellContents("A5", 5);

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.IsTrue(5.0.Equals((double)result, 0.000001));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteDoubleCellWithDoubleOnMultiCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateMultiCellSheet("A5", 5);
        spreadsheet.SetCellContents("A5", 59);

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.IsTrue(59.0.Equals((double)result, 0.000001));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteStringCellWithStringOnMultiCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateMultiCellSheet("A5", "Hello");
        spreadsheet.SetCellContents("A5", "World");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("World", (string)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteFormulaCellWithFormulaOnMultiCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateMultiCellSheet("A5", new Formula("3 * 80"));
        spreadsheet.SetCellContents("A5", new Formula("2e2 / 5"));

        object result = spreadsheet.GetCellContents("A5");

        Formula expected = new("2e2/5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Formula>(result);
        Assert.AreEqual(expected, (Formula)result);
    }


    // - Tests on overwriting existing cell with different type of value -
    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteDoubleCellWithStringOnMultiCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateMultiCellSheet("A5", 5);
        spreadsheet.SetCellContents("A5", "Hello");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("Hello", (string)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteDoubleCellWithFormulaOnMultiCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateMultiCellSheet("A5", 90);
        spreadsheet.SetCellContents("A5", new Formula("6 / 2.78"));

        object result = spreadsheet.GetCellContents("A5");

        Formula expected = new("6/2.78");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Formula>(result);
        Assert.AreEqual(expected, (Formula)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteStringCellWithDoubleOnMultiCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateMultiCellSheet("A5", "Hello World");
        spreadsheet.SetCellContents("A5", 56.7);

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.IsTrue(56.7.Equals((double)result, 0.000001));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteStringCellWithFormulaOnMultiCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateMultiCellSheet("A5", "Test");
        spreadsheet.SetCellContents("A5", new Formula("2 * 6 + 5"));

        object result = spreadsheet.GetCellContents("A5");

        Formula expected = new("2*6+5");
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<Formula>(result);
        Assert.AreEqual(expected, (Formula)result);
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteFormulaCellWithDoubleOnMultiCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateMultiCellSheet("A5", new Formula("3 * 80"));
        spreadsheet.SetCellContents("A5", 80);

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<double>(result);
        Assert.IsTrue(80.0.Equals((double)result, 0.000001));
    }

    [TestMethod]
    public void SpreadsheetSetCellContents_OverwriteFormulaCellWithStringOnMultiCellSheet_SuccessfullyOverwrites() {
        Spreadsheet spreadsheet = CreateMultiCellSheet("A5", new Formula("3 * 80"));
        spreadsheet.SetCellContents("A5", "Tester");

        object result = spreadsheet.GetCellContents("A5");

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("Tester", (string)result);
    }


    // - Tests on deleting existing cell -
    [TestMethod]
    public void SpreadhseetSetCellContents_DeletingExistingDoubleCellOnMultiCellSheet_OverwritesToEmptyString() {
        Spreadsheet spreadsheet = CreateMultiCellSheet("A5", 5);
        spreadsheet.SetCellContents("A5", ""); // Should remove the cell from the backend data structure...

        object result = spreadsheet.GetCellContents("A5"); // but it should return a "" because that denotes an empty cell

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("", (string)result);
    }

    [TestMethod]
    public void SpreadhseetSetCellContents_DeletingExistingStringCellOnMultiCellSheet_OverwritesToEmptyString() {
        Spreadsheet spreadsheet = CreateMultiCellSheet("A5", "Tester");
        spreadsheet.SetCellContents("A5", ""); // Should remove the cell from the backend data structure...

        object result = spreadsheet.GetCellContents("A5"); // but it should return a "" because that denotes an empty cell

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("", (string)result);
    }

    [TestMethod]
    public void SpreadhseetSetCellContents_DeletingExistingFormulaCellOnMultiCellSheet_OverwritesToEmptyString() {
        Spreadsheet spreadsheet = CreateMultiCellSheet("A5", new Formula("2+2"));
        spreadsheet.SetCellContents("A5", ""); // Should remove the cell from the backend data structure...

        object result = spreadsheet.GetCellContents("A5"); // but it should return a "" because that denotes an empty cell

        Assert.IsNotNull(result);
        Assert.IsInstanceOfType<string>(result);
        Assert.AreEqual("", (string)result);
    }
}
