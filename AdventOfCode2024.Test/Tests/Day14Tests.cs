namespace AdventOfCode2024.Test.Tests;

public class Day14Tests
{

        
    [Fact]
    public async void Solve_1_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            p=0,4 v=3,-3
            p=6,3 v=-1,-3
            p=10,3 v=-1,2
            p=2,0 v=2,-1
            p=0,0 v=1,3
            p=3,0 v=-2,-2
            p=7,6 v=-1,-3
            p=3,0 v=-1,-2
            p=9,3 v=2,3
            p=7,3 v=-1,2
            p=2,4 v=2,-3
            p=9,5 v=-3,-3
            """;

        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day14
        {
            TestInputFilePath = tempFile.FilePath,
            Width = 11,
            Height = 7
        };

        // Act
        var result = await day.Solve_1();

        // Assert
        Assert.Equal("12", result);
    }
}