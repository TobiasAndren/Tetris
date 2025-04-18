namespace Tetris;

public class DrawGame
{
    public void DrawUi(GameGrid gameGrid, int score, Block nextBlock, bool isWaitingToStart, bool gameOver, bool isPaused)
    {
        var uiColumn = gameGrid.Columns * 4 + 2;
        var row = 0;

        Console.SetCursorPosition(uiColumn, row++);
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("===================================");
        Console.SetCursorPosition(uiColumn, row++);
        Console.WriteLine("              TETRIS              ");

        if (!isWaitingToStart)
        {
            Console.SetCursorPosition(uiColumn, row++);
            Console.WriteLine("-----------------------------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(uiColumn, row++);
            Console.WriteLine($"            Score: {score}         ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(uiColumn, row++);
            Console.WriteLine("-----------------------------------");


            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(uiColumn, row++);
            Console.WriteLine("            Next Block:            ");
            
            DrawNextBlock(nextBlock, 5, gameGrid.Columns - 1);
        }
        
        Console.WriteLine("");
        Console.SetCursorPosition(uiColumn, row++);
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("===================================");

        if (isWaitingToStart)
        {
            Console.SetCursorPosition(uiColumn, row++);
            Console.WriteLine("Press 'enter' to start the game...");
        }

        if (isPaused)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(uiColumn, row++);
            Console.WriteLine("              PAUSED              ");
            Console.SetCursorPosition(uiColumn, row++);
            Console.WriteLine("        Press 'p' to resume         ");
        }
        
        Console.ResetColor();
    }

    public void DrawGameOver(int gridCols, int gridRows)
    {
        var uiStartCol = gridCols * 4 - 2;
        var centerRow = gridRows / 1;
        
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(uiStartCol, centerRow);
        Console.WriteLine("                 GAME OVER                ");
        
        Console.SetCursorPosition(uiStartCol - 2, centerRow + 2);
        Console.WriteLine("      Press 'enter' to restart the game...    ");
        
        Console.SetCursorPosition(uiStartCol - 2, centerRow + 4);
        Console.WriteLine("      Press 'esc' to exit the game...    ");
    }
    
    public void DrawBlock(int row, int col, ConsoleColor color)
    {
        if (row >= 0 && col >= 0 && row < Console.WindowHeight && col < Console.WindowWidth)
        {
            Console.SetCursorPosition(col * 4, row * 2);
            Console.ForegroundColor = color;
            Console.Write("+---+");
            Console.SetCursorPosition(col * 4, row * 2 + 1);
            Console.Write("|   |");
            Console.SetCursorPosition(col * 4, row * 2 + 2);
            Console.Write("+---+"); 
            Console.ResetColor();
        }
    }

    public void DrawNextBlock(Block nextBlock, int offsetRow, int offsetCol)
    {
        nextBlock.Reset();

        foreach (Position pos in nextBlock.TilePositions())
        {
            var drawRow = offsetRow + pos.Row;
            var drawCol = offsetCol + pos.Column;

            if (drawRow >= 0 && drawCol >= 0 && drawCol * 4 + 4 < Console.WindowWidth && drawRow * 2 + 2 < Console.WindowHeight)
            {
                DrawBlock(drawRow, drawCol, GetColor(nextBlock.Id));
            }
        }
    }

    public void DrawBoard(GameGrid gameGrid)
    {
        for (int row = 0; row < gameGrid.Rows; row++)
        {
            for (int col = 0; col < gameGrid.Columns; col++)
            {
                int tile = gameGrid[row, col];
                if (tile != 0)
                {
                    DrawBlock(row, col, GetColor(tile));
                }
                else
                {
                    DrawBlock(row, col, ConsoleColor.Black);
                }
            }
        }
    }

    public ConsoleColor GetColor(int value)
    {
        return value switch
        {
            1 => ConsoleColor.Cyan,
            2 => ConsoleColor.Yellow,
            3 => ConsoleColor.Magenta,
            4 => ConsoleColor.Green,
            5 => ConsoleColor.Red,
            6 => ConsoleColor.Blue,
            7 => ConsoleColor.DarkYellow,
            _ => ConsoleColor.Black,
        };
    }
}