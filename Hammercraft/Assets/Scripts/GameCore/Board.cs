using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Board {
    [SerializeField] private Cell[] cells;
    [SerializeField] private List<Unit> units;
    public List<Unit> Units => units;
    public Vector2Int size;

    public Board() {
        size = new Vector2Int(50, 50);
        units = new List<Unit>();
        ResetGrid();
    }
    
    public Board(Vector2Int size) {
        this.size = size;
        units = new List<Unit>();
        ResetGrid();
    }

    public Board(int width, int height) {
        this.size = new Vector2Int(width, height);
        units = new List<Unit>();
        ResetGrid();
    }

    private int CellIndex(int x, int y) {
        return x % size.x + y * size.x;
    }

    private void ResetGrid() {
        cells = new Cell[size.x * size.y];
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Cell cell = new Cell(new Vector2Int(x, y));
                cells[CellIndex(x, y)] = cell;
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
        return cells[CellIndex(x, y)];
        
    }

    public Vector3 CellToLocal(Vector2Int cell) {
        return CellToLocal(cell.x, cell.y);
    }

    public Vector3 CellToLocal(int x, int y) {
        float locx = x - size.x / 2f;
        float locy = y - size.y / 2f;
        Vector3 pos = new Vector3(locx * 3f / 2f, 0, locx * Mathf.Sqrt(3)/2f + locy * Mathf.Sqrt(3));
        if (HasCell(x, y)) pos += GetCell(x, y).Height * Vector3.up;
        return pos;
    }

    public Vector2Int LocalToCell(Vector3 point) {
        return new Vector2Int(Mathf.RoundToInt(point.x * 2f / 3f), Mathf.RoundToInt(point.x / -3f + Mathf.Sqrt(3f)/3f * point.z)) + size / 2;
    }


    public Unit GetUnit(Cell cell) {
        IEnumerable<Unit> e = units.Where(u => u.position == cell.position);
        if (e.Count() > 0) {
            return e.First();
        } else {
            return null;
        }
    }
    
    public Unit GetUnit(int unitId) {
        IEnumerable<Unit> e = units.Where(u => u.Id == unitId);
        if (e.Count() > 0) {
            return e.First();
        } else {
            return null;
        }
    }

    public Cell GetCell(Unit unit) {
        return GetCell(unit.position);
    }

    public Vector3 LocalPosition(Unit unit) => CellToLocal(unit.position);
    public Vector3 LocalPosition(Cell cell) => CellToLocal(cell.position);

    public IEnumerable<Cell> Neighbors(Cell cell) => Ring(cell, 2);
    public IEnumerable<Cell> Ring(Cell cell, int distance) {
        return 
            cells
            .Where(c => cell.Distance(c) == distance)
            .OrderBy(c => Mathf.Atan2((c.position - cell.position).y, (c.position - cell.position).x));
    }

    public IEnumerable<Cell> Disc(Cell cell, int distance) {
        for (int d=0; d<distance; d++)
            foreach(var c in Ring(cell, d))
                yield return c;
    }

    public IEnumerable<Cell> FreeNeighbors(Cell cell) => Ring(cell, 2).Where(c => c.cellState == Cell.CellState.Free && c.cellType == Cell.CellType.Field);

    public IEnumerable<Unit> PlayerUnits(PlayerRole player) {
        foreach(var unit in units) {
            if (unit.Player == player)
                yield return unit;
        }
    }

    public void AddUnit(string unitResource, Cell target, PlayerRole owner) {
        units.Add(new Unit(unitResource, target.position, units.Count, owner));
    }
}