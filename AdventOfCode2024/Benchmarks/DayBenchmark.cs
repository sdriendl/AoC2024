using AdventOfCode2024.Days;
using AoCHelper;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode2024.Benchmarks;

[MemoryDiagnoser]
public class DayBenchmark
{
    private readonly Day01 _day01 = new();

    [Benchmark]
    public ValueTask<string> Day1Part1() => _day01.Solve_1();
    
    [Benchmark]
    public ValueTask<string> Day1Part1Linq() => _day01.Solve_1_Linq();
    
    [Benchmark]
    public ValueTask<string> Day1Part2() => _day01.Solve_2();
}