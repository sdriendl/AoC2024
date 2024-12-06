﻿using AdventOfCode.Common;
using System.Linq;

namespace AdventOfCode2024.Days;

public sealed class Day06 : CustomInputPathBaseDay
{
    private List<List<char>> _input = new();
    private Board _board;

    public Day06()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        _input.Clear();
        foreach (var line in File.ReadLines(InputFilePath))
        {
            _input.Add(line.ToCharArray().ToList());
        }
        _board = new Board(_input);
    }

    public override async ValueTask<string> Solve_1()
    {
        var result = _board.FindPath().Count();
        return result.ToString();
    }
    public override async ValueTask<string> Solve_2()
    {
        var path = _board.FindPath();
        var result = path.AsParallel().Count(obstruction => _board.HasLoop(obstruction));
        return result.ToString();
    }

    private class Guard
    {
        public (int X, int Y) Position;
        public (int X, int Y) Direction;

        public void Turn()
        {
            Direction = Direction switch
            {
                (-1, 0) => (0, -1),
                (0, -1) => (1, 0),
                (1, 0) => (0, 1),
                (0, 1) => (-1, 0),
                _ => throw new ArgumentException($"invalid direction {Direction}")
            };
        }

        public (int X, int Y) NextPosition =>
            (Position.X + Direction.X, Position.Y + Direction.Y);

        public void Move()
        {
            Position = NextPosition;
        }
    }

    private class Board
    {
        public List<List<char>> Tiles { get; private set; }
        public (int X, int Y) GuardPosition { get; protected set; }
        public (int X, int Y) GuardDirection { get; protected set; }

        public Board(List<List<char>> input)
        {
            Tiles = new List<List<char>>();
            for (int y = 0; y < input.Count; y++)
            {
                var newLine = new List<char>();
                for (var x = 0; x < input[0].Count; x++)
                {
                    newLine.Add('.');
                }
                Tiles.Add(newLine);
            }
            for (int y = 0; y < input.Count; y++)
            {
                for (int x = 0; x < input[y].Count; x++)
                {
                    var sym = input[y][x];
                    if (sym == '.' || sym == '#')
                    {
                        Tiles[y][x] = sym;
                    }
                    if (new char[] { '<', '^', '>', 'v' }.Contains(sym))
                    {
                        Tiles[y][x] = sym;
                        GuardPosition = (x, y);
                        GuardDirection = DirectionFromGlyph(sym);
                    }
                }
            }
        }


        private (int X, int Y) DirectionFromGlyph(char glyph)
            => glyph switch
            {
                '<' => (-1, 0),
                '^' => (0, -1),
                '>' => (1, 0),
                'v' => (0, 1),
            };

        private CheckResult CheckGuardPath(Guard guard)
        {
            if (guard.NextPosition.X < 0 || guard.NextPosition.Y < 0 || guard.NextPosition.X >= Tiles[0].Count || guard.NextPosition.Y >= Tiles.Count)
            {
                return CheckResult.LeftPath;
            }
            if (Tiles[guard.NextPosition.Y][guard.NextPosition.X] == '#')
            {
                return CheckResult.HitWall;
            }
            return CheckResult.Free;
        }

        private CheckResult CheckGuardPathObstructed(Guard guard, (int X, int Y) obstruction)
        {
            if (guard.NextPosition.X < 0 || guard.NextPosition.Y < 0 || guard.NextPosition.X >= Tiles[0].Count || guard.NextPosition.Y >= Tiles.Count)
            {
                return CheckResult.LeftPath;
            }
            if (Tiles[guard.NextPosition.Y][guard.NextPosition.X] == '#')
            {
                return CheckResult.HitWall;
            }
            if (guard.NextPosition.X == obstruction.X && guard.NextPosition.Y == obstruction.Y)
            {
                return CheckResult.HitWall;
            }
            return CheckResult.Free;
        }

        public ISet<(int X, int Y)> FindPath()
        {
            var visited = new HashSet<(int X, int Y)>();
            var guard = new Guard()
            {
                Position = GuardPosition,
                Direction = GuardDirection,
            };
            while (true)
            {
                visited.Add((guard.Position.X, guard.Position.Y));
                var checkResult = CheckGuardPath(guard);
                if (checkResult == CheckResult.LeftPath)
                {
                    return visited;
                }
                else if (checkResult == CheckResult.HitWall)
                {
                    guard.Turn();
                }
                else if (checkResult == CheckResult.Free)
                {
                    guard.Move();
                }
            }
        }

        public bool HasLoop((int X, int Y) obstruction)
        {
            var guard = new Guard()
            {
                Position = GuardPosition,
                Direction = GuardDirection,
            };
            var guardState = new HashSet<(int X, int Y, int dx, int dy)>();
            while (true)
            {
                if (guardState.Contains((guard.Position.X, guard.Position.Y, guard.Direction.X, guard.Direction.Y)))
                {
                    return true;
                }
                guardState.Add((guard.Position.X, guard.Position.Y, guard.Direction.X, guard.Direction.Y));
                var checkResult = CheckGuardPathObstructed(guard, obstruction);
                if (checkResult == CheckResult.LeftPath)
                {
                    return false;
                }
                else if (checkResult == CheckResult.HitWall)
                {
                    guard.Turn();
                }
                else if (checkResult == CheckResult.Free)
                {
                    guard.Move();
                }
            }
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Tiles.Select(l => string.Join("", l)));
        }
    }
    private enum CheckResult
    {
        Free,
        HitWall,
        LeftPath
    }
}