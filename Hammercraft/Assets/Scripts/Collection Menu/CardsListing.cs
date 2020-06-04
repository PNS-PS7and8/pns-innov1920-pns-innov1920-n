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


    public void SetCardInfo(CardBase card) {
        this.card = card;
        Image image = GetComponent<Image>();
        image.color = (card is SpellCard)? Color.red : Color.blue;
        _name.text = card.Name;
        _cost.text = card.Cost.ToString();

        GameObject cardObject = Instantiate(cardPrefab.gameObject, transform);
        collectionCard = cardObject.GetComponent<CollectionCard>();
        cardObject.SetActive(false);
        collectionCard.card = card;
        collectionCard.transform.localScale = new Vector3(1800,1800,1800);

        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        pos.z += 200;
        collectionCard.transform.localPosition = pos;
    }

    public void on_click_card() {
        DeckCardList dc = Object.FindObjectOfType<DeckCardList>();
        dc.AddCard(card);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        collectionCard.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData data)
    {
        collectionCard.gameObject.SetActive(false);
    }
}
