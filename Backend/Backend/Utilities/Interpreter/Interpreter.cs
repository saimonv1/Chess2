using Backend.Utilities.Interpreter.Enums;
using System.Text.RegularExpressions;

namespace Backend.Utilities.Interpreter;

public static class Interpreter
{
    /// Order should be: unit? action direction length/amount of moves
    public static InterpretedCommand? Interpret(string text)
    {
        string[] tokens = text.Split(' ');

        return InterpretTokens(tokens, false);
    }

    private static InterpretedCommand? InterpretTokens(string[] tokens, bool wasUnitParsedYet)
    {
        try
        {
            if (!wasUnitParsedYet)
            {
                var unit = InterpretUnit(tokens[0]);

                if (unit is not null)
                {
                    var command = InterpretTokens(tokens.Skip(1).ToArray(), true);

                    if (command is not null)
                    {
                        command.unit = unit;
                    }

                    return command;
                }

                return InterpretTokens(tokens, true);
            }

            var action = InterpretAction(tokens[0]);

            if (action is null)
            {
                return null;
            }

            var nullDirection = InterpretDirection(tokens[1]);

            if (nullDirection is null)
            {
                return null;
            }

            var direction = nullDirection.Value;

            switch (action)
            {
                case Enums.Action.Shoot:
                    var nullLength = InterpretLength(tokens[2]);

                    if (nullLength is null)
                    {
                        return null;
                    }

                    var length = nullLength.Value;

                    return new InterpretedShootCommand(null, direction, length);

                case Enums.Action.Move:
                    var amount = 1;
                    
                    if (tokens.Length > 2)
                    {
                        var nullAmount = ParseNumber(tokens[2]);

                        if (nullAmount is null)
                        {
                            return null;
                        }

                        amount = nullAmount.Value;
                    }

                    return new InterpretedMoveCommand(null, direction, amount);

                default:
                    return null;
            }
        }
        catch
        {
            return null;
        }
    }

    private static SelectedUnit? InterpretUnit(string token)
    {
        if (IsHeli(token))
        {
            return SelectedUnit.Helicopter;
        }

        if (IsTank(token))
        {
            return SelectedUnit.Tank;
        }

        return null;
    }

    private static Enums.Action? InterpretAction(string token)
    {
        if (IsShoot(token))
        {
            return Enums.Action.Shoot;
        }

        if (IsMove(token))
        {
            return Enums.Action.Move;
        }

        return null;
    }

    private static Direction? InterpretDirection(string token)
    {
        if (IsUp(token))
        {
            return Direction.Up;
        }

        if (IsDown(token))
        {
            return Direction.Down;
        }

        if (IsLeft(token))
        {
            return Direction.Left;
        }

        if (IsRight(token))
        {
            return Direction.Right;
        }

        return null;
    }

    private static Length? InterpretLength(string token)
    {
        if (IsShort(token))
        {
            return Length.Short;
        }

        if (IsLong(token))
        {
            return Length.Long;
        }

        return null;
    }

    private static int? ParseNumber(string token)
    {
        try
        {
            return int.Parse(token);
        }
        catch
        {
            return null;
        }
    }

    private static bool IsHeli(string token)
    {
        string pattern = @"^(heli|copter)+$";

        return IsMatching(token, pattern);
    }

    private static bool IsTank(string token)
    {
        string pattern = @"^tank$";

        return IsMatching(token, pattern);
    }

    private static bool IsUp(string token)
    {
        string pattern = @"^(u(u*p)?|\^)$";

        return IsMatching(token, pattern);
    }

    private static bool IsDown(string token)
    {
        string pattern = @"^(d(o+wn)?|V)$";

        return IsMatching(token, pattern);
    }

    private static bool IsLeft(string token)
    {
        string pattern = @"^(l(e+ft)?|<)$";

        return IsMatching(token, pattern);
    }

    private static bool IsRight(string token)
    {
        string pattern = @"^(r(i+ght)?|>)$";

        return IsMatching(token, pattern);
    }

    private static bool IsShoot(string token)
    {
        string pattern = @"^(p[eo]+w|s(h(o+t)?)?|b(a+m|a+ng|oo+m)|murder|k(apow|ill)|d(estroy|amage))$";

        return IsMatching(token, pattern);
    }

    private static bool IsMove(string token)
    {
        string pattern = @"^(m(v|o+ve)?|r(u+n)?|w(a+lk)?)$";

        return IsMatching(token, pattern);
    }

    private static bool IsLong(string token)
    {
        string pattern = @"^(l(o+ng)?)$";

        return IsMatching(token, pattern);
    }

    private static bool IsShort(string token)
    {
        string pattern = @"^(s(h(o+rt)?)?)$";

        return IsMatching(token, pattern);
    }

    private static bool IsMatching(string token, string exp)
    {
        Regex regex = new Regex(exp, RegexOptions.IgnoreCase);

        return regex.IsMatch(token);
    }
}