using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class DeckCardList : MonoBehaviour
{
    [SerializeField] private CollectionCard cardPrefab;
    [SerializeField] private TMP_Text UnitCountText;
    [SerializeField] private TMP_Text SpellCountText;
    [SerializeField] private TMP_Text SaveText;
    [SerializeField] private TMP_Text DuplicateText;
    [SerializeField] private TMP_Text DeckName;
    [SerializeField] private TMP_Text InvalidDeck;
    public CollectionCard[] CardUnit { get; private set; }
    public CollectionCard[] CardSpell { get; private set; }
    private Deck currentDeck = null;
    private int MAX = 5;
    private float spacing = 190f;
    private int unitCount = 0;
    private int spellCount = 0;


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

    bool IsNotTriplicate(CardBase cb){
        int cpt = 0;
        if (cb.GetType().IsAssignableFrom(typeof(UnitCard))){
            for (int i = 0; i < currentDeck.units.Count(); i++){
                if (currentDeck.units[i].Name == cb.Name){cpt++;}
            }
        }

        if (!cb.GetType().IsAssignableFrom(typeof(UnitCard))){
            for (int i = 0; i < currentDeck.spells.Count(); i++){
                if (currentDeck.spells[i].Name == cb.Name){cpt++;}
            }
        }

        if (cpt < 2){
            return true;
        }
        DuplicateText.gameObject.SetActive(true);
        return false;
    }

    bool IsValidDeck(){
        if(currentDeck.spells.Count() == 5 && currentDeck.units.Count() == 5){
            return true;
        } else {
            InvalidDeck.gameObject.SetActive(true);    
            return false;
        }
    }

    void TextUpdate(){
        SaveText.gameObject.SetActive(false);
        DuplicateText.gameObject.SetActive(false);
        InvalidDeck.gameObject.SetActive(false);
    }

    public void Save(){
        TextUpdate();
        if (IsValidDeck()){
            DeckListingMenu dlm = Object.FindObjectOfType<DeckListingMenu>();
            dlm.setDeck(currentDeck);
            SaveText.gameObject.SetActive(true);
            AccessDatabase.SaveDeck(PlayerPrefs.GetString("username"), currentDeck);
        }
    }

    public void AddCard(CardBase cb){
        TextUpdate();
        if (currentDeck != null){

            if (cb.GetType().IsAssignableFrom(typeof(UnitCard)) && currentDeck.units.Count() < MAX && IsNotTriplicate(cb)){
                for (int i = 0; i < CardUnit.Count(); i++){
                    if(CardUnit[i].gameObject.activeSelf == false){
                        CardUnit[i].card = cb;
                        CardUnit[i].gameObject.SetActive(true);
                        currentDeck.AddCard(cb);
                        unitCount++;
                        break;
                    }
                }
            } 
            
            if (!cb.GetType().IsAssignableFrom(typeof(UnitCard)) && currentDeck.spells.Count() < MAX && IsNotTriplicate(cb)){
                for (int i = 0; i < CardSpell.Count(); i++){
                    if(CardSpell[i].gameObject.activeSelf == false){
                        CardSpell[i].card = cb;
                        CardSpell[i].gameObject.SetActive(true);
                        currentDeck.AddCard(cb);
                        spellCount++;
                        break;
                    }
                }
            }  
        }
        DisplayCount();
    }

    public void DeleteCard(CollectionCard cc){
        TextUpdate();
        currentDeck.RemoveCard(cc.card);
        cc.gameObject.SetActive(false);
        cc.transform.localScale = new Vector3 (1800,1800,1800);
        if (cc.card.GetType().IsAssignableFrom(typeof(UnitCard))){
            unitCount--;
        } else {spellCount--;}
        DisplayCount();
    }

    void DisplayCount(){
        UnitCountText.text = unitCount+"/"+MAX;
        SpellCountText.text = spellCount+"/"+MAX;
    }

    public void LoadDeck(Deck deck){
        Reset();
        Start();
        DeckName.text = deck.Name;
        currentDeck = deck;
        List<UnitCard> lu = deck.units;
        List<SpellCard> ls = deck.spells;
        unitCount = lu.Count();
        spellCount = ls.Count();
        DisplayCount();
        for (int i = 0; i<MAX; i++){
            if (lu.Count > i){
                CardUnit[i].card = lu[i];
                CardUnit[i].gameObject.SetActive(true);
            }
            Vector3 pos = CardUnit[i].transform.localPosition;
            Vector3 scale = new Vector3(2000,2000,2000);
            CardUnit[i].transform.localScale = scale;
            pos.x = (-380)+(i * spacing);
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
            Vector3 scale = new Vector3(2000,2000,2000);
            CardSpell[i].transform.localScale = scale;
            pos.x = (-370)+(i * spacing);
            pos.z = -10;
            pos.y = -200;
            CardSpell[i].transform.localPosition = pos;
        }
    }
}
