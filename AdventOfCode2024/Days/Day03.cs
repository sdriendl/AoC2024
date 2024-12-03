using System.Text.RegularExpressions;

namespace AdventOfCode2024.Days;

public class Day03 : CustomInputPathBaseDay
{
    private string _input;
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
        List<(int, int)> numbers = [];
        var rx = new Regex(@"mul\((\d+),(\d+)\)");
        var matches = rx.Matches(_input);
        
        foreach(Match match in matches)
        {
            var groups = match.Groups;
            numbers.Add((int.Parse(groups[1].Value), 
                         int.Parse(groups[2].Value)));
        }
        
        var result = numbers.Sum(t => t.Item1 * t.Item2).ToString();
        return new ValueTask<string>(result);
    }

    public override ValueTask<string> Solve_2()
    {
        List<(int, int)> numbers = [];
        var rx = new Regex(@"(mul\((\d+),(\d+)\))|do\(\)|don't\(\)");
        var matches = rx.Matches(_input);

        var enabled = true;
        foreach(Match match in matches)
        {
            var groups = match.Groups;
            if (groups[0].Value == "don't()")
            {
                enabled = false;
                continue;
            }

            if (groups[0].Value == "do()")
            {
                enabled = true;
                continue;
            }

            if (enabled)
            {
                numbers.Add((int.Parse(groups[2].Value), 
                             int.Parse(groups[3].Value)));
            }
        }

        var result = numbers.Sum(t => t.Item1 * t.Item2).ToString();
        return new ValueTask<string>(result);
    }

}
