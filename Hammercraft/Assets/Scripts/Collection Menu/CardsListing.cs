using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardsListing : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _cost;
    private Image image;

    public void SetCardInfo(CardBase card) {
        image = GetComponent<Image>();
        image.color = (card is SpellCard)? Color.red : Color.blue;
        _name.text = card.Name;
        _cost.text = card.Cost.ToString();
    }

    public void on_click_card() {

    }
}
