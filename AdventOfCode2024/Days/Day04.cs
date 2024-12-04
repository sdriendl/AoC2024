using AdventOfCode.Common;

namespace AdventOfCode2024.Days;

public sealed class Day04 : CustomInputPathBaseDay
{
    List<List<char>> _input = new();
    public Day04()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        _input = new();
        foreach (var line in File.ReadAllLines(InputFilePath))
        {
            _input.Add(line.ToCharArray().ToList());
        }
    }

    public override ValueTask<string> Solve_1()
    {
        var result = CountXmasSubstrings(_input);
        return new ValueTask<string>(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var result = CountMaxXs(_input);
        return new ValueTask<string>(result.ToString());
    }

    private int CountMaxXs(List<List<char>> input)
    {
        var count = 0;
        var height = input.Count;
        var width = input[0].Count;
        foreach (var (x, y) in FindSymbol(input, 'A'))
        {
            var inBounds = x >= 1 && y >= 1 && x < width - 1 && y < height - 1;
            if (!inBounds)
            {
                continue;
            }

            char nw = input[y - 1][x - 1];
            char se = input[y + 1][x + 1];
            char sw = input[y + 1][x - 1];
            char ne = input[y - 1][x + 1];


            if ((nw == 'M' && se == 'S'
                || nw == 'S' && se == 'M') && 
                (sw == 'M' && ne == 'S'
                || sw == 'S' && ne == 'M'))
            {
                count++;
            }
        }

        return count;
    }

    private int CountXmasSubstrings(List<List<char>> input)
    {
        ReadOnlySpan<(int Y, int X)> directions = [
            (-1, -1), (-1, 0), (-1, 1),
            (0, -1), (0, 1),
            (1, -1), (1, 0), (1, 1),
        ];

        var count = 0;
        var height = input.Count;
        var width = input[0].Count;
        foreach (var (x, y) in FindSymbol(input, 'X'))
        {
            foreach (var (dy, dx) in directions)
            {
                if (CheckBounds(x + dx * 3, y + dy * 3, width, height) &&
                    input[y + dy * 1][x + dx * 1] == 'M' &&
                    input[y + dy * 2][x + dx * 2] == 'A' &&
                    input[y + dy * 3][x + dx * 3] == 'S')
                {
                    count++;
                }

            }

        }

        return count;
    }

    private IEnumerable<(int X, int Y)> FindSymbol(List<List<char>> m, char c)
    {
        for (int y = 0; y < m.Count; y++)
        {
            for (int x = 0; x < m[y].Count; x++)
            {
                if (m[y][x] == c)
                {
                    yield return (x, y);
                }
            }
        }
    }

    private bool CheckBounds(int x, int y, int width, int height)
    {
        return x >= 0 && y >= 0 && x < width && y < height;
    }
}
