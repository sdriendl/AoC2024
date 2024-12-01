using AdventOfCode2024.Benchmarks;
using AoCHelper;
using BenchmarkDotNet.Running;

await Solver.SolveAll(opt =>
{
    opt.ShowTotalElapsedTimePerDay = true;
    opt.ShowConstructorElapsedTime = true;
});
