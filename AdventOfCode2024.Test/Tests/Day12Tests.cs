namespace AdventOfCode2024.Test.Tests;

public class Day12Tests
{
    [Fact]
    public async void Solve_1_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            RRRRIICCFF
            RRRRIICCCF
            VVRRRCCFFF
            VVRCCCJFFF
            VVVVCJJCFE
            VVIVCCJJEE
            VVIIICJJEE
            MIIIIIJJEE
            MIIISIJEEE
            MMMISSJEEE
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day12
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_1();

        // Assert
        Assert.Equal("1930", result);
    }

    [Fact]
    public async void Solve_2_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            RRRRIICCFF
            RRRRIICCCF
            VVRRRCCFFF
            VVRCCCJFFF
            VVVVCJJCFE
            VVIVCCJJEE
            VVIIICJJEE
            MIIIIIJJEE
            MIIISIJEEE
            MMMISSJEEE
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day12
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("1206", result);
    }

    [Fact]
    public async void Solve_2_Simple()
    {
        // Arrange
        var testInput =
            """
            AAAA
            BBCD
            BBCC
            EEEC
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day12
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("80", result);
    }    
    
    [Fact]
    public async void Solve_2_SimpleXO()
    {
        // Arrange
        var testInput =
            """
            OOOOO
            OXOXO
            OOOOO
            OXOXO
            OOOOO
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day12
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("436", result);
    }   
    
    [Fact]
    public async void Solve_2_SimpleE()
    {
        // Arrange
        var testInput =
            """
            EEEEE
            EXXXX
            EEEEE
            EXXXX
            EEEEE
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day12
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("236", result);
    }    
    
    [Fact]
    public async void Solve_2_SimpleAB()
    {
        // Arrange
        var testInput =
            """
            AAAAAA
            AAABBA
            AAABBA
            ABBAAA
            ABBAAA
            AAAAAA
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day12
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("368", result);
    }
}
