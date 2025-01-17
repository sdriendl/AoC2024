using AdventOfCode.Common;

namespace AdventOfCode2019.Days;

public sealed class Day02 : CustomInputPathBaseDay
{
    private string _input;
    protected override void Initialize()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public async override ValueTask<string> Solve_1()
    {
        var vm = new IntCode(_input);
        vm.Memory[1] = 12;
        vm.Memory[2] = 2;
        vm.Run();
        return vm.Memory[0].ToString();
    }

    public async override ValueTask<string> Solve_2()
    {
        var vm = new IntCode(_input);
        for (int i = 0; i <= 99; i++)
        {
            for (int j = 0; j <= 99; j++)
            {
                vm.Reset();
                vm.Memory[1] = i;
                vm.Memory[2] = j;

                vm.Run();
                if (vm.Memory[0] == 19690720)
                {
                    return (i * 100 + j).ToString();
                }
            }
        }
        return string.Empty;
    }

}
