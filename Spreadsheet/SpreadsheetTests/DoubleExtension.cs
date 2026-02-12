// <author> Carson Angell </author>
// <date> 2/11/2026 </date>

namespace SpreadsheetTests;

public static class DoubleExtension {
    /// <summary>
    ///     Compares the current double with the target double to see if they are equal.
    ///     <remarks><para>
    ///         Compares the two doubles with traditional range comparing. It subtracts the doubles
    ///         from each other and returns whether the difference is within a specified range.
    ///         Follows this formula to compare the doubles: <code>Math.Abs(num - self) &lt; range;</code>
    ///     </para></remarks>
    /// </summary>
    /// <param name="self"> Current double </param>
    /// <param name="num"> Double to compare to </param>
    /// <param name="range"> Range to check against </param>
    /// <returns> true if the doubles are the same, false if not </returns>
    public static bool Equals(this double self, double num, double range){
        return Math.Abs(num - self) < range;
    }
}
