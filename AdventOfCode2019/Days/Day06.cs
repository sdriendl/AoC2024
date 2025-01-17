using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Days;

public sealed class Day06 : CustomInputPathBaseDay
{
    private Dictionary<string, List<string>> _orbitedBy;
    private Dictionary<string, List<string>> _adjacency;
    public Day06()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        _orbitedBy = [];
        _adjacency = [];
        foreach (var line in File.ReadAllLines(InputFilePath))
        {
            var sl = line.Split(')');
            var l = _orbitedBy.GetValueOrDefault(sl[0], []);
            l.Add(sl[1]);
            _orbitedBy[sl[0]] = l;
            l = _adjacency.GetValueOrDefault(sl[0], []);
            l.Add(sl[1]);
            _adjacency[sl[0]] = l;
            l = _adjacency.GetValueOrDefault(sl[1], []);
            l.Add(sl[0]);
            _adjacency[sl[1]] = l;
        }
    }


    public async override ValueTask<string> Solve_1()
    {
        return Orbits("COM", 0).ToString();
    }

    public async override ValueTask<string> Solve_2()
    {
        var start = _adjacency["YOU"].First();
        var target = _adjacency["SAN"].First();

        var path = BFS(start, target, x => _adjacency[x]);
        return (path.Count() - 1).ToString();
    }

    private int Orbits(string sat, int n)
    {
        return n + _orbitedBy.GetValueOrDefault(sat, []).Sum(s => Orbits(s, n + 1));
    }

    private static List<T> BFS<T>(T start, T target, Func<T, IEnumerable<T>> neighbors)
        where T : IEquatable<T>
    {
        var seen = new HashSet<T>
        {
            start
        };
        var queue = new Queue<List<T>>();
        queue.Enqueue([start]);

        while (queue.TryDequeue(out var s))
        {
            foreach (var ns in neighbors(s.Last()))
            {
                List<T> path = [.. s, ns];
                if (ns.Equals(target)) return path;
                if (!seen.Contains(ns)) queue.Enqueue(path);
                seen.Add(ns);
            }
        }
        return [];
    }
}
