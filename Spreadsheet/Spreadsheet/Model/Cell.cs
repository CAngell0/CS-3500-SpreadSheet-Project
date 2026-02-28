// <author> Carson Angell </author>
// <date> 2/26/2026 </date>

namespace Spreadsheet.Model;

/// <summary>
///     Wrapper class to store contents and values of cells inside the spreadsheet
/// </summary>
/// <param name="contents"> Contents of the spreadsheet, can be either a double, string, or Formula </param>
public class Cell(object contents) {
    /// <summary> The contents of the formula. Can be either a double, string or Formula </summary>
    public object Contents { get; set; } = contents;
    /// <summary> Evaluated value of the cell. Can either be a double, string or FormulaError </summary>
    public object Value { get; set; } = "";
}
