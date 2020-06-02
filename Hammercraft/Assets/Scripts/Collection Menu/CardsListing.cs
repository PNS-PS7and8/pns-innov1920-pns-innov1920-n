using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardsListing : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private CollectionCard cardPrefab;

    public CardBase card;
    private CollectionCard collectionCard;

    public void SetCardInfo(CardBase card) {
        this.card = card;
        Image image = GetComponent<Image>();
        image.color = (card is SpellCard)? Color.red : Color.blue;
        _name.text = card.Name;
        _cost.text = card.Cost.ToString();

        GameObject cardObject = Instantiate(cardPrefab.gameObject, transform);
        collectionCard = cardObject.GetComponent<CollectionCard>();
        cardObject.SetActive(false);
    }

    public void on_click_card() {
        DeckCardList dc = Object.FindObjectOfType<DeckCardList>();
        dc.AddCard(card);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        collectionCard.card = card;
        collectionCard.gameObject.SetActive(true);
        collectionCard.transform.localScale = new Vector3(1800,1800,1800);
        collectionCard.transform.localPosition = Input.mousePosition;
    }

    public void OnMouseExit()
    {
        collectionCard.gameObject.SetActive(false);
    }
}
