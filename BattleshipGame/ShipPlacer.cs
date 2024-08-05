namespace BattleshipGame;

public class ShipPlacer : IShipPlacer
{
    
    public void PlaceShips(GameBoard gameBoard)
    {
        int failedAttemptsToPlaceShip = 0;
        int[,] shipPlacementBoard = new int[gameBoard.Board.GetLength(1), gameBoard.Board.GetLength(1)];
        Random rng = new Random();
        for (int i = 0; i < gameBoard.Ships.Length; i++)
        {
            List<(int, int)> shipCoords = new List<(int, int)>(); 
            int shipCoord1 = rng.Next(gameBoard.Board.GetLength(1) - gameBoard.Ships[i].ShipTiles.Length);
            int colOrRow = rng.Next(2);
            if (colOrRow == 0)
            {
                int shipCoord2 = rng.Next(gameBoard.Board.GetLength(1) - gameBoard.Ships[i].ShipTiles.Length);
                for (int j = 0; j < gameBoard.Ships[i].ShipTiles.Length; j++, shipCoord2++)
                {
                    if (shipPlacementBoard[shipCoord1, shipCoord2] == 0)
                    {
                        gameBoard.Ships[i].ShipTiles[j] = gameBoard.Board[shipCoord1, shipCoord2];
                        shipPlacementBoard[shipCoord1, shipCoord2] = 1;
                        gameBoard.Ships[i].ShipTiles[j].State = TileState.OccupiedByShip;
                        shipCoords.Add((shipCoord1, shipCoord2));
                    }
                    else
                    {
                        for (int k = 0; k < shipCoords.Count; k++)
                        {
                            shipPlacementBoard[shipCoords[k].Item1, shipCoords[k].Item2] = 0;
                            gameBoard.Ships[i].ShipTiles[k].State = TileState.Empty;
                            gameBoard.Ships[i].ShipTiles[k] = new Tile();
                        }
                        i--;
                        shipCoords.Clear();
                        failedAttemptsToPlaceShip++;
                        break;
                    } 
                }    
            }
            else
            {
                int shipCoord2 = rng.Next(gameBoard.Board.GetLength(1) - gameBoard.Ships[i].ShipTiles.Length);
                for (int j = 0; j < gameBoard.Ships[i].ShipTiles.Length; j++, shipCoord1++)
                {
                    if (shipPlacementBoard[shipCoord1, shipCoord2] == 0)
                    {
                        gameBoard.Ships[i].ShipTiles[j] = gameBoard.Board[shipCoord1, shipCoord2];
                        shipPlacementBoard[shipCoord1, shipCoord2] = 1;
                        gameBoard.Ships[i].ShipTiles[j].State = TileState.OccupiedByShip;
                        shipCoords.Add((shipCoord1, shipCoord2));
                    }
                    else
                    {
                        for (int k = 0; k < shipCoords.Count; k++)
                        {
                            shipPlacementBoard[shipCoords[k].Item1, shipCoords[k].Item2] = 0;
                            gameBoard.Ships[i].ShipTiles[k].State = TileState.Empty;
                            gameBoard.Ships[i].ShipTiles[k] = new Tile();
                        }
                        i--;
                        shipCoords.Clear();
                        failedAttemptsToPlaceShip++;
                        break;
                    } 
                }    
            }

            if (shipCoords.Count > 0)
            {
                MarkAdjacentTiles(shipCoords, shipPlacementBoard);
                failedAttemptsToPlaceShip = 0;
            }

            if (failedAttemptsToPlaceShip > 1000)
            {
                ResetShipPositions(shipPlacementBoard, gameBoard.Ships);
                i = -1;
                failedAttemptsToPlaceShip = 0;
            }
        }
    }
    private void ResetShipPositions(int[,] board, Ship[] ships)
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(0); j++)
            {
                board[i, j] = 0;
            }
        }
        for (int i = 0; i < ships.Length; i++)
        {
            for (int j = 0; j < ships[i].ShipTiles.Length; j++)
            {
                if (ships[i].ShipTiles[j] is null)
                {
                    continue;
                }
                ships[i].ShipTiles[j].State = TileState.Empty;
                ships[i].ShipTiles[j] = new Tile();
            }
        }
    }
    public void MarkAdjacentTiles(List<(int, int)> coords, int[,] board)
    {
        HashSet<(int, int)> adjacentTiles = new HashSet<(int, int)>();
        for (int i = 0; i < coords.Count; i++)
        {
           adjacentTiles.Add((coords[i].Item1 + 1, coords[i].Item2 - 1)); 
           adjacentTiles.Add((coords[i].Item1 + 1, coords[i].Item2)); 
           adjacentTiles.Add((coords[i].Item1 + 1, coords[i].Item2 + 1)); 
           adjacentTiles.Add((coords[i].Item1 - 1, coords[i].Item2 - 1)); 
           adjacentTiles.Add((coords[i].Item1 - 1, coords[i].Item2)); 
           adjacentTiles.Add((coords[i].Item1 - 1, coords[i].Item2 + 1)); 
           adjacentTiles.Add((coords[i].Item1, coords[i].Item2 - 1)); 
           adjacentTiles.Add((coords[i].Item1, coords[i].Item2 + 1)); 
        }

        foreach (var tile in adjacentTiles)
        {
            try
            {
                if (board[tile.Item1, tile.Item2] == 0)
                {
                    board[tile.Item1, tile.Item2] = 2;
                }
            }
            catch
            {
               //Console.WriteLine("error caught"); 
            } 
        }
    }
}
