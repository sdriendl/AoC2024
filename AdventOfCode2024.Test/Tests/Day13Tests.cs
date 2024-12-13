using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Test.Tests;

public class Day13Tests
{
    [Fact]
    public async void Solve_1_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            Button A: X+94, Y+34
            Button B: X+22, Y+67
            Prize: X=8400, Y=5400

            Button A: X+26, Y+66
            Button B: X+67, Y+21
            Prize: X=12748, Y=12176

            Button A: X+17, Y+86
            Button B: X+84, Y+37
            Prize: X=7870, Y=6450

            Button A: X+69, Y+23
            Button B: X+27, Y+71
            Prize: X=18641, Y=10279
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day13
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_1();

        // Assert
        Assert.Equal("480", result);
    }

    [Fact]
    public async void Solve_2_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            Button A: X+94, Y+34
            Button B: X+22, Y+67
            Prize: X=8400, Y=5400
            
            Button A: X+26, Y+66
            Button B: X+67, Y+21
            Prize: X=12748, Y=12176
            
            Button A: X+17, Y+86
            Button B: X+84, Y+37
            Prize: X=7870, Y=6450
            
            Button A: X+69, Y+23
            Button B: X+27, Y+71
            Prize: X=18641, Y=10279
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day13
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("875318608908", result);
    }
}
