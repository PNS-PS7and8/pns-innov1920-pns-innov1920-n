using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Cell {
    public enum CellState { Free, Occupied, Blocked }
    public enum CellType { Water, Field, Mountain, None }

    public Board board;
    public Vector2Int position;
    public CellState cellState;
    public CellType cellType;
    public Unit unit;

    public List<Cell> Neighbors { get {
        List<Cell> cells = new List<Cell>();
        foreach(var pos in new []{ (0,-1), (1,-1), (1,0), (0,1), (-1,1), (-1,0) }) {
            int x = position.x + pos.Item1;
            int y = position.y + pos.Item2;
            if (board.HasCell(x, y)) {
                cells.Add(board.GetCell(x, y));
            }
        }
        return cells;
    }}

    public List<Cell> FreeNeighbors { get {
        List<Cell> cells = new List<Cell>();
        foreach(var pos in new []{ (0,-1), (1,-1), (1,0), (0,1), (-1,1), (-1,0) }) {
            int x = position.x + pos.Item1;
            int y = position.y + pos.Item2;
            if (board.HasCell(x, y) && board.GetCell(x, y).cellState == CellState.Free && board.GetCell(x, y).cellType == CellType.Field) {
                cells.Add(board.GetCell(x, y));
            }
        }
        return cells;
    }}

    public float Height { get {
        switch (cellType) {
            case CellType.Water: return 1;
            case CellType.Field: return 2;
            case CellType.Mountain: return 3;
            default: return 0;
        }
    }}

    public Vector3 LocalPosition => board.CellToLocal(position);

    public float Distance(Cell other) {
        return 
            Mathf.Abs(position.x - other.position.x) +
            Mathf.Abs(position.y - other.position.y) +
            Mathf.Abs(position.x + position.y - other.position.x - other.position.y);
    }

    public IEnumerable<Cell> Ring(int distance) {
        return 
            board.Cells()
            .Where(cell => Distance(cell) == distance)
            .OrderBy(cell => Mathf.Atan2((cell.position - position).y, (cell.position - position).x));
    }

    public IEnumerable<Cell> Disc(int distance) {
        for (int d=0; d<distance; d++)
            foreach(var cell in Ring(d))
                yield return cell;
    }
}