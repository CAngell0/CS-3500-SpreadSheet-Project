// Skeleton implementation written by Joe Zachary for CS 3500, September 2013
// Version 1.1 - Joe Zachary
//   (Fixed error in comment for RemoveDependency)
// Version 1.2 - Daniel Kopta Fall 2018
//   (Clarified meaning of dependent and dependee)
//   (Clarified names in solution/project structure)
// Version 1.3 - H. James de St. Germain Fall 2024

namespace DependencyGraph;

/// <summary>
///   <para>
///     (s1,t1) is an ordered pair of strings, meaning t1 depends on s1.
///     (in other words: s1 must be evaluated before t1.)
///   </para>
///   <para>
///     A DependencyGraph can be modeled as a set of ordered pairs of strings.
///     Two ordered pairs (s1,t1) and (s2,t2) are considered equal if and only
///     if s1 equals s2 and t1 equals t2.
///   </para>
///   <remarks>
///     Recall that sets never contain duplicates.
///     If an attempt is made to add an element to a set, and the element is already
///     in the set, the set remains unchanged.
///   </remarks>
///   <para>
///     Given a DependencyGraph DG:
///   </para>
///   <list type="number">
///     <item>
///       If s is a string, the set of all strings t such that (s,t) is in DG is called dependents(s).
///       (The set of things that depend on s.)
///     </item>
///     <item>
///       If s is a string, the set of all strings t such that (t,s) is in DG is called dependees(s).
///       (The set of things that s depends on.)
///     </item>
///   </list>
///   <para>
///      For example, suppose DG = {("a", "b"), ("a", "c"), ("b", "d"), ("d", "d")}.
///   </para>
///   <code>
///     dependents("a") = {"b", "c"}
///     dependents("b") = {"d"}
///     dependents("c") = {}
///     dependents("d") = {"d"}
///     dependees("a")  = {}
///     dependees("b")  = {"a"}
///     dependees("c")  = {"a"}
///     dependees("d")  = {"b", "d"}
///   </code>
/// </summary>
public class DependencyGraph {
    private Dictionary<string, HashSet<string>> _dependentMap;
    private Dictionary<string, HashSet<string>> _dependendeeMap;
    private int _size;


    /// <summary>
    ///   Initializes a new instance of the <see cref="DependencyGraph"/> class.
    ///   The initial DependencyGraph is empty.
    /// </summary>
    public DependencyGraph() {
        _dependentMap = new Dictionary<string, HashSet<string>>();
        _dependendeeMap = new Dictionary<string, HashSet<string>>();
        _size = 0;
    }

    /// <summary>
    /// The number of ordered pairs in the DependencyGraph.
    /// </summary>
    public int Size {
        get { return _size; }
    }

    /// <summary>
    ///   Reports whether the given node has dependents (i.e., other nodes depend on it).
    /// </summary>
    /// <param name="nodeName"> The name of the node.</param>
    /// <returns> true if the node has dependents. </returns>
    public bool HasDependents(string nodeName) {
        return _dependentMap.TryGetValue(nodeName, out HashSet<string>? depSet) && depSet.Count > 0;
    }

    /// <summary>
    ///   Reports whether the given node has dependees (i.e., depends on one or more other nodes).
    /// </summary>
    /// <returns> true if the node has dependees.</returns>
    /// <param name="nodeName">The name of the node.</param>
    public bool HasDependees(string nodeName) {
        return _dependendeeMap.TryGetValue(nodeName, out HashSet<string>? depSet) && depSet.Count > 0;
    }

    /// <summary>
    ///   <para>
    ///     Returns the dependents of the node with the given name.
    ///   </para>
    /// </summary>
    /// <param name="nodeName"> The node we are looking at.</param>
    /// <returns> The dependents of nodeName. </returns>
    public IEnumerable<string> GetDependents(string nodeName) {
        HashSet<string> dependents = [];
        if (!_dependentMap.TryGetValue(nodeName, out HashSet<string>? depSet)) return dependents;

        foreach (string dependency in depSet) dependents.Add(dependency);
        return dependents;
    }

    /// <summary>
    ///   <para>
    ///     Returns the dependees of the node with the given name.
    ///   </para>
    /// </summary>
    /// <param name="nodeName"> The node we are looking at.</param>
    /// <returns> The dependees of nodeName. </returns>
    public IEnumerable<string> GetDependees(string nodeName) {
        HashSet<string> dependees = [];
        if (!_dependendeeMap.TryGetValue(nodeName, out HashSet<string>? depSet)) return dependees;

        foreach (string dependency in depSet) dependees.Add(dependency);
        return dependees;
    }

    /// <summary>
    /// <para>Adds the ordered pair (dependee, dependent), if it doesn't exist.</para>
    ///
    /// <para>
    ///   This can be thought of as: dependee must be evaluated before dependent
    /// </para>
    /// </summary>
    /// <param name="dependee"> the name of the node that must be evaluated first</param>
    /// <param name="dependent"> the name of the node that cannot be evaluated until after dependee</param>
    public void AddDependency(string dependee, string dependent) {
        bool alreadyExisted = !AddToDependentMap(dependee, dependent);
        AddToDependeeMap(dependee, dependent);
        if (!alreadyExisted) _size++;
    }

    /// <summary>
    ///   <para>
    ///     Removes the ordered pair (dependee, dependent), if it exists.
    ///   </para>
    /// </summary>
    /// <param name="dependee"> The name of the node that must be evaluated first</param>
    /// <param name="dependent"> The name of the node that cannot be evaluated until after dependee</param>
    public void RemoveDependency(string dependee, string dependent) {
        bool wasAlreadyRemoved = !RemoveFromDependentMap(dependee, dependent);
        RemoveFromDependeeMap(dependee, dependent);
        if (!wasAlreadyRemoved) _size--;
    }

    /// <summary>
    ///   Removes all existing ordered pairs of the form (nodeName, *).  Then, for each
    ///   t in newDependents, adds the ordered pair (nodeName, t).
    /// </summary>
    /// <param name="nodeName"> The name of the node whose dependents are being replaced </param>
    /// <param name="newDependents"> The new dependents for nodeName</param>
    public void ReplaceDependents(string nodeName, IEnumerable<string> newDependents) {
        bool wasOldDepRetrieved = _dependentMap.TryGetValue(nodeName, out HashSet<string>? depSet);
        HashSet<string> oldDepSet = (wasOldDepRetrieved && depSet != null) ? depSet : [];
        HashSet<string> newDepSet = newDependents.ToHashSet();

        string[] dependentsToRemove = oldDepSet.Except(newDepSet).ToArray();
        string[] dependentsToAdd = newDepSet.Except(oldDepSet).ToArray();

        foreach (string dep in dependentsToRemove) RemoveFromDependeeMap(nodeName, dep);
        foreach (string dep in dependentsToAdd) AddToDependeeMap(nodeName, dep);

        _dependentMap[nodeName] = newDepSet;
        _size += dependentsToAdd.Length - dependentsToRemove.Length;
    }

    /// <summary>
    ///   <para>
    ///     Removes all existing ordered pairs of the form (*, nodeName).  Then, for each
    ///     t in newDependees, adds the ordered pair (t, nodeName).
    ///   </para>
    /// </summary>
    /// <param name="nodeName"> The name of the node who's dependees are being replaced</param>
    /// <param name="newDependees"> The new dependees for nodeName</param>
    public void ReplaceDependees(string nodeName, IEnumerable<string> newDependees) {
        bool wasOldDepRetrieved = _dependendeeMap.TryGetValue(nodeName, out HashSet<string>? depSet);
        HashSet<string> oldDepSet = (wasOldDepRetrieved && depSet != null) ? depSet : [];
        HashSet<string> newDepSet = newDependees.ToHashSet();

        string[] dependeesToRemove = oldDepSet.Except(newDepSet).ToArray();
        string[] dependeesToAdd = newDepSet.Except(oldDepSet).ToArray();

        foreach (string dep in dependeesToRemove) RemoveFromDependentMap(dep, nodeName);
        foreach (string dep in dependeesToAdd) AddToDependentMap(dep, nodeName);

        _dependendeeMap[nodeName] = newDepSet;
        _size += dependeesToAdd.Length - dependeesToRemove.Length;
    }

    private bool AddToDependentMap(string dependee, string dependent) {
        bool result = true;
        if (_dependentMap.TryGetValue(dependee, out HashSet<string>? depSet)) result = depSet.Add(dependent);
        else if (_dependentMap.ContainsKey(dependee)) _dependentMap[dependee] = [dependent];
        else _dependentMap.Add(dependee, [dependent]);
        return result;
    }

    private bool AddToDependeeMap(string dependee, string dependent) {
        bool result = true;
        if (_dependendeeMap.TryGetValue(dependent, out HashSet<string>? depSet)) result = depSet.Add(dependee);
        else if (_dependendeeMap.ContainsKey(dependent)) _dependendeeMap[dependent] = [dependee];
        else _dependendeeMap.Add(dependent, [dependee]);
        return result;
    }

    private bool RemoveFromDependentMap(string dependee, string dependent) {
        bool result = false;
        if (_dependentMap.TryGetValue(dependee, out HashSet<string>? depSet)) {
            result = depSet.Remove(dependent);
            if (depSet.Count == 0) _dependentMap.Remove(dependee);
        }
        return result;
    }

    private bool RemoveFromDependeeMap(string dependee, string dependent) {
        bool result = false;
        if (_dependendeeMap.TryGetValue(dependent, out HashSet<string>? depSet)) {
            result = depSet.Remove(dependee);
            if (depSet.Count == 0) _dependendeeMap.Remove(dependent);
        }
        return result;
    }
}

// - WHERE I LEFT OFF:
// - Just finished refactoring the replacemenet methods
// - Begin unit testing and debugging

//TODO - Comment helper methods
//TODO - Add comment headers to files
//TODO - Ensure 100% coverage
