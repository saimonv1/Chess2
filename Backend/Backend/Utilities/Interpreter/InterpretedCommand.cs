using Backend.Utilities.Interpreter.Enums;

namespace Backend.Utilities.Interpreter;

public class InterpretedCommand
{
    public Enums.Action? action;
    public SelectedUnit? unit;
    public Direction? direction;
    public int? distance;
}