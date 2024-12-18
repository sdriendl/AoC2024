using AdventOfCode.Common;
using BenchmarkDotNet.Disassemblers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Days;

public class Day18 : CustomInputPathBaseDay
{
    private List<(int X, int Y)> _input;
    private HashSet<(int, int)> _corrupted;
    public int Width { get; set; } = 71;
    public int Height { get; set; } = 71;

    public Day18()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        _input = File.ReadAllLines(InputFilePath)
            .Select(l =>
            {
                var s = l.Split(",");
                return (int.Parse(s[0]), int.Parse(s[1]));
            })
            .ToList();
        _corrupted = _input.Take(1024).ToHashSet();
    }

    public async override ValueTask<string> Solve_1()
    {
        var result = SuperLinq.SuperEnumerable.GetShortestPathCost<(int, int), int>((0, 0), getNeighbors: GetNeighbors, (Width - 1, Height - 1));
        return result.ToString();
    }
    public async override ValueTask<string> Solve_2()
    {
        var idx = FirstBlocked(0, _input.Count());
        return _input.Take(idx + 1).Last().ToString();
    }

    private int FirstBlocked(int lower, int upper)
    {
        var mid = (lower + upper) / 2;
        if (lower >= upper)
            return lower;

        if (IsBlocked(mid))
        {
            return FirstBlocked(lower, mid - 1);
        }
        else
        {
            return FirstBlocked(mid + 1, upper);
        }
    }

    private bool IsBlocked(int index)
    {
        index++;
        _corrupted = _input.Take(index).ToHashSet();
        try
        {
            var result = SuperLinq.SuperEnumerable.GetShortestPathCost<(int, int), int>((0, 0), getNeighbors: GetNeighbors, (Width - 1, Height - 1));
            return false;
        }
        catch (Exception ex)
        {
            return true;
        }
    }


    private IEnumerable<((int, int) nextState, int cost, int bestGuess)> GetNeighbors((int X, int Y) state, int cost)
    {
        return new[]
        {
            (state.X + 1, state.Y),
            (state.X - 1, state.Y),
            (state.X, state.Y-1),
            (state.X, state.Y+1)
        }
        .Where(InBounds)
        .Where(p => !_corrupted.Contains(p))
        .Select(p => (p, cost + 1, cost + (Width - state.X + Height - state.Y)));
    }

    private bool InBounds((int X, int Y) p)
    {
        return p.X >= 0 && p.Y >= 0 && p.X < Width && p.Y < Height;
    }

}
