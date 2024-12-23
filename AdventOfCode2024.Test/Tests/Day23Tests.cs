namespace AdventOfCode2024.Test.Tests;

public class Day23Tests
{
    [Fact]
    public async void Solve_1_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            kh-tc
            qp-kh
            de-cg
            ka-co
            yn-aq
            qp-ub
            cg-tb
            vc-aq
            tb-ka
            wh-tc
            yn-cg
            kh-ub
            ta-co
            de-co
            tc-td
            tb-wq
            wh-td
            ta-ka
            td-qp
            aq-cg
            wq-ub
            ub-vc
            de-ta
            wq-aq
            wq-vc
            wh-yn
            ka-de
            kh-ta
            co-tc
            wh-qp
            tb-vc
            td-yn
            """;

        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day23
        {
            TestInputFilePath = tempFile.FilePath,
        };

        // Act
        var result = await day.Solve_1();

        // Assert
        Assert.Equal("7", result);
    }
    
    [Fact]
    public async void Solve_2_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            kh-tc
            qp-kh
            de-cg
            ka-co
            yn-aq
            qp-ub
            cg-tb
            vc-aq
            tb-ka
            wh-tc
            yn-cg
            kh-ub
            ta-co
            de-co
            tc-td
            tb-wq
            wh-td
            ta-ka
            td-qp
            aq-cg
            wq-ub
            ub-vc
            de-ta
            wq-aq
            wq-vc
            wh-yn
            ka-de
            kh-ta
            co-tc
            wh-qp
            tb-vc
            td-yn
            """;

        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day23
        {
            TestInputFilePath = tempFile.FilePath,
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("co,de,ka,ta", result);
    }
}