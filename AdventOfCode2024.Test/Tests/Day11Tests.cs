namespace AdventOfCode2024.Test.Tests;

public class Day11Tests
{
    [Fact]
    public async void Solve_1_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            125 17
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day11
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_1();

        // Assert
        Assert.Equal("55312", result);
    }
}
