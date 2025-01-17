using AdventOfCode2019.Days;
using AoCHelper;


//// JIT warm up, to get results closer to full BDN benchmark
for (int i = 0; i < 1; i++)
{
    await Solver.SolveAll(opt =>
    {
        opt.ShowTotalElapsedTimePerDay = true;
        opt.ShowConstructorElapsedTime = true;
    });
}