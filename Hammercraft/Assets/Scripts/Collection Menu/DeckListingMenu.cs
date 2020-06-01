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

    public void Start() {
        UnitCard c1 = Resources.Load<UnitCard>("Cards/Noob");
        UnitCard c2 = Resources.Load<UnitCard>("Cards/Fish");
        UnitCard c3 = Resources.Load<UnitCard>("Cards/Eagle");
        SpellCard c4 = Resources.Load<SpellCard>("Cards/Fireball");
        Deck basicDeck = new Deck("Basic deck", new UnitCard[] { c1, c1, c2, c2, c3, c3 }, new SpellCard[] { c4, c4, c4, c4, c4 } );
        DeckListing listing = Instantiate(_deckListing, _content);
        ListDecks["Basic deck"] = listing;
        ListDecks["Basic deck"].SetDeckInfo(basicDeck);
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
