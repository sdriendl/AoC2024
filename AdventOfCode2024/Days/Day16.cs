using AdventOfCode.Common;
using SuperLinq;
using System.Dynamic;

namespace AdventOfCode2024.Days;

public class Day16 : CustomInputPathBaseDay
{
    private List<string> _input;
    private (int X, int Y) _startPos;
    private (int X, int Y) _targetPos;

    public Day16()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        _input = File.ReadAllLines(InputFilePath).ToList();
        for (int y = 0; y < _input.Count; y++)
        {
            for (int x = 0; x < _input[0].Length; x++)
            {
                if (_input[y][x] == 'S')
                    _startPos = (x, y);
                if (_input[y][x] == 'E')
                    _targetPos = (x, y);
            }
        }
    }

    public async override ValueTask<string> Solve_1()
    {
        return ShortestPath(_startPos, _targetPos).ToString();
    }

    public async override ValueTask<string> Solve_2()
    {
        return OnShortestPath(_startPos, _targetPos).ToString();
    }

    private int ShortestPath((int X, int Y) startPos, (int X, int Y) targetPos)
    {
        var start = (startPos, (1, 0));
        bool TargetPos(((int X, int Y) Pos, (int X, int Y) Dir) state)
        {
            return state.Pos == targetPos;
        }
        var path = SuperEnumerable.GetShortestPath<((int X, int Y) Pos, (int X, int Y) Dir), int>(start, GetNeighbors, predicate: TargetPos);

        return SuperEnumerable.GetShortestPathCost<((int X, int Y) Pos, (int X, int Y) Dir), int>(start, GetNeighbors, predicate: TargetPos);
    }

    private int OnShortestPath((int X, int Y) startPos, (int X, int Y) targetPos)
    {
        var start = (startPos, (1, 0));
        bool TargetPos(((int X, int Y) Pos, (int X, int Y) Dir) state)
        {
            return state.Pos == targetPos;
        }
        var shortestPath = SuperEnumerable.GetShortestPathCost<((int X, int Y) Pos, (int X, int Y) Dir), int>(start, GetNeighbors, predicate: TargetPos);

        var pathsFromStart = SuperEnumerable.GetShortestPaths<((int X, int Y) Pos, (int X, int Y) Dir), int>(start, GetNeighbors);
        
        var pathsFromTargetW = SuperEnumerable.GetShortestPaths<((int X, int Y) Pos, (int X, int Y) Dir), int>((targetPos, (-1, 0)), GetNeighbors);
        var pathsFromTargetE = SuperEnumerable.GetShortestPaths<((int X, int Y) Pos, (int X, int Y) Dir), int>((targetPos, (1, 0)), GetNeighbors);
        var pathsFromTargetN = SuperEnumerable.GetShortestPaths<((int X, int Y) Pos, (int X, int Y) Dir), int>((targetPos, (0, -1)), GetNeighbors);
        var pathsFromTargetS = SuperEnumerable.GetShortestPaths<((int X, int Y) Pos, (int X, int Y) Dir), int>((targetPos, (0, 1)), GetNeighbors);


        var onShortestPath = new HashSet<(int X, int Y)>();
        foreach (var (state, path) in pathsFromStart)
        {
            if (pathsFromTargetW.TryGetValue(((state.Pos), (-state.Dir.X, -state.Dir.Y)), out var otherPath))
            {
                if (path.cost + otherPath.cost == shortestPath)
                {
                    onShortestPath.Add(state.Pos);
                }
            }
            if (pathsFromTargetE.TryGetValue(((state.Pos), (-state.Dir.X, -state.Dir.Y)), out var otherPath2))
            {
                if (path.cost + otherPath2.cost == shortestPath)
                {
                    onShortestPath.Add(state.Pos);
                }
            }
            if (pathsFromTargetN.TryGetValue(((state.Pos), (-state.Dir.X, -state.Dir.Y)), out var otherPath3))
            {
                if (path.cost + otherPath3.cost == shortestPath)
                {
                    onShortestPath.Add(state.Pos);
                }
            }
            if (pathsFromTargetS.TryGetValue(((state.Pos), (-state.Dir.X, -state.Dir.Y)), out var otherPath4))
            {
                if (path.cost + otherPath4.cost == shortestPath)
                {
                    onShortestPath.Add(state.Pos);
                }
            }
        }
        return onShortestPath.Count();
    }

    private IEnumerable<(((int X, int Y) Pos, (int X, int Y) Dir) nextState, int cost)> GetNeighbors(((int X, int Y) Pos, (int X, int Y) Dir) state, int cost)
    {
        (((int X, int Y) Pos, (int, int)), int) turnNorth = (((state.Pos.X, state.Pos.Y), (0, -1)), 1000);
        (((int X, int Y) Pos, (int, int)), int) turnSouth = (((state.Pos.X, state.Pos.Y), (0, 1)), 1000);
        (((int X, int Y) Pos, (int, int)), int) turnWest = (((state.Pos.X, state.Pos.Y), (-1, 0)), 1000);
        (((int X, int Y) Pos, (int, int)), int) turnEast = (((state.Pos.X, state.Pos.Y), (1, 0)), 1000);
        (((int X, int Y) Pos, (int, int)), int) moveForward = (((state.Pos.X + state.Dir.X, state.Pos.Y + state.Dir.Y), state.Dir), 1);

        foreach (var (s, c) in new[]
        {
            turnNorth, turnSouth, turnWest, turnEast, moveForward,
        })
        {
            if (InBounds(s.Pos))
            {
                yield return (s, c + cost);
            }
        }
    }

    private bool InBounds((int X, int Y) p)
    {
        if (p.X < 0 || p.Y < 0 || p.X >= _input[0].Length || p.Y >= _input.Count)
            return false;
        if (_input[p.Y][p.X] == '#')
            return false;
        return true;
    }
}
