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

public class BoardManager : MonoBehaviourPunCallbacks, IPunObservable {
    public Board board => manager.Board;
    public GameManager Manager => manager;
    [SerializeField] private Vector2Int boardSize = new Vector2Int(50, 50);
    [SerializeField] private float perlinNoiseScale;
    [SerializeField] private Vector3 perlinNoiseOffset;
    [SerializeField] private Transform EndTurnButton;
    [SerializeField] private TMP_Text TimerText;
    [SerializeField] private BoardCardDraw[] draws;
    [SerializeField] private Hand _hand;
    [SerializeField] public TMP_Text scoreText1;
    [SerializeField] public TMP_Text scoreText2;
    [SerializeField] public TMP_Text infoWin;
    [SerializeField] public GameObject YourTurnButton;


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
      
        UpdateScore(manager.GetScore(PhotonNetwork.LocalPlayer.ActorNumber), manager.GetScore(-1),1);
    }

    public void UpdateScore(int localscore, int maxscore,int score){
        if(score == 1)
        {
            scoreText1.text = localscore.ToString()+ " / " + maxscore.ToString();

        } else
        {
            scoreText2.text = localscore.ToString() + " / " + maxscore.ToString();

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
        YourTurnButton.SetActive(true) ;
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
        for(int i=60; i > 0; i--)
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
        GameManager.Setup setup = new GameManager.Setup();
        setup.boardSize = boardSize;
        setup.noiseScale = perlinNoiseScale;
        setup.noiseOffset = perlinNoiseOffset;
        Deck deck1 = null;
        Deck deck2 = null;
        /*
        UnitCard c1 = Resources.Load<UnitCard>("Cards/Unit/Noob");
        UnitCard c2 = Resources.Load<UnitCard>("Cards/Unit/Noob");
        UnitCard c3 = Resources.Load<UnitCard>("Cards/Unit/Noob");
        SpellCard c4 = Resources.Load<SpellCard>("Cards/Spell/Fireball");
        Deck deck = new Deck( new UnitCard[] { c1, c1, c2, c2, c3, c3 }, new SpellCard[] { c4, c4, c4, c4, c4 } );
        */
        if (PhotonNetwork.IsConnected)
        {

            deck1 = PlayersExtension.GetDeckLocalPlayer();
            deck2 = PlayersExtension.GetDeckRemotePlayer();
        } else
        {
            
            UnitCard c1 = Resources.Load<UnitCard>("Cards/Unit/Noob");
            UnitCard c2 = Resources.Load<UnitCard>("Cards/Unit/Noob");
            UnitCard c3 = Resources.Load<UnitCard>("Cards/Unit/Noob");
            SpellCard c4 = Resources.Load<SpellCard>("Cards/Spell/Fireball");
            deck1 = new Deck( new UnitCard[] { c1, c1, c2, c2, c3, c3 }, new SpellCard[] { c4, c4, c4, c4, c4 } );
            deck2 = new Deck(new UnitCard[] { c1, c1, c2, c2, c3, c3 }, new SpellCard[] { c4, c4, c4, c4, c4 });

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