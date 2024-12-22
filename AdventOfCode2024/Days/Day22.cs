using System.Collections.Concurrent;
using AdventOfCode.Common;
using MoreLinq;

namespace AdventOfCode2024.Days;

public sealed class Day22 : CustomInputPathBaseDay
{
    private List<int> _input;
    public int N { get; set; } = 2000;

    public Day22()
    {
        Initialize();
    }

    protected override void Initialize()
    {
        _input = File.ReadAllLines(InputFilePath)
                     .Select(int.Parse)
                     .ToList();
    }

    public override async ValueTask<string> Solve_1()
    {
        return _input.Sum(seed => NthSecretNumber(seed, N))
                     .ToString();
    }

    public override async ValueTask<string> Solve_2()
    {
        var dict = new ConcurrentDictionary<(long, long, long, long), long>();

        Parallel.ForEach(_input, seed =>
        {
            AddSellPrices(dict, seed, N);
        });
        return dict.Max(d => d.Value).ToString();
    }

    private void AddSellPrices(
        ConcurrentDictionary<(long, long, long, long), long> dict,
        int seed, 
        int n)
    {
        var seen = new HashSet<(long, long, long, long)>();
        var secrets = NSecretNumbers(seed, n);
        foreach (var window in secrets.Window(5))
        {
            var deltas =
                (
                    window[1] % 10 - window[0] % 10,
                    window[2] % 10 - window[1] % 10,
                    window[3] % 10 - window[2] % 10,
                    window[4] % 10 - window[3] % 10
                );
            if (seen.Add(deltas))
            {
                var rp = window[4] % 10;
                dict.AddOrUpdate(deltas, window[4] % 10, (k,v) => v + rp);
            }
        }
    }

    private long NextSecretNumber(long prev)
    {
        prev ^= prev << 6;
        prev %= 16777216;
        prev ^= prev >> 5;
        prev %= 16777216;
        prev ^= prev << 11;
        prev %= 16777216;

        return prev;
    }

    private IEnumerable<long> NSecretNumbers(long seed, int n)
    {
        var next = seed;
        yield return next;
        for (int i = 0; i < n; i++)
        {
            next = NextSecretNumber(next);
            yield return next;
        }
    }

    private long NthSecretNumber(long seed, int n)
    {
        for (int i = 0; i < n; i++)
        {
            seed = NextSecretNumber(seed);
        }

        return seed;
    }
}