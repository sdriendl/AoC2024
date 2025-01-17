using MoreLinq;

namespace AdventOfCode2019;

public class IntCode
{
    private long[] _program;
    private int IP;
    private long _relativeBase;

    public ProgramState State { get; private set; }
    public Queue<long> InputBuffer { get; set; }
    public Queue<long> OutputBuffer { get; set; }
    public long[] Memory { get; set; }
    private Dictionary<long, long> ExtendedMemory { get; set; }

    public IntCode(string program)
    {
        _program = program.Split(",").Select(long.Parse).ToArray();
        Memory = new long[_program.Length];
        ExtendedMemory = [];
        _program.CopyTo(Memory, 0);
        InputBuffer = [];
        OutputBuffer = [];
        IP = 0;
        _relativeBase = 0;
    }

    public void Run()
    {
        Run([]);
    }

    public void Run(IEnumerable<long> input)
    {
        State = ProgramState.Running;
        foreach (var n in input)
        {
            InputBuffer.Enqueue(n);
        }

        while (true)
        {
            var inst = Instruction.Parse(Memory[IP]);
            switch (inst.Code)
            {
                case OpCode.Add:
                    SetMemory(3, Value(inst, 1) + Value(inst, 2), inst);
                    break;
                case OpCode.Mul:
                    SetMemory(3, Value(inst, 1) * Value(inst, 2), inst);
                    break;
                case OpCode.Input:
                    if (InputBuffer.Count == 0)
                    {
                        State = ProgramState.WaitingForInput;
                        return;
                    }
                    SetMemory(1, InputBuffer.Dequeue(), inst);
                    break;
                case OpCode.Output:
                    OutputBuffer.Enqueue(Value(inst, 1));
                    break;
                case OpCode.JumpTrue:
                    if (Value(inst, 1) != 0)
                    {
                        inst = inst with { Stride = (int)Value(inst, 2) - IP };
                    }
                    break;
                case OpCode.JumpFalse:
                    if (Value(inst, 1) == 0)
                    {
                        inst = inst with { Stride = (int)Value(inst, 2) - IP };
                    }
                    break;
                case OpCode.LessThan:
                    SetMemory(3, Value(inst, 1) < Value(inst, 2) ? 1 : 0, inst);
                    break;
                case OpCode.Equals:
                    SetMemory(3, Value(inst, 1) == Value(inst, 2) ? 1 : 0, inst);
                    break;
                case OpCode.SetRelative:
                    _relativeBase += Value(inst, 1);
                    break;
                case OpCode.Halt:
                    State = ProgramState.Halted;
                    return;
            }
            IP += inst.Stride;
        }
    }

    private long ReadMemory(long position)
    {
        if (position < Memory.Length)
        {
            return Memory[position];
        }
        return ExtendedMemory.GetValueOrDefault(position, 0L);
    }

    private void SetMemory(int offset, long value, Instruction inst)
    {
        var position = ReadMemory(IP + offset);
        if (inst.GetMode(offset - 1) == ParameterMode.Relative)
        {
            position += _relativeBase;
        }
        if (position < Memory.Length)
        {
            Memory[position] = value;
        }
        else
        {
            ExtendedMemory[position] = value;
        }
    }

    private long Value(Instruction instruction, int offset)
    {
        var mode = instruction.GetMode(offset - 1);
        long parameter = ReadMemory(IP + offset);
        return mode switch
        {
            ParameterMode.Position => ReadMemory(parameter),
            ParameterMode.Immediate => parameter,
            ParameterMode.Relative => ReadMemory(parameter + _relativeBase),
        };
    }

    public void Reset()
    {
        _program.CopyTo(Memory, 0);
        ExtendedMemory.Clear();
        InputBuffer = [];
        OutputBuffer = [];
        IP = 0;
        _relativeBase = 0;
    }

    private record Instruction(OpCode Code, IList<ParameterMode> Modes, int Stride)
    {
        public static Instruction Parse(long input)
        {
            int codeNumber = (int)(input % 100);
            if (!Enum.IsDefined(typeof(OpCode), codeNumber))
            {
                throw new ArgumentException($"Invalid instruction {input}");
            }
            var code = (OpCode)codeNumber;
            var stride = code switch
            {
                OpCode.Halt => 0,
                var x when new[] { OpCode.SetRelative, OpCode.Input, OpCode.Output }.Contains(x) => 2,
                var x when new[] { OpCode.JumpTrue, OpCode.JumpFalse }.Contains(x) => 3,
                var x when new[] { OpCode.Add, OpCode.Mul, OpCode.LessThan, OpCode.Equals }.Contains(x) => 4,
                _ => throw new ArgumentException($"Unkown Opcode: {code}")
            };
            input /= 100;
            var modes = new List<ParameterMode>();
            while (input > 0)
            {
                modes.Add((input % 10) switch
                {
                    0 => ParameterMode.Position,
                    1 => ParameterMode.Immediate,
                    2 => ParameterMode.Relative,
                    _ => throw new ArgumentException($"Invalid Instruction: {input}")
                });
                input /= 10;
            }
            return new Instruction(code, modes, stride);
        }

        public ParameterMode GetMode(int n)
        {
            if (n >= Modes.Count)
            {
                return ParameterMode.Position;
            }
            return Modes[n];
        }
    }

    private enum OpCode
    {
        Add = 1,
        Mul = 2,
        Input = 3,
        Output = 4,
        JumpTrue = 5,
        JumpFalse = 6,
        LessThan = 7,
        Equals = 8,
        SetRelative = 9,
        Halt = 99,
    }

    private enum ParameterMode
    {
        Position = 0,
        Immediate = 1,
        Relative = 2,
    }

    public enum ProgramState
    {
        Running,
        Halted,
        WaitingForInput,
    }
}
