namespace AdventOfCode2024.Test;

internal class TemporaryInputFile : IDisposable
{
    public string FilePath { get; }

    public TemporaryInputFile(string[] lines)
    {
        var tempFilePath = Path.GetTempFileName();
        File.WriteAllLines(tempFilePath, lines);
        FilePath = tempFilePath;
    }

    public TemporaryInputFile(string input)
    {
        var tempFilePath = Path.GetTempFileName();
        File.WriteAllText(tempFilePath, input);
        FilePath = tempFilePath;
    }

    public void Dispose()
    {
        if (File.Exists(FilePath))
        {
            File.Delete(FilePath);
        }
    }
}