using AdventOfCode.Common;
using Microsoft.Diagnostics.Runtime;

namespace AdventOfCode2024.Days;

public class Day15 : CustomInputPathBaseDay
{
    private List<List<char>> _map;
    private List<char> _moves;
    public Day15()
    {
        Initialize();
    }
    protected override void Initialize()
    {
        _moves = [];
        _map = [];
        var inputSplit = File.ReadAllText(InputFilePath).Split("\n\n");
        foreach (var line in inputSplit[0].Split())
        {
            _map.Add(line.ToList());
        }
        foreach (var line in inputSplit[1].Split())
        {
            _moves.AddRange(line);
        }
    }

    public async override ValueTask<string> Solve_1()
    {
        Initialize();
        (int X, int Y) robot = (0, 0);
        for (int y = 0; y < _map.Count; y++)
        {
            for (int x = 0; x < _map[y].Count; x++)
            {
                if (_map[y][x] == '@')
                    robot = (x, y);

            }
        }

        foreach (var move in _moves)
        {
            var dir = move switch
            {
                '^' => (0, -1),
                '<' => (-1, 0),
                '>' => (1, 0),
                'v' => (0, 1),
            };
            if (TryMove(robot, dir, out var nextPos))
            {
                robot = nextPos;
            }
        }

        var result = 0;
        for (int y = 0; y < _map.Count; y++)
        {
            for (int x = 0; x < _map[y].Count; x++)
            {
                if (_map[y][x] == 'O')
                    result += 100 * y + x;
            }
        }


        return result.ToString();
    }

    public async override ValueTask<string> Solve_2()
    {
        Initialize();
        for (int y = 0; y < _map.Count; y++)
        {
            _map[y] = _map[y].SelectMany<char, char>(x => x switch
            {
                '#' => "##",
                'O' => "[]",
                '.' => "..",
                '@' => "@."
            }).ToList();
        }

        (int X, int Y) robot = (0, 0);
        for (int y = 0; y < _map.Count; y++)
        {
            for (int x = 0; x < _map[y].Count; x++)
            {
                if (_map[y][x] == '@')
                    robot = (x, y);

            }
        }
        foreach (var move in _moves)
        {
            var dir = move switch
            {
                '^' => (0, -1),
                '<' => (-1, 0),
                '>' => (1, 0),
                'v' => (0, 1),
            };
            if (TryMoveExpanded(robot, dir, out var nextPos))
            {
                robot = nextPos;
            }
        }

        var result = 0;
        for (int y = 0; y < _map.Count; y++)
        {
            for (int x = 0; x < _map[y].Count; x++)
            {
                if (_map[y][x] == '[')
                    result += 100 * y + x;
            }
        }



        return result.ToString();
    }

    private bool TryMoveExpanded((int X, int Y) p, (int X, int Y) dir, out (int X, int Y) nextPos)
    {
        nextPos = (p.X + dir.X, p.Y + dir.Y);
        if (CanMoveExpanded(p, dir))
        {
            MoveRecursive(p, dir);
            return true;
        }
        return false;
    }

    private void MoveRecursive((int X, int Y) p, (int X, int Y) dir)
    {
        (int X, int Y) nextPos = (p.X + dir.X, p.Y + dir.Y);
        if (_map[p.Y][p.X] == '.')
            return;
        if (dir.Y == 0)
        {
            MoveRecursive(nextPos, dir);
        }
        else
        {
            if (_map[nextPos.Y][nextPos.X] == '[')
            {
                MoveRecursive(nextPos, dir);
                MoveRecursive((nextPos.X + 1, nextPos.Y), dir);
            }
            if (_map[nextPos.Y][nextPos.X] == ']')
            {
                MoveRecursive(nextPos, dir);
                MoveRecursive((nextPos.X - 1, nextPos.Y), dir);
            }
        }
        (_map[p.Y][p.X], _map[nextPos.Y][nextPos.X]) = (_map[nextPos.Y][nextPos.X], _map[p.Y][p.X]);
    }

    private bool CanMoveExpanded((int X, int Y) p, (int X, int Y) dir)
    {
        (int X, int Y) nextPos = (p.X + dir.X, p.Y + dir.Y);
        if (!InBounds(nextPos)) return false;
        if (_map[nextPos.Y][nextPos.X] == '.')
            return true;
        if (dir.Y == 0)
        {
            return CanMoveExpanded(nextPos, dir);
        }
        if (_map[nextPos.Y][nextPos.X] == '[')
        {
            return CanMoveExpanded(nextPos, dir) && CanMoveExpanded((nextPos.X + 1, nextPos.Y), dir);
        }
        if (_map[nextPos.Y][nextPos.X] == ']')
        {
            return CanMoveExpanded(nextPos, dir) && CanMoveExpanded((nextPos.X - 1, nextPos.Y), dir);
        }
        return false;
    }

    private bool TryMove((int X, int Y) p, (int X, int Y) dir, out (int X, int Y) nextPos)
    {
        nextPos = (p.X + dir.X, p.Y + dir.Y);
        if (!InBounds(nextPos)) return false;
        if (_map[nextPos.Y][nextPos.X] == '.')
        {
            (_map[p.Y][p.X], _map[nextPos.Y][nextPos.X]) = (_map[nextPos.Y][nextPos.X], _map[p.Y][p.X]);
            return true;
        }
        else if (TryMove(nextPos, dir, out var _))
        {
            (_map[p.Y][p.X], _map[nextPos.Y][nextPos.X]) = (_map[nextPos.Y][nextPos.X], _map[p.Y][p.X]);
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool InBounds((int X, int Y) p)
    {
        if (p.X < 0 || p.Y < 0 || p.X >= _map[0].Count || p.Y >= _map.Count)
            return false;
        if (_map[p.Y][p.X] == '#')
            return false;
        return true;
    }

}
