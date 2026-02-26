namespace Spreadsheet.Model;

public record SheetJSON (Dictionary<string, CellJSON> Cells);
public record CellJSON(string StringForm);
