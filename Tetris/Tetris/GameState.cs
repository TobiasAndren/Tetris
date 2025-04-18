namespace Tetris;

public class GameState
{
    private Block currentBlock;
    public int Score { get; private set; } = 0;

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
    
    private DrawGame _drawGame;

    public GameState()
    {
        GameGrid = new GameGrid(22, 10);
        BlockQueue = new BlockQueue();
        _drawGame = new DrawGame();
        CurrentBlock = BlockQueue.GetAndUpdate();
        GameOver = false;
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
        var currentRotationState = currentBlock.TilePositions().ToArray();
        
        CurrentBlock.RotateClockwise();
        
        if (!BlockFits())
        {
            currentBlock.Reset();
        }
    }

    public void RotateBlockCounterClockwise()
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
        Console.WriteLine($"Checking Game Over... Row 0 empty: {GameGrid.IsRowEmpty(0)}, Row 1 empty: {GameGrid.IsRowEmpty(1)}");
        return !(GameGrid.IsRowEmpty(0) && GameGrid.IsRowEmpty(1));
    }

    private void PlaceBlock()
    {
        foreach (Position pos in CurrentBlock.TilePositions())
        {
            Console.WriteLine($"Placing Block at Row: {pos.Row}, Column: {pos.Column}");
            if (pos.Row >= 0 && pos.Row < GameGrid.Rows && pos.Column >= 0 && pos.Column < GameGrid.Columns)
            {
                GameGrid[pos.Row, pos.Column] = CurrentBlock.Id;
            }
            else
            {
                GameOver = true;
                return;
            }
        }

        int rowsCleared = GameGrid.ClearFullRow();
        
        Score += rowsCleared * 100;

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
        currentBlock.Move(1, 0);

        if (!BlockFits())
        {
            CurrentBlock.Move(-1, 0);
            PlaceBlock();
        }
    }
    
    public void Render()
    {
        Console.Clear();


        _drawGame.DrawBoard(GameGrid);
        _drawGame.DrawUi(GameGrid, Score, BlockQueue.NextBlock, false);
        
        for (int row = 0; row < GameGrid.Rows; row++)
        {
            for (int col = 0; col < GameGrid.Columns; col++)
            {
                int tile = GameGrid[row, col];  

                if (tile != 0)  
                {
                    _drawGame.DrawBlock(row, col, _drawGame.GetColor(tile));
                }
            }
        }
        
        foreach (Position pos in CurrentBlock.TilePositions())
        {
            _drawGame.DrawBlock(pos.Row, pos.Column, _drawGame.GetColor(CurrentBlock.Id));
        }
    }

    public void HandleInput()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    RotateBlockClockwise();
                    break;
                case ConsoleKey.DownArrow:
                    RotateBlockCounterClockwise();
                    break;
                case ConsoleKey.LeftArrow:
                    MoveBlockLeft();
                    break;
                case ConsoleKey.RightArrow:
                    MoveBlockRight();
                    break;
                case ConsoleKey.Spacebar:
                    MoveBlockDown();
                    break;
                case ConsoleKey.Escape:
                    GameOver = true;
                    break;
            }
        }
    }

    public void GameLoop()
    {
        Console.Clear();
        _drawGame.DrawUi(GameGrid, Score, BlockQueue.NextBlock, true);

        while (Console.KeyAvailable)
        {
            Console.ReadKey(true);
        }

        while (Console.ReadKey(true).Key != ConsoleKey.Enter)
        {
            
        }
        while (!GameOver)
        {
            HandleInput();
            //MoveBlockDown();
            Render();
            Thread.Sleep(250);
        }
        Console.WriteLine("Game Over!");
    }
}