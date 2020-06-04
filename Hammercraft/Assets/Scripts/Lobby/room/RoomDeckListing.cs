using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomDeckListing : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _name;

    public Deck Deck {get; set;}
    
    public void SetDeckInfo(Deck deck) {
        Deck = deck;
        _name.text = deck.name;
    }

    public void on_click_deck() {
        RoomDeckListMenu deckList = Object.FindObjectOfType<RoomDeckListMenu>();
        deckList.SetSelectedDeck(Deck);
    }
}
