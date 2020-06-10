using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeckListingMenu : MonoBehaviour
{
    [SerializeField]
    private DeckListing _deckListing = null;
    [SerializeField]
    private Transform _content = null;

    public Dictionary<string,DeckListing> ListDecks = new Dictionary<string, DeckListing>();
    private Deck selectedDeck = null;

    private void OnEnable() {
        FetchServer();  
    }

    public void setDeck(Deck NewDeck){
        foreach(string name in ListDecks.Keys){
            if (name == NewDeck.Name){
                ListDecks[name].Deck = NewDeck;
            }
        }
    }

    public void RemoveDeck(Deck deck) {
        Destroy(ListDecks[deck.Name].gameObject);
        ListDecks.Remove(deck.Name);
    }

    public void ClickDeck(Deck deck) {
        if(selectedDeck != null) {
            ListDecks[selectedDeck.Name].SetBgColor(new Color(130/255f, 130/255f, 130/255f));
        }
        selectedDeck = deck;
        ListDecks[selectedDeck.Name].SetBgColor(new Color(65/255f, 200/255f, 65/255f));
    }
    
    public void on_click_create_deck(TMP_InputField field) {
        string name = field.text;
        if(name != "") {
            DeckListing listing = null;
            if(!ListDecks.ContainsKey(name)) {
                listing = Instantiate(_deckListing, _content);
            }
            if(listing != null || ListDecks.ContainsKey(name)) {
                ListDecks[name] = (listing!=null) ? listing : ListDecks[name];
                ListDecks[name].SetDeckInfo(new Deck(name));
            }
        }
    }
    public void FetchServer() {
        string user = PlayerPrefs.GetString("username");
        foreach (var deckName in AccessDatabase.GetDecksOf(user)) {
            var deck = AccessDatabase.GetDeck(user, deckName);
            if (!ListDecks.ContainsKey(deck.Name)){
                DeckListing listing = Instantiate(_deckListing, _content);
                ListDecks[deck.Name] = listing;
                ListDecks[deck.Name].SetDeckInfo(deck);
            }
        }
    }
}
