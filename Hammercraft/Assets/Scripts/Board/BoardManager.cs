using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using TMPro;

using PHashTable = ExitGames.Client.Photon.Hashtable;

public class BoardManager : MonoBehaviourPunCallbacks, IPunObservable {
    public Board board => manager.Board;
    public GameManager Manager => manager;
    [SerializeField] private Vector2Int boardSize = new Vector2Int(50, 50);
    [SerializeField] private float perlinNoiseScale;
    [SerializeField] private Vector3 perlinNoiseOffset;
    [SerializeField] private Button EndTurnButton;
    [SerializeField] private TMP_Text TimerText;
    [SerializeField] private DeckUnit DeckUnit;


    public UnityEvent onReset;

    [SerializeField] private GameManager manager;
    private Coroutine _timer = null;
    private bool startOfTurn = false;
    private bool startOfEnnemyTurn;

    private void Awake() {
        PhotonPeer.RegisterType(typeof(GameManager), (byte) 'G', GameManager.Serialize, GameManager.Deserialize);
        
        Reset(NewGame());

        if (PhotonNetwork.IsConnectedAndReady) {
            if (PhotonNetwork.LocalPlayer.ActorNumber == PhotonNetwork.PlayerList[0].ActorNumber) {
                PlayersExtension.RegisterLocalPlayer(PlayerRole.PlayerOne);
                SubmitManager();
            } else {
                PlayersExtension.RegisterLocalPlayer(PlayerRole.PlayerTwo);
            }
        }
    }

    private void Start() {
        startOfTurn = manager.MyTurn();
        startOfEnnemyTurn = !startOfTurn;
    }

    private IEnumerator waitForWin(){
            WinText text = WinText.FindObjectOfType<WinText>();
            text.OnWin(Manager.GameState);
            SubmitManager();
            yield return new WaitForSecondsRealtime(1f);
            PhotonNetwork.LeaveRoom();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) SubmitManager();
        if (manager.GameState.Finished() && PhotonNetwork.IsConnectedAndReady) {
            StartCoroutine(waitForWin());
        }
        if (manager.MyTurn() && startOfTurn)
        {
            startOfTurn = false;
            StartOfTurn();
        }
        if (!manager.MyTurn() && startOfEnnemyTurn)
        {
            startOfEnnemyTurn = false;
            StartOfEnnemyTurn();
        }
       
    }

    private void StartOfTurn()
    {
        startOfEnnemyTurn = true;
        if (_timer != null)
        {
            StopCoroutine(_timer);
        }
        _timer = StartCoroutine(Timer());
        EndTurnButton.enabled = true;
        EndTurnButton.gameObject.GetComponent<Image>().color = new Color(1, 0.75f, 0);
        EndTurnButton.GetComponentInChildren<TMP_Text>().text = "END TURN";
        DeckUnit.DrawUnit(manager.CurrentPlayer.DrawUnit());
    }

    private void StartOfEnnemyTurn()
    {
        startOfTurn = true;
        if(_timer != null)
        {
            StopCoroutine(_timer);
        }
        _timer = StartCoroutine(Timer());
        EndTurnButton.enabled = (PhotonNetwork.IsConnected) ? false : true;
        EndTurnButton.gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        EndTurnButton.GetComponentInChildren<TMP_Text>().text = "ENNEMY TURN";
    }

    private IEnumerator Timer()
    {
        for(int i=45; i > 0; i--)
        {
            TimerText.text = i.ToString();
            
            TimerText.color = (i <= 15 && manager.MyTurn()) ? Color.red : Color.black;
            
            yield return new WaitForSecondsRealtime(1f);
        }
        if (manager.MyTurn() || !PhotonNetwork.IsConnected)
        {
            NextTurn();
        }
    }

    public void SubmitManager() {
        if (PhotonNetwork.IsConnected)
            photonView.RPC("Reset", RpcTarget.All, manager);
    }

    public static Photon.Realtime.Player RemotePhotonPlayer => PhotonNetwork.PlayerListOthers[0];
    public static Photon.Realtime.Player LocalPhotonPlayer => PhotonNetwork.LocalPlayer;

    private GameManager NewGame() {
        GameManager.Setup setup = new GameManager.Setup();
        setup.boardSize = boardSize;
        setup.noiseScale = perlinNoiseScale;
        setup.noiseOffset = perlinNoiseOffset;
        setup.gameMode = GameModes.KillToWin;
        
        UnitCard c1 = Resources.Load<UnitCard>("Cards/Noob");
        UnitCard c2 = Resources.Load<UnitCard>("Cards/Fish");
        UnitCard c3 = Resources.Load<UnitCard>("Cards/Eagle");
        SpellCard c4 = Resources.Load<SpellCard>("Cards/Fireball");
        Deck deck = new Deck( new UnitCard[] { c1, c1, c2, c2, c3, c3 }, new SpellCard[] { c4, c4, c4, c4, c4 } );

        GameManager manager = new GameManager(setup, deck, deck);
        
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
        PhotonNetwork.JoinLobby();
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        SceneManager.LoadScene("Rooms");
        PhotonNetwork.JoinLobby();
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