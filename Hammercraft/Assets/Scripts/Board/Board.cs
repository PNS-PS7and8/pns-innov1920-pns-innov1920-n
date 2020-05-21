using System.Collections.Generic;
using UnityEngine;

public class Board {
    public Cell[,] cells;
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
                        cellType = CellType.Free,
                        unit = null
                };
            }
        }
    }

    public Cell GetCell(Vector2Int cellPosition) {
        return GetCell(cellPosition.x, cellPosition.y);
    }

    public Cell GetCell(int x, int y) {
        x = Mathf.Clamp(x, 0, 100);
        y = Mathf.Clamp(y, 0, 100);
        return cells[x, y];
        
    }

    public Vector3 CellToLocal(Vector2Int cell) {
        return CellToLocal(cell.x, cell.y);
    }

    public Vector3 CellToLocal(int x, int y) {
        return new Vector3(x * 1.5f, 0, x * 0.866f + y * 1.732f);
    }

    public Vector2Int LocalToCell(Vector3 point) {
        return new Vector2Int(Mathf.RoundToInt(point.x * 0.666f), Mathf.RoundToInt(point.x * -0.333f + 0.577f * point.z));
    }
}