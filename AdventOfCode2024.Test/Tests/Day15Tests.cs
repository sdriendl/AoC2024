﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Test.Tests;

public class Day15Tests
{
    [Fact]
    public async void Solve_1_Smaller_Example_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            ########
            #..O.O.#
            ##@.O..#
            #...O..#
            #.#.O..#
            #...O..#
            #......#
            ########

            <^^>>>vv<v>>v<<
            """;

        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day15
        {
            TestInputFilePath = tempFile.FilePath,
        };

        // Act
        var result = await day.Solve_1();

        // Assert
        Assert.Equal("2028", result);
    }

    [Fact]
    public async void Solve_1_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            ##########
            #..O..O.O#
            #......O.#
            #.OO..O.O#
            #..O@..O.#
            #O#..O...#
            #O..O..O.#
            #.OO.O.OO#
            #....O...#
            ##########

            <vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^
            vvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v
            ><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<
            <<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^
            ^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><
            ^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^
            >^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^
            <><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>
            ^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>
            v^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^
            """;

        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day15
        {
            TestInputFilePath = tempFile.FilePath,
        };

        // Act
        var result = await day.Solve_1();

        // Assert
        Assert.Equal("10092", result);
    }

    [Fact]
    public async void Solve_2_ReturnsCorrectResult()
    {
        // Arrange
        var testInput =
            """
            ##########
            #..O..O.O#
            #......O.#
            #.OO..O.O#
            #..O@..O.#
            #O#..O...#
            #O..O..O.#
            #.OO.O.OO#
            #....O...#
            ##########

            <vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^
            vvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v
            ><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<
            <<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^
            ^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><
            ^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^
            >^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^
            <><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>
            ^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>
            v^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^
            """;

        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day15
        {
            TestInputFilePath = tempFile.FilePath,
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("9021", result);
    }

    [Fact]
    public async void Solve_2_ReturnsCorrectResult2()
    {
        // Arrange
        var testInput =
            """
            ##########
            #O..O.OO.#
            #.O......#
            #......OO#
            #.O.OO.#O#
            #.OO.@...#
            #OOOO..O.#
            #.O.O....#
            #..OO###.#
            ##########

            <v<>v><^><vv<^><<>>v<<v^^^^v^<^^v<>^>^^v<<^v<vv<<<>v<<<<<<>^v>^^>>^v>>^^^><<<>vv^<><<^vv<^v>>vvv<v^^
            """;

        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day15
        {
            TestInputFilePath = tempFile.FilePath,
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("10577", result);
    }

    [Fact]
    public async void Solve_2_ReturnsCorrectResult_Edge_Case_Cone()
    {
        // Arrange
        var testInput =
            """
            #######
            #...#.#
            #.....#
            #.....#
            #.....#
            #.....#
            #.OOO@#
            #.OOO.#
            #..O..#
            #.....#
            #.....#
            #######

            v<vv<<^^^^^
            """;

        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day15
        {
            TestInputFilePath = tempFile.FilePath,
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("2339", result);
    }

    [Fact]
    public async void Solve_2_ReturnsCorrectResult_Edge_Case_2()
    {
        // Arrange
        var testInput =
            """
            #######
            #.....#
            #.O.O@#
            #..O..#
            #..O..#
            #.....#
            #######

            <v<<>vv<^^
            """;

        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day15
        {
            TestInputFilePath = tempFile.FilePath,
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("822", result);
    }

    [Fact]
    public async void Solve_2_ReturnsCorrectResult_Edge_Case_3()
    {
        // Arrange
        var testInput =
            """
            ########
            #......#
            #OO....#
            #.O....#
            #.O....#
            ##O....#
            #O..O@.#
            #......#
            ########

            <^^<<>^^^<v
            """;

        using var tempFile = new TemporaryInputFile(testInput);
        var day = new Day15
        {
            TestInputFilePath = tempFile.FilePath,
        };

        // Act
        var result = await day.Solve_2();

        // Assert
        Assert.Equal("2827", result);
    }
}
