using Backend.Utilities.Interpreter.Enums;

namespace Backend.Utilities.Interpreter;

public abstract class InterpretedCommand
{
    public SelectedUnit? unit;
    public Direction direction;
}