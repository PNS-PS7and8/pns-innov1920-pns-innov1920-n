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
    private bool isHovered;

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
        

        isHovered = false;
    }

    public void on_click_card() {
        DeckCardList dc = Object.FindObjectOfType<DeckCardList>();
        dc.AddCard(card);
    }

    private void Update() {
        if(isHovered) {
            //collectionCard.transform.localScale = new Vector3(1800,1800,1800);
            collectionCard.transform.position = new Vector3(Input.mousePosition.x-80, Input.mousePosition.y-100, -10);
        } else {
            
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        collectionCard.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData data)
    {
        isHovered = false;
        collectionCard.gameObject.SetActive(false);
    }
}
