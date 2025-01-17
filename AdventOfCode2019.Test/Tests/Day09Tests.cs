namespace AdventOfCode2019.Test.Tests;

public class Day09Tests
{
    [Fact]
    public void Quine_Test()
    {
        var program = "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99";
        var vm = new IntCode(program);
        vm.Run([]);
        var actual = string.Join(",", vm.OutputBuffer);
        Assert.Equal(program, actual);
    }

    [Fact]
    public void BigNumber_Test()
    {
        var program = "1102,34915192,34915192,7,4,7,99,0";
        var vm = new IntCode(program);
        vm.Run([]);
        var actual = vm.OutputBuffer.Dequeue().ToString().Length;
        Assert.Equal(16, actual);
    }

    [Fact]
    public void BigInput_Test()
    {
        var program = "104,1125899906842624,99";
        var vm = new IntCode(program);
        vm.Run([]);
        var actual = vm.OutputBuffer.Dequeue();
        Assert.Equal(1125899906842624, actual);
    }

    [Theory]
    [InlineData(10, 17)]
    [InlineData(100000, 454396537)]
    public void SumOfPrimes(long input, long expected)
    {
        var program = "3,100,1007,100,2,7,1105,-1,87,1007,100,1,14,1105,-1,27,101,-2,100,100,101,1,101,101,1105,1,9,101,105,101,105,101,2,104,104,101,1,102,102,1,102,102,103,101,1,103,103,7,102,101,52,1106,-1,87,101,105,102,59,1005,-1,65,1,103,104,104,101,105,102,83,1,103,83,83,7,83,105,78,1106,-1,35,1101,0,1,-1,1105,1,69,4,104,99";
        var vm = new IntCode(program);
        vm.Run([input]);
        Assert.Equal(expected, vm.OutputBuffer.Dequeue());
    }
}
