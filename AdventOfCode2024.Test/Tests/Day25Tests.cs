namespace AdventOfCode2024.Test.Tests;

public class Day25Tests
{
    [Fact]
    public async void Solve_1_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            #####
            .####
            .####
            .####
            .#.#.
            .#...
            .....
            
            #####
            ##.##
            .#.##
            ...##
            ...#.
            ...#.
            .....
            
            .....
            #....
            #....
            #...#
            #.#.#
            #.###
            #####
            
            .....
            .....
            #.#..
            ###..
            ###.#
            ###.#
            #####
            
            .....
            .....
            .....
            #....
            #.#..
            #.#.#
            #####
            """;

        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day25
        {
            TestInputFilePath = tempFile.FilePath,
        };

        // Act
        var result = await day.Solve_1();

        // Assert
        Assert.Equal("3", result);
    }
}