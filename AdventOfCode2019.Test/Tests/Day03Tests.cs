namespace AdventOfCode2019.Test.Tests;

public class Day03Tests
{
    [Fact]
    public async void Solve_1_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            R8,U5,L5,D3
            U7,R6,D4,L4
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day03
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_1();

        // Assert
        Assert.Equal("6", result);
    }

    [Fact]
    public async void Solve_2_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            R8,U5,L5,D3
            U7,R6,D4,L4
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day03
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("30", result);
    }
}
