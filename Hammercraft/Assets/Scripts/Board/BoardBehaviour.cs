using UnityEngine;

public class BoardBehaviour : MonoBehaviour {
    public BoardManager boardManager {get; private set; }
    public Board board => boardManager?.board;
    public GameManager manager => boardManager?.Manager;

    protected virtual void Awake() {
        boardManager = FindObjectOfType<BoardManager>();
    }

    public virtual void OnResetBoard(BoardManager boardManager) {
        this.boardManager = boardManager;
    }
}