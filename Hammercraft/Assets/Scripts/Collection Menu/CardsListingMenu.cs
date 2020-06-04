using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class CardsListingMenu : MonoBehaviour
{
    [SerializeField]
    private CardsListing _cardsListing;
    [SerializeField]
    private Transform _content;
    [SerializeField] private TMP_InputField field;

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
        //Destroy(cardsToDisplay["Fireball"]);
        //Debug.Log(cardsToDisplay["Fireball"]);
        /*
        foreach(string name in cardsToDisplay.Keys) {
            cardsToDisplay[name].SetActive(name.ToLower().Contains(field.text));
        }*/
    }
}
