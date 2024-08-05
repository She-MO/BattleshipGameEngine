namespace BattleshipGame;

public class Ship
{
   internal Tile?[] ShipTiles { get; set; }

   public Ship(int size)
   {
      ShipTiles = new Tile[size];
   }

   public bool ShipIsSunk()
   {
      bool isSunk = true;
      for (int i = 0; i < ShipTiles.Length; i++)
      {
         if (ShipTiles[i].State == TileState.OccupiedByShip)
         {
            isSunk = false;
            break;
         }
      }

      return isSunk;
   }
}

