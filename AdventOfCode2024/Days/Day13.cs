using AdventOfCode.Common;
using Sprache;

namespace AdventOfCode2024.Days;

public sealed class Day13 : CustomInputPathBaseDay
{
    private IEnumerable<Machine> _input;
    public Day13()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        _input = Machine.MachineParser.Token().Many().Parse(File.ReadAllText(InputFilePath));
    }

    public async override ValueTask<string> Solve_1()
    {
        return _input
            .Sum(Tokens)
            .ToString();
    }

    public async override ValueTask<string> Solve_2()
    {
        return _input
            .Select(machine => machine with
            {
                P = machine.P with
                {
                    X = machine.P.X + 10000000000000,
                    Y = machine.P.Y + 10000000000000
                }
            })
            .Sum(Tokens)
            .ToString();
    }

    private long Tokens(Machine machine)
    {
        var m = new long[,]
        {
            { machine.A.X, machine.B.X },
            { machine.A.Y, machine.B.Y}
        };
        var v = new long[]
        {
            machine.P.X, machine.P.Y,
        };
        var (pa, pb) = Solve(m, v);
        if (pa * machine.A.X + pb * machine.B.X != machine.P.X
            || pa * machine.A.Y + pb * machine.B.Y != machine.P.Y)
        {
            return 0;
        }
        return pa * 3 + pb;
    }

    private (long pa, long pb) Solve(long[,] m, long[] v)
    {
        var det = 1.0 / (m[0, 0] * m[1, 1] - m[0, 1] * m[1, 0]);
        var invA = new double[,]
        {
            { det * m[1, 1], -det * m[0, 1] },
            { -det * m[1, 0], det * m[0, 0] }
        };
        var pa = v[0] * invA[0, 0] + v[1] * invA[0, 1];
        var pb = v[0] * invA[1, 0] + v[1] * invA[1, 1];
        return ((long)Math.Round(pa), (long)Math.Round(pb));
    }

    private record Machine(Button A, Button B, Prize P)
    {
        public static Parser<Machine> MachineParser
            => from buttonA in Button.ButtonParser("A").Token()
               from buttonB in Button.ButtonParser("B").Token()
               from prize in Prize.PrizeParser.Token()
               select new Machine(buttonA, buttonB, prize);
    }
    private record Button(long X, long Y)
    {
        public static Parser<Button> ButtonParser(string name)
            => from _ in Parse.String($"Button {name}:").Token()
               from x in Parse.String("X+").Then(_ => ParseDigits())
               from _2 in Parse.Char(',').Token()
               from y in Parse.String("Y+").Then(_ => ParseDigits())
               select new Button(x, y);
    }
    private record Prize(long X, long Y)
    {
        public static Parser<Prize> PrizeParser
            => from _ in Parse.String("Prize:").Token()
               from x in Parse.String("X=").Then(_ => ParseDigits())
               from _2 in Parse.Char(',').Token()
               from y in Parse.String("Y=").Then(_ => ParseDigits())
               select new Prize(x, y);
    }

    private static Parser<long> ParseDigits()
        => Parse.Digit.Many().Text().Select(long.Parse);
}
