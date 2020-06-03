﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DeckCardList : MonoBehaviour
{
    [SerializeField] private CollectionCard cardPrefab;
    public CollectionCard[] CardUnit { get; private set; }
    public CollectionCard[] CardSpell { get; private set; }
    private Deck currentDeck;
    private Deck savedDeck;
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
        savedDeck = null;
    }

    public void Save(){
        DeckListingMenu dlm = Object.FindObjectOfType<DeckListingMenu>();
        dlm.setDeck(currentDeck);
    }

    public void AddCard(CardBase cb){
        string type = cb.GetType().ToString();
        if (cb.GetType().IsAssignableFrom(typeof(UnitCard)) && currentDeck.units.Count() < MAX){
            currentDeck.units.Add((UnitCard)cb);
            for (int i = 0; i < CardUnit.Count(); i++){
                if(CardUnit[i].gameObject.active == false){
                    CardUnit[i].card = cb;
                    CardUnit[i].gameObject.SetActive(true);
                    break;
                }
            }
        } else if (currentDeck.spells.Count() < MAX){
            currentDeck.spells.Add((SpellCard)cb);
            for (int i = 0; i < CardSpell.Count(); i++){
                if(CardSpell[i].gameObject.active == false){
                    CardSpell[i].card = cb;
                    CardSpell[i].gameObject.SetActive(true);
                    break;
                }
            }
        }  
    }

    public void DeleteCard(CollectionCard cc){
        if (cc.card.GetType().IsAssignableFrom(typeof(UnitCard))){
            currentDeck.units.Remove((UnitCard)cc.card);
        } else {
            currentDeck.spells.Remove((SpellCard)cc.card);
        }
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
