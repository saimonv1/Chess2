namespace Backend.Utilities.Command
{
    public class MoveCommand : IMoveCommand
    {
        private Game game = Game.GetGameInstance();

        public static Stack<(int, int)> history;

        public void ClearHistory()
        {
            history.Clear();
        }

        public int Execute(int moveType, string connectionId)
        {
            if(history == null) history = new Stack<(int, int)>();

            var unit = game.GetPlayerByConnectionId(connectionId)!.Units.First();

            if(unit.RemainingTurns == 0)
            {
                return 0;
            }

            var (oldX, oldY) = (unit.PosX, unit.PosY);
            var (newX, newY) = (oldX, oldY);
            (newX, newY) = moveType switch
            {
                0 => (oldX, oldY - 1),
                1 => (oldX + 1, oldY),
                2 => (oldX, oldY + 1),
                3 => (oldX - 1, oldY),
                _ => (newX, newY)
            };

            var map = game.GetMap();

            if (map.Tiles[newX, newY].IsObstacle || map.Tiles[newX, newY].Unit is not null)
                return unit.RemainingTurns;

            history.Push((oldX, oldY));

            game.MoveItem(oldX, oldY, newX, newY);

            unit.PosX = newX;
            unit.PosY = newY;

            unit.RemainingTurns -= 1;

            return unit.RemainingTurns;
        }

        public int Undo(string connectionId)
        {
            var unit = game.GetPlayerByConnectionId(connectionId)!.Units.First();

            if (history == null || history.Count == 0)
            {
                return unit.RemainingTurns;
            }

            var coords = history.Pop();

            game.MoveItem(unit.PosX, unit.PosY, coords.Item1, coords.Item2);

            unit.PosX = coords.Item1;
            unit.PosY = coords.Item2;

            unit.RemainingTurns += 1;

            return unit.RemainingTurns;
        }
    }
}
