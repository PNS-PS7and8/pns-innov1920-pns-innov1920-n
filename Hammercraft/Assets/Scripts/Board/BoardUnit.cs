using UnityEngine;
using TMPro;
using Photon.Pun;
public class BoardUnit : BoardBehaviour {
    public int unitId;
    private Unit unit => board.GetUnit(unitId);
    private new Renderer renderer;
    [SerializeField] private Material ally;
    [SerializeField] private Material ennemy;

    private void Start() {
        SyncPosition();
        renderer = GetComponent<Renderer>();
        renderer.material = (manager.PlayerTurn == unit.Player) ? ally : ennemy;
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