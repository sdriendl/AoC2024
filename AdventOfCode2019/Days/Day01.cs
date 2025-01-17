using AdventOfCode.Common;
using System.Text;

namespace AdventOfCode2019.Days;

public sealed class Day01 : CustomInputPathBaseDay
{
    private IEnumerable<int> _input;

    public Day01()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        _input = File.ReadAllLines(InputFilePath).Select(int.Parse);
    }

    public async override ValueTask<string> Solve_1()
    {
        return _input.Sum(n => n/3 - 2).ToString();
    }

    public async override ValueTask<string> Solve_2()
    {
        return _input.Sum(FuelCosts).ToString();
    }

    private int FuelCosts(int n)
    {
        var sum = 0;
        n = n / 3 - 2;
        while (n > 0)
        {
            sum += n;
            n = n / 3 - 2;
        }
        return sum;
    }
}
