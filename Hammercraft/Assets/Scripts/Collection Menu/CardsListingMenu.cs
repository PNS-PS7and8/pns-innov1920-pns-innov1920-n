using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class CardsListingMenu : MonoBehaviour
{
    [SerializeField]
    private CardsListing _cardsListing = null;
    [SerializeField]
    private Transform _content = null;
    [SerializeField] private TMP_InputField field = null;

    private Dictionary<string,CardsListing> cardsToDisplay = new Dictionary<string, CardsListing>();
    private List<CardBase> listCards = new List<CardBase>();
    void Start()
    {
        foreach (UnitCard card in Resources.LoadAll("Cards/Unit", typeof(UnitCard)))
        {
            listCards.Add(card);
            cardsToDisplay[card.Name] = Instantiate(_cardsListing, _content);
            cardsToDisplay[card.Name].SetCardInfo(card);
        }

        foreach (SpellCard card in Resources.LoadAll("Cards/Spell", typeof(SpellCard)))
        {
            listCards.Add(card);
            cardsToDisplay[card.Name] = Instantiate(_cardsListing, _content);
            cardsToDisplay[card.Name].SetCardInfo(card);
        }
        valueChanged();   
    }

    public void valueChanged() {
        foreach(CardBase cb in listCards) {
            if (cardsToDisplay[cb.Name] != null)
                Destroy(cardsToDisplay[cb.Name].gameObject);
        }
        foreach(CardBase cb in listCards) {
            if(cb.Name.ToLower().Contains(field.text.ToLower())) {
                cardsToDisplay[cb.Name] = Instantiate(_cardsListing, _content);
                cardsToDisplay[cb.Name].SetCardInfo(cb);
            }
        }
    }
}
