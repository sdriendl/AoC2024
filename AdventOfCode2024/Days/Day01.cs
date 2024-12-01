namespace AdventOfCode2024.Days; 
public sealed class Day01 : CustomInputPathBaseDay
{
    private int[]? _left;
    private int[]? _right;

    public Day01()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        var input = File.ReadAllLines(InputFilePath);
        var n = input.Length;
        _left = new int[n];
        _right = new int [n];
        for (var i = 0; i < n; i++)
        {
            var s = input[i].Split("   ");
            _left[i] = int.Parse(s[0]);
            _right[i] = int.Parse(s[1]);
        }
    }

    public override ValueTask<string> Solve_1()
    {
        Array.Sort(_left!);
        Array.Sort(_right!);

        var difference = 0;
        for (int i = 0; i < _left!.Length; i++)
        {
            difference += Math.Abs(_left[i] - _right![i]);
        }

        return new ValueTask<string>(difference.ToString());
    }

    public ValueTask<string> Solve_1_Linq()
    {
        var difference = _left!.Order()
            .Zip(_right!.Order())
            .Sum(t => Math.Abs(t.First - t.Second));

        return new ValueTask<string>(difference.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var counts = _right!
            .GroupBy(x => x)
            .ToDictionary(g => g.Key, g => g.Count());
        
        var similarity = _left!.Sum(n => n * counts.GetValueOrDefault(n, 0));

        return new ValueTask<string>(similarity.ToString());
    }
}