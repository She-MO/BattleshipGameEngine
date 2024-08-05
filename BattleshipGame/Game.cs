namespace BattleshipGame;

internal class Game
{
    private readonly IShipPlacer _shipPlacer;
    internal GameBoard P1Board { get; private set; }
    internal GameBoard P2Board { get; private set; }
    public int Turn { get; private set; }
    public int LostPlayer { get; private set; }

    public Game(IShipPlacer shipPlacer)
    {
        _shipPlacer = shipPlacer;
    }
    public void StartNewGame(int boardSize, int[] numberOfTiles, int[] numberOfShips)
    {
        P1Board = new GameBoard(boardSize);
        P2Board = new GameBoard(boardSize);
        Turn = 0;
        LostPlayer = -1;
        int nextShipIndex = 0;
        P1Board.Ships = new Ship[numberOfShips.Sum()];
        P2Board.Ships = new Ship[numberOfShips.Sum()];
        for (int i = 0; i < numberOfTiles.Length; i++)
        {
            for (int j = 0; j < numberOfShips[i]; j++, nextShipIndex++)
            {
                P1Board.Ships[nextShipIndex] = new Ship(numberOfTiles[i]);
                P2Board.Ships[nextShipIndex] = new Ship(numberOfTiles[i]);
            }
        }
        _shipPlacer.PlaceShips(P1Board);
        _shipPlacer.PlaceShips(P2Board);
    }
    internal GameBoard this[int player]
    {
        get
        {
            switch (player)
            {
                case 1:
                    return P1Board;
                    break;
                case 2:
                    return P2Board;
                    break;
                default:
                    throw new Exception();
            }
        }
    }
    public void Shoot(int x, int y)
    {
        int currentPlayer = (Turn % 2 == 0) ? 2 : 1;
        int opponent = (Turn % 2 == 0) ? 1 : 2;
        if (this[opponent].Board[x, y].IsHit)
        {
            return;
        }

        this[opponent].Board[x, y].IsHit = true;
        if (this[opponent].Board[x, y].State == TileState.OccupiedByShip)
        {
            this[opponent].Board[x, y].State = TileState.PartOfShipDestroyed;
            this[opponent].MarkSunkShips();
            if (this[opponent].AllShipsSank())
            {
                LostPlayer = opponent;
            }
        }
    }

    public void NextTurn()
    {
        Turn++;
    }
}

