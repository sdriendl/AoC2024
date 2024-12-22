namespace AdventOfCode2024.Test.Tests;

public class Day22Tests
{
    [Fact]
    public async void Solve_1_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            1
            10
            100
            2024
            """;

        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day22
        {
            TestInputFilePath = tempFile.FilePath,
        };

        // Act
        var result = await day.Solve_1();

        // Assert
        Assert.Equal("37327623", result);
    }

    [Fact]
    public async void Solve_2_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            1
            2
            3
            2024
            """;

        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day22
        {
            TestInputFilePath = tempFile.FilePath,
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("23", result);
    }
    
    [Fact]
    public async void Solve_2_SimpleExample_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            123
            """;

        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day22
        {
            TestInputFilePath = tempFile.FilePath,
            N = 10,
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("6", result);
    }
}