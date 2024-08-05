namespace BattleshipGame;
class Tile
{
    public TileState State { get; set; } = TileState.Empty;
    public bool IsHit { get; set; } = false;
}
