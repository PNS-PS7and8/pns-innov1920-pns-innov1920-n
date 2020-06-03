using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckListingMenu : MonoBehaviour
{
    [SerializeField]
    private DeckListing _deckListing;
    [SerializeField]
    private Transform _content;

    private Dictionary<string,DeckListing> ListDecks = new Dictionary<string, DeckListing>();
    public Deck selectedDeck;

    public void Start() {
        UnitCard c1 = Resources.Load<UnitCard>("Cards/Unit/Noob");
        UnitCard c2 = Resources.Load<UnitCard>("Cards/Unit/Fish");
        UnitCard c3 = Resources.Load<UnitCard>("Cards/Unit/Eagle");
        SpellCard c4 = Resources.Load<SpellCard>("Cards/Spell/Fireball");
        SpellCard c5 = Resources.Load<SpellCard>("Cards/Spell/Heal");
        SpellCard c6 = Resources.Load<SpellCard>("Cards/Spell/Rage");
        UnitCard[] u = new UnitCard[] { c1, c1, c2, c2, c3 };
        SpellCard[] s = new SpellCard[] { c4, c4, c5, c5, c6 };
        Deck basicDeck = new Deck("Deck One", u, s);
        DeckListing listing = Instantiate(_deckListing, _content);
        listing.SetDeckInfo(basicDeck);
        ListDecks["Deck One"] = listing;
        ListDecks["Deck One"].SetDeckInfo(basicDeck);
    }

    public void setDeck(Deck NewDeck){
        foreach(string name in ListDecks.Keys){
            if (name == NewDeck.name){
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
}
