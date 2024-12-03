using System.Text.RegularExpressions;

namespace AdventOfCode2024.Days;

public sealed class Day03 : CustomInputPathBaseDay
{
    private string _input = string.Empty;
    public Day03()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var rx = new Regex(@"mul\((?<FirstNumber>\d{1,3}),(?<SecondNumber>\d{1,3})\)");
        var matches = rx.Matches(_input);

        var result = matches.Sum(m =>
                      int.Parse(m.Groups["FirstNumber"].Value) *
                      int.Parse(m.Groups["SecondNumber"].Value));

        return new ValueTask<string>(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var rx = new Regex(@"(mul\((?<FirstNumber>\d{1,3}),(?<SecondNumber>\d{1,3})\))|do\(\)|don't\(\)");
        var matches = rx.Matches(_input);

        var enabled = true;
        var result = 0;
        foreach (Match match in matches)
        {
            var groups = match.Groups;
            if (groups[0].Value == "don't()")
            {
                enabled = false;
            }
            else if (groups[0].Value == "do()")
            {
                enabled = true;
            }
            else if (enabled)
            {
                result += int.Parse(groups["FirstNumber"].Value) *
                          int.Parse(groups["SecondNumber"].Value);
            }
        }

        return new ValueTask<string>(result.ToString());
    }

}
