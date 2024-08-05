namespace BattleshipGame;

enum TileState
{
   Empty,
   OccupiedByShip,
   PartOfShipDestroyed,
   SunkShip
}
class Program
{
    static void Main(string[] args)
    {
        ShipPlacer shipPlacer = new ShipPlacer();
        Game game = new Game(shipPlacer);
        int[] numberOfTiles = new int[] { 2, 3, 4 };
        int[] numberOfShips = new int[] { 2, 2, 2 };
        int boardSize = 9;
        game.StartNewGame(boardSize, numberOfTiles, numberOfShips);
        
        while (game.LostPlayer == -1)
        {
            ShowBoards(game);
            Console.WriteLine("Enter coordinates:");
            string[]? hitCoordsStr = Console.ReadLine()?.Split(' ');
            (int x, int y) hitCoords = (Int32.Parse(hitCoordsStr[0]), Int32.Parse(hitCoordsStr[1]));
            game.Shoot(hitCoords.x, hitCoords.y);
            ShowBoards(game);
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("\x1b[3J");
            Console.Clear();
            Console.ReadLine();
            game.NextTurn();
        }
        Console.WriteLine("Player " + game.LostPlayer + " lost");
    }
    internal static void ShowBoards(Game game)
    {
        
        int currentPlayer = (game.Turn % 2 == 0) ? 2 : 1;
        int opponent = (game.Turn % 2 == 0) ? 1 : 2;
        Console.WriteLine("Your board:");
        Console.Write("     ");
        for (int i = 0; i < game[currentPlayer].Board.GetLength(0); i++)
        {
            Console.Write(i + "    ");
        }
        Console.WriteLine();
        for (int i = 0; i < game[currentPlayer].Board.GetLength(0); i++)
        {
            Console.Write(i + "    ");
            for (int j = 0; j < game[currentPlayer].Board.GetLength(0); j++)
            {
                    switch (game[currentPlayer].Board[i, j].State)
                    {
                       case TileState.Empty:
                           Console.Write("O    ");
                           break;
                       case TileState.PartOfShipDestroyed:
                           Console.Write("H    ");
                           break;
                       case TileState.SunkShip:
                           Console.Write("X    ");
                           break;
                       case TileState.OccupiedByShip:
                           Console.Write("S    ");
                           break;
                           
                    }
            }
            Console.WriteLine();
        }
        Console.WriteLine("Opponents board:");
        Console.Write("     ");
        for (int i = 0; i < game[currentPlayer].Board.GetLength(0); i++)
        {
            Console.Write(i + "    ");
        }
        Console.WriteLine();
        for (int i = 0; i < game[opponent].Board.GetLength(0); i++)
        {
            Console.Write(i + "    ");
            for (int j = 0; j < game[opponent].Board.GetLength(0); j++)
            {
                if (game[opponent].Board[i, j].IsHit)
                {
                    switch (game[opponent].Board[i, j].State)
                    {
                        case TileState.Empty:
                            Console.Write("O    ");
                            break;
                        case TileState.PartOfShipDestroyed:
                            Console.Write("H    ");
                            break;
                        case TileState.SunkShip:
                            Console.Write("X    ");
                            break;

                    }
                }
                else
                {
                    Console.Write("?    ");
                }
            }
            Console.WriteLine();
        }
        
    }
}

