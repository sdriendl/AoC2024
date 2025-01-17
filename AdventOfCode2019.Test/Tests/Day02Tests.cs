namespace AdventOfCode2019.Test.Tests;

public class Day02Tests
{
    [Fact]
    public async void Solve_1_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            1,9,10,3,2,3,11,0,99,30,40,50
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day02
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_1();

        // Assert
        Assert.Equal("3500", result);
    }
}
