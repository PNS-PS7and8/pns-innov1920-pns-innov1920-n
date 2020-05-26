using UnityEngine;
using TMPro;

public class BoardUnit : BoardBehaviour {
    public Unit unit;

    private void Start() {
        SyncPosition();
    }

    private void Update() {
        SyncPosition();
        if (unit.Health <= 0) {
            Destroy(gameObject);
        }
    }

    private void SyncPosition() {
        transform.position = boardManager.transform.TransformPoint(board.LocalPosition(unit));
    }
}