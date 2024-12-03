using AdventOfCode.Common;
using MoreLinq;

namespace AdventOfCode2024.Days;

public class Day02 : CustomInputPathBaseDay
{
    private List<List<int>> _input;

    public Day02()
    {
        _input = [];
        Initialize();
    }

    protected override void Initialize()
    {
        _input = [];

        var input = File.ReadAllLines(InputFilePath);
        foreach (var line in input)
        {
            _input.Add(line.Split(" ")
                           .Select(int.Parse)
                           .ToList());
        }
    }

    public override ValueTask<string> Solve_1()
    {
        var safe = _input.Count(IsSafe);
        return new ValueTask<string>(safe.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var safe = _input.Count(l => RemoveSteps(l).Any(IsSafe));
        return new ValueTask<string>(safe.ToString());
    }

    private static IEnumerable<List<int>> RemoveSteps(List<int> steps)
    {
        for (int i = 0; i < steps.Count; i++)
        {
            yield return [.. steps[..i], .. steps[(i + 1)..]];
        }
    }

    private static bool IsSafe(List<int> line)
    {
        var increasing = true;
        var decreasing = true;
        var levelDifferenceSafe = true;
        for (var i = 0; i < line.Count - 1; i++)
        {
            var levelDiff = line[i] - line[i + 1];
            increasing &= levelDiff >= 0;
            decreasing &= levelDiff <= 0;
            levelDifferenceSafe &= Math.Abs(levelDiff) > 0 && Math.Abs(levelDiff) <= 3;
        }
        if (levelDifferenceSafe && (increasing ^ decreasing))
        {
            return true;
        }
        return false;
    }

    // slow, but neat
    private static bool IsSafeSlow(List<int> line)
    {
        var stepSizes = line.Window(2)
                            .Select(w => w[0] - w[1])
                            .ToHashSet();

        return stepSizes.IsSubsetOf([-1, -2, -3])
            || stepSizes.IsSubsetOf([1, 2, 3]);
    }
}
