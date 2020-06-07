using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;

public class RoomDeckListMenu : MonoBehaviour
{
    [SerializeField]
    private RoomDeckListing _roomDeckListing = null;
    [SerializeField]
    private Transform _content = null;
    [SerializeField]
    private DeckListingMenu _deckListingMenu = null;
    [SerializeField]
    private GameObject button;
    private Deck selectedDeck = null;
    private bool display = false;
    
    private Dictionary<Deck,RoomDeckListing> ListDecks = new Dictionary<Deck, RoomDeckListing>();

    private void Awake() {
        PhotonPeer.RegisterType(typeof(Deck), (byte) 'D', Deck.Serialize, Deck.Deserialize);
    }

    private void Update() {
        if(gameObject.activeSelf && !display) {
            DisplayDecks();
            display = true;
        }
        else if (!gameObject.activeSelf) {
            display = false;
        }
    }

    private void DisplayDecks() {
        _deckListingMenu.FetchServer(); // A enlever quand on aura BDD
        if (_deckListingMenu.ListDecks.Count == 0){
            UnitCard u1 = Resources.Load<UnitCard>("Cards/Unit/Noob");
            UnitCard u2 = Resources.Load<UnitCard>("Cards/Unit/Fish");
            UnitCard u3 = Resources.Load<UnitCard>("Cards/Unit/Eagle");
            UnitCard u4 = Resources.Load<UnitCard>("Cards/Unit/Pro");
            UnitCard u5 = Resources.Load<UnitCard>("Cards/Unit/Big Eagle");
            SpellCard s1 = Resources.Load<SpellCard>("Cards/Spell/Fireball");
            SpellCard s2 = Resources.Load<SpellCard>("Cards/Spell/Heal");
            SpellCard s3 = Resources.Load<SpellCard>("Cards/Spell/Rage");
            SpellCard s4 = Resources.Load<SpellCard>("Cards/Spell/Tidal wave");
            SpellCard s5 = Resources.Load<SpellCard>("Cards/Spell/Big Fireball");
            UnitCard[] us1 = new UnitCard[] { u1, u1, u2, u2, u3 , u3, u4, u4, u5, u5 };
            SpellCard[] ss1 = new SpellCard[] { s1, s1, s2, s2, s3 , s3, s4, s4, s5, s5 };
            Deck deck = new Deck("Default Deck", us1, ss1);
            RoomDeckListing listing = Instantiate(_roomDeckListing, _content);
            listing.SetDeckInfo(deck);
            ListDecks[deck] = listing;
            ListDecks[deck].SetDeckInfo(deck);
            selectedDeck = deck;
        } else {
            foreach (DeckListing deckListing in _deckListingMenu.ListDecks.Values)
            {
                Deck deck = deckListing.Deck;            
                RoomDeckListing listing = Instantiate(_roomDeckListing, _content);
                listing.SetDeckInfo(deck);
                ListDecks[deck] = listing;
                ListDecks[deck].SetDeckInfo(deck);
                selectedDeck = deck;
            } 
        }
    }

    public void SetSelectedDeck(Deck deck) {
        button.SetActive(true);
        Debug.Log(deck.Name);
        selectedDeck = deck;
    }

    public void on_click_confirm_deck() {
        if (PhotonNetwork.IsConnectedAndReady) {
            if (PhotonNetwork.LocalPlayer.ActorNumber == PhotonNetwork.PlayerList[0].ActorNumber) {
                PlayersExtension.RegisterLocalPlayer(PlayerRole.PlayerOne, selectedDeck);
            } else {
                PlayersExtension.RegisterLocalPlayer(PlayerRole.PlayerTwo, selectedDeck);
            }
            gameObject.SetActive(false);
        }
    }
}
