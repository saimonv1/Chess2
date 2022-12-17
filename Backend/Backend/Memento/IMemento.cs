using Backend.Entities;

namespace Backend.Memento
{
    public interface IMemento
    {
        Map GetState();
        DateTime GetDate();
    }
}
