using System.Linq;
using System.Collections.Generic;
using UnityEngine;

//Objet représentant une cellule composant le plateau (Board)
[System.Serializable]
public class Cell {
    public enum CellState { Free, Occupied, Blocked }
    public enum CellType { Water, Field, Mountain, None }

    public CellType cellType;

    public readonly Vector2Int position;

    public Cell() {
        this.position = new Vector2Int();
        this.cellType = CellType.Field;
    }

    public Cell(Vector2Int position, CellType cellType = CellType.Field) {
        this.position = position;
        this.cellType = cellType;
    }

    public float Height { get {
        switch (cellType) {
            case CellType.Water: return 1;
            case CellType.Field: return 2;
            case CellType.Mountain: return 3;
            default: return 0;
        }
    }}

    //Permet d'obtenir la distance entre deux cellules
    public float Distance(Cell other) {
        return 
            Mathf.Abs(position.x - other.position.x) +
            Mathf.Abs(position.y - other.position.y) +
            Mathf.Abs(position.x + position.y - other.position.x - other.position.y);
    }

    

    
}