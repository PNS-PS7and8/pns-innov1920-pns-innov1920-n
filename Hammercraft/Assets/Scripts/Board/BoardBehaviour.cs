using UnityEngine;

public class BoardBehaviour : MonoBehaviour {
    public BoardManager boardManager {get; private set; }
    public Board board => boardManager?.board;

    protected virtual void Start() {
        boardManager = FindObjectOfType<BoardManager>();
    }
}