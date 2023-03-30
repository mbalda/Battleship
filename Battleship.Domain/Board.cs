using System.Text;
using CommunityToolkit.HighPerformance;

namespace Battleship.Domain
{
    internal class Board
    {
        const char EmptyField = '_';
        const char HitField = '+';
        const char ShipOnField = '#';
        const char HitShipOnField = '@';

        private char[,] _fields;
        private byte _boardSize;

        public Board(byte boardSize)
        {
            if (boardSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(boardSize));

            _boardSize = boardSize;
            CreateBoard();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            // Add header
            sb.Append(" ABCDEFGHIJ");
            sb.Append(Environment.NewLine);

            // Add column with numbers and board
            for (int i = 0; i < _boardSize; i++)
            {
                var row = _fields.GetRow(i);

                sb.Append(i);
                sb.Append(row.ToArray());
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        public int AddShips(IEnumerable<Ship> ships)
        {
            int addedShips = 0;

            foreach(var ship in ships)
            {
                if(ShipCanBeAddedOnPositions(ship))
                {
                    AddShipToBoard(ship);
                    addedShips++;
                }

                continue;
            }

            return addedShips;
        }

        private void CreateBoard()
        {
            if (_boardSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(_boardSize));

            _fields = new char[_boardSize, _boardSize];

            for (int i = 0; i < _boardSize; i++)
                for (int j = 0; j < _boardSize; j++)
                {
                    _fields[i, j] = EmptyField;
                }
        }

        private void AddShipToBoard(Ship ship)
        {
            foreach(var position in ship.Positions)
            {
                _fields[position.Y, position.X] = ShipOnField;
            }
        }

        private bool ShipCanBeAddedOnPositions(Ship ship)
        {
            foreach(var position in ship.Positions)
            {
                if(IsPositionOutOfBoard(position))
                    return false;

                if (IsPositionAlreadyTaken(position))
                    return false;
            }

            return true;
        }

        private bool IsPositionOutOfBoard(Position position)
        {
            return position.X >= _boardSize || position.Y >= _boardSize;
        }

        private bool IsPositionAlreadyTaken(Position position)
        {
            return _fields[position.Y, position.X] != EmptyField;
        }
    }
}
