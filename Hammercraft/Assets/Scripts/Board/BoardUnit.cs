using UnityEngine;
using TMPro;
using Photon.Pun;
public class BoardUnit : BoardBehaviour {
    public int unitId;
    private Unit unit => board.GetUnit(unitId);
    private new Renderer renderer;

    private void Start() {
        SyncPosition();
        renderer = GetComponent<Renderer>();
    }

    private void Update() {
        SyncPosition();
        DisableIfNeeded();
    }

    private void SyncPosition() {
        if (unit != null) {
            transform.position = boardManager.transform.TransformPoint(board.LocalPosition(unit));
            transform.localScale = Vector3.one * unit.Card.ModelScale;
        }
    }

    private void DisableIfNeeded() {
        if (unit.Dead)
            gameObject.SetActive(false);
    }
}