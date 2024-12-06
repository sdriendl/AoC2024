namespace AdventOfCode2024.Test.Tests;

public class Day01Tests
{
    [Fact]
    public async void Solve_1_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            3   4
            4   3
            2   5
            1   3
            3   9
            3   3
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day01
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_1();

        // Assert
        Assert.Equal("11", result);
    }

    [Fact]
    public async void Solve_2_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            3   4
            4   3
            2   5
            1   3
            3   9
            3   3
            """;

        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day01
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("31", result);
    }
}