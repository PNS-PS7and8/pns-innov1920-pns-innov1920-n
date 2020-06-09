using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Board {
    [SerializeField] private Cell[] cells = null;
    [SerializeField] private List<Unit> units = null;
    public List<Unit> AllUnits => units;
    public List<Unit> Units => units.Where(u => u.Dead == false).ToList();
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

    public int CellIndex(int x, int y) {
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


    public Unit GetUnit(Cell cell, bool dead = false) {
        IEnumerable<Unit> e = units.Where(u => u.position == cell.position && (dead || !u.Dead));
        if (e.Count() > 0) {
            return e.Last();
        } else {
            return null;
        }
    }
    
    public Unit GetUnit(int unitId, bool dead = false) {
        IEnumerable<Unit> e = units.Where(u => u.Id == unitId && (dead || !u.Dead));
        if (e.Count() > 0) {
            return e.Last();
        } else {
            return null;
        }
    }

    public Cell GetCell(Unit unit) {
        return GetCell(unit.position);
    }

    public Vector3 LocalPosition(Unit unit) => CellToLocal(unit.position);
    public Vector3 LocalPosition(Cell cell) => CellToLocal(cell.position);

    public IEnumerable<Cell> Neighbors(Cell cell, bool addDummy=false) {
        int x = cell.position.x;
        int y = cell.position.y;
        int[] dx = new int[] {1,1,0,-1,-1,0};
        int[] dy = new int[] {0,-1,-1,0,1,1};
        for (int i = 0; i < 6; i++) {
            if (addDummy || HasCell(x+dx[i], y+dy[i])) {
                if (addDummy)
                    yield return new Cell(new Vector2Int(x+dx[i], y+dy[i]), Cell.CellType.None);
                else
                    yield return GetCell(x+dx[i], y+dy[i]);
            }
        }
    }

    public IEnumerable<Cell> Ring(Cell cell, int distance) {
        Vector2Int pos = cell.position + new Vector2Int(distance, 0);
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < distance; j++)
            {
                if (HasCell(pos))
                    yield return GetCell(pos);
                pos = Neighbors(cell, true).ElementAt(i).position;
            }
        }
    }

    public IEnumerable<Cell> Disc(Cell cell, int distance) {
        for (int d=0; d<distance; d++)
            foreach(var c in Ring(cell, d))
                yield return c;
    }

    public IEnumerable<Unit> PlayerUnits(PlayerRole player) {
        foreach(var unit in units) {
            if (unit.Player == player)
                yield return unit;
        }
    }

    public void AddUnit(UnitCard card, Cell target, PlayerRole owner) {
        units.Add(new Unit(card, target.position, units.Count, owner));
    }
}