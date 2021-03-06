using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using TMPro;

using PHashTable = ExitGames.Client.Photon.Hashtable;
using DG.Tweening;

//Classe s'occupant de la synchronization des joueurs lors d'une partie en réseau
public class BoardManager : MonoBehaviourPunCallbacks, IPunObservable {
    public Board board => manager.Board;
    public GameManager Manager => manager;
    public GameObject ValidateButton => validateButton;
    [SerializeField] private Vector2Int boardSize = new Vector2Int(50, 50);
    [SerializeField] private float perlinNoiseScale = 0.0f;
    [SerializeField] private Vector3 perlinNoiseOffset = Vector3.zero;
    [SerializeField] private Transform EndTurnButton = null;
    [SerializeField] private TMP_Text TimerText = null;
    [SerializeField] private BoardCardDraw[] draws = null;
    [SerializeField] private Hand _hand = null;
    [SerializeField] public TMP_Text scoreText1 = null;
    [SerializeField] public TMP_Text scoreText2 = null;
    [SerializeField] public TMP_Text infoWin = null;
    [SerializeField] public GameObject YourTurnButton = null;
    [SerializeField] private BoardPlayer BoardPlayer = null;
    [SerializeField] private GameObject validateButton = null;
    [SerializeField] private GameObject DrawUI = null;


    public Hand Hand { get { return _hand; } }

    
    public UnityEvent onReset;

    [SerializeField] private GameManager manager;
    private Coroutine _timer = null;
    private bool startOfTurn = false;
    private bool startOfEnnemyTurn;

    private void Awake() {
        PhotonPeer.RegisterType(typeof(GameManager), (byte) 'G', GameManager.Serialize, GameManager.Deserialize);
        
        Reset(NewGame());
        SubmitManager();
    }

    private void Start() {
        startOfTurn = manager.MyTurn();
        startOfEnnemyTurn = !startOfTurn;
    }

    private IEnumerator waitForWin(){
            WinText text = infoWin.GetComponent<WinText>();
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
      
        UpdateScore(manager.GetScore(PlayersExtension.LocalPlayer()), manager.GetScore(PlayerRole.Spectator),1);
    }

    public void UpdateScore(int localscore, int maxscore,int score){
        if(score == 1)
        {
            scoreText1.text = "My Score\n" + localscore.ToString()+ " / " + maxscore.ToString();

        } else
        {
            scoreText2.text = "Ennemy Score\n" + localscore.ToString() + " / " + maxscore.ToString();

        }
    }

    private void StartOfTurn()
    {
       
        StartCoroutine(YourTurn());
        startOfEnnemyTurn = true;
        if (_timer != null)
        {
            StopCoroutine(_timer);
        }
        _timer = StartCoroutine(Timer());
        EndTurnButton.gameObject.GetComponent<BoxCollider>().enabled = true;
        EndTurnButton.DORotate(new Vector3(90, 0, 180), 0.2f);
        DOTween.Play(EndTurnButton);

        if (draws != null) {
            foreach(var d in draws) {
                d.AllowDraw();
            }
        }
    }

    IEnumerator YourTurn(){
        if (manager.Turn == 1)
        {
            BoardPlayer.Mulligan();
            yield return new WaitUntil(() => validateButton.activeInHierarchy);
            yield return new WaitWhile(() => validateButton.activeInHierarchy);

        }
            yield return new WaitUntil(() => DrawUI.activeInHierarchy);
            yield return new WaitWhile(() => DrawUI.activeInHierarchy);
        
        YourTurnButton.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        YourTurnButton.SetActive(false);
    }

    private void StartOfEnnemyTurn()
    {
        startOfTurn = true;
        if (_timer != null)
        {
            StopCoroutine(_timer);
        }
        _timer = StartCoroutine(Timer());
        EndTurnButton.gameObject.GetComponent<BoxCollider>().enabled = (PhotonNetwork.IsConnected) ? false : true;
        EndTurnButton.DORotate(new Vector3(-90, 0, 180), 0.2f);
        DOTween.Play(EndTurnButton);
    }

    private IEnumerator Timer()
    {
        TimerText.enabled = false;
        if (manager.Turn == 1)
        {
            yield return new WaitUntil(() => validateButton.activeInHierarchy);
            yield return new WaitWhile(() => validateButton.activeInHierarchy);
        }
        yield return new WaitUntil(() => DrawUI.activeInHierarchy);
        yield return new WaitWhile(() => DrawUI.activeInHierarchy);

        for (int i=60; i > 0; i--)
        {
            TimerText.text = i.ToString();
            
            TimerText.enabled = (i <= 15 && manager.MyTurn());
            
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
        GameModes mode = GameModes.DestroyTheBase;
        if (PhotonNetwork.InRoom) {
            mode = (GameModes) PhotonNetwork.CurrentRoom.CustomProperties["GameMode"];
        }
        GameManager.Setup setup = new GameManager.Setup();
        setup.boardSize = boardSize;
        setup.noiseScale = perlinNoiseScale;
        setup.noiseOffset = perlinNoiseOffset;
        setup.gameMode = mode;
        Deck deck1 = null;
        Deck deck2 = null;
        if (PhotonNetwork.IsConnected)
        {

            deck2 = PlayersExtension.GetDeckLocalPlayer();
            deck1 = PlayersExtension.GetDeckRemotePlayer();
        } else
        {
            
            UnitCard c1 = Resources.Load<UnitCard>("Cards/Unit/Noob");
            UnitCard c2 = Resources.Load<UnitCard>("Cards/Unit/Eagle");
            UnitCard c3 = Resources.Load<UnitCard>("Cards/Unit/Dragon");
            SpellCard c4 = Resources.Load<SpellCard>("Cards/Spell/Fireball");
            deck1 = new Deck( new UnitCard[] { c1, c1, c2, c2, c3, c3, c2, c3, c3}, new SpellCard[] { c4, c4, c4, c4, c4 } );
            deck2 = new Deck(new UnitCard[] { c1, c1, c2, c2, c3, c3, c2, c3, c3 }, new SpellCard[] { c4, c4, c4, c4, c4 });

        }

        GameManager manager = new GameManager(setup, deck1, deck2);
        
        return manager;
    }

    [PunRPC]
    public void Reset(GameManager manager) {
        this.manager = manager;
        onReset.Invoke();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
       // Debug.Log("Game manager synced");
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