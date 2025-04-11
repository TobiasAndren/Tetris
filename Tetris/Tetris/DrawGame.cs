namespace Tetris;

public class DrawGame
{
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