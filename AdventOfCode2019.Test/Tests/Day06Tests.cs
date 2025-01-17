using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Test.Tests;

public class Day06Tests
{
    [Fact]
    public async void Solve_1_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            COM)B
            B)C
            C)D
            D)E
            E)F
            B)G
            G)H
            D)I
            E)J
            J)K
            K)L
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day06
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_1();

        // Assert
        Assert.Equal("42", result);
    }    
    
    [Fact]
    public async void Solve_2_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            COM)B
            B)C
            C)D
            D)E
            E)F
            B)G
            G)H
            D)I
            E)J
            J)K
            K)L
            K)YOU
            I)SAN
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day06
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("4", result);
    }
}
