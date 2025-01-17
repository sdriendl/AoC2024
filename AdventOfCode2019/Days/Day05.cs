using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Days;

public sealed class Day05 : CustomInputPathBaseDay
{
    private string _input;
    public Day05()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public async override ValueTask<string> Solve_1()
    {
        var vm = new IntCode(_input);
        vm.Run([1]);
        return vm.OutputBuffer.Last().ToString();
    }

    public async override ValueTask<string> Solve_2()
    {
        var vm = new IntCode(_input);
        vm.Run([5]);
        return vm.OutputBuffer.First().ToString();
    }

}
