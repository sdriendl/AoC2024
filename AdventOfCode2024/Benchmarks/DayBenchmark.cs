﻿using AdventOfCode2024.Days;
using AoCHelper;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024.Benchmarks;

[MemoryDiagnoser, ShortRunJob]
public class DayBenchmark
{
    private readonly Day01 _day01 = new();
    private readonly Day02 _day02 = new();
    private readonly Day03 _day03 = new();
    private readonly Day04 _day04 = new();

    [Benchmark]
    public ValueTask<string> Day1Part1() => _day01.Solve_1();
    [Benchmark]
    public ValueTask<string> Day1Part1Linq() => _day01.Solve_1_Linq();
    [Benchmark]
    public ValueTask<string> Day1Part2() => _day01.Solve_2();
    
    [Benchmark]
    public ValueTask<string> Day2Part1() => _day02.Solve_1();
    [Benchmark]
    public ValueTask<string> Day2Part2() => _day02.Solve_2();

    [Benchmark]
    public ValueTask<string> Day3Part1() => _day03.Solve_1();
    [Benchmark]
    public ValueTask<string> Day3Part2() => _day03.Solve_2();

    [Benchmark]
    public ValueTask<string> Day4Part1() => _day04.Solve_1();
    [Benchmark]
    public ValueTask<string> Day4Part2() => _day04.Solve_2();
}