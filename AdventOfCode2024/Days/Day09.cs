using AdventOfCode.Common;
using MoreLinq;

namespace AdventOfCode2024.Days;

public sealed class Day09 : CustomInputPathBaseDay
{
    private List<int> _input;
    public Day09()
    {
        Initialize();
    }
    protected override void Initialize()
    {
        _input = File.ReadAllText(InputFilePath)
                     .Select(n => n - '0')
                     .ToList();
    }

    public async override ValueTask<string> Solve_1()
    {
        var mem = Compact(ExpandLayout(_input));

        return mem
            .Select((x, i) => x == -1 ? 0L : x * i)
            .Sum()
            .ToString();
    }

    public async override ValueTask<string> Solve_2()
    {
        var mem = Defrag(ExpandLayout(_input));

        var result = mem
            .Select((x, i) => x == -1 ? 0L : (long)x * i)
            .Sum()
            .ToString();

        return result;
    }

    private List<int> Defrag(List<int> input)
    {
        var rle = input.RunLengthEncode();
        var chunks = new List<(int Key, int Size, int StartingIndex)>();
        var idx = 0;
        foreach (var e in rle)
        {
            if (e.Key != -1)
            {
                chunks.Add((e.Key, e.Value, idx));
            }
            idx += e.Value;
        }
        chunks = chunks.OrderByDescending(x => x.Key).ToList();
        var firstPossibleFree = new int[chunks.Max(x => x.Size) + 1];

        foreach (var (Key, Size, StartingIndex) in chunks)
        {
            SwapChunk(Key, Size, StartingIndex);
        }

        void SwapChunk(int key, int size, int startingIndex)
        {
            for (int i = firstPossibleFree[size]; i < startingIndex - size + 1; i++)
            {
                var fits = true;
                for (int j = i; j < i + size; j++)
                {
                    if (input[j] != -1)
                    {
                        fits = false;
                        break;
                    }
                }
                if (fits)
                {
                    for (var k = size; k < firstPossibleFree.Length; k++)
                        firstPossibleFree[k] = Math.Max(i + size, firstPossibleFree[k]);
                    for (int k = i; k < i + size; k++)
                    {
                        input[k] = key;
                    }
                    for (int k = startingIndex; k < startingIndex + size; k++)
                    {
                        input[k] = -1;
                    }
                    break;
                }
            }
        }
        return input;
    }

    private List<int> Compact(List<int> input)
    {
        var left = 0;
        for (int i = input.Count - 1; i >= 0; i--)
        {
            if (input[i] == -1) continue;
            if (i <= left) break;
            for (int j = left; j < input.Count; j++)
            {
                if (input[j] != -1) continue;
                (input[i], input[j]) = (input[j], input[i]);
                left = j + 1;
                break;
            }
        }
        return input;
    }

    private List<int> ExpandLayout(List<int> input)
    {
        List<int> result = [];
        for (int i = 0; i < input.Count; i++)
        {
            if (i % 2 == 0)
            {
                for (int j = 0; j < input[i]; j++)
                {
                    result.Add(i / 2);
                }
            }
            else
            {
                for (int j = 0; j < input[i]; j++)
                {
                    result.Add(-1);
                }
            }
        }
        return result;
    }
}
