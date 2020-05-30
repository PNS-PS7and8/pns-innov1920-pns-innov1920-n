using System.Text;
using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

[System.Serializable]
public class GameManager {
    [System.Serializable]
    public struct Setup {
        public Vector2Int boardSize;
        public Vector3 noiseOffset;
        public float noiseScale;
        public GameModes gameMode;
    }

    [SerializeField] private Board board;
    [SerializeField] private List<Player> players;
    [SerializeField] private int turn;
    [SerializeField] private int midturn;
    [SerializeField] private PlayerRole playerTurn;
    [SerializeField] private Setup setup;
    [SerializeField] private GameHistory history;
    
    public Board Board => board;
    public Player LocalPlayer => players[PlayersExtension.LocalPlayerIndex()];
    public Player RemotePlayer => players[PlayersExtension.RemotePlayerIndex()];
    public Player CurrentPlayer => players[(int)playerTurn];
    public GameHistory History => history;
    public PlayerRole PlayerTurn => playerTurn;
    public int Turn => turn;


    public GameMode GameMode => setup.gameMode.GameMode();
    public GameState GameState => GameMode.CurrentGameState(this);
    public void ResetBoard() {
        board = new Board(setup.boardSize);
        Cell center = board.GetCell(setup.boardSize.x/2, setup.boardSize.y/2);
        int radius = Mathf.FloorToInt(setup.boardSize.magnitude)/2;
        foreach(var cell in board.Cells()) {
            var pos = board.CellToLocal(cell.position) * setup.noiseScale + setup.noiseOffset;
            if (center.Distance(cell) < radius) {
                var height = Mathf.PerlinNoise(pos.x, pos.z);
                if (height < 0.33f)      cell.cellType = Cell.CellType.Water;
                else if (height < 0.66f) cell.cellType = Cell.CellType.Field;
                else                     cell.cellType = Cell.CellType.Mountain;
            } else {
                cell.cellType = Cell.CellType.None;
            }
        }
    }

    public GameManager(Setup setup, Deck playerOneDeck, Deck playerTwoDeck) {
        history = new GameHistory();
        this.setup = setup;
        ResetBoard();
        turn = 1;
        midturn = 0;
        playerTurn = 0;
        players = new List<Player> {
            new Player(playerOneDeck, PlayerRole.PlayerOne),
            new Player(playerTwoDeck, PlayerRole.PlayerTwo)
        };
    }

    public bool CanPlay(PlayerRole player) {
        return player == playerTurn;
    }

    public void NextTurn() {
        playerTurn = playerTurn.Other();
    }
    
    public Player GetPlayer(PlayerRole role) {
        return players[(int)role];
    }

    public static byte[] Serialize(object manager) {
        string json = JsonUtility.ToJson(manager);
        return Encoding.UTF8.GetBytes(json);
    }

    public static object Deserialize(byte[] data) {
        string json = Encoding.UTF8.GetString(data);
        return JsonUtility.FromJson<GameManager>(json);
    }

    public bool MyTurn(){
        return playerTurn == PlayersExtension.LocalPlayer();
    }

    public void IncreaseTurn(){
        if (midturn == 1){
            midturn = 0;
            turn++;
            IncreaseGold();
        } else { 
            midturn = 1; 
        }
    }

    public void IncreaseGold(){
        foreach (Player p in players){
            p.SetGold(turn);
        }
    }

    public void ResetGold(){
        foreach (Player p in players){
            p.SetCurrentGold(p.Gold);
        }
    }
}