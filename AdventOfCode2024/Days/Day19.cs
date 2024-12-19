using AdventOfCode.Common;
using MoreLinq.Extensions;

namespace AdventOfCode2024.Days;

public sealed class Day19 : CustomInputPathBaseDay
{
    private List<string> _patterns;
    private IEnumerable<string> _designs;

    public Day19()
    {
        Initialize();
    }
    protected override void Initialize()
    {
        var sp = File.ReadAllText(InputFilePath).Split("\n\n");
        _patterns = sp[0].Split(", ").ToList();
        _designs = sp[1].Split("\n", options: StringSplitOptions.RemoveEmptyEntries);
    }
    public async override ValueTask<string> Solve_1()
    {
        return _designs.AsParallel().Count(IsDesignPossible).ToString();
    }

    public async override ValueTask<string> Solve_2()
    {
        return _designs.Sum(s => CountDesignsBottomUp(s.AsSpan())).ToString();
    }

    public bool IsDesignPossible(string design)
    {
        if (string.IsNullOrWhiteSpace(design)) return false;

        ReadOnlySpan<char> designSpan = design.AsSpan();
        Dictionary<string, bool> cache = [];
        return IsDesignPossibleInternal(designSpan, cache);
    }

    private bool IsDesignPossibleInternal(ReadOnlySpan<char> design, Dictionary<string, bool> cache)
    {
        if (design.IsEmpty) return false;

        string designKey = design.ToString();
        if (cache.TryGetValue(designKey, out var cachedResult))
        {
            return cachedResult;
        }

        foreach (var pattern in _patterns)
        {
            ReadOnlySpan<char> patternSpan = pattern.AsSpan();

            if (design.SequenceEqual(patternSpan)) return true;

            if (design.StartsWith(patternSpan, StringComparison.Ordinal))
            {
                if (IsDesignPossibleInternal(design[patternSpan.Length..], cache))
                {
                    cache[designKey] = true;
                    return true;
                };
            }
        }
        cache[designKey] = false;
        return false;
    }

    public long CountDesigns(string design)
    {
        if (string.IsNullOrWhiteSpace(design)) return 1;

        ReadOnlySpan<char> designSpan = design.AsSpan();
        Dictionary<string, long> cache = [];

        return CountDesignsInternal(designSpan, cache);
    }

    private long CountDesignsInternal(ReadOnlySpan<char> design, Dictionary<string, long> cache)
    {
        if (design.IsEmpty) return 1;

        string designKey = design.ToString();
        if (cache.TryGetValue(designKey, out var cachedResult))
        {
            return cachedResult;
        }

        long ways = 0;

        foreach (var pattern in _patterns)
        {
            ReadOnlySpan<char> patternSpan = pattern.AsSpan();

            if (design.StartsWith(patternSpan, StringComparison.Ordinal))
            {
                ways += CountDesignsInternal(design[patternSpan.Length..], cache);
            }
        }

        cache[designKey] = ways;
        return ways;
    }

    private long CountDesignsBottomUp(ReadOnlySpan<char> design)
    {
        long[] dp = new long[design.Length + 1];
        dp[design.Length] = 1;
        for (int i = design.Length - 1; i >= 0; i--)
        {
            foreach (var pattern in _patterns)
            {
                if (design[i..].StartsWith(pattern))
                {
                    dp[i] += dp[i + pattern.Length];
                }
            }
        }
        return dp[0];
    }
}
