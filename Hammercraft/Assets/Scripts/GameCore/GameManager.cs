using System.Text;
using UnityEngine;

[System.Serializable]
public class GameManager {
    [System.Serializable]
    public struct Setup {
        public Vector2Int boardSize;
        public Vector3 noiseOffset;
        public float noiseScale;
    }

    [SerializeField] private Board board;
    [SerializeField] private Player[] players;

    [SerializeField] private int turn;
    [SerializeField] private int playerTurn;
    [SerializeField] private Setup setup;
    [SerializeField] private History history;
    
    public Board Board => board;
    public Player CurrentPlayer => players[playerTurn];
    public History History => history;


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

    public void StartGame(Setup setup, Deck playerOneDeck, Deck playerTwoDeck) {
        history = new History();
        this.setup = setup;
        ResetBoard();
        turn = 0;
        playerTurn = 0;
        players = new Player[2] {
            new Player(playerOneDeck, 0),
            new Player(playerTwoDeck, 1)
        };
    }

    public bool CanPlay(Player player) {
        return player == players[playerTurn];
    }

    public void NextTurn() {
        playerTurn = (playerTurn + 1) % 2;
    }

    public static byte[] Serialize(object manager) {
        string json = JsonUtility.ToJson(manager);
        return Encoding.UTF8.GetBytes(json);
    }

    public static object Deserialize(byte[] data) {
        string json = Encoding.UTF8.GetString(data);
        return JsonUtility.FromJson<GameManager>(json);
    }
}