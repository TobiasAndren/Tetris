namespace Tetris;

public class GameState
{
    private Block currentBlock;

    public Block CurrentBlock
    {
        get => currentBlock;
        private set
        {
            currentBlock = value;
            currentBlock.Reset();
        }
    }
    
    public GameGrid GameGrid { get; }
    
    public BlockQueue BlockQueue { get; }
    
    public bool GameOver { get; private set; }

    public GameState()
    {
        GameGrid = new GameGrid(22, 10);
        BlockQueue = new BlockQueue();
        CurrentBlock = BlockQueue.GetAndUpdate();
    }

    private bool BlockFits()
    {
        foreach (Position pos in CurrentBlock.TilePositions())
        {
            if (!GameGrid.IsEmpty(pos.Row, pos.Column))
            {
                return false;
            }
        }
        return true;
    }

    public void RotateBlockClockwise()
    {
        CurrentBlock.RotateClockwise();

        if (!BlockFits())
        {
            CurrentBlock.RotateCounterCLockwise();
        }
    }

    public void RotateCounterClockwise()
    {
        CurrentBlock.RotateCounterCLockwise();

        if (!BlockFits())
        {
            CurrentBlock.RotateClockwise();
        }
    }

    public void MoveBlockLeft()
    {
        CurrentBlock.Move(0, -1);

        if (!BlockFits())
        {
            CurrentBlock.Move(0, 1);
        }
    }

    public void MoveBlockRight()
    {
        CurrentBlock.Move(0, 1);

        if (!BlockFits())
        {
            CurrentBlock.Move(0, -1);
        }
    }

    private bool IsGameOver()
    {
        return !(GameGrid.IsRowEmpty(0) && GameGrid.IsRowEmpty(1));
    }

    private void PlaceBlock()
    {
        foreach (Position pos in CurrentBlock.TilePositions())
        {
            GameGrid[pos.Row, pos.Column] = CurrentBlock.Id;
        }

        GameGrid.ClearFullRow();

        if (IsGameOver())
        {
            GameOver = true;
        }
        else
        {
            CurrentBlock = BlockQueue.GetAndUpdate();
        }
    }

    public void MoveBlockDown()
    {
        currentBlock.Move(-1, 0);

        if (!BlockFits())
        {
            CurrentBlock.Move(-1, 0);
            PlaceBlock();
        }
    }
}