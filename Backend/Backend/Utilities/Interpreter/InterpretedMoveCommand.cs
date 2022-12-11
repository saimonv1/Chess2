using Backend.Utilities.Interpreter.Enums;

namespace Backend.Utilities.Interpreter;

public class InterpretedMoveCommand : InterpretedCommand
{
    public int amount;

    public InterpretedMoveCommand(SelectedUnit? unit, Direction direction, int amount)
    {
        this.unit = unit;
        this.direction = direction;
        this.amount = amount;
    }
}