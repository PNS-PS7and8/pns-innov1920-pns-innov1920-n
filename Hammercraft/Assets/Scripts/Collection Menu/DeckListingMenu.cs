using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckListingMenu : MonoBehaviour
{
    [SerializeField]
    private DeckListing _deckListing;
    [SerializeField]
    private Transform _content;

    public Dictionary<string,DeckListing> ListDecks = new Dictionary<string, DeckListing>();
    public Deck selectedDeck;

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
    
    public void on_click_create_deck(string name) {
        DeckListing listing = null;
        if(!ListDecks.ContainsKey(name)) {
            listing = Instantiate(_deckListing, _content);
        }
        if(listing != null || ListDecks.ContainsKey(name)) {
            ListDecks[name] = (listing!=null) ? listing : ListDecks[name];
            ListDecks[name].SetDeckInfo(new Deck(name));
        }
    }
    public void FetchServer() {
        string user = PlayerPrefs.GetString("username");
        foreach (var deckName in AccessDatabase.GetDecksOf(user)) {
            var deck = AccessDatabase.GetDeck(user, deckName);

            DeckListing listing = Instantiate(_deckListing, _content);
            ListDecks[deck.Name] = listing;
            ListDecks[deck.Name].SetDeckInfo(deck);
        }
    }
}
