namespace Tetris;

public class SBlock : Block
{
    private readonly Position[][] _tiles = new Position[][]
    {
        new Position[] { new(2, 2), new(2, 1), new(1, 1), new(3, 2) },
        new Position[] { new(2, 1), new(1, 1), new(1, 2), new(2, 0) },
    };

    public override int Id => 5;
    protected override Position StartOffset => new Position(-1, 3);
    protected override Position[][] Tiles => _tiles;
}