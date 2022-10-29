namespace Backend.Utilities.Command
{
    public interface IMoveCommand
    {
        public int Execute(int moveType, string connectionId);
        public int Undo(string connectionId);
    }
}
