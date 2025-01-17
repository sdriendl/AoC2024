using AdventOfCode.Common;

namespace AdventOfCode2019.Days;

public sealed class Day08 : CustomInputPathBaseDay
{
    private string _input;

    public Day08()
    {
        Initialize();
    }
    protected override void Initialize()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public async override ValueTask<string> Solve_1()
    {
        var layers = _input.Chunk(25 * 6).Where(l => l.Length > 1);
        var minLayer = layers.MinBy(x => x.Count(c => c == '0'));
        return (minLayer.Count(c => c == '1') * minLayer.Count(c => c == '2')).ToString();
    }

    public async override ValueTask<string> Solve_2()
    {
        var layers = _input.Chunk(25 * 6).Where(l => l.Length > 1);

        char[][] result = new char[6][];
        for (int i = 0; i < 6; i++)
        {
            result[i] = new char[25];
        }
        for (int y = 0; y < 6; y++)
        {
            for (int x = 0; x < 25; x++) 
            {
                result[y][x] = Condense(layers.Select(l => l[y * 25 + x]));
            }
        }
        return string.Join("\n", result.Select(l => string.Join("", l.Select(c => c switch 
        {
            '0' => ' ',
            '1' => '#',
            '2' => ' ',
        }))));
    }
    private char Condense(IEnumerable<char> input)
    {
        var output = input.FirstOrDefault(c => c != '2');
        return output == default(char) ? ' ' : output;
    }
}
