using AdventOfCode.Common;

namespace AdventOfCode2024.Days;

public class Day11 : CustomInputPathBaseDay
{
    private List<long> _input;
    public Day11()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        _input = File.ReadAllText(InputFilePath)
            .Split(" ", options: StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToList();
    }

    public async override ValueTask<string> Solve_1()
    {
        var s = _input
            .GroupBy(x => x)
            .ToDictionary(g => g.Key,
            g => (long)g.Count());
        for (int i = 0; i < 25; i++)
        {
            var newDict = new Dictionary<long, long>();
            foreach (var stone in s.Keys)
            {
                var n = s[stone];
                foreach (var newStone in ApplyRule(stone))
                {
                    var t = newDict.GetValueOrDefault(newStone, 0);
                    newDict[newStone] = t + n;
                }
            }
            s = newDict;
        }
        return s.Sum(k => k.Value).ToString();
    }

    public async override ValueTask<string> Solve_2()
    {
        var s = _input
            .GroupBy(x => x)
            .ToDictionary(g => g.Key,
            g => (long)g.Count());
        for (int i = 0; i < 75; i++)
        {
            var newDict = new Dictionary<long, long>();
            foreach (var stone in s.Keys)
            {
                var n = s[stone];
                foreach (var newStone in ApplyRule(stone))
                {
                    var t = newDict.GetValueOrDefault(newStone, 0);
                    newDict[newStone] = t + n;
                }
            }
            s = newDict;
        }
        return s.Sum(k => k.Value).ToString();
    }

    private IEnumerable<long> ApplyRule(long stone)
    {
        var stoneString = stone.ToString();
        var nLength = stoneString.Length;
        if (stone == 0) return [1];
        else if (nLength % 2 == 0)
        {
            var split = SplitLong(stone);
            return [split.firstHalf, split.secondHalf];
        }
        else
        {
            return [stone * 2024];
        }
    }

    private static (long firstHalf, long secondHalf) SplitLong(long number)
    {
        int numDigits = (int)Math.Floor(Math.Log10(number) + 1);

        int midIndex = numDigits / 2;

        long divisor = (long)Math.Pow(10, midIndex);

        long firstHalf = number / divisor;
        long secondHalf = number % divisor;

        return (firstHalf, secondHalf);
    }
}
