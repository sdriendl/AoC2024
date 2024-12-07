namespace AdventOfCode.Common;

public static class EnumerableExtensions
{
    public static IEnumerable<IEnumerable<T>> CartesianPower<T>(this IEnumerable<T> source, int n)
    {
        if (n < 0) throw new ArgumentOutOfRangeException(nameof(n), "The power must be non-negative.");

        if (n == 0) return [[]];
        
        var previousPower = CartesianPower(source, n - 1);
        return previousPower.SelectMany(seq => source.Select(seq.Append));
    }
}