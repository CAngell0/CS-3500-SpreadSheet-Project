// <copyright file="Spreadsheet.cs" company="UofU-CS3500">
// Copyright (c) 2024 UofU-CS3500. All rights reserved.
// </copyright>

// Written by Joe Zachary for CS 3500, September 2013
// Update by Profs Kopta and de St. Germain, Fall 2021, Fall 2024
//     - Updated return types
//     - Updated documentation

// <author> Carson Angell </author>
// <date> 2/9/2026 </date>
namespace Spreadsheet;

using Formula;
using DependencyGraph;

using System.Text.RegularExpressions;
using System.Text.Json;

/// <summary>
///   <para>
///     Thrown to indicate that a change to a cell will cause a circular dependency.
///   </para>
/// </summary>
public class CircularException : Exception {
}

/// <summary>
///   <para>
///     Thrown to indicate that a name parameter was invalid.
///   </para>
/// </summary>
public class InvalidNameException : Exception {
}

/// <summary>
///   <para>
///     An Spreadsheet object represents the state of a simple spreadsheet.  A
///     spreadsheet represents an infinite number of named cells.
///   </para>
/// <para>
///     Valid Cell Names: A string is a valid cell name if and only if it is one or
///     more letters followed by one or more numbers, e.g., A5, BC27.
/// </para>
/// <para>
///    Cell names are case insensitive, so "x1" and "X1" are the same cell name.
///    Your code should normalize (uppercased) any stored name but accept either.
/// </para>
/// <para>
///     A spreadsheet represents a cell corresponding to every possible cell name.  (This
///     means that a spreadsheet contains an infinite number of cells.)  In addition to
///     a name, each cell has a contents and a value.  The distinction is important.
/// </para>
/// <para>
///     The <b>contents</b> of a cell can be (1) a string, (2) a double, or (3) a Formula.
///     If the contents of a cell is set to the empty string, the cell is considered empty.
/// </para>
/// <para>
///     By analogy, the contents of a cell in Excel is what is displayed on
///     the editing line when the cell is selected.
/// </para>
/// <para>
///     In a new spreadsheet, the contents of every cell is the empty string. Note:
///     this is by definition (it is IMPLIED, not stored).
/// </para>
/// <para>
///     The <b>value</b> of a cell can be (1) a string, (2) a double, or (3) a FormulaError.
///     (By analogy, the value of an Excel cell is what is displayed in that cell's position
///     in the grid.) We are not concerned with cell values yet, only with their contents,
///     but for context:
/// </para>
/// <list type="number">
///   <item>If a cell's contents is a string, its value is that string.</item>
///   <item>If a cell's contents is a double, its value is that double.</item>
///   <item>
///     <para>
///       If a cell's contents is a Formula, its value is either a double or a FormulaError,
///       as reported by the Evaluate method of the Formula class.  For this assignment,
///       you are not dealing with values yet.
///     </para>
///   </item>
/// </list>
/// <para>
///     Spreadsheets are never allowed to contain a combination of Formulas that establish
///     a circular dependency.  A circular dependency exists when a cell depends on itself,
///     either directly or indirectly.
///     For example, suppose that A1 contains B1*2, B1 contains C1*2, and C1 contains A1*2.
///     A1 depends on B1, which depends on C1, which depends on A1.  That's a circular
///     dependency.
/// </para>
/// </summary>
public class Spreadsheet {
    /// <summary> Stores cells with content inside them. Uses the Cell wrapper class for the values, and cell names for the keys. </summary>
    private readonly Dictionary<string, Cell> _cells;
    /// <summary> Dependency graph to manage formula dependencies in the spreadsheet </summary>
    private readonly DependencyGraph _dependencyGraph;
    /// <summary> Variable Regex pattern that matches to strings with only one cannonical variable (ie. "A1", "BC23", etc.) </summary>
    private const string VariableRegExPattern = @"^[A-Z]+\d+$";
    /// <summary> Regex object that corresponds to the regex pattern above. </summary>
    private readonly Regex _variableRegex;

    /// <summary>
    /// True if this spreadsheet has been changed since it was
    /// created or saved (whichever happened most recently),
    /// False otherwise.
    /// </summary>
    public bool Changed { get; private set; }

    public Spreadsheet() {
        _cells = new Dictionary<string, Cell>();
        _dependencyGraph = new DependencyGraph();
        _variableRegex = new Regex(VariableRegExPattern);
    }

    /// <summary>
    /// Constructs a spreadsheet using the saved data in the file referred to by
    /// the given filename.
    /// <see cref="Save(string)"/>
    /// </summary>
    /// <exception cref="SpreadsheetReadWriteException">
    ///   Thrown if the file can not be loaded into a spreadsheet for any reason
    /// </exception>
    /// <param name="filename">The path to the file containing the spreadsheet to load</param>
    public Spreadsheet(string filename) { //TODO - Implement this method
        throw new NotImplementedException();
    }

    /// <summary>
    ///   Provides a copy of the normalized names of all of the cells in the spreadsheet
    ///   that contain information (i.e., non-empty cells).
    /// </summary>
    /// <returns>
    ///   A set of the names of all the non-empty cells in the spreadsheet.
    /// </returns>
    public ISet<string> GetNamesOfAllNonemptyCells() {
        return _cells.Keys.ToHashSet();
    }

    /// <summary>
    ///   Returns the contents (as opposed to the value) of the named cell.
    /// </summary>
    ///
    /// <exception cref="InvalidNameException">
    ///   Thrown if the name is invalid.
    /// </exception>
    ///
    /// <param name="name">The name of the spreadsheet cell to query. </param>
    /// <returns>
    ///   The contents as either a string, a double, or a Formula.
    ///   See the class header summary.
    /// </returns>
    public object GetCellContents(string name) {
        if (!_variableRegex.IsMatch(name)) throw new InvalidNameException();

        if (_cells.TryGetValue(name, out Cell? cell)) return cell.Contents;
        else return "";
    }

    /// <summary>
    ///   <para>
    ///     Return the value of the named cell.
    ///   </para>
    /// </summary>
    /// <param name="name"> The cell in question. </param>
    /// <returns>
    ///   Returns the value (as opposed to the contents) of the named cell.  The return
    ///   value should be either a string, a double, or a CS3500.Formula.FormulaError.
    /// </returns>
    /// <exception cref="InvalidNameException">
    ///   If the provided name is invalid, throws an InvalidNameException.
    /// </exception>
    public object GetCellValue(string name) { //TODO - Implement this method
        throw new NotImplementedException();
    }

    /// <summary>
    ///   <para>
    ///     Set the contents of the named cell to be the provided string
    ///     which will either represent (1) a string, (2) a number, or
    ///     (3) a formula (based on the prepended '=' character).
    ///   </para>
    ///   <para>
    ///     Rules of parsing the input string:
    ///   </para>
    ///   <list type="bullet">
    ///     <item>
    ///       <para>
    ///         If 'content' parses as a double, the contents of the named
    ///         cell becomes that double.
    ///       </para>
    ///     </item>
    ///     <item>
    ///         If the string does not begin with an '=', the contents of the
    ///         named cell becomes 'content'.
    ///     </item>
    ///     <item>
    ///       <para>
    ///         If 'content' begins with the character '=', an attempt is made
    ///         to parse the remainder of content into a Formula f using the Formula
    ///         constructor.  There are then three possibilities:
    ///       </para>
    ///       <list type="number">
    ///         <item>
    ///           If the remainder of content cannot be parsed into a Formula, a
    ///           CS3500.Formula.FormulaFormatException is thrown.
    ///         </item>
    ///         <item>
    ///           Otherwise, if changing the contents of the named cell to be f
    ///           would cause a circular dependency, a CircularException is thrown,
    ///           and no change is made to the spreadsheet.
    ///         </item>
    ///         <item>
    ///           Otherwise, the contents of the named cell becomes f.
    ///         </item>
    ///       </list>
    ///     </item>
    ///   </list>
    /// </summary>
    /// <returns>
    ///   <para>
    ///     The method returns a list consisting of the name plus the names
    ///     of all other cells whose value depends, directly or indirectly,
    ///     on the named cell. The order of the list should be any order
    ///     such that if cells are re-evaluated in that order, their dependencies
    ///     are satisfied by the time they are evaluated.
    ///   </para>
    ///   <example>
    ///     For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
    ///     list {A1, B1, C1} is returned.
    ///   </example>
    /// </returns>
    /// <exception cref="InvalidNameException">
    ///     If name is invalid, throws an InvalidNameException.
    /// </exception>
    /// <exception cref="CircularException">
    ///     If a formula would result in a circular dependency, throws CircularException.
    /// </exception>
    public IList<string> SetContentsOfCell(string name, string content) { //TODO - Implement this method
        throw new NotImplementedException();
    }

    /// <summary> Set the contents of the named cell to the given number. </summary>
    /// <exception cref="InvalidNameException"> If the name is invalid, throw an InvalidNameException. </exception>
    /// <param name="name"> The name of the cell. </param>
    /// <param name="number"> The new contents of the cell. </param>
    /// <returns>
    ///   <para>
    ///     This method returns an ordered list consisting of the passed in name
    ///     followed by the names of all other cells whose value depends, directly
    ///     or indirectly, on the named cell.
    ///   </para>
    ///   <para>
    ///     The order must correspond to a valid dependency ordering for recomputing
    ///     all of the cells, i.e., if you re-evaluate each cells in the order of the list,
    ///     the overall spreadsheet will be correctly updated.
    ///   </para>
    /// </returns>
    private IList<string> SetCellContents(string name, double number) {
        if (!_variableRegex.IsMatch(name)) throw new InvalidNameException();

        // Adds the cell if it wasn't already there
        if (!_cells.TryGetValue(name, out Cell? cell)) _cells.Add(name, new Cell(number));

        // Sets the cells contents, updates dependees
        else {
            if (cell.Contents is Formula) _dependencyGraph.ReplaceDependees(name, []);
            cell.Contents = number;
        }

        return GetCellsToRecalculate(name).ToList();
    }

    /// <summary> The contents of the named cell becomes the given text. </summary>
    /// <exception cref="InvalidNameException"> If the name is invalid, throw an InvalidNameException. </exception>
    /// <param name="name"> The name of the cell. </param>
    /// <param name="text"> The new contents of the cell. </param>
    /// <returns> The same list as defined in <see cref="SetCellContents(string, double)"/>. </returns>
    private IList<string> SetCellContents(string name, string text) {
        if (!_variableRegex.IsMatch(name)) throw new InvalidNameException();

        // Adds the cell if it wasn't already there
        if (!_cells.TryGetValue(name, out Cell? cell)) _cells.Add(name, new Cell(text));
        // Sets the cells contents, updates dependees and removes the cell if it was set to an empty string
        else {
            if (cell.Contents is Formula) _dependencyGraph.ReplaceDependees(name, []);
            if (text.Equals("")) _cells.Remove(name);
            else cell.Contents = text;
        }

        return GetCellsToRecalculate(name).ToList();
    }

    /// <summary>
    ///   Set the contents of the named cell to the given formula.
    /// </summary>
    /// <exception cref="InvalidNameException">
    ///   If the name is invalid, throw an InvalidNameException.
    /// </exception>
    /// <exception cref="CircularException">
    ///   <para>
    ///     If changing the contents of the named cell to be the formula would
    ///     cause a circular dependency, throw a CircularException, and no
    ///     change is made to the spreadsheet.
    ///   </para>
    /// </exception>
    /// <param name="name"> The name of the cell. </param>
    /// <param name="formula"> The new contents of the cell. </param>
    /// <returns>
    ///   The same list as defined in <see cref="SetCellContents(string, double)"/>.
    /// </returns>
    private IList<string> SetCellContents(string name, Formula formula) {
        if (!_variableRegex.IsMatch(name)) throw new InvalidNameException();

        // Perfroms a BFS traversal (same as the GetCellsToRecalculate method)
        // I needed the visited set in order to check for circular dependencies. That's why I couldn't use GetCellsToRecalculate.
        LinkedList<string> changed = [];
        HashSet<string> visited = [];
        Visit(name, name, visited, changed);

        // Checks if the formula depends on any of the visited cells (checks for a circular dependency)
        ISet<string> formulaDependees = formula.GetVariables();
        foreach (string dependee in formulaDependees) if (visited.Contains(dependee)) throw new CircularException();

        // Sets the cell's contents in the dictionary
        if (_cells.TryGetValue(name, out Cell? cell)) cell.Contents = formula;
        else _cells.Add(name, new Cell(formula));

        // Replaces the dependees of the cell in the dependency graph
        _dependencyGraph.ReplaceDependees(name, formulaDependees);

        return changed.ToList();
    }

    /// <summary>
    ///   Returns an enumeration, without duplicates, of the names of all cells whose
    ///   values depend directly on the value of the named cell.
    /// </summary>
    /// <param name="name"> This <b>MUST</b> be a valid name.  </param>
    /// <returns>
    ///   <para>
    ///     Returns an enumeration, without duplicates, of the names of all cells
    ///     that contain formulas containing name.
    ///   </para>
    ///   <para>For example, suppose that: </para>
    ///   <list type="bullet">
    ///      <item>A1 contains 3</item>
    ///      <item>B1 contains the formula A1 * A1</item>
    ///      <item>C1 contains the formula B1 + A1</item>
    ///      <item>D1 contains the formula B1 - C1</item>
    ///   </list>
    ///   <para> The direct dependents of A1 are B1 and C1. </para>
    /// </returns>
    private IEnumerable<string> GetDirectDependents(string name) {
        return _dependencyGraph.GetDependents(name);
    }

    /// <summary>
    ///   <para>
    ///     Returns an enumeration of the names of all cells whose values must
    ///     be recalculated, assuming that the contents of the cell referred
    ///     to by name has changed.  The cell names are enumerated in an order
    ///     in which the calculations should be done.
    ///   </para>
    ///   <exception cref="CircularException">
    ///     If the cell referred to by name is involved in a circular dependency,
    ///     throws a CircularException.
    ///   </exception>
    ///   <para>
    ///     For example, suppose that:
    ///   </para>
    ///   <list type="number">
    ///     <item> A1 contains 5 </item>
    ///     <item> B1 contains the formula A1 + 2. </item>
    ///     <item> C1 contains the formula A1 + B1. </item>
    ///     <item> D1 contains the formula A1 * 7. </item>
    ///     <item> E1 contains 15 </item>
    ///   </list>
    ///   <para>
    ///     If A1 has changed, then A1, B1, C1, and D1 must be recalculated,
    ///     and they must be recalculated in an order which has A1 first, and B1 before C1
    ///     (there are multiple such valid orders).
    ///     The method will produce one of those enumerations.
    ///   </para>
    /// </summary>
    /// <param name="name"> The name of the cell.  Requires that name be a valid cell name.</param>
    /// <returns>
    ///    Returns an enumeration of the names of all cells whose values must
    ///    be recalculated.
    /// </returns>
    private IEnumerable<string> GetCellsToRecalculate(string name) {
        LinkedList<string> changed = new();
        HashSet<string> visited = [];
        Visit(name, name, visited, changed);
        return changed;
    }

    /// <summary>
    ///     A helper for the GetCellsToRecalculate method and SetCellContents method. Visits all the cells that are dependents of the cell specified in the 'name' parameter and 
    ///     puts them in the 'changed' linked list paremeter. Some other thing to node:
    ///     <list type="bullet">
    ///         <item> Items in the 'changed' linked list are in order of what ones would need to be updated first if the root cell was changed </item>
    ///         <item> Performs a breath-first-search (BFS) traversal in order to get the correct ordering of cells. </item>
    ///         <item> The 'visited' holds what cells were visited during its search. </item>
    ///     </list> 
    ///     <exception cref="CircularException">
    ///         If the cell referred to by name is involved in a circular dependency, throws a CircularException.
    ///     </exception>
    /// </summary>
    /// <param name="start"> The cell you want to start searching from </param>
    /// <param name="name"> Should be the same as start </param>
    /// <param name="visited"> Set used to store what cells have been visited </param>
    /// <param name="changed"> LinkedList used to store the cells in dependency-order </param>
    private void Visit(string start, string name, ISet<string> visited, LinkedList<string> changed) {
        visited.Add(name);

        // Performs a BFS traversal starting from the cell provided in the params
        foreach (string n in GetDirectDependents(name)) {
            if (n.Equals(start)) { //!
                throw new CircularException();
            }
            else if (!visited.Contains(n)) {
                Visit(start, n, visited, changed);
            }
        }

        changed.AddFirst(name);
    }

    /// <summary>
    /// Saves this spreadsheet to a file
    /// </summary>
    /// <param name="filename"> The name (with path) of the file to save to.</param>
    /// <exception cref="SpreadsheetReadWriteException">
    ///   If there are any problems opening, writing, or closing the file,
    ///   the method should throw a SpreadsheetReadWriteException with an
    ///   explanatory message.
    /// </exception>
    public void Save(string filename) { //TODO - Implement this method
        throw new NotImplementedException();
    }


    /// <summary>
    ///   <para>
    ///     Return the value of the named cell, as defined by
    ///     <see cref="GetCellValue(string)"/>.
    ///   </para>
    /// </summary>
    /// <param name="name"> The cell in question. </param>
    /// <returns>
    ///   <see cref="GetCellValue(string)"/>
    /// </returns>
    /// <exception cref="InvalidNameException">
    ///   If the provided name is invalid, throws an InvalidNameException.
    /// </exception>
    public object this[string name] { //TODO - Implement this method
        get { throw new NotImplementedException(); }
    }


    /// <summary>
    ///     Stored in the spreadsheet dictionaries as its keys. Represents a singular cell
    ///     with contents in it.
    /// </summary>
    private class Cell {
        public object Contents { get; set; }
        public Cell(object contents) {
            Contents = contents;
        }
    }
}

/// <summary>
/// <para>
///   Thrown to indicate that a read or write attempt has failed with
///   an expected error message informing the user of what went wrong.
/// </para>
/// </summary>
public class SpreadsheetReadWriteException : Exception {
    /// <summary>
    ///   <para>
    ///     Creates the exception with a message defining what went wrong.
    ///   </para>
    /// </summary>
    /// <param name="msg"> An informative message to the user. </param>
    public SpreadsheetReadWriteException(string msg) : base(msg) { }
}
