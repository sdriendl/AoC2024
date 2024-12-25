using AdventOfCode.Common;

namespace AdventOfCode2024.Days;

public sealed class Day25 : CustomInputPathBaseDay
{
    private List<int[]> _locks;
    private List<int[]> _keys;
    public Day25()
    {
        Initialize();    
    }
    
    protected override void Initialize()
    {
        _locks = [];
        _keys = [];
        var patterns = File.ReadAllText(InputFilePath).Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
        foreach (var pattern in patterns)
        {
            if (pattern.StartsWith("#####"))
            {
                _locks.Add(ParsePattern(pattern));
            }
            else
            {
                _keys.Add(ParsePattern(pattern));
            }
        }
    }
    
    public override async ValueTask<string> Solve_1()
    {
        return MoreLinq.MoreEnumerable
                .Cartesian(_keys, _locks, 
                           (key, @lock) => key.Zip(@lock, (x, y) => x + y <= 7)
                                              .All(x => x))
                .Count(x => x)
                .ToString();
    }

    public override async ValueTask<string> Solve_2()
    {
        return string.Empty;
    }

    private int[] ParsePattern(string pattern)
    {
        var lines = pattern.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var heights = new int[5];
        for (int i = 0; i < 5; i++)
        {
            heights[i] = lines.Select(l => l[i]).Count(c => c == '#');
        }
        return heights;
    }
}