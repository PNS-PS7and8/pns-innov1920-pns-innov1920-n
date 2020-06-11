using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardsListing : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text _name = null;
    [SerializeField] private TMP_Text _cost = null;
    [SerializeField] private CollectionCard cardPrefab = null;

    public CardBase card;
    private CollectionCard collectionCard;


    public void SetCardInfo(CardBase NewCard) {
        this.card = NewCard;
        Image image = GetComponent<Image>();
        image.color = (NewCard is SpellCard)? Color.red : Color.blue;
        _name.text = NewCard.Name;
        _cost.text = NewCard.Cost.ToString();
        this.card = NewCard;
        GameObject cardObject = Instantiate(cardPrefab.gameObject, transform);
        GameObject ListObj = GameObject.Find("ListCards");
        cardObject.transform.parent = ListObj.transform;
        collectionCard = cardObject.GetComponent<CollectionCard>();
        card = NewCard;
        cardObject.SetActive(false);
    }

    public void on_click_card() {
        DeckCardList dc = Object.FindObjectOfType<DeckCardList>();
        dc.AddCard(card);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        collectionCard.card = card;
        collectionCard.transform.localScale = new Vector3(4000,4000,4000);
        collectionCard.transform.localPosition = new Vector3(1150, 0, -50);
        collectionCard.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData data)
    {
        collectionCard.gameObject.SetActive(false);
    }
}
