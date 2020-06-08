using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoomDeckListing : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _name = null;
    [SerializeField]
    private Image image = null;

    public Deck Deck {get; set;}
    
    public void SetDeckInfo(Deck deck) {
        Deck = deck;
        _name.text = deck.Name;
    }

    public void on_click_deck() {
        RoomDeckListMenu deckList = Object.FindObjectOfType<RoomDeckListMenu>();
        deckList.SetSelectedDeck(Deck);
    }

    public void SetBgColor(Color color) {
        image.color = color;
    }
}
