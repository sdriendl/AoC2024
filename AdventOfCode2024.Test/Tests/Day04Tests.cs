using AdventOfCode.Common;
using AdventOfCode2024.Days;

namespace AdventOfCode2024.Test.Tests;

public class Day04Tests
{
    [Fact]
    public async void Solve_1_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """

            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day04
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_1();

        // Assert
        Assert.Equal("123", result);
    }

    [Fact]
    public async void Solve_2_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """

            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day04
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("123", result);
    }
}
