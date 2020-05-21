using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
    public Board board { get; private set; }
    [SerializeField] private Vector2Int boardSize = new Vector2Int(50, 50);

    private void Awake() {
        ResetBoard();
    }

    public void ResetBoard() {
        board = new Board(boardSize);
    }
}