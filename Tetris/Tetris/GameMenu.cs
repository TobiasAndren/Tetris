namespace Tetris;

public class GameMenu
{
    public void ShowStartMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("========================");
        Console.WriteLine("         TETRIS         ");
        Console.WriteLine("========================");
        Console.WriteLine("Press 'enter' to start the game...");
        Console.ResetColor();
        ConsoleKeyInfo key = Console.ReadKey();
        while (key.Key != ConsoleKey.Enter)
        {
            key = Console.ReadKey();
        }
        Console.Clear();
    }
}