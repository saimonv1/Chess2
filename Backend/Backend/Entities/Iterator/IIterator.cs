namespace Backend.Entities.Iterator
{
    public interface IIterator<T>
    {
        T First();
        T Next();
        bool IsDone();
        T Current();
    }
}
