using AdventOfCode.Common;

namespace AdventOfCode2019.Days;

public sealed class Day09 : CustomInputPathBaseDay
{
    private string _program;

    public Day09()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        _program = File.ReadAllText(InputFilePath);
    }

    public async override ValueTask<string> Solve_1()
    {
        var vm = new IntCode(_program);
        vm.Run([1]);
        return vm.OutputBuffer.Dequeue().ToString();
    }

    public async override ValueTask<string> Solve_2()
    {
        var vm = new IntCode(_program);
        vm.Run([2]);
        return vm.OutputBuffer.Dequeue().ToString();
    }
}
