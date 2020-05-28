using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;

public class BoardManager : MonoBehaviourPunCallbacks, IPunObservable {
    public Board board => manager.Board;
    public GameManager Manager => manager;
    [SerializeField] private Vector2Int boardSize = new Vector2Int(50, 50);
    [SerializeField] private float perlinNoiseScale;
    [SerializeField] private Vector3 perlinNoiseOffset;

    public UnityEvent onReset;

    [SerializeField] private GameManager manager;

    private void Awake() {
        PhotonPeer.RegisterType(typeof(GameManager), (byte) 'G', GameManager.Serialize, GameManager.Deserialize);
        Reset(NewGame());
        SubmitManager();
    }

    private IEnumerator waitForWin(){
            WinText text = WinText.FindObjectOfType<WinText>();
            text.OnWin();
            SubmitManager();
            yield return new WaitForSecondsRealtime(1f);
            PhotonNetwork.LeaveRoom();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) SubmitManager();
        if (manager.GameState.Finished() && PhotonNetwork.IsConnectedAndReady) {
            StartCoroutine(waitForWin());
        }
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
        setup.gameMode = GameModes.KillToWin;
        
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
    public override void OnLeftRoom()
    {

        SceneManager.LoadScene("Rooms");
    }

    public void NextTurn(){
        if (manager.MyTurn() || !PhotonNetwork.IsConnected){
            manager.IncreaseTurn();
            manager.ResetGold();
            manager.NextTurn();
            SubmitManager();
        }
    }

}