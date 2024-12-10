using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Test.Tests;

public class Day10Tests
{
    [Fact]
    public async void Solve_1_Simple()
    {
        // Arrange
        var testInput =
            """
            ...0...
            ...1...
            ...2...
            6543456
            7.....7
            8.....8
            9.....9
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day10
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_1();

        // Assert
        Assert.Equal("2", result);
    }

    [Fact]
    public async void Solve_1_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            89010123
            78121874
            87430965
            96549874
            45678903
            32019012
            01329801
            10456732
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day10
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_1();

        // Assert
        Assert.Equal("36", result);
    }

    [Fact]
    public async void Solve_2_Simple_1()
    {
        // Arrange
        var testInput =
            """
            ..90..9
            ...1.98
            ...2..7
            6543456
            765.987
            876....
            987....
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day10
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("13", result);
    }

    [Fact]
    public async void Solve_2_Simple_2()
    {
        // Arrange
        var testInput =
            """
            012345
            123456
            234567
            345678
            4.6789
            56789.
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day10
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("227", result);
    }

    [Fact]
    public async void Solve_2_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            89010123
            78121874
            87430965
            96549874
            45678903
            32019012
            01329801
            10456732
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day10
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("81", result);
    }
}
