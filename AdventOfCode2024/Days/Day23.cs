using AdventOfCode.Common;

namespace AdventOfCode2024.Days;

public sealed class Day23 : CustomInputPathBaseDay
{
    private List<(string, string)> _input;
    private HashSet<string> _vertices;
    private Dictionary<string, List<string>> _adjacencyList;

    public Day23()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        _input = File.ReadAllLines(InputFilePath)
                     .Select(l => l.Split("-"))
                     .Select(s => (s[0], s[1]))
                     .ToList();
        _vertices = _input.SelectMany(t => new[] { t.Item1, t.Item2 }).ToHashSet();
        _adjacencyList = new Dictionary<string, List<string>>();
        foreach (var vertex in _vertices)
        {
            var list = _input.Where(t => t.Item1 == vertex).Select(n => n.Item2).ToList();
            list.AddRange(_input.Where(t => t.Item2 == vertex).Select(n => n.Item1));
            _adjacencyList.Add(vertex, list);
        }
    }

    private bool IsClique(IEnumerable<string> vertices)
    {
        foreach (var v in vertices)
        {
            foreach (var w in vertices)
            {
                if (v == w) continue;
                if (!_adjacencyList[v].Contains(w)) return false;
            }
        }

        return true;
    }

    public IEnumerable<HashSet<string>> EmbiggenClique(HashSet<string> R, bool abortEarly = false)
    {
        var boundary = R.SelectMany(x => _adjacencyList[x]).ToHashSet();
        foreach (var v in boundary.Except(R))
        {
            if (IsClique(R.Append(v)))
            {
                yield return R.Append(v).ToHashSet();
                if (abortEarly) yield break;
            }
        }
    }

    public override async ValueTask<string> Solve_1()
    {
        var cliques = _vertices.Where(v => v.StartsWith("t"))
                               .Select(v => new HashSet<string> { v })
                               .ToList();

        for (int i = 1; i < 3; i++)
        {
            cliques = cliques.SelectMany(x => EmbiggenClique(x))
                             .Where(l => l.Count >= i)
                             .Distinct(comparer: HashSet<string>.CreateSetComparer())
                             .ToList();
        }

        return cliques.Count.ToString();
    }

    public override async ValueTask<string> Solve_2()
    {
        var cliques = _vertices.Select(v => new HashSet<string> { v }).ToList();

        var minSize = 1;
        var newCliques = cliques;
        while (newCliques.Count > 0)
        {
            cliques = newCliques;
            minSize++;
            newCliques = cliques.SelectMany(x => EmbiggenClique(x, true))
                                .Where(l => l.Count >= minSize)
                                .Distinct(comparer: HashSet<string>.CreateSetComparer())
                                .ToList();
        }

        var maxClique = cliques.First();
        return string.Join(",", maxClique.Order());
    }
}