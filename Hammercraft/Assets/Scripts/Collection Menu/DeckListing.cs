using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeckListing : MonoBehaviour, IPointerDownHandler 
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

    public void OnPointerDown(PointerEventData eventData) {
        DeckCardList dc = Object.FindObjectOfType<DeckCardList>();
        dc.LoadDeck(Deck);
        DeckListingMenu dlm = Object.FindObjectOfType<DeckListingMenu>();
        dlm.ClickDeck(Deck);
    }

    public void SetBgColor(Color color) {
        image.color = color;
    }
}
