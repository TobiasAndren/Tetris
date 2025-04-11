namespace Tetris;

public class GameGrid
{
    private readonly int[,] _grid;
    public int Rows { get; }
    public int Columns { get; }

    public int this[int row, int col]
    {
        get => _grid[row, col];
        set => _grid[row, col] = value;
    }

    public GameGrid(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        _grid = new int[Rows, Columns];
    }

    private bool IsInside(int row, int col)
    {
        return row >= 0 && row < Rows && col >= 0 && col < Columns;
    }

    public bool IsEmpty(int row, int col)
    {
        return IsInside(row, col) && _grid[row, col] == 0;
    }

    private bool IsRowFull(int row)
    {
        for (var col = 0; col < Columns; col++)
        {
            if (_grid[row, col] == 0)
            {
                return false;
            }
        }

        return true;
    }

    public bool IsRowEmpty(int row)
    {
        for (var col = 0; col < Columns; col++)
        {
            if (_grid[row, col] != 0)
            {
                return false;
            }
        }
        
        return true;
    }

    private void ClearRow(int row)
    {
        for (var col = 0; col < Columns; col++)
        {
            _grid[row, col] = 0;
        }
    }

    private void MoveRowDown(int row, int numRows)
    {
        for (var col = 0; col < Columns; col++)
        {
            _grid[row + numRows, col] = _grid[row, col];
            _grid[row, col] = 0;
        }
    }

    public int ClearFullRow()
    {
        var cleared = 0;

        for (var row = Rows - 1; row >= 0; row--)
        {
            if (IsRowFull(row))
            {
                ClearRow(row);
                cleared++;
            }
            else if (cleared > 0)
            {
                MoveRowDown(row, cleared);
            }
        }

        return cleared;
    }
}