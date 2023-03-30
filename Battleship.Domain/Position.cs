namespace Battleship.Domain
{
    internal class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public override bool Equals(object? obj)
        {
            if(obj is not null 
                && obj is Position position)
            {
                return position.X == X 
                    && position.Y == Y;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
    }
}
