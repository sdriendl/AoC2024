using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Test.Tests;

public class Day19Tests
{
    [Fact]
    public async void Solve_1_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            r, wr, b, g, bwu, rb, gb, br

            brwrr
            bggr
            gbbr
            rrbgbr
            ubwu
            bwurrg
            brgr
            bbrgwb
            """;

        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day19
        {
            TestInputFilePath = tempFile.FilePath,
        };

        // Act
        var result = await day.Solve_1();

        // Assert
        Assert.Equal("6", result);
    }

    [Fact]
    public async void Solve_2_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            r, wr, b, g, bwu, rb, gb, br

            brwrr
            bggr
            gbbr
            rrbgbr
            ubwu
            bwurrg
            brgr
            bbrgwb

            """;

        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day19
        {
            TestInputFilePath = tempFile.FilePath,
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("16", result);
    }
}
