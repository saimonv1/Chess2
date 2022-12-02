using Backend.Entities;

namespace Backend.Utilities.Command
{
    public class MoveRightCommand : ICommand
    {
        public int Execute(Unit unit, Map map, Game game)
        {
            var (oldX, oldY) = (unit.PosX, unit.PosY);
            var (newX, newY) = (oldX + 1, oldY);

            if (map.Tiles[newX, newY].IsObstacle || map.Tiles[newX, newY] as TileUnit is not null)
                return unit.RemainingTurns;

            game.MoveItem(oldX, oldY, newX, newY);

            unit.PosX = newX;
            unit.PosY = newY;

            unit.RemainingTurns -= 1;

            return unit.RemainingTurns;
        }

        public int Undo(Unit unit, Map map, Game game)
        {
            ICommand command = new MoveLeftCommand();
            command.Execute(unit, map, game);
            return unit.RemainingTurns += 2;
        }
    }

    public class MoveLeftCommand : ICommand
    {
        public int Execute(Unit unit, Map map, Game game)
        {
            var (oldX, oldY) = (unit.PosX, unit.PosY);
            var (newX, newY) = (oldX - 1, oldY);

            if (map.Tiles[newX, newY].IsObstacle || map.Tiles[newX, newY] as TileUnit is not null)
                return unit.RemainingTurns;

            game.MoveItem(oldX, oldY, newX, newY);

            unit.PosX = newX;
            unit.PosY = newY;

            unit.RemainingTurns -= 1;

            return unit.RemainingTurns;
        }

        public int Undo(Unit unit, Map map, Game game)
        {
            ICommand command = new MoveRightCommand();
            command.Execute(unit, map, game);
            return unit.RemainingTurns += 2;
        }
    }

    public class MoveUpCommand : ICommand
    {
        public int Execute(Unit unit, Map map, Game game)
        {
            var (oldX, oldY) = (unit.PosX, unit.PosY);
            var (newX, newY) = (oldX, oldY - 1);

            if (map.Tiles[newX, newY].IsObstacle || map.Tiles[newX, newY] as TileUnit is not null)
                return unit.RemainingTurns;

            game.MoveItem(oldX, oldY, newX, newY);

            unit.PosX = newX;
            unit.PosY = newY;

            unit.RemainingTurns -= 1;

            return unit.RemainingTurns;
        }

        public int Undo(Unit unit, Map map, Game game)
        {
            ICommand command = new MoveDownCommand();
            command.Execute(unit, map, game);
            return unit.RemainingTurns += 2;
        }
    }

    public class MoveDownCommand : ICommand
    {
        public int Execute(Unit unit, Map map, Game game)
        {
            var (oldX, oldY) = (unit.PosX, unit.PosY);
            var (newX, newY) = (oldX, oldY + 1);

            if (map.Tiles[newX, newY].IsObstacle || map.Tiles[newX, newY] as TileUnit is not null)
                return unit.RemainingTurns;

            game.MoveItem(oldX, oldY, newX, newY);

            unit.PosX = newX;
            unit.PosY = newY;

            unit.RemainingTurns -= 1;

            return unit.RemainingTurns;
        }

        public int Undo(Unit unit, Map map, Game game)
        {
            ICommand command = new MoveUpCommand();
            command.Execute(unit, map, game);
            return unit.RemainingTurns += 2;
        }
    }

    public class Mover
    {
        private Game game = Game.GetGameInstance();
        public static Stack<ICommand> history = new Stack<ICommand>();
        public void Clear()
        {
            history.Clear();
        }

        public int ExecuteCommand(ICommand command, string connectionId)
        {
            var unit = game.GetPlayerByConnectionId(connectionId)!.Units.First();
            if (unit.RemainingTurns == 0)
                return 0;

            var map = game.GetMap();
            var remainingTurnsBefore = unit.RemainingTurns;
            var remainingTurnsAfter = command.Execute(unit, map, game);

            if (remainingTurnsBefore > remainingTurnsAfter)
            {
                history.Push(command);
            }

            return remainingTurnsAfter;
        }

        public int UndoCommand(string connectionId)
        {
            var unit = game.GetPlayerByConnectionId(connectionId)!.Units.First();

            if (history.Count == 0)
            {
                return unit.RemainingTurns;
            }

            ICommand command = history.Pop();
            var map = game.GetMap();

            return command.Undo(unit, map, game);
        }
    }


    //public class MoveCommand : IMoveCommand
    //{
    //    private Game game = Game.GetGameInstance();

    //    public static Stack<(int, int)> history = new Stack<(int, int)>();

    //    public void ClearHistory()
    //    {
    //        history.Clear();
    //    }


    //    public int Execute(int moveType, string connectionId)
    //    {
    //        Unit unit = game.GetPlayerByConnectionId(connectionId)!.Units.First();

    //        if (unit.RemainingTurns == 0)
    //        {
    //            return 0;
    //        }

    //        var (oldX, oldY) = (unit.PosX, unit.PosY);
    //        var (newX, newY) = (oldX, oldY);
    //        (newX, newY) = moveType switch
    //        {
    //            0 => (oldX, oldY - 1),
    //            1 => (oldX + 1, oldY),
    //            2 => (oldX, oldY + 1),
    //            3 => (oldX - 1, oldY),
    //            _ => (newX, newY)
    //        };

    //        var map = game.GetMap();

    //        if (map.Tiles[newX, newY].IsObstacle || map.Tiles[newX, newY].Unit is not null)
    //            return unit.RemainingTurns;

    //        history.Push((oldX, oldY));

    //        game.MoveItem(oldX, oldY, newX, newY);

    //        unit.PosX = newX;
    //        unit.PosY = newY;

    //        unit.RemainingTurns -= 1;

    //        return unit.RemainingTurns;
    //    }

    //    public int Undo(string connectionId)
    //    {
    //        var unit = game.GetPlayerByConnectionId(connectionId)!.Units.First();

    //        if (history.Count == 0)
    //        {
    //            return unit.RemainingTurns;
    //        }

    //        var coords = history.Pop();

    //        game.MoveItem(unit.PosX, unit.PosY, coords.Item1, coords.Item2);

    //        unit.PosX = coords.Item1;
    //        unit.PosY = coords.Item2;

    //        unit.RemainingTurns += 1;

    //        return unit.RemainingTurns;
    //    }
    //}
}
