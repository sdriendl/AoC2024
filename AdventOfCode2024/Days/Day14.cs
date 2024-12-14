using System.Text.RegularExpressions;
using AdventOfCode.Common;

namespace AdventOfCode2024.Days;

public sealed partial class Day14 : CustomInputPathBaseDay
{
    public int Width { get; set; } = 101;
    public int Height { get; set; } = 103;
    private List<Robot> _input;

    public Day14()
    {
        Initialize();


    }

    protected override void Initialize()
    {
        _input = File.ReadLines(InputFilePath)
                     .Select(Robot.Parse)
                     .ToList();
    }

    public override async ValueTask<string> Solve_1()
    {
        Initialize();
        for (int i = 0; i < 100; i++)
        {
            foreach (var robot in _input)
            {
                robot.Move(Width, Height);
            }
        }

        var q1 = _input.Count(r => r.Position.X < Width / 2 && r.Position.Y < Height / 2);
        var q2 = _input.Count(r => r.Position.X > Width / 2 && r.Position.Y < Height / 2);
        var q3 = _input.Count(r => r.Position.X < Width / 2 && r.Position.Y > Height / 2);
        var q4 = _input.Count(r => r.Position.X > Width / 2 && r.Position.Y > Height / 2);

        return (q1 * q2 * q3 * q4).ToString();
    }

    public override async ValueTask<string> Solve_2()
    {
        double ClusteringScore()
        {
            var sqMag = _input
                .Select(r => r.Position.X * r.Position.X + r.Position.Y * r.Position.Y)
                .ToList();
            double average = sqMag.Average();
            double sumOfSquaresOfDifferences = sqMag
                                               .Select(val => (val - average) * (val - average))
                                               .Sum();
            double sd = Math.Sqrt(sumOfSquaresOfDifferences / sqMag.Count);
            return sd;
        }

        Initialize();
        var it = 0;
        var entropy = double.MaxValue;
        while (true)
        {
            it++;
            foreach (var robot in _input)
            {
                robot.Move(Width, Height);
            }

            var newValue = ClusteringScore();
            if (newValue < 3100) // magic value, found by inspection
            {
                return it.ToString();
            }
            entropy = Math.Min(entropy, newValue);
        }
    }

    private partial class Robot
    {
        public (int X, int Y) Position { get; set; }
        public (int X, int Y) Velocity { get; set; }

        public static Robot Parse(string line)
        {
            var rx = RobotRegex();
            var match = rx.Match(line);
            if (match.Success)
            {
                return new Robot
                {
                    Position = (int.Parse(match.Groups["p1"].Value), int.Parse(match.Groups["p2"].Value)),
                    Velocity = (int.Parse(match.Groups["v1"].Value), int.Parse(match.Groups["v2"].Value)),
                };
            }

            throw new ArgumentException(line);
        }

        public void Move(int w, int h)
        {
            Position = (Position.X + Velocity.X, Position.Y + Velocity.Y);
            Position = (Mod(Position.X, w), Mod(Position.Y, h));
        }

        private int Mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        [GeneratedRegex(@"p=(?<p1>-?\d+),(?<p2>-?\d+)\s+v=(?<v1>-?\d+),(?<v2>-?\d+)")]
        private static partial Regex RobotRegex();
    }
}