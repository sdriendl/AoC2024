using AdventOfCode.Common;

namespace AdventOfCode2024.Days;

public sealed class Day17 : CustomInputPathBaseDay
{
    public int[] program = [2, 4, 1, 2, 7, 5, 4, 7, 1, 3, 5, 5, 0, 3, 3, 0];
    public Day17()
    {
        Initialize();
    }

    protected override void Initialize()
    {
    }

    public override async ValueTask<string> Solve_1()
    {
        return string.Join(",", Run(32773, 0, 0));
    }

    public override async ValueTask<string> Solve_2()
    {
        return Quine(0, 0, program).Min().ToString();
    }

    private IEnumerable<long> Quine(long regA, int n, int[] program)
    {
        if (Run(regA, 0, 0).SequenceEqual(program))
        {
            yield return regA;
        }

        if (Run(regA, 0, 0).SequenceEqual(program.TakeLast(n)) || n == 0)
        {
            for (var i = 0; i < 8; i++)
            {
                foreach (var q in Quine(8 * regA + i, n + 1, program))
                {
                    yield return q;
                }
            }
        }
    }

    public IEnumerable<int> Run(long a, long b, long c)
    {
        do
        {
            b = a % 8;
            b ^= 2;
            c = a / (1 << (int)b);
            b ^= c;
            b ^= 3;
            yield return (int)(b % 8);
            a /= 1 << 3;
        } while (a != 0);
    }
}