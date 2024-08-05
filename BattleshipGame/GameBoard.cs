namespace BattleshipGame;

public class GameBoard
{
    internal Tile[,] Board { get; private set; }
    internal Ship[] Ships { get; set; }

    public GameBoard(int boardSize)
    {
        Board = new Tile[boardSize, boardSize];
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                Board[i, j] = new Tile();
            }
        }
    }
    public bool AllShipsSank()
    {
        bool result = true;
        for (int i = 0; i < this.Ships.Length; i++)
        {
            if (!this.Ships[i].ShipIsSunk())
            {
                result = false;
                break;
            }
        }
        return result;
    }
    internal void MarkSunkShips()
    {
       for (int i = 0; i < Ships.Length; i++)
       {
           if (Ships[i].ShipIsSunk())
           {
               for (int j = 0; j < Ships[i].ShipTiles.Length; j++)
               {
                   Ships[i].ShipTiles[j].State = TileState.SunkShip;
               }
           }
       }
    }
}