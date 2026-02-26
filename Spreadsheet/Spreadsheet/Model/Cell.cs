namespace Spreadsheet.Model;

public class Cell {
    public object Contents { get; set; }
    public object Value { get; set; }

    public Cell() {
        Contents = "";
        Value = "";
    }

    public Cell(object contents) {
        Contents = contents;
        Value = "";
    }
}
