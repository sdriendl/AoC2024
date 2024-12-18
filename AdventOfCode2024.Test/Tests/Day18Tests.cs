using FastSerialization;

namespace AdventOfCode2024.Test.Tests;

public class Day18Tests
{

    [Fact]
    public async void Solve_2_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            5,4
            4,2
            4,5
            3,0
            2,1
            6,3
            2,4
            1,5
            0,6
            3,3
            2,6
            5,1
            """;

        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day18
        {
            TestInputFilePath = tempFile.FilePath,
            Width = 7,
            Height = 7,
        };

        // Act
        var result = await day.Solve_1();

        // Assert
        Assert.Equal("22", result);
    }
}
