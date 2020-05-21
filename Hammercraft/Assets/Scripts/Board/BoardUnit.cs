using UnityEngine;
using TMPro;

public class BoardUnit : BoardBehaviour {
    public Unit unit;

    protected override void Start() {
        base.Start();
    }

    private void Update() {
        transform.position = boardManager.transform.TransformPoint(board.CellToLocal(unit.Cell.position));
        if (unit.Health <= 0) {
            Destroy(gameObject);
        }
    }
}