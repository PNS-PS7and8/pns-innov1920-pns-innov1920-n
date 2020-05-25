using System.Collections.Generic;
using UnityEngine;

public class Board {
    private Cell[,] cells;
    public readonly Vector2Int size;

    public Board() {
        this.size = new Vector2Int(50, 50);
        ResetGrid();
    }
    
    public Board(Vector2Int size) {
        this.size = size;
        ResetGrid();
    }

    public Board(int width, int height) {
        this.size = new Vector2Int(width, height);
        ResetGrid();
    }


    private void ResetGrid() {
        cells = new Cell[size.x, size.y];
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                cells[x, y] = new Cell() {
                        board = this,
                        position = new Vector2Int(x, y),
                        cellState = Cell.CellState.Free,
                        cellType = Cell.CellType.Field,
                        unit = null
                };
            }
        }
    }

    public IEnumerable<Cell> Cells(bool ignoreTypeNone = true) {
        foreach (var cell in cells)
            if (cell.cellType != Cell.CellType.None || !ignoreTypeNone)
                yield return cell;
    }

    public bool HasCell(Vector2Int cellPosition) {
        return HasCell(cellPosition.x, cellPosition.y);
    }

    public bool HasCell(int x, int y) {
        return 0 <= x && x < size.x && 0 <= y && y < size.y && GetCell(x, y).cellType != Cell.CellType.None;
    }

    public Cell GetCell(Vector2Int cellPosition) {
        return GetCell(cellPosition.x, cellPosition.y);
    }

    public Cell GetCell(int x, int y) {
        return cells[x, y];
        
    }

    public Vector3 CellToLocal(Vector2Int cell) {
        return CellToLocal(cell.x, cell.y);
    }

    public Vector3 CellToLocal(int x, int y) {
        float locx = x - size.x / 2f;
        float locy = y - size.y / 2f;
        Vector3 pos = new Vector3(locx * 3f / 2f, 0, locx * Mathf.Sqrt(3)/2f + locy * Mathf.Sqrt(3));
        if (HasCell(x, y)) pos += cells[x, y].Height * Vector3.up;
        return pos;
    }

    public Vector2Int LocalToCell(Vector3 point) {
        return new Vector2Int(Mathf.RoundToInt(point.x * 2f / 3f), Mathf.RoundToInt(point.x / -3f + Mathf.Sqrt(3f)/3f * point.z)) + size / 2;
    }
}