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
        _deckListingMenu.Start(); // A enlever quand on aura BDD
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

    public void SetSelectedDeck(Deck deck) {
        button.SetActive(true);
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
