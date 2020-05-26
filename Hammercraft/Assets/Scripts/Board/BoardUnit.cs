using UnityEngine;
using TMPro;

public class BoardUnit : BoardBehaviour {
    public int unitId;
    private Unit unit => board.GetUnit(unitId);

    private void Start() {
        SyncPosition();
    }

    private void Update() {
        SyncPosition();
        DisableIfNeeded();
    }

    private void SyncPosition() {
        if (unit != null)
            transform.position = boardManager.transform.TransformPoint(board.LocalPosition(unit));
    }

    private void DisableIfNeeded() {
        if (unit.Dead)
            gameObject.SetActive(false);
    }
}