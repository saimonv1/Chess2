using Backend.Entities;

namespace Backend.Memento
{
    public class MapMemento : IMemento
    {
        private Map _state;
        private DateTime _date;
        public MapMemento(Map state)
        {
            this._state = state;
            this._date = DateTime.Now;
        }

        public Map GetState()
        {
            return this._state;
        }
        public DateTime GetDate()
        {
            return this._date;
        }
    }
}
