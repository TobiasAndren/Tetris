namespace Tetris;

public class JBlock : Block
{
    private readonly Position[][] _tiles = new Position[][]
    {
        new Position[] { new(1, 1), new(2, 1), new(2, 2), new(2, 3) }, 
        new Position[] { new(1, 2), new(1, 1), new(2, 1), new(3, 1) }, 
        new Position[] { new(2, 2), new(1, 2), new(1, 1), new(1, 0) }, 
        new Position[] { new(2, 1), new(2, 2), new(1, 2), new(0, 2) }  
    };

    public override int Id => 2;
    protected override Position StartOffset => new Position(0, 3);
    protected override Position[][] Tiles => _tiles;
}