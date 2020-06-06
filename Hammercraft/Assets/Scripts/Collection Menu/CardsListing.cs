using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardsListing : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private CollectionCard cardPrefab;

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
        Vector3 posList = transform.localPosition;
        collectionCard.transform.localScale = new Vector3(4000,4000,4000);
        collectionCard.transform.localPosition = new Vector3(-500, -300, -50);
        collectionCard.transform.localPosition -= posList;
        collectionCard.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData data)
    {
        collectionCard.gameObject.SetActive(false);
    }
}
