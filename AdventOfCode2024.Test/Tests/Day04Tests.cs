namespace AdventOfCode2024.Test.Tests;

public class Day04Tests
{
    [Fact]
    public async void Solve_1_SmallSample()
    {
        // Arrange
        var testInput =
            """
            ..X...
            .SAMX.
            .A..A.
            XMAS.S
            .X....
            """;

        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day04
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_1();

        // Assert
        Assert.Equal("4", result);
    }

    [Fact]
    public async void Solve_1_EdgeCase()
    {
        // Arrange
        var testInput =
            """
            XXXX
            XXXM
            XXXX
            AXXX
            XSXX
            """;
        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day04
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_1();

        // Assert
        Assert.Equal("0", result);
    }

    [Fact]
    public async void Solve_1_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            MMMSXXMASM
            MSAMXMSMSA
            AMXSXMAAMM
            MSAMASMSMX
            XMASAMXAMM
            XXAMMXXAMA
            SMSMSASXSS
            SAXAMASAAA
            MAMMMXMMMM
            MXMXAXMASX
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day04
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_1();

        // Assert
        Assert.Equal("18", result);
    }

    [Fact]
    public async void Solve_2_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            .M.S......
            ..A..MSMS.
            .M.S.MAA..
            ..A.ASMSM.
            .M.S.M....
            ..........
            S.S.S.S.S.
            .A.A.A.A..
            M.M.M.M.M.
            ..........
            """;


        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day04
        {
            TestInputFilePath = tempFile.FilePath
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("9", result);
    }
}
