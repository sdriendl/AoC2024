using AdventOfCode.Common;
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
        while (_board.Move()) ;
        var result = _board.Marked;
        return result.ToString();
    }
    public override ValueTask<string> Solve_2()
    {
        _board = new Board(_input);
        while (_board.Move()) ;
        var path = _board.Visited.ToList();
        var result = path.Count(obstruction => _board.HasLoop(obstruction));
        return new ValueTask<string>(result.ToString());
    }

    private class Board
    {
        public List<List<char>> Tiles { get; protected set; }
        public (int X, int Y) GuardPosition { get; protected set; }
        public (int X, int Y) GuardDirection { get; protected set; }
        public HashSet<(int X, int Y)> Visited { get; protected set; } = [];
        private HashSet<(int X, int Y, int dx, int dy)> GuardState = [];
        public (int X, int Y) GuardPositionInitial { get; protected set; }
        public (int X, int Y) GuardDirectionInital { get; protected set; }
        public int Marked => Visited.Count;

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
                        GuardPositionInitial = GuardPosition;
                        GuardDirectionInital = GuardDirection;
                    }
                }
            }
        }

        public void Reset()
        {
            Visited.Clear();
            GuardState.Clear();
            Tiles[GuardPosition.Y][GuardPosition.X] = '.';
            GuardPosition = GuardPositionInitial;
            GuardDirection = GuardDirectionInital;
        }

        public void TurnGuard()
        {
            GuardDirection = GuardDirection switch
            {
                (-1, 0) => (0, -1),
                (0, -1) => (1, 0),
                (1, 0) => (0, 1),
                (0, 1) => (-1, 0),
                _ => throw new ArgumentException($"invalid direction {GuardDirection}")
            };
        }

        private char GuardGlpyh =>
            GuardDirection switch
            {
                (-1, 0) => '<',
                (0, -1) => '^',
                (1, 0) => '>',
                (0, 1) => 'v'
            };

        private (int X, int Y) DirectionFromGlyph(char glyph)
            => glyph switch
            {
                '<' => (-1, 0),
                '^' => (0, -1),
                '>' => (1, 0),
                'v' => (0, 1),
            };

        private CheckResult CheckGuardPath()
        {
            var newPosX = GuardPosition.X + GuardDirection.X;
            var newPosY = GuardPosition.Y + GuardDirection.Y;
            if (newPosX < 0 || newPosY < 0 || newPosX >= Tiles[0].Count || newPosY >= Tiles.Count)
            {
                return CheckResult.LeftPath;
            }
            if (Tiles[newPosY][newPosX] == '#')
            {
                return CheckResult.HitWall;
            }
            return CheckResult.Free;
        }

        private CheckResult CheckGuardPathObstructed((int X, int Y) obstruction)
        {
            var newPosX = GuardPosition.X + GuardDirection.X;
            var newPosY = GuardPosition.Y + GuardDirection.Y;
            if (newPosX < 0 || newPosY < 0 || newPosX >= Tiles[0].Count || newPosY >= Tiles.Count)
            {
                return CheckResult.LeftPath;
            }
            if (Tiles[newPosY][newPosX] == '#')
            {
                return CheckResult.HitWall;
            }
            if (newPosX == obstruction.X && newPosY == obstruction.Y)
            {
                return CheckResult.HitWall;
            }
            return CheckResult.Free;
        }

        public bool Move()
        {
            Visited.Add((GuardPosition.X, GuardPosition.Y));
            var checkResult = CheckGuardPath();
            if (checkResult == CheckResult.LeftPath)
            {
                return false;
            }
            else if (checkResult == CheckResult.HitWall)
            {
                TurnGuard();
                Tiles[GuardPosition.Y][GuardPosition.X] = GuardGlpyh;
            }
            else if (checkResult == CheckResult.Free)
            {
                var newPosX = GuardPosition.X + GuardDirection.X;
                var newPosY = GuardPosition.Y + GuardDirection.Y;
                Tiles[GuardPosition.Y][GuardPosition.X] = '.';
                GuardPosition = (newPosX, newPosY);
                Tiles[GuardPosition.Y][GuardPosition.X] = GuardGlpyh;
            }
            return true;
        }

        private LoopResult MoveLoop((int X, int Y) obstruction)
        {
            if (GuardState.Contains((GuardPosition.X, GuardPosition.Y, GuardDirection.X, GuardDirection.Y)))
            {
                return LoopResult.Loop;
            }
            GuardState.Add((GuardPosition.X, GuardPosition.Y, GuardDirection.X, GuardDirection.Y));
            var checkResult = CheckGuardPathObstructed(obstruction);
            if (checkResult == CheckResult.LeftPath)
            {
                return LoopResult.NoLoop;
            }
            else if (checkResult == CheckResult.HitWall)
            {
                TurnGuard();
                Tiles[GuardPosition.Y][GuardPosition.X] = GuardGlpyh;
            }
            else if (checkResult == CheckResult.Free)
            {
                var newPosX = GuardPosition.X + GuardDirection.X;
                var newPosY = GuardPosition.Y + GuardDirection.Y;
                Tiles[GuardPosition.Y][GuardPosition.X] = '.';
                GuardPosition = (newPosX, newPosY);
                Tiles[GuardPosition.Y][GuardPosition.X] = GuardGlpyh;
            }
            return LoopResult.Working;
        }

        public bool HasLoop((int X, int Y) obstruction)
        {
            while (true)
            {
                var res = MoveLoop(obstruction);
                if (res == LoopResult.NoLoop)
                {
                    Reset();
                    return false;
                }
                if (res == LoopResult.Loop)
                {
                    Reset();
                    return true;
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
    private enum LoopResult
    {
        Working,
        NoLoop,
        Loop,
    }
}
