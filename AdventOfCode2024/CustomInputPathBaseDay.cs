using AoCHelper;

namespace AdventOfCode2024;

public abstract class CustomInputPathBaseDay : BaseDay
{
    private string _testInputFilePath;

    public string TestInputFilePath
    {
        private get => _testInputFilePath;
        set
        {
            _testInputFilePath = value;
            Initialize();
        }
    }

    public override string InputFilePath => TestInputFilePath;

    protected CustomInputPathBaseDay()
    {
        TestInputFilePath = base.InputFilePath;
    }

    protected abstract void Initialize();
}