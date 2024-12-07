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
        Func<long, long, long>[] ops =
        [
            (a, b) => a * b,
            (a, b) => a + b
        ];
        return _input
            .AsParallel()
            .Where(x => EquationSatisfiable(x.Target, x.Values, 0,0, ops))
            .Sum(e => e.Target)
            .ToString();
    }

    public override async ValueTask<string> Solve_2()
    {
        Func<long, long, long>[] ops =
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
            .Where(x => EquationSatisfiable(x.Target, x.Values, 0,0, ops))
            .Sum(e => e.Target)
            .ToString();
    }


    private bool EquationSatisfiable(long target, long[] numbers, long result, int index, Func<long, long, long>[] ops)
    {
        if (index == numbers.Length) return result == target;
        if (result > target) return false;
       
        return ops.Any(op 
            => EquationSatisfiable(target, numbers, op(result, numbers[index]), index + 1, ops));
    }
    
    private record Equation(long Target, long[] Values)
    {
        public static Equation Parse(string line)
        {
            var parts = line.Split(':', StringSplitOptions.RemoveEmptyEntries);

            var target = long.Parse(parts[0].Trim());

            var values = parts[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToArray();

            return new Equation(target, values);
        }
    }
}