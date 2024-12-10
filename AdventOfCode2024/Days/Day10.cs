using AdventOfCode.Common;

namespace AdventOfCode2024.Days;

public class Day10 : CustomInputPathBaseDay
{
    private List<List<int>> _input;
    public Day10()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        _input= File.ReadAllLines(InputFilePath)
            .Select(line => line.Select(n => n - '0').ToList())
            .ToList();
    }

    public async override ValueTask<string> Solve_1()
    {
        var result = 0;
        for (int y = 0; y < _input.Count; y++) 
        {
            for (int x = 0; x < _input[0].Count; x++)
            {
                if (_input[y][x] != 0) continue;
                result += TrailsStartingAt(x, y)
                    .Select(p => p.Last())
                    .Distinct()
                    .Count();
            }
        }

        return result.ToString();
    }

    public async override ValueTask<string> Solve_2()
    {
        var result = 0;
        for (int y = 0; y < _input.Count; y++)
        {
            for (int x = 0; x < _input[0].Count; x++)
            {
                if (_input[y][x] != 0) continue;
                result += TrailsStartingAt(x, y).Count;
            }
        }

        return result.ToString();
    }

    private List<List<(int X, int Y)>> TrailsStartingAt(int x, int y)
    {
        var frontier = new Stack<List<(int X, int Y)>>();
        var trails = new List<List<(int X, int Y)>>();

        frontier.Push([(x,y)]);

        while (frontier.TryPop(out var p)) 
        {
            var w = p.Last();

            if (_input[w.Y][w.X] == 9)
            {
                trails.Add(p);
                continue;
            }

            foreach (var neighbor in Neighbors(w)) 
            {
                frontier.Push([.. p, neighbor]);
            }
        }

        return trails;
    }

    private IEnumerable<(int X, int Y)> Neighbors((int X, int Y) p)
    {
        return new (int X, int Y)[]
        {
                (p.X + 1, p.Y),
                (p.X - 1, p.Y),
                (p.X,  p.Y + 1),
                (p.X,  p.Y - 1),

        }
        .Where(InBounds)
        .Where(x => _input[x.Y][x.X] == _input[p.Y][p.X] + 1);
    }

    private bool InBounds((int x, int y) p)
    {
        return p.x >= 0 && p.y >= 0 && p.x < _input[0].Count && p.y < _input.Count;
    }
}
