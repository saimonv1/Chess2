using Backend.Utilities.Interpreter.Enums;

namespace Backend.Utilities.Interpreter;

public class InterpretedShootCommand : InterpretedCommand
{
    public Length length;

    public InterpretedShootCommand(SelectedUnit? unit, Direction direction, Length length)
    {
        this.unit = unit;
        this.direction = direction;
        this.length = length;
    }
}