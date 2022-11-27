namespace Backend.Entities.Iterator
{
    public class TileIterator : IIterator<Tile>
    {
        private Tile[,] tiles;
        private int currentXIndex;
        private int currentYIndex;
        public TileIterator(Tile[,] mapTiles)
        {
            this.tiles = mapTiles;
            currentXIndex = 0;
            currentYIndex = 0;
        }
        public Tile Current()
        {
            //Console.WriteLine("Current: X[" + currentXIndex + "] Y[" + currentYIndex + "]");
            return tiles[currentXIndex, currentYIndex];
        }

        public Tile First()
        {
            return tiles[0, 0];
        }

        public bool IsDone()
        {
            return currentYIndex >= tiles.GetLength(1) || currentXIndex >= tiles.GetLength(0);
        }

        public Tile Next()
        {
            if(currentYIndex == tiles.GetLength(1) - 1)
            {
                currentXIndex++;
                currentYIndex = 0;
            }
            else
            {
                currentYIndex++;
            }
            if (!IsDone())
            {
                return tiles[currentXIndex, currentYIndex];

            }
            else
            {
                return null;
            }
        }
    }
}
