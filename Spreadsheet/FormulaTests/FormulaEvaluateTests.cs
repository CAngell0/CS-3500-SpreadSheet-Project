// <author> Carson ANgell </author>
// <date> 2/6/2026 </date>

using Formula;

namespace FormulaTests;

[TestClass]
public class FormulaEvaluateTests {
    [TestInitialize]
    public void DefineLookupMethod() {
        Lookup lookup = (variable) => {
            return variable switch {
                "A1" => 10,
                "B1" => 20,
                "C1" => 100,
                "D1" => 30.6,
                _ => throw new ArgumentException(),
            };
        };
    }
}
