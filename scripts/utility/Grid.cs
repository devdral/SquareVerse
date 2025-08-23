using System.Collections;
using System.Collections.Generic;
using Godot;

namespace SquareVerse.Utility;

public class Grid : IEnumerable<QualifiedCell>
{
    private Cell[] _grid;
    
    public int Width { get; }
    public int Height { get; }

    public Grid(int width, int height)
    {
        _grid = new Cell[width * height];
        Width = width;
        Height = height;
    }

    public Grid(Grid other)
    {
        Width = other.Width;
        Height = other.Height;
        _grid = new Cell[Width * Height];
        other._grid.CopyTo(_grid, 0);
    }

    public Cell this[int x, int y]
    {
        get
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return new Cell(GridManager.EMPTY);
            return _grid[x + (y * Height)];
        }
        set => _grid[x + (y * Height)] = value;
    }

    public Cell this[Vector2I pos]
    {
        get => this[pos.X, pos.Y];
        set => this[pos.X, pos.Y] = value;
    }

    public int[] GetPackedGrid()
    {
        int[] grid = new int[Width * Height];
        for (int i = 0; i < Width * Height; i++)
        {
            grid[i] = _grid[i].Type;
        }

        return grid;
    }

    public IEnumerator<QualifiedCell> GetEnumerator()
    {
        return new GridEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class GridEnumerator : IEnumerator<QualifiedCell>
{
    private Grid _grid;
    
    private int _x;
    private int _y;

    public GridEnumerator(Grid grid)
    {
        _grid = grid;
    }

    public object Current => new QualifiedCell(_grid[_x, _y], new Vector2I(_x, _y));

    public bool MoveNext()
    {
        _x++;
        if (_x >= _grid.Width)
        {
            _x = 0;
            _y++;
        }
        return _y < _grid.Height;
    }

    public void Reset()
    {
        _x = 0;
        _y = 0;
    }

    QualifiedCell IEnumerator<QualifiedCell>.Current => new(_grid[_x, _y], new Vector2I(_x, _y));

    public void Dispose() {}
}