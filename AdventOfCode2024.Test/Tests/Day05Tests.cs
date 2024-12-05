using AdventOfCode.Common;
using AdventOfCode2024.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Test.Tests;

public class Day05Tests
{
    [Fact]
    public async void Solve_1_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            47|53
            97|13
            97|61
            97|47
            75|29
            61|13
            75|53
            29|13
            97|29
            53|29
            61|53
            97|53
            61|29
            47|13
            75|47
            97|75
            47|61
            75|61
            47|29
            75|13
            53|13

            75,47,61,53,29
            97,61,53,29,13
            75,29,13
            75,97,47,61,53
            61,13,29
            97,13,75,29,47
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day05
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_1();

        // Assert
        Assert.Equal("143", result);
    }

    [Fact]
    public async void Solve_2_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            47|53
            97|13
            97|61
            97|47
            75|29
            61|13
            75|53
            29|13
            97|29
            53|29
            61|53
            97|53
            61|29
            47|13
            75|47
            97|75
            47|61
            75|61
            47|29
            75|13
            53|13

            75,47,61,53,29
            97,61,53,29,13
            75,29,13
            75,97,47,61,53
            61,13,29
            97,13,75,29,47
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day05
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("123", result);
    }
}
