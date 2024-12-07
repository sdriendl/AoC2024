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
        // For use with EquationSatisfiable, elegant, but sadly slow
        Func<long, long, long>[] ops =
        [
            (a, b) => a * b,
            (a, b) => a + b
        ];

        return _input
            .AsParallel()
            .Where(x => EquationSatisfiableAddMul(x.Target, x.Values, 0, 0))
            .Sum(e => e.Target)
            .ToString();
    }

    public override async ValueTask<string> Solve_2()
    {
        // For use with EquationSatisfiable, elegant, but sadly slow
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
            .Where(x => EquationSatisfiableAddMulConcat(x.Target, x.Values, 0, 0))
            .Sum(e => e.Target)
            .ToString();
    }


    private bool EquationSatisfiable(long target, long[] values, long result, int index, Func<long, long, long>[] ops)
    {
        if (index == values.Length) return result == target;
        if (result > target) return false;

        return ops.Any(op
            => EquationSatisfiable(target, values, op(result, values[index]), index + 1, ops));
    }

    private bool EquationSatisfiableAddMulConcat(long target, long[] values, long result, int index)
    {
        if (index == values.Length) return result == target;
        if (result > target) return false;

        return EquationSatisfiableAddMulConcat(target, values, result + values[index], index + 1)
               || EquationSatisfiableAddMulConcat(target, values, result * values[index], index + 1)
               || EquationSatisfiableAddMulConcat(target, values, Concat(result, values[index]), index + 1);

        long Concat(long a, long b)
        {
            var p = (int)Math.Pow(10, b.ToString().Length);
            return a * p + b;
        }
    }

    private bool EquationSatisfiableAddMul(long target, long[] values, long result, int index)
    {
        if (index == values.Length) return result == target;
        if (result > target) return false;

        return EquationSatisfiableAddMul(target, values, result + values[index], index + 1)
               || EquationSatisfiableAddMul(target, values, result * values[index], index + 1);
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