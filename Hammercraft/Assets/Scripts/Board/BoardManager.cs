using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoardManager : MonoBehaviour {
    public Board board { get; private set; }
    [SerializeField] private Vector2Int boardSize = new Vector2Int(50, 50);
    [SerializeField] private float perlinNoiseScale;
    [SerializeField] private Vector3 perlinNoiseOffset;

    [SerializeField] private UnityEvent onReset;


    private void Awake() {
        ResetBoard();
    }

    public void ResetBoard() {
        board = new Board(boardSize);
        Cell center = board.GetCell(boardSize.x/2, boardSize.y/2);
        int radius = Mathf.FloorToInt(boardSize.magnitude)/2;
        foreach(var cell in board.Cells()) {
            var pos = board.CellToLocal(cell.position) * perlinNoiseScale + perlinNoiseOffset;
            if (center.Distance(cell) < radius) {
                var height = Mathf.PerlinNoise(pos.x, pos.z);
                if (height < 0.33f)      cell.cellType = Cell.CellType.Water;
                else if (height < 0.66f) cell.cellType = Cell.CellType.Field;
                else                     cell.cellType = Cell.CellType.Mountain;
            } else {
                cell.cellType = Cell.CellType.None;
            }
        }
        onReset.Invoke();
    }
}