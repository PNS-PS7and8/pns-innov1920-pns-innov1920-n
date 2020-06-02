using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeckListing : MonoBehaviour, IPointerDownHandler 
{
    [SerializeField]
    private TMP_Text _name;
    public Deck Deck {get; set;}

    public void Update(){
    
    }

    public void SetDeckInfo(Deck deck) {
        Deck = deck;
        _name.text = deck.name;
    }

    public void OnPointerDown(PointerEventData eventData) {
        DeckCardList dc = Object.FindObjectOfType<DeckCardList>();
        dc.LoadDeck(Deck);
    }
}
