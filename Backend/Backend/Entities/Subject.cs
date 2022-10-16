namespace Backend.Entities
{
    public class Subject
    {
        private List<Player> _observers = new List<Player>();
        private Map _map;
        public Map Map
        {
            get { return _map; }
            set
            {
                _map = value;
                Notify();
            }
        }
        public void Subscribe(Player observer)
        {
            _observers.Add(observer);
        }
        public void Unsubscribe(Player observer)
        {
            if (_observers.Contains(observer))
                _observers.Remove(observer);
        }
        public void Notify()
        {
            //Console.WriteLine(this + " updated");
            _observers.ForEach(o =>
            {
                o.Update(this._map);
            });
        }
    }
}
