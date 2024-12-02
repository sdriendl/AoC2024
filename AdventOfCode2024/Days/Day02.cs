﻿namespace AdventOfCode2024.Days;

public class Day02 : CustomInputPathBaseDay
{
    private List<List<int>> _input;

    public Day02()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        _input = [];
        var input = File.ReadAllLines(InputFilePath);
        foreach (var line in input) 
        {
            _input.Add(line.Split(" ").Select(int.Parse).ToList());
        }
        return;
    }

    public override ValueTask<string> Solve_1()
    {
        var safe = 0;
        foreach (var line in _input)
        {
            if (IsSafe(line)) safe++;
        }
        return new ValueTask<string>(safe.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var safe = 0;
        foreach (var line in _input)
        {
            if (RemoveSteps(line).Any(IsSafe))
            {
                safe++;
            }
        }
        return new ValueTask<string>(safe.ToString());
    }

    private static IEnumerable<List<int>> RemoveSteps(List<int> steps)
    {
        for(int i = 0; i < steps.Count; i++)
        {
            var newSteps = steps.ToList();
            newSteps.RemoveAt(i);
            yield return newSteps;
        }
    }

    private static bool IsSafe(List<int> line)
    {
        var increasing = true;
        var decreasing = true;
        var levelDifferenceSafe = true;
        for (var i = 0; i < line.Count - 1; i++)
        {
            var levelDiff = line[i] - line[i + 1];
            increasing &= levelDiff > 0;
            decreasing &= levelDiff < 0;
            levelDifferenceSafe &= Math.Abs(levelDiff) >= 0 && Math.Abs(levelDiff) <= 3;
        }
        if (levelDifferenceSafe && (increasing ^ decreasing))
        {
            return true;
        }
        return false;
    }
}
