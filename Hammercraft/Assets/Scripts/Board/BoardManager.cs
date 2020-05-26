using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class BoardManager : MonoBehaviourPun, IPunObservable {
    public Board board => manager.Board;
    [SerializeField] private Vector2Int boardSize = new Vector2Int(50, 50);
    [SerializeField] private float perlinNoiseScale;
    [SerializeField] private Vector3 perlinNoiseOffset;

    public UnityEvent onReset;

    [SerializeField] private GameManager manager;

    private void Awake() {
        PhotonPeer.RegisterType(typeof(GameManager), (byte) 'G', GameManager.Serialize, GameManager.Deserialize);
        // Serializer.Register<Board>((byte) 'B');
        // Serializer.Register<Cell>((byte) 'C');
        // Serializer.Register<Unit>((byte) 'U');
        // Serializer.Register<Player>((byte) 'P');
        // Serializer.Register<Deck>((byte) 'D');
        Reset(NewGame());
        SubmitManager();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) SubmitManager();
    }

    public void SubmitManager() {
        if (PhotonNetwork.IsConnected)
            photonView.RPC("Reset", RpcTarget.All, manager);
    }

    private GameManager NewGame() {
        GameManager manager = new GameManager();
        
        GameManager.Setup setup = new GameManager.Setup();
        setup.boardSize = boardSize;
        setup.noiseScale = perlinNoiseScale;
        setup.noiseOffset = perlinNoiseOffset;
        
        Deck deck = new Deck(
            new UnitCard[] {
                UnitCard.CreateInstance<UnitCard>(), UnitCard.CreateInstance<UnitCard>()
            },
            new SpellCard[] {
                SpellCard.CreateInstance<SpellCard>(), SpellCard.CreateInstance<SpellCard>()
            }
        );

        manager.StartGame(setup, deck, deck);
        
        return manager;
    }

    [PunRPC]
    public void Reset(GameManager manager) {
        this.manager = manager;
        onReset.Invoke();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("Game manager synced");
    }
}