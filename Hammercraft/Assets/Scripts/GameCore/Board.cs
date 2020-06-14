using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Classe représentant le plateau de jeu hexagonale

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

    //Permet d'obtenir la cell à l'index x y donné
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

    //Retourne la cellule pour la position demandée
    public Cell GetCell(Vector2Int cellPosition) {
        return GetCell(cellPosition.x, cellPosition.y);
    }

    public Cell GetCell(int x, int y) {
        return cells[CellIndex(x, y)];
        
    }

    //Permet d'obtenir les coordonnées dans l'espace de la cellule donnée
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

    //Permet d'obtenir la cellule présente aux coordonnées dans l'espace données
    public Vector2Int LocalToCell(Vector3 point) {
        return new Vector2Int(Mathf.RoundToInt(point.x * 2f / 3f), Mathf.RoundToInt(point.x / -3f + Mathf.Sqrt(3f)/3f * point.z)) + size / 2;
    }

    //Permet d'obtenir l'unité présente sur la cellule donnée
    //null si aucune cellule n'est présente
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

    //Permet d'obtenir la cellule sur laquelle se trouve l'unité
    public Cell GetCell(Unit unit) {
        return GetCell(unit.position);
    }

    public Vector3 LocalPosition(Unit unit) => CellToLocal(unit.position);
    public Vector3 LocalPosition(Cell cell) => CellToLocal(cell.position);

    //Permet d'obtenir une liste de cellules voisines pour la position ou la cellule donnée
    public IEnumerable<Cell> Neighbors(Vector2Int pos, bool addDummy=false) {
        int[] dx = new int[] {1,1,0,-1,-1,0};
        int[] dy = new int[] {0,-1,-1,0,1,1};
        for (int i = 0; i < 6; i++) {
            if (addDummy || HasCell(pos.x+dx[i], pos.y+dy[i])) {
                if (addDummy)
                    yield return new Cell(new Vector2Int(pos.x+dx[i], pos.y+dy[i]), Cell.CellType.None);
                else
                    yield return GetCell(pos.x+dx[i], pos.y+dy[i]);
            }
        }
    }

    public IEnumerable<Cell> Neighbors(Cell cell, bool addDummy=false) {
        return Neighbors(cell.position, addDummy);
    }

    //Permet d'obtenir une liste de cellules en anneau autour d'une cellule pour une distance donnée
    public IEnumerable<Cell> Ring(Cell cell, int distance) {
        if (distance == 0){
            yield return cell;
        } else {
            Vector2Int pos = cell.position + new Vector2Int(-distance, distance);
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < distance; j++)
                {
                    if (HasCell(pos))
                        yield return GetCell(pos);
                    pos = Neighbors(pos, true).ElementAt(i).position;
                }
            }
        }
    }

    //Permet d'obtenir une liste de toutes les cellules autour d'une cellule pour une distance donnée
    public IEnumerable<Cell> Disc(Cell cell, int distance) {
        for (int d=0; d<distance; d++)
            foreach(var c in Ring(cell, d))
                yield return c;
    }

    //Retourne la liste des unités appartenant au player donnée
    public IEnumerable<Unit> PlayerUnits(PlayerRole player) {
        foreach(var unit in units) {
            if (unit.Player == player)
                yield return unit;
        }
    }

    //Permet d'ajouter une unité sur une cellule du plateau
    public void AddUnit(UnitCard card, Cell target, PlayerRole owner) {
        units.Add(new Unit(card, target.position, units.Count, owner));
    }
}