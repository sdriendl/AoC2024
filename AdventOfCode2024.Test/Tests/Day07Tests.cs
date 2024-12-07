namespace AdventOfCode2024.Test.Tests;

public class Day07Tests
{
    [Fact]
    public async void Solve_1_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            190: 10 19
            3267: 81 40 27
            83: 17 5
            156: 15 6
            7290: 6 8 6 15
            161011: 16 10 13
            192: 17 8 14
            21037: 9 7 18 13
            292: 11 6 16 20
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day07
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_1();

        // Assert
        Assert.Equal("3749", result);
    }
    
    [Fact]
    public async void Solve_2_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            190: 10 19
            3267: 81 40 27
            83: 17 5
            156: 15 6
            7290: 6 8 6 15
            161011: 16 10 13
            192: 17 8 14
            21037: 9 7 18 13
            292: 11 6 16 20
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day07
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("11387", result);
    }
}