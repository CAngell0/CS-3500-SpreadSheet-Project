namespace DependencyGraphTests;

using DependencyGraph;

/// <summary>
///   This is a test class for DependencyGraphTest and is intended
///   to contain all DependencyGraphTest Unit Tests
/// </summary>
[TestClass]
public class DependencyGraphTests {
    [TestMethod]
    public void DependencyGraphConstructor_EmptyGraph_SizeIsZero() {
        DependencyGraph graph = new();

        Assert.IsNotNull(graph);
        Assert.AreEqual(0, graph.Size);
    }

    // --- Tests For Single Dependency Pairs ---

    [TestMethod]
    public void DependencyGraphAdd_OneDependencyPair_SizeIsOne() {
        DependencyGraph graph = new();
        graph.AddDependency("A1", "B2");

        Assert.IsNotNull(graph);
        Assert.AreEqual(1, graph.Size);
        Assert.IsTrue(graph.HasDependents("A1"));
        Assert.IsTrue(graph.HasDependees("B2"));
    }

    [TestMethod]
    public void DependencyGraphRemove_OneDependencyPair_SizeIsZero() {
        DependencyGraph graph = new();
        graph.AddDependency("A1", "B2");
        graph.RemoveDependency("A1", "B2");

        Assert.IsNotNull(graph);
        Assert.AreEqual(0, graph.Size);
        Assert.IsFalse(graph.HasDependents("A1"));
        Assert.IsFalse(graph.HasDependees("B2"));
    }

    [TestMethod]
    public void DependencyGraphGetDependents_OneDependencyPair_CorrectDependent() {
        DependencyGraph graph = new();
        graph.AddDependency("A1", "B2");

        Assert.IsNotNull(graph);
        Assert.AreEqual(1, graph.Size);

        HashSet<string> correctDependents = ["B2"];
        HashSet<string> testedDependents = graph.GetDependents("A1").ToHashSet();

        Assert.IsNotNull(testedDependents);
        Assert.HasCount(correctDependents.Count, testedDependents);
        foreach (string dep in testedDependents) {
            Assert.Contains(dep, correctDependents);
        }
    }

    [TestMethod]
    public void DependencyGraphGetDependendees_OneDependencyPair_CorrectDependee() {
        DependencyGraph graph = new();
        graph.AddDependency("A1", "B2");

        Assert.IsNotNull(graph);
        Assert.AreEqual(1, graph.Size);

        List<string> correctDependees = ["A1"];
        List<string> testedDependees = graph.GetDependees("B2").ToList();

        Assert.IsNotNull(testedDependees);
        Assert.HasCount(correctDependees.Count, testedDependees);
        foreach (string dep in testedDependees) {
            Assert.Contains(dep, correctDependees);
        }
    }

    [TestMethod]
    public void DependencyGraphReplaceDependents_OneDependencyPair_SuccessfullyReplaced() {
        DependencyGraph graph = new();
        graph.AddDependency("A1", "B2");

        Assert.IsNotNull(graph);
        Assert.AreEqual(1, graph.Size);

        List<string> newDependents = ["C4"];
        graph.ReplaceDependents("A1", newDependents);
        List<string> testedDependents = graph.GetDependents("A1").ToList();

        Assert.IsNotNull(testedDependents);
        Assert.HasCount(newDependents.Count, testedDependents);
        Assert.IsFalse(graph.HasDependees("B2"));
        foreach (string dep in testedDependents) {
            Assert.Contains(dep, newDependents);
        }
    }

    [TestMethod]
    public void DependencyGraphReplaceDependees_OneDependencyPair_SuccessfullyReplaced() {
        DependencyGraph graph = new();
        graph.AddDependency("A1", "B2");

        Assert.IsNotNull(graph);
        Assert.AreEqual(1, graph.Size);

        List<string> newDependees = ["C4"];
        graph.ReplaceDependees("B2", newDependees);
        List<string> testedDependees = graph.GetDependees("B2").ToList();

        Assert.IsNotNull(testedDependees);
        Assert.HasCount(newDependees.Count, testedDependees);
        Assert.IsFalse(graph.HasDependents("A1"));
        foreach (string dep in testedDependees) {
            Assert.Contains(dep, newDependees);
        }
    }




    // --- Tests With Multiple Dependents and One Dependee ---
    
    [TestMethod]
    public void DependencyGraphAdd_OneDependeeWithMultipleDependents_SizeMatchesEdges() {
        DependencyGraph graph = new();
        graph.AddDependency("A1", "B2");
        graph.AddDependency("A1", "C3");
        graph.AddDependency("A1", "D4");

        Assert.IsNotNull(graph);
        Assert.AreEqual(3, graph.Size);
        Assert.IsTrue(graph.HasDependents("A1"));
        Assert.IsTrue(graph.HasDependees("B2"));
        Assert.IsTrue(graph.HasDependees("C3"));
        Assert.IsTrue(graph.HasDependees("D4"));
    }

    [TestMethod]
    public void DependencyGraphRemove_OneDependeeWithMultipleDependents_SizeMatchesEdges() {
        DependencyGraph graph = new();
        graph.AddDependency("A1", "B2");
        graph.AddDependency("A1", "C3");
        graph.AddDependency("A1", "D4");

        graph.RemoveDependency("A1", "B2");
        graph.RemoveDependency("A1", "D4");

        Assert.IsNotNull(graph);
        Assert.AreEqual(1, graph.Size);
        Assert.IsTrue(graph.HasDependents("A1"));
        Assert.IsTrue(graph.HasDependees("C3"));
        Assert.IsFalse(graph.HasDependees("B2"));
        Assert.IsFalse(graph.HasDependees("D4"));
    }


    /// <summary>
    ///   TODO:  Explain carefully what this code tests.
    ///          Also, update in-line comments as appropriate.
    /// </summary>
    [TestMethod]
    [Timeout(2000, CooperativeCancellation = true)]  // 2 second run time limit
    public void StressTest() {
        DependencyGraph dg = new();

        // A bunch of strings to use
        const int SIZE = 200;
        string[] letters = new string[SIZE];
        for (int i = 0; i < SIZE; i++) {
            letters[i] = string.Empty + ((char)('a' + i));
        }

        // The correct answers
        HashSet<string>[] dependents = new HashSet<string>[SIZE];
        HashSet<string>[] dependees = new HashSet<string>[SIZE];
        for (int i = 0; i < SIZE; i++) {
            dependents[i] = [];
            dependees[i] = [];
        }

        // Add a bunch of dependencies
        for (int i = 0; i < SIZE; i++) {
            for (int j = i + 1; j < SIZE; j++) {
                dg.AddDependency(letters[i], letters[j]);
                dependents[i].Add(letters[j]);
                dependees[j].Add(letters[i]);
            }
        }

        // Remove a bunch of dependencies
        for (int i = 0; i < SIZE; i++) {
            for (int j = i + 4; j < SIZE; j += 4) {
                dg.RemoveDependency(letters[i], letters[j]);
                dependents[i].Remove(letters[j]);
                dependees[j].Remove(letters[i]);
            }
        }

        // Add some back
        for (int i = 0; i < SIZE; i++) {
            for (int j = i + 1; j < SIZE; j += 2) {
                dg.AddDependency(letters[i], letters[j]);
                dependents[i].Add(letters[j]);
                dependees[j].Add(letters[i]);
            }
        }

        // Remove some more
        for (int i = 0; i < SIZE; i += 2) {
            for (int j = i + 3; j < SIZE; j += 3) {
                dg.RemoveDependency(letters[i], letters[j]);
                dependents[i].Remove(letters[j]);
                dependees[j].Remove(letters[i]);
            }
        }

        // Make sure everything is right
        for (int i = 0; i < SIZE; i++) {
            Assert.IsTrue(dependents[i].SetEquals(new HashSet<string>(dg.GetDependents(letters[i]))));
            Assert.IsTrue(dependees[i].SetEquals(new HashSet<string>(dg.GetDependees(letters[i]))));
        }
    }
}
