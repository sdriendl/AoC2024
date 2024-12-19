using AdventOfCode.Common;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Days;

public sealed class Day19 : CustomInputPathBaseDay
{
    private List<string> _patterns;
    private List<string> _designs;
    private Dictionary<string, long> _cache;

    public Day19()
    {
        Initialize();
    }
    protected override void Initialize()
    {
        var sp = File.ReadAllText(InputFilePath).Split("\n\n");
        _patterns = sp[0].Split(", ").ToList();
        _designs = sp[1].Split("\n", options: StringSplitOptions.RemoveEmptyEntries).ToList();
    }
    public async override ValueTask<string> Solve_1()
    {
        _cache = [];
        var count = 0;
        var i = 0;
        foreach (var design in _designs)
        {
            if (IsDesignPossible(design)) count++;
        }
        return count.ToString();
    }

    public async override ValueTask<string> Solve_2()
    {
        _cache = [];

        long count = 0;
        var i = 0;
        foreach (var design in _designs)
        {
            count += CountDesigns(design);
        }
        return count.ToString();
    }

    public bool IsDesignPossible(string design)
    {
        if (string.IsNullOrWhiteSpace(design)) return false;
        if (_cache.TryGetValue(design, out var v))
        {
            return v > 0;
        }
        var result = false;
        foreach (var pattern in _patterns)
        {
            if (design == pattern) return true;
            if (design.StartsWith(pattern))
            {
                var dr = IsDesignPossible(string.Join("", design.Skip(pattern.Length)));
                if (dr) { result = true; break; }
            }
        }
        var ways = _cache.GetValueOrDefault(design, 0);
        if (result)
        {
            _cache[design] = ways + 1;
        }
        else
        {
            _cache[design] = ways;
        }

        return result;
    }

    public long CountDesigns(string design)
    {
        if (string.IsNullOrWhiteSpace(design)) return 1;
        if (_cache.TryGetValue(design, out var v))
        {
            return v;
        }
        long ways = 0;
        foreach (var pattern in _patterns)
        {
            if (design.StartsWith(pattern))
            {
                ways += CountDesigns(string.Join("", design.Skip(pattern.Length)));
            }
        }
        _cache[design] = ways;

        return _cache.GetValueOrDefault(design, 0);
    }
}
