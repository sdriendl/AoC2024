using AdventOfCode.Common;
using MoreLinq;

namespace AdventOfCode2019.Days;

public sealed class Day07 : CustomInputPathBaseDay
{
    private string _input;
    private static readonly int[] phases = [0, 1, 2, 3, 4];
    private static readonly int[] chainPhases = [5, 6, 7, 8, 9];

    protected override void Initialize()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public async override ValueTask<string> Solve_1()
    {
        var vm = new IntCode(_input);
        return phases.Permutations().Select(p =>
         {
             var o1 = Amp(p[0], 0, vm);
             vm.Reset();
             var o2 = Amp(p[1], o1, vm);
             vm.Reset();
             var o3 = Amp(p[2], o2, vm);
             vm.Reset();
             var o4 = Amp(p[3], o3, vm);
             vm.Reset();
             var o5 = Amp(p[4], o4, vm);
             vm.Reset();
             return o5;
         }).Max().ToString();
    }



    public async override ValueTask<string> Solve_2()
    {
        return chainPhases.Permutations().Select(ChainedAmps).Max().ToString();
    }

    private long Amp(int phase, long input, IntCode vm)
    {
        vm.Run([phase, input]);
        var output = vm.OutputBuffer.Dequeue();
        return output;
    }

    private long ChainedAmps(IList<int> phases)
    {
        List<IntCode> vms = phases.Select(phase =>
        {
            var vm = new IntCode(_input);
            vm.Run([phase]);
            return vm;
        }).ToList();

        var n = 0;
        var output = 0l;
        while (vms.Last().State != IntCode.ProgramState.Halted)
        {
            var vm = vms[n];
            vm.Run([output]);
            output = vm.OutputBuffer.Dequeue();
            n++;
            n %= vms.Count;
        }

        return output;
    }

}
