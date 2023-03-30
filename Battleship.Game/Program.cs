using Battleship.Domain;

namespace Battleship.Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Battleship!");

            Random random = new Random();
            var positions = new List<Position>();

            int firstPosition = random.Next(0, 9);
            int column = random.Next(0, 9);

            for (int i = 0; i < 5; i++)
            {
                var position = new Position { X = column, Y = firstPosition + i };

                positions.Add(position);
            }

            var ships = new List<Ship>();
            ships.Add(new Ship(positions));

            Board board = new Board(10);

            board.AddShips(ships);

            Console.WriteLine(board.ToString());

            Console.ReadKey();
        }
    }
}