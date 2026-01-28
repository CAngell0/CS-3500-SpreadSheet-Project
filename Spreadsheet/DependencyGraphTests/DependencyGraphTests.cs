namespace DependencyGraphTests;

using DependencyGraph;

/// <summary>
///   This is a test class for DependencyGraphTest and is intended
///   to contain all DependencyGraphTest Unit Tests
/// </summary>
[TestClass]
public class DependencyGraphTests {
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
        List<string> testedDependents = graph.GetDependents("A1").ToList();

        Assert.IsNotNull(testedDependents);
        Assert.HasCount(1, testedDependents);
        Assert.Contains("B2", testedDependents);
    }

    [TestMethod]
    public void DependencyGraphGetDependendees_OneDependencyPair_CorrectDependee() {
        DependencyGraph graph = new();
        graph.AddDependency("A1", "B2");
        List<string> testedDependees = graph.GetDependees("B2").ToList();

        Assert.IsNotNull(testedDependees);
        Assert.HasCount(1, testedDependees);
        Assert.Contains("A1", testedDependees);
    }

    [TestMethod]
    public void DependencyGraphReplaceDependents_OneDependencyPair_SuccessfullyReplaced() {
        DependencyGraph graph = new();
        graph.AddDependency("A1", "B2");

        graph.ReplaceDependents("A1", ["C4"]);
        List<string> testedDependents = graph.GetDependents("A1").ToList();

        Assert.IsNotNull(testedDependents);
        Assert.HasCount(1, testedDependents);
        Assert.IsFalse(graph.HasDependees("B2"));
        Assert.Contains("C4", testedDependents);
    }

    [TestMethod]
    public void DependencyGraphReplaceDependees_OneDependencyPair_SuccessfullyReplaced() {
        DependencyGraph graph = new();
        graph.AddDependency("A1", "B2");

        List<string> newDependees = ["C4"];
        graph.ReplaceDependees("B2", newDependees);
        List<string> testedDependees = graph.GetDependees("B2").ToList();

        Assert.IsNotNull(testedDependees);
        Assert.HasCount(newDependees.Count, testedDependees);
        Assert.IsFalse(graph.HasDependents("A1"));
        Assert.Contains("C4", testedDependees);
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

    [TestMethod]
    public void DependencyGraphGetDependents_OneDependeeWithMultipleDependents_ThreeDependents() {
        DependencyGraph graph = new();
        graph.AddDependency("A1", "B2");
        graph.AddDependency("A1", "C3");
        graph.AddDependency("A1", "D4");

        List<string> dependents = graph.GetDependents("A1").ToList();
        
        Assert.IsNotNull(dependents);
        Assert.HasCount(3, dependents);
        Assert.Contains("B2", dependents);
        Assert.Contains("C3", dependents);
        Assert.Contains("D4", dependents);
    }

    [TestMethod]
    public void DependencyGraphGetDependees_OneDependeeWithMultipleDependents_OneDependee() {
        DependencyGraph graph = new();
        graph.AddDependency("A1", "B2");
        graph.AddDependency("A1", "C3");
        graph.AddDependency("A1", "D4");

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

    // TODO - Go over the comments for the next three tests

    [TestMethod]
    public void DependencyGraphReplaceDependents_WithoutIntersection_AllNewDependents() {
        DependencyGraph graph = new();
        List<string> newDependents = ["E5", "F6", "G7"];
        List<string> oldDependants = ["B2", "C3", "D4"];    
        foreach (string oldDep in oldDependants) graph.AddDependency("A1", oldDep);

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
        foreach (string oldDep in oldDependants) {
            // Makes sure they are not listed as a dependent
            Assert.DoesNotContain(oldDep, testedDependents);

            // Checks to make sure the old dependents don't have any dependees
            List<string> dependeesOfOldDep = graph.GetDependees(oldDep).ToList();
            Assert.IsNotNull(dependeesOfOldDep);
            Assert.IsEmpty(dependeesOfOldDep);
        }
    }

    [TestMethod]
    public void DependencyGraphReplaceDependents_WithIntersection_TwoDeletionsOneAddition() {
        DependencyGraph graph = new();
        List<string> newDependents = ["C3", "D4", "F5"];
        List<string> oldDependants = ["B2", "C3", "D4", "E5"];    
        foreach (string oldDep in oldDependants) graph.AddDependency("A1", oldDep);

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

        List<string> deletedDeps = oldDependants.Except(newDependents).ToList();
        //Checks the old dependents
        foreach (string delDep in deletedDeps) {
            // Makes sure they are not listed as a dependent
            Assert.DoesNotContain(delDep, testedDependents);

            // Checks to make sure the old dependents don't have any dependees
            List<string> dependeesOfOldDep = graph.GetDependees(delDep).ToList();
            Assert.IsNotNull(dependeesOfOldDep);
            Assert.IsEmpty(dependeesOfOldDep);
        }
    }

    [TestMethod]
    public void DependencyGraphReplaceDependents_FullIntersection_NoChange() {
        DependencyGraph graph = new();
        List<string> newDependents = ["B2", "C3", "D4"];
        List<string> oldDependants = ["B2", "C3", "D4"];    
        foreach (string oldDep in oldDependants) graph.AddDependency("A1", oldDep);

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
