using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class CardsListingMenu : MonoBehaviour
{
    [SerializeField]
    private CardsListing _cardsListing = null;
    [SerializeField]
    private Transform _content = null;
    [SerializeField] private TMP_InputField field = null;
    [SerializeField] private Toggle unitToggle = null;
    [SerializeField] private Toggle spellToggle = null;

    private Dictionary<string,CardsListing> cardsToDisplay = new Dictionary<string, CardsListing>();
    private List<CardBase> listCards = new List<CardBase>();
    void Start()
    {
        HashSet<CardBase> cards = new HashSet<CardBase>();
        int mostExpensive = 0;
        foreach (UnitCard card in Resources.LoadAll("Cards/Unit", typeof(UnitCard)))
        {
            if(card.Cost > mostExpensive) {
                mostExpensive = card.Cost;
            }
            cards.Add(card);
        }

        foreach (SpellCard card in Resources.LoadAll("Cards/Spell", typeof(SpellCard)))
        {
            if(card.Cost > mostExpensive) {
                mostExpensive = card.Cost;
            }
            cards.Add(card);
        }
        Sort(cards, mostExpensive);
        foreach(CardBase card in listCards) {
            cardsToDisplay[card.Name] = Instantiate(_cardsListing, _content);
            cardsToDisplay[card.Name].SetCardInfo(card);
        }
        valueChanged();   
    }

    private void Sort(HashSet<CardBase> cards, int mostExpensive) {
        List<CardBase> toRemove;
        for(int i=1; i<=mostExpensive; i++) {
            toRemove = new List<CardBase>();
            foreach(CardBase card in cards) {
                if(card.Cost == i) {
                    listCards.Add(card);
                    toRemove.Add(card);
                }
            }
            foreach(CardBase card in toRemove) {
                cards.Remove(card);
            }
        }
    }

    public void valueChanged() {
        foreach(CardBase cb in listCards) {
            if (cardsToDisplay[cb.Name] != null)
                Destroy(cardsToDisplay[cb.Name].gameObject);
        }
        foreach(CardBase cb in listCards) {
            if(cb.Name.ToLower().Contains(field.text.ToLower()) && 
            ((cb is UnitCard && unitToggle.isOn) || (cb is SpellCard && spellToggle.isOn))) {
                cardsToDisplay[cb.Name] = Instantiate(_cardsListing, _content);
                cardsToDisplay[cb.Name].SetCardInfo(cb);
            }
        }
    }
}
