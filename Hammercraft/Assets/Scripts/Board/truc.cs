using UnityEngine;

public class truc : MonoBehaviour {
    public BoardClicker gc;

    private void Update() {
        if (!gc) return;
        if (gc.HoverCell(out var cell)) {
            transform.position = gc.transform.TransformPoint(gc.board.CellToLocal(cell.position));
        }
    }
}