// <author> Carson Angell </author>
// <date> 1/26/2026 </date>

namespace DependencyGraphTests;

using DependencyGraph;

/// <summary>
///   This is a test class for DependencyGraphTest and is intended
///   to contain all DependencyGraphTest Unit Tests
/// </summary>
[TestClass]
public class DependencyGraphTests {
    /// <summary>
    ///     Helper method that creates a dependency graph using dependency pairs.
    /// </summary>
    /// <param name="dependencies">
    ///     The inner string arrays should containing dependee-dependent pairs.
    ///     Same as though you call DependencyGraph.AddDependency(dependee, dependent).
    /// </param>
    /// <returns> Dependency graph with the pairs added </returns>
    private DependencyGraph CreateDependencyGraph(string[][] dependencies) {
        DependencyGraph graph = new();
        foreach (string[] dependency in dependencies) graph.AddDependency(dependency[0], dependency[1]);
        return graph;
    }



    

    // --- Tests With Empty Graph ---

    [TestMethod]
    public void DependencyGraphConstructor_EmptyGraph_SizeIsZero() {
        DependencyGraph graph = new();

        Assert.IsNotNull(graph);
        Assert.AreEqual(0, graph.Size);
    }

    [TestMethod]
    public void DependencyGraphHasDependents_EmptyGraph_False() {
        DependencyGraph graph = new();

        Assert.IsNotNull(graph);
        Assert.IsFalse(graph.HasDependents("A1"));
    }

    [TestMethod]
    public void DependencyGraphHasDependees_EmptyGraph_False() {
        DependencyGraph graph = new();

        Assert.IsNotNull(graph);
        Assert.IsFalse(graph.HasDependees("A1"));
    }

    [TestMethod]
    public void DependencyGraphRemove_EmptyGraph_NoChange() {
        DependencyGraph graph = new();
        graph.RemoveDependency("A1", "B2");

        Assert.IsNotNull(graph);
        Assert.AreEqual(0, graph.Size);
    }

    [TestMethod]
    public void DependencyGraphGetDependents_EmptyGraph_EmptyList() {
        DependencyGraph graph = new();
        List<string> dependents = graph.GetDependents("A1").ToList();

        Assert.IsNotNull(dependents);
        Assert.IsEmpty(dependents);
    }

    [TestMethod]
    public void DependencyGraphGetDependees_EmptyGraph_EmptyList() {
        DependencyGraph graph = new();
        List<string> dependees = graph.GetDependents("B2").ToList();

        Assert.IsNotNull(dependees);
        Assert.IsEmpty(dependees);
    }

    [TestMethod]
    public void DependencyGraphReplaceDependents_EmptyGraph_DependenciesAdded() {
        DependencyGraph graph = new();
        graph.ReplaceDependents("A1", ["B2"]);

        Assert.AreEqual(1, graph.Size);
        Assert.IsTrue(graph.HasDependents("A1"));
        Assert.IsTrue(graph.HasDependees("B2"));
    }

    [TestMethod]
    public void DependencyGraphReplaceDependees_EmptyGraph_DependenciesAdded() {
        DependencyGraph graph = new();
        graph.ReplaceDependees("A1", ["B2"]);

        Assert.AreEqual(1, graph.Size);
        Assert.IsTrue(graph.HasDependents("B2"));
        Assert.IsTrue(graph.HasDependees("A1"));
    }




    // --- Tests For Single Dependency Pairs ---

    [TestMethod]
    public void DependencyGraphAdd_OneDependencyPair_SizeIsOne() {
        DependencyGraph graph = CreateDependencyGraph([["A1", "B2"]]);

        Assert.IsNotNull(graph);
        Assert.AreEqual(1, graph.Size);
        Assert.IsTrue(graph.HasDependents("A1"));
        Assert.IsTrue(graph.HasDependees("B2"));
    }

    [TestMethod]
    public void DependencyGraphRemove_OneDependencyPair_SizeIsZero() {
        DependencyGraph graph = CreateDependencyGraph([["A1", "B2"]]);
        graph.RemoveDependency("A1", "B2");

        Assert.IsNotNull(graph);
        Assert.AreEqual(0, graph.Size);
        Assert.IsFalse(graph.HasDependents("A1"));
        Assert.IsFalse(graph.HasDependees("B2"));
    }

    [TestMethod]
    public void DependencyGraphGetDependents_OneDependencyPair_CorrectDependent() {
        DependencyGraph graph = CreateDependencyGraph([["A1", "B2"]]);
        List<string> testedDependents = graph.GetDependents("A1").ToList();

        Assert.IsNotNull(testedDependents);
        Assert.HasCount(1, testedDependents);
        Assert.Contains("B2", testedDependents);
    }

    [TestMethod]
    public void DependencyGraphGetDependees_OneDependencyPair_CorrectDependee() {
        DependencyGraph graph = CreateDependencyGraph([["A1", "B2"]]);
        List<string> testedDependees = graph.GetDependees("B2").ToList();

        Assert.IsNotNull(testedDependees);
        Assert.HasCount(1, testedDependees);
        Assert.Contains("A1", testedDependees);
    }

    [TestMethod]
    public void DependencyGraphReplaceDependents_OneDependencyPair_SuccessfullyReplaced() {
        DependencyGraph graph = CreateDependencyGraph([["A1", "B2"]]);

        graph.ReplaceDependents("A1", ["C4"]);
        List<string> testedDependents = graph.GetDependents("A1").ToList();

        Assert.IsNotNull(testedDependents);
        Assert.HasCount(1, testedDependents);
        Assert.IsFalse(graph.HasDependees("B2"));
        Assert.Contains("C4", testedDependents);
    }

    [TestMethod]
    public void DependencyGraphReplaceDependees_OneDependencyPair_SuccessfullyReplaced() {
        DependencyGraph graph = CreateDependencyGraph([["A1", "B2"]]);

        List<string> newDependees = ["C4"];
        graph.ReplaceDependees("B2", newDependees);
        List<string> testedDependees = graph.GetDependees("B2").ToList();

        Assert.IsNotNull(testedDependees);
        Assert.HasCount(newDependees.Count, testedDependees);
        Assert.IsFalse(graph.HasDependents("A1"));
        Assert.Contains("C4", testedDependees);
    }

    [TestMethod]
    public void DependencyGraphReplaceDependees_OneDependencyPairReplaceWithEmptyDependees_EmptyGraph() {
        DependencyGraph graph = CreateDependencyGraph([ ["A1", "B2"] ]);
        graph.ReplaceDependees("B2", []);

        Assert.AreEqual(0, graph.Size);
    }




    // --- Tests With Multiple Dependents and One Dependee ---
    
    [TestMethod]
    public void DependencyGraphAdd_OneDependeeWithMultipleDependents_SizeMatchesEdges() {
        DependencyGraph graph = CreateDependencyGraph([ ["A1", "B2"], ["A1", "C3"], ["A1", "D4"] ]);

        Assert.IsNotNull(graph);
        Assert.AreEqual(3, graph.Size);
        Assert.IsTrue(graph.HasDependents("A1"));
        Assert.IsTrue(graph.HasDependees("B2"));
        Assert.IsTrue(graph.HasDependees("C3"));
        Assert.IsTrue(graph.HasDependees("D4"));
    }

    [TestMethod]
    public void DependencyGraphRemove_OneDependeeWithMultipleDependents_SizeMatchesEdges() {
        DependencyGraph graph = CreateDependencyGraph([ ["A1", "B2"], ["A1", "C3"], ["A1", "D4"] ]);

        graph.RemoveDependency("A1", "B2");
        graph.RemoveDependency("A1", "D4");

        Assert.IsNotNull(graph);
        Assert.AreEqual(1, graph.Size);
        Assert.IsTrue(graph.HasDependents("A1"));
        Assert.IsTrue(graph.HasDependees("C3"));
        Assert.IsFalse(graph.HasDependees("B2"));
        Assert.IsFalse(graph.HasDependees("D4"));
    }

    [TestMethod]
    public void DependencyGraphGetDependents_OneDependeeWithMultipleDependents_ThreeDependents() {
        DependencyGraph graph = CreateDependencyGraph([ ["A1", "B2"], ["A1", "C3"], ["A1", "D4"] ]);

        List<string> dependents = graph.GetDependents("A1").ToList();
        
        Assert.IsNotNull(dependents);
        Assert.HasCount(3, dependents);
        Assert.Contains("B2", dependents);
        Assert.Contains("C3", dependents);
        Assert.Contains("D4", dependents);
    }

    [TestMethod]
    public void DependencyGraphGetDependees_OneDependeeWithMultipleDependents_OneDependee() {
        DependencyGraph graph = CreateDependencyGraph([ ["A1", "B2"], ["A1", "C3"], ["A1", "D4"] ]);

        List<string>[] dependeeGroups = [
            graph.GetDependees("B2").ToList(),
            graph.GetDependees("C3").ToList(),
            graph.GetDependees("D4").ToList()
        ];

        foreach (List<string> dependeeGroup in dependeeGroups) {
            Assert.IsNotNull(dependeeGroup);
            Assert.HasCount(1, dependeeGroup);
            Assert.Contains("A1", dependeeGroup);
        }
    }

    /// <summary>
    ///     Takes a graph with one dependee and three dependents and replaces the dependents
    ///     with a completely new set of dependents. Without intersection means that the dependents
    ///     used to replace don't have any variables in common with the old dependents; they are
    ///     completely new.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependents_OneDependeeWithMultipleDependentsWithoutIntersection_AllNewDependents() {
        DependencyGraph graph = CreateDependencyGraph([ ["A1", "B2"], ["A1", "C3"], ["A1", "D4"] ]);
        List<string> newDependents = ["E5", "F6", "G7"];
        List<string> oldDependents = ["B2", "C3", "D4"];

        graph.ReplaceDependents("A1", newDependents);
        List<string> testedDependents = graph.GetDependents("A1").ToList();

        Assert.IsNotNull(testedDependents);
        Assert.HasCount(3, testedDependents);

        // Checks the new dependents
        foreach (string newDep in newDependents) {
            // Make sure they are listed as a dependent
            Assert.Contains(newDep, testedDependents);
            
            // Checks to make sure the new dependents have only A1 has a dependee
            List<string> dependeesOfNewDep = graph.GetDependees(newDep).ToList();
            Assert.IsNotNull(dependeesOfNewDep);
            Assert.HasCount(1, dependeesOfNewDep);
            Assert.Contains("A1", dependeesOfNewDep);
        }

        //Checks the old dependents
        foreach (string oldDep in oldDependents) {
            // Makes sure they are not listed as a dependent
            Assert.DoesNotContain(oldDep, testedDependents);
            // Checks to make sure the old dependents don't have any dependees
            Assert.IsEmpty(graph.GetDependees(oldDep).ToList());
        }
    }

    /// <summary>
    ///     Same as last test. With intersection means that the dependents
    ///     used to replace do have some variables in common with the old dependents.
    ///     But there are still some new/different ones mixed into the "replaces".
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependents_OneDependeeWithMultipleDependentsWithIntersection_TwoDeletionsOneAddition() {
        DependencyGraph graph = CreateDependencyGraph([ ["A1", "B2"], ["A1", "C3"], ["A1", "D4"], ["A1", "E5"] ]);
        List<string> newDependents = ["C3", "D4", "F5"];
        List<string> oldDependents = ["B2", "C3", "D4", "E5"];    

        graph.ReplaceDependents("A1", newDependents);
        List<string> testedDependents = graph.GetDependents("A1").ToList();
        Assert.IsNotNull(testedDependents);
        Assert.HasCount(3, testedDependents);

        // Checks the new dependents
        foreach (string newDep in newDependents) {
            // Make sure they are listed as a dependent
            Assert.Contains(newDep, testedDependents);
            
            // Checks to make sure the new dependents have only A1 has a dependee
            List<string> dependeesOfNewDep = graph.GetDependees(newDep).ToList();
            Assert.IsNotNull(dependeesOfNewDep);
            Assert.HasCount(1, dependeesOfNewDep);
            Assert.Contains("A1", dependeesOfNewDep);
        }

        List<string> deletedDeps = oldDependents.Except(newDependents).ToList();
        //Checks the old dependents
        foreach (string delDep in deletedDeps) {
            // Makes sure they are not listed as a dependent
            Assert.DoesNotContain(delDep, testedDependents);

            // Checks to make sure the old dependents don't have any dependees
            Assert.IsEmpty(graph.GetDependees(delDep).ToList());
        }
    }

    /// <summary>
    ///     Same as last test. But this time the dependents used to replace are the exact
    ///     same as the old ones. No new dependents will be added and no old ones will be removed.
    ///     There should not be any change to the graph.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependents_OneDependeeWithMultipleDependentsFullIntersection_NoChange() {
        DependencyGraph graph = CreateDependencyGraph([ ["A1", "B2"], ["A1", "C3"], ["A1", "D4"] ]);
        List<string> newDependents = ["B2", "C3", "D4"];
        List<string> oldDependents = ["B2", "C3", "D4"];

        graph.ReplaceDependents("A1", newDependents);
        List<string> testedDependents = graph.GetDependents("A1").ToList();
        Assert.IsNotNull(testedDependents);
        Assert.HasCount(3, testedDependents);

        // Checks the new dependents
        foreach (string newDep in newDependents) {
            // Make sure they are listed as a dependent
            Assert.Contains(newDep, testedDependents);
            
            // Checks to make sure the new dependents have only A1 has a dependee
            List<string> dependeesOfNewDep = graph.GetDependees(newDep).ToList();
            Assert.IsNotNull(dependeesOfNewDep);
            Assert.HasCount(1, dependeesOfNewDep);
            Assert.Contains("A1", dependeesOfNewDep);
        }
    }

    [TestMethod]
    public void DependencyGraphReplaceDependents_ReplaceMultipleDependentsWithEmptyDependents_EmptyGraph() {
        DependencyGraph graph = CreateDependencyGraph([ ["A1", "B2"], ["A1", "C3"], ["A1", "D4"] ]);
        graph.ReplaceDependents("A1", []);

        Assert.AreEqual(0, graph.Size);
    }

    [TestMethod]
    public void DependencyGraphReplaceDependents_ReplaceMultipleDependentsWithEmptyDependentsWithOneLeftOver_SizeOneGraph() {
        DependencyGraph graph = CreateDependencyGraph([ ["A1", "B2"], ["A1", "C3"], ["A1", "D4"], ["E5", "A1"] ]);
        graph.ReplaceDependents("A1", []);

        HashSet<string> expectedDependees = ["E5"];
        Assert.AreEqual(1, graph.Size);
        Assert.IsEmpty(expectedDependees.Except(graph.GetDependees("A1")));
    }

    // --- Tests with multiple dependees and one depenent ---
    [TestMethod]
    public void DependencyGraphReplaceDependees_ReplaceMultipleDependeesWithEmptyDependees_EmptyGraph() {
        DependencyGraph graph = CreateDependencyGraph([ ["B2", "A1"], ["C3", "A1"], ["D4", "A1"] ]);
        graph.ReplaceDependees("A1", []);

        Assert.AreEqual(0, graph.Size);
    }

    [TestMethod]
    public void DependencyGraphReplaceDependees_ReplaceMultipleDependeesWithEmptyDependeesWithOneLeftOver_SizeOneGraph() {
        DependencyGraph graph = CreateDependencyGraph([ ["B2", "A1"], ["C3", "A1"], ["D4", "A1"], ["A1", "E5"] ]);
        graph.ReplaceDependees("A1", []);

        HashSet<string> expectedDependents = ["E5"];
        Assert.AreEqual(1, graph.Size);
        Assert.IsEmpty(expectedDependents.Except(graph.GetDependents("A1")));
    }


    /// <summary>
    ///         Explain carefully what this code tests.
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
