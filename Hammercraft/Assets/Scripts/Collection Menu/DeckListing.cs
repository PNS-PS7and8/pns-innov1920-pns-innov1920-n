using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckListing : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _name;

    public Deck Deck {get; private set;}
    public void SetDeckInfo(Deck deck) {
        Deck = deck;
        _name.text = deck.name;
    }

    public void on_click_show_deck() {

    }
}
