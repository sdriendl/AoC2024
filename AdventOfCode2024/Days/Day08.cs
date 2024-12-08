﻿using AdventOfCode.Common;
using MoreLinq;

namespace AdventOfCode2024.Days;

public sealed class Day08 : CustomInputPathBaseDay
{
    private List<Antenna> _input;
    private int _width = 0;
    private int _height = 0;

    public Day08()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        _input = [];
        var input = File.ReadAllLines(InputFilePath);
        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == '.') continue;
                _input.Add(new Antenna(input[y][x], x, y));
            }
        }

        _width = input[0].Length;
        _height = input.Length;
    }

    public override async ValueTask<string> Solve_1()
    {
        HashSet<Antenna> antiSpots = [];
        foreach (var freq in _input.GroupBy(x => x.Frequency))
        {
            freq.Subsets(2)
                .SelectMany(x => new[]
                {
                    new Antenna('#', x[0].X - (x[1].X - x[0].X), x[0].Y - (x[1].Y - x[0].Y)),
                    new Antenna('#', x[1].X - (x[0].X - x[1].X), x[1].Y - (x[0].Y - x[1].Y))
                })
                .Where(AntennaInBounds)
                .ForEach(x => antiSpots.Add(x));
        }

        return antiSpots.Count.ToString();
    }

    public override async ValueTask<string> Solve_2()
    {
        HashSet<Antenna> antiSpots = [];
        foreach (var freq in _input.GroupBy(x => x.Frequency))
        {
            freq.Subsets(2)
                .SelectMany(x => AntiSpotsInLine(x[0], x[1]))
                .Where(AntennaInBounds)
                .ForEach(x => antiSpots.Add(x));
        }

        return antiSpots.Count.ToString();
    }

    private bool AntennaInBounds(Antenna a)
    {
        return a is { X: >= 0, Y: >= 0 } && a.X < _width && a.Y < _height;
    }

    private IEnumerable<Antenna> AntiSpotsInLine(Antenna a, Antenna b)
    {
        var k = 0;
        while (AntennaInBounds(new Antenna('#', a.X - k * (b.X - a.X), a.Y - k * (b.Y - a.Y))))
        {
            yield return new Antenna('#', a.X - k * (b.X - a.X), a.Y - k * (b.Y - a.Y));
            k++;
        }

        k = 0;
        while (AntennaInBounds(new Antenna('#', b.X - k * (a.X - b.X), b.Y - k * (a.Y - b.Y))))
        {
            yield return new Antenna('#', b.X - k * (a.X - b.X), b.Y - k * (a.Y - b.Y));
            k++;
        }
    }
    
    private record Antenna(char Frequency, int X, int Y);
}