// <author> Carson Angell </author>
// <date> 2/26/2026 </date>

namespace Spreadsheet.Model;

/// <summary>
///     Used to parse the JSON content of loaded spreadsheet files. Outlines the most shallow
///     part of the JSON; the "Cells" property.
/// </summary>
/// <param name="Cells"> Dictionary of cell names as keys and CellJSON data as values </param>
public record SheetJSON (Dictionary<string, CellJSON> Cells);

/// <summary>
///     Used to parse the JSON content of loaded spreadsheet files. Outlines the individual
///     cell data entries.
/// </summary>
/// <param name="StringForm"> The string form of the formula </param>
public record CellJSON(string StringForm);
