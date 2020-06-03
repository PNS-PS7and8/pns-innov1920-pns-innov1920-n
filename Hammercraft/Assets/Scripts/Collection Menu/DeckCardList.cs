using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DeckCardList : MonoBehaviour
{
    [SerializeField] private CollectionCard cardPrefab;
    public CollectionCard[] CardUnit { get; private set; }
    public CollectionCard[] CardSpell { get; private set; }
    private Deck currentDeck = null;
    private int MAX = 5;
    private float spacing = 190f;


    void Start()
    {
        CardUnit = new CollectionCard[MAX];
        CardSpell = new CollectionCard[MAX];
        for (int i = 0; i < CardUnit.Length; i++)
        {
            GameObject cardObject = Instantiate(cardPrefab.gameObject, transform);
            CardUnit[i] = cardObject.GetComponent<CollectionCard>();
            cardObject.SetActive(false);
        }
        for (int i = 0; i < CardSpell.Length; i++)
        {
            GameObject cardObject = Instantiate(cardPrefab.gameObject, transform);
            CardSpell[i] = cardObject.GetComponent<CollectionCard>();
            cardObject.SetActive(false);
        }
    }

    void Reset(){
        foreach(CollectionCard card in CardUnit){
            Destroy(card.gameObject);
        }
        foreach(CollectionCard card in CardSpell){
            Destroy(card.gameObject);
        }
        currentDeck = null;
    }

    public void Save(){
        DeckListingMenu dlm = Object.FindObjectOfType<DeckListingMenu>();
        dlm.setDeck(currentDeck);
    }

    public void AddCard(CardBase cb){
        if (currentDeck != null){

            if (cb.GetType().IsAssignableFrom(typeof(UnitCard)) && currentDeck.units.Count() < MAX){
                for (int i = 0; i < CardUnit.Count(); i++){
                    if(CardUnit[i].gameObject.activeSelf == false){
                        CardUnit[i].card = cb;
                        CardUnit[i].gameObject.SetActive(true);
                        currentDeck.AddCard(cb);
                        break;
                    }
                }
            } 
            
            if (!cb.GetType().IsAssignableFrom(typeof(UnitCard)) && currentDeck.spells.Count() < MAX){
                for (int i = 0; i < CardSpell.Count(); i++){
                    if(CardSpell[i].gameObject.activeSelf == false){
                        CardSpell[i].card = cb;
                        CardSpell[i].gameObject.SetActive(true);
                        currentDeck.AddCard(cb);
                        break;
                    }
                }
            }  
        }
    }

    public void DeleteCard(CollectionCard cc){
        currentDeck.RemoveCard(cc.card);
        cc.gameObject.SetActive(false);
        cc.transform.localScale = new Vector3 (1800,1800,1800);
    }

    public void LoadDeck(Deck deck){
        Reset();
        Start();
        currentDeck = deck;
        List<UnitCard> lu = deck.units;
        List<SpellCard> ls = deck.spells;
        for (int i = 0; i<MAX; i++){
            if (lu.Count > i){
                CardUnit[i].card = lu[i];
                CardUnit[i].gameObject.SetActive(true);
            }
            Vector3 pos = CardUnit[i].transform.localPosition;
            Vector3 scale = new Vector3(1800,1800,1800);
            CardUnit[i].transform.localScale = scale;
            pos.x = (-370)+(i * spacing);
            pos.z = -10;
            pos.y = 200;
            CardUnit[i].transform.localPosition = pos;
        }
        
        for (int i = 0; i<MAX; i++){
            if (ls.Count > i){
                CardSpell[i].card = ls[i];
                CardSpell[i].gameObject.SetActive(true);
            }
            Vector3 pos = CardSpell[i].transform.localPosition;
            Vector3 scale = new Vector3(1800,1800,1800);
            CardSpell[i].transform.localScale = scale;
            pos.x = (-370)+(i * spacing);
            pos.z = -10;
            pos.y = -200;
            CardSpell[i].transform.localPosition = pos;
        }
    }
}
