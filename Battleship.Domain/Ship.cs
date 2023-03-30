namespace Battleship.Domain
{
    internal class Ship
    {
        private IEnumerable<Position> _positions;
        private IEnumerable<Position> _hitPositions;
        
        internal IEnumerable<Position> Positions => _positions;

        public Ship(IEnumerable<Position> positions)
        {
            if(positions is null)
                throw new ArgumentNullException(nameof(positions));

            _positions = positions;
            _hitPositions = new List<Position>();
        }

        internal HitResult IsHit(Position target)
        {
            foreach(var position in _positions)
            {
                if(target.Equals(position))
                {
                    if(IsPositionPreviouslyHit(position))
                    {
                        return HitResult.AlreadyAimed;
                    } 
                    else
                    {
                        _hitPositions.Append(position);

                        return _positions.Count() == _hitPositions.Count() 
                            ? HitResult.Sunk 
                            : HitResult.Aimed;
                    }
                }
            }

            return HitResult.Missed;
        }

        private bool IsPositionPreviouslyHit(Position position)
        {
            return _hitPositions.Any(x => x.Equals(position));
        }
    }
}
