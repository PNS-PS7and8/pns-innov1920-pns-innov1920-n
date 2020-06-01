using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardsListingMenu : MonoBehaviour
{
    [SerializeField]
    private CardsListing _cardsListing;
    [SerializeField]
    private Transform _content;

    private Dictionary<string,CardsListing> ListCards = new Dictionary<string, CardsListing>();
    void Start()
    {
        foreach (UnitCard card in Resources.LoadAll("Cards/Unit", typeof(UnitCard)))
        {
            ListCards[card.Name] = Instantiate(_cardsListing, _content);
            ListCards[card.Name].SetCardInfo(card);
        }

        foreach (SpellCard card in Resources.LoadAll("Cards/Spell", typeof(SpellCard)))
        {
            ListCards[card.Name] = Instantiate(_cardsListing, _content);
            ListCards[card.Name].SetCardInfo(card);
        }        
    }
}
