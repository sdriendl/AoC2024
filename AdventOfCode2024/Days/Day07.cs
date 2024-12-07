using AdventOfCode.Common;
using MoreLinq;

namespace AdventOfCode2024.Days;

public sealed class Day07 : CustomInputPathBaseDay
{
    private IEnumerable<Equation> _input;

    public Day07()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        _input = File.ReadAllLines(InputFilePath).Select(Equation.Parse);
    }

    public override async ValueTask<string> Solve_1()
    {
        List<Func<long, long, long>> ops =
        [
            (a, b) => a * b,
            (a, b) => a + b
        ];
        return _input
            .AsParallel()
            .Where(x => EquationSatisifiable(x, ops))
            .Sum(e => e.Target)
            .ToString();
    }

    public override async ValueTask<string> Solve_2()
    {
        List<Func<long, long, long>> ops =
        [
            (a, b) => a * b,
            (a, b) => a + b,
            (a, b) =>
            {
                var p = (int)Math.Pow(10, b.ToString().Length);
                return a * p + b;
            }
        ];
        return _input
            .AsParallel()
            .Where(x => EquationSatisifiable(x, ops))
            .Sum(e => e.Target)
            .ToString();
    }

    private bool EquationSatisifiable(Equation equation, List<Func<long, long, long>> ops)
    {
        var combinations = ops.CartesianPower(equation.Values.Count - 1);

        return combinations.Any(combination =>
        {
            return equation.Values.Skip(1).Zip(combination)
                       .Aggregate(equation.Values.First(), (acc, x) => x.Second(acc, x.First))
                   == equation.Target;
        });
    }

    private record Equation(long Target, List<long> Values)
    {
        public static Equation Parse(string line)
        {
            var parts = line.Split(':', StringSplitOptions.RemoveEmptyEntries);

            var target = long.Parse(parts[0].Trim());

            var values = parts[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToList();

            return new Equation(target, values);
        }
    }
}