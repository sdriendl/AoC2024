using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Test.Tests;

public class Day16Tests
{
    [Fact]
    public async void Solve_1_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            ###############
            #.......#....E#
            #.#.###.#.###.#
            #.....#.#...#.#
            #.###.#####.#.#
            #.#.#.......#.#
            #.#.#####.###.#
            #...........#.#
            ###.#.#####.#.#
            #...#.....#.#.#
            #.#.#.###.#.#.#
            #.....#...#.#.#
            #.###.#.#.#.#.#
            #S..#.....#...#
            ###############
            """;

        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day16
        {
            TestInputFilePath = tempFile.FilePath,
        };

        // Act
        var result = await day.Solve_1();

        // Assert
        Assert.Equal("7036", result);
    }

    [Fact]
    public async void Solve_1_ReturnsCorrectResult2()
    {
        // Arrange
        var testInput =
            """
            #################
            #...#...#...#..E#
            #.#.#.#.#.#.#.#.#
            #.#.#.#...#...#.#
            #.#.#.#.###.#.#.#
            #...#.#.#.....#.#
            #.#.#.#.#.#####.#
            #.#...#.#.#.....#
            #.#.#####.#.###.#
            #.#.#.......#...#
            #.#.###.#####.###
            #.#.#...#.....#.#
            #.#.#.#####.###.#
            #.#.#.........#.#
            #.#.#.#########.#
            #S#.............#
            #################
            """;

        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day16
        {
            TestInputFilePath = tempFile.FilePath,
        };

        // Act
        var result = await day.Solve_1();

        // Assert
        Assert.Equal("11048", result);
    }

    [Fact]
    public async void Solve_2_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            ###############
            #.......#....E#
            #.#.###.#.###.#
            #.....#.#...#.#
            #.###.#####.#.#
            #.#.#.......#.#
            #.#.#####.###.#
            #...........#.#
            ###.#.#####.#.#
            #...#.....#.#.#
            #.#.#.###.#.#.#
            #.....#...#.#.#
            #.###.#.#.#.#.#
            #S..#.....#...#
            ###############
            """;

        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day16
        {
            TestInputFilePath = tempFile.FilePath,
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("45", result);
    }

    [Fact]
    public async void Solve_2_ReturnsCorrectResult2()
    {
        // Arrange
        var testInput =
            """
            #################
            #...#...#...#..E#
            #.#.#.#.#.#.#.#.#
            #.#.#.#...#...#.#
            #.#.#.#.###.#.#.#
            #...#.#.#.....#.#
            #.#.#.#.#.#####.#
            #.#...#.#.#.....#
            #.#.#####.#.###.#
            #.#.#.......#...#
            #.#.###.#####.###
            #.#.#...#.....#.#
            #.#.#.#####.###.#
            #.#.#.........#.#
            #.#.#.#########.#
            #S#.............#
            #################
            """;

        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day16
        {
            TestInputFilePath = tempFile.FilePath,
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("64", result);
    }
}
