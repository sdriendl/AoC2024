using AdventOfCode2024.Benchmarks;
using AoCHelper;
using BenchmarkDotNet.Running;


// JIT warm up, to get results closer to full BDN benchmark
for (int i = 0; i < 25; i++)
{
    await Solver.SolveAll(opt =>
    {
        opt.ShowTotalElapsedTimePerDay = true;
        opt.ShowConstructorElapsedTime = true;
    });
}
