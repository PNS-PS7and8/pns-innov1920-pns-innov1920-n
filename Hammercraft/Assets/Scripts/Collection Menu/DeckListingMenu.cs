using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeckListingMenu : MonoBehaviour
{
    [SerializeField]
    private DeckListing _deckListing;
    [SerializeField]
    private Transform _content;

    public Dictionary<string,DeckListing> ListDecks = new Dictionary<string, DeckListing>();
    public Deck selectedDeck;

    public void Awake() {
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
        Deck basicDeck1 = new Deck("Deck One", us1, ss1);
        DeckListing listing1 = Instantiate(_deckListing, _content);
        ListDecks["Deck One"] = listing1;
        ListDecks["Deck One"].SetDeckInfo(basicDeck1);
    }

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
            if (!ListDecks.ContainsKey(deck.Name) || deckName == "Deck One"){
                if(deckName=="Deck One"){
                    ListDecks[deck.Name].SetDeckInfo(deck);
                } else {
                    DeckListing listing = Instantiate(_deckListing, _content);
                    ListDecks[deck.Name] = listing;
                    ListDecks[deck.Name].SetDeckInfo(deck);
                }
            }
        }
    }
}
