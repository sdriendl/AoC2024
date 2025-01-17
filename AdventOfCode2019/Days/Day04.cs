using AdventOfCode.Common;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019.Days;

public sealed class Day04 : CustomInputPathBaseDay
{
    private int _lower;
    private int _upper;
    protected override void Initialize()
    {
        var input = File.ReadAllText(InputFilePath).Split("-");
        _lower = int.Parse(input[0]);
        _upper = int.Parse(input[1]);
    }
    public async override ValueTask<string> Solve_1()
    {
        var result = 0;
        for (int i1 = 0; i1 < 10; i1++)
        {
            for (int i2 = i1; i2 < 10; i2++)
            {
                for (int i3 = i2; i3 < 10; i3++)
                {
                    for (int i4 = i3; i4 < 10; i4++)
                    {
                        for (var i5 = i4; i5 < 10; i5++)
                        {
                            for (var i6 = i5; i6 < 10; i6++)
                            {
                                var n = i6 + i5 * 10 + i4 * 100 + i3 * 1000 + i2 * 10000 + i1 * 100000;
                                if (n < _lower || n > _upper) continue;
                                if (i1 != i2 && i2 != i3 && i3 != i4 && i4 != i5 && i5 != i6) continue;
                                result++;
                            }
                        }
                    }
                }
            }
        }
        return result.ToString();
    }

    public async override ValueTask<string> Solve_2()
    {
        var result = 0;
        for (int i1 = 0; i1 < 10; i1++)
        {
            for (int i2 = i1; i2 < 10; i2++)
            {
                for (int i3 = i2; i3 < 10; i3++)
                {
                    for (int i4 = i3; i4 < 10; i4++)
                    {
                        for (var i5 = i4; i5 < 10; i5++)
                        {
                            for (var i6 = i5; i6 < 10; i6++)
                            {
                                var n = i6 + i5 * 10 + i4 * 100 + i3 * 1000 + i2 * 10000 + i1 * 100000;
                                if (n < _lower || n > _upper) continue;
                                var rle = new int[] { i1, i2, i3, i4, i5, i6 }.RunLengthEncode();
                                if (rle.Any(kv => kv.Value == 2))
                                {
                                    result++;
                                }
                            }
                        }
                    }
                }
            }
        }
        return result.ToString();
    }

}
