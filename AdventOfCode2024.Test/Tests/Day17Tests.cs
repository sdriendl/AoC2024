namespace AdventOfCode2024.Test.Tests;

public class Day17Tests
{
    [Fact]
    public async void Verify_Part2_Solution()
    {
        var day = new Day17();

        var expected = string.Join(",", day.program);

        var regA = await day.Solve_2();
        var actual = day.Run(long.Parse(regA), 0, 0);
        
        Assert.Equal(expected, string.Join(",", actual));
    }
}