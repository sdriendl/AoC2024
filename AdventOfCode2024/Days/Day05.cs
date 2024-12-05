using AdventOfCode.Common;

namespace AdventOfCode2024.Days;

public sealed class Day05 : CustomInputPathBaseDay
{
    private HashSet<(int, int)> _rules = [];
    private List<List<int>> _updates = [];
    private IComparer<int> _updateComparer;
    public Day05()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        _rules.Clear();
        _updates.Clear();
        var input = File.ReadAllText(InputFilePath);
        var inputSplit = input.Split("\n\n");
        var rulesInput = inputSplit[0];
        var updatesInput = inputSplit[1];
        foreach (var line in rulesInput
            .Split("\n"))
        {
            var r = line.Split("|");
            _rules.Add((int.Parse(r[0]), int.Parse(r[1])));
        }
        foreach (var line in updatesInput
            .Split("\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            _updates.Add(line.Split(",").Select(int.Parse).ToList());
        }
        _updateComparer = new UpdateComparer(_rules);
    }

    public override ValueTask<string> Solve_1()
    {
        var result = _updates.Where(u => IsOrdered(u, _updateComparer))
                             .Sum(u => u.Skip(u.Count / 2).Take(1).First());

        return new ValueTask<string>(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var result = _updates.Where(u => !IsOrdered(u, _updateComparer))
                             .Select(u => u.Order(_updateComparer))
                             .Sum(u => u.Skip(u.Count() / 2).Take(1).First());

        return new ValueTask<string>(result.ToString());
    }

    private class UpdateComparer(ISet<(int, int)> rules) : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (rules.Contains((x, y))) return -1;
            if (rules.Contains((y, x))) return 1;
            return 0;
        }
    }

    private bool IsOrdered(IEnumerable<int> updates, IComparer<int> comparer)
    {
        return updates.Order(comparer).SequenceEqual(updates);
    }
}
