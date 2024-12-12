using AdventOfCode.Common;
using Microsoft.Diagnostics.Tracing.Parsers;
using MoreLinq;

namespace AdventOfCode2024.Days;


public sealed class Day12 : CustomInputPathBaseDay
{
    private List<List<char>> _map;
    private HashSet<(int X, int Y)> _visited;

    public Day12()
    {
        Initialize();
    }
    protected override void Initialize()
    {
        _visited = [];
        _map = File.ReadAllLines(InputFilePath)
            .Select(l => l.ToList())
            .ToList();
    }

    public async override ValueTask<string> Solve_1()
    {
        _visited = [];
        var price = 0;
        for (int y = 0; y < _map.Count; y++)
        {
            for (int x = 0; x < _map[y].Count; x++)
            {
                if (_visited.Contains((x, y)))
                    continue;
                var ff = FloodFill(x, y).ToList();
                var ap = AreaPerimeter(ff);
                price += ap.Area * ap.Perimeter;
            }
        }
        return price.ToString();
    }

    public async override ValueTask<string> Solve_2()
    {
        _visited = [];
        var price = 0;
        for (int y = 0; y < _map.Count; y++)
        {
            for (int x = 0; x < _map[y].Count; x++)
            {
                if (_visited.Contains((x, y)))
                    continue;
                var ff = FloodFill(x, y).ToList();
                var P = ff.First();
                var c = _map[P.Y][P.X];
                var ap = AreaPerimeter(ff);
                var sides = CountSides(ff);
                price += ap.Area * sides;
            }
        }
        return price.ToString();
    }

    private int CountSides(IEnumerable<(int X, int Y)> region)
    {
        var p = region.First();
        var c = _map[p.Y][p.X];
        var x_min = region.Min(p => p.X);
        var x_max = region.Max(p => p.X);
        var y_min = region.Min(p => p.Y);
        var y_max = region.Max(p => p.Y);
        var scanRight = new bool[y_max - y_min + 1];
        var scanRightFrontier = new bool[y_max - y_min + 1];
        var sides = 0;
        for (int x = x_min; x < x_max + 2; x++)
        {
            for (int i = 0; i < y_max - y_min + 1; i++)
            {
                scanRightFrontier[i] = !Edge((x, y_min + i), c);
            }
            for (int i = 0; i < y_max - y_min + 1; i++)
            {
                scanRight[i] = !scanRight[i] && scanRightFrontier[i];
            }
            sides += scanRight.RunLengthEncode().Count(x => x.Key);
            
            scanRightFrontier.CopyTo(scanRight, 0);
        }

        var scanDown = new bool[x_max - x_min + 1];
        var scanDownFrontier = new bool[x_max - x_min + 1];
        for (int y = y_min; y < y_max + 2; y++)
        {
            for (int i = 0; i < x_max - x_min + 1; i++)
            {
                scanDownFrontier[i] = !Edge((x_min + i, y), c);
            }
            for (int i = 0; i < x_max - x_min + 1; i++)
            {
                scanDown[i] = !scanDown[i] && scanDownFrontier[i];
            }
            sides += scanDown.RunLengthEncode().Count(x => x.Key);
            scanDownFrontier.CopyTo(scanDown, 0);
        }

        var scanLeft = new bool[y_max - y_min + 1];
        var scanLeftFrontier = new bool[y_max - y_min + 1];
        for (int x = x_max + 1; x >= x_min; x--)
        {
            for (int i = 0; i < y_max - y_min + 1; i++)
            {
                scanLeftFrontier[i] = !Edge((x, y_min + i), c);
            }
            for (int i = 0; i < y_max - y_min + 1; i++)
            {
                scanLeft[i] = !scanLeft[i] && scanLeftFrontier[i];
            }
            sides += scanLeft.RunLengthEncode().Count(x => x.Key);

            scanLeftFrontier.CopyTo(scanLeft, 0);
        }

        var scanUp = new bool[x_max - x_min + 1];
        var scanUpFrontier = new bool[x_max - x_min + 1];
        for (int y = y_max + 1; y >=  y_min; y--)
        {
            for (int i = 0; i < x_max - x_min + 1; i++)
            {
                scanUpFrontier[i] = !Edge((x_min + i, y), c);
            }
            for (int i = 0; i < x_max - x_min + 1; i++)
            {
                scanUp[i] = !scanUp[i] && scanUpFrontier[i];
            }
            sides += scanUp.RunLengthEncode().Count(x => x.Key);
            scanUpFrontier.CopyTo(scanUp, 0);
        }

        bool Edge((int X, int Y) p, char toChar)
        {
            if (p.X < 0 || p.Y < 0 || p.X >= _map[0].Count || p.Y >= _map.Count)
                return true;
            if (!region.Contains(p))
                return true;
            return false;
        }

        return sides;
    }

    private (int Area, int Perimeter) AreaPerimeter(IEnumerable<(int X, int Y)> region)
    {
        var area = 0;
        var perimeter = 0;
        foreach (var p in region)
        {
            var edges = new (int X, int Y)[]
            {
                (p.X + 1, p.Y),
                (p.X - 1, p.Y),
                (p.X, p.Y - 1),
                (p.X, p.Y + 1),
            }.Where(x => !InBounds(x));
            area++;
            perimeter += Neighbors(p.X, p.Y)
                .Where(k => _map[k.Y][k.X] != _map[p.Y][p.X])
                .Count();
            perimeter += edges.Count();
            _visited.Add(p);
        }
        return (area, perimeter);
    }

    private IEnumerable<(int X, int Y)> FloodFill(int x, int y)
    {
        var startingChar = _map[y][x];
        var stack = new Stack<(int X, int Y)>();
        var visited = new HashSet<(int X, int Y)>();
        stack.Push((x, y));
        while (stack.TryPop(out var next))
        {
            visited.Add(next);
            var neighbors = Neighbors(next.X, next.Y);
            foreach (var neighbor in neighbors)
            {
                if (_map[neighbor.Y][neighbor.X] != startingChar)
                    continue;
                if (visited.Contains(neighbor))
                    continue;
                stack.Push(neighbor);
            }
        }
        return visited;
    }

    private IEnumerable<(int X, int Y)> Neighbors(int x, int y)
    {
        return new[]
        {
            (x + 1, y),
            (x - 1, y),
            (x, y + 1),
            (x, y - 1),
        }.Where(InBounds);
    }

    private bool InBounds((int x, int y) p)
    {
        return p.x >= 0 && p.y >= 0 && p.x < _map[0].Count && p.y < _map.Count;
    }
}
