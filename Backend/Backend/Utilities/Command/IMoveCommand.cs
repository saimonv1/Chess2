using Backend.Entities;

namespace Backend.Utilities.Command
{
    public interface ICommand
    {
        public int Execute(Unit unit, Map map, Game game);
        public int Undo(Unit unit, Map map, Game game);
    }

    //public interface IMoveCommand
    //{
    //    public int Execute(int moveType, string connectionId);
    //    public int Undo(string connectionId);
    //}
}
