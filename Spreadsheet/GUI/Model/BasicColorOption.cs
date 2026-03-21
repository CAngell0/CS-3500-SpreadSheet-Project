namespace GUI.Model;

/// <summary>
///     A basic enum containg color scheme options for this project.
///     Each component implements these color schemes differently according
///     to its structure.
/// </summary>
public enum BasicColorOption {
    White,
    Gray,
    Red,
    Blue
}

/// <summary>
///     I created this extension so that the enum can be mapped to class names for
///     CSS styling. This was useful because it made general color scheme settings for
///     components more readable and easier to implement.
/// </summary>
public static class BasicColorOptionExtension {
    /// <summary>
    ///     Converts the enum into a string form that can be used for class names.
    ///     It simply maps the literal enum values to the lowercase version of their names.
    ///     For example, .Blue -> "blue" and .Red -> "red".
    /// </summary>
    /// <param name="option"> Value to convert </param>
    /// <returns> String version of the enum </returns>
    public static string GetColorClass (this BasicColorOption option) {
        return option switch {
            BasicColorOption.Red => "red",
            BasicColorOption.Blue => "blue",
            BasicColorOption.White => "white",
            BasicColorOption.Gray => "gray",
            _ => "white"
        };
    }
}
