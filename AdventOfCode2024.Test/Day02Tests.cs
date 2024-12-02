using AdventOfCode2024.Days;

namespace AdventOfCode2024.Test;

public class Day02Tests
{
    [Fact]
    public async void Solve_1_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            7 6 4 2 1
            1 2 7 8 9
            9 7 6 2 1
            1 3 2 4 5
            8 6 4 4 1
            1 3 6 7 9
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day02
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_1();

        // Assert
        Assert.Equal("2", result);
    }

    [Fact]
    public async void Solve_2_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            7 6 4 2 1
            1 2 7 8 9
            9 7 6 2 1
            1 3 2 4 5
            8 6 4 4 1
            1 3 6 7 9
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day02
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("4", result);
    }
}
