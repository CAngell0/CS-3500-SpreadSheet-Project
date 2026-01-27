//TODO - Remove this file when finished
namespace DependencyGraphTests;

[TestClass]
public class DevTests {
    [TestMethod]
    public void TestMethod1() {
        HashSet<string> set1 = ["a", "b", "c", "d"];
        HashSet<string> set2 = ["b", "c", "e"];
        string[] result = set1.Except(set2).ToArray();

        foreach (string res in result) Console.WriteLine(res);
    }
}
