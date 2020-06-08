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
    [SerializeField] private TMP_Text saveMessage;
    [SerializeField] private TMP_Text DeckName;

    public CollectionCard[] CardUnit { get; private set; }
    public CollectionCard[] CardSpell { get; private set; }
    private Deck currentDeck = null;
    private int MAX = 10;
    private float spacing = 210f;
    private int unitCount = 0;
    private int spellCount = 0;

    private Vector3 size = new Vector3(1700,1700,1700);


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
        if ( (cb.GetType().IsAssignableFrom(typeof(UnitCard)) || cb.GetType().IsSubclassOf(typeof(UnitCard)) )){
            for (int i = 0; i < currentDeck.units.Count(); i++){
                if (currentDeck.units[i].Name == cb.Name){cpt++;}
            }
        } else if (!cb.GetType().IsAssignableFrom(typeof(UnitCard))){
            for (int i = 0; i < currentDeck.spells.Count(); i++){
                if (currentDeck.spells[i].Name == cb.Name){cpt++;}
            }
        }
        if (cpt < 2){
            return true;
        }
        saveMessage.gameObject.SetActive(true);
        saveMessage.color = new Color(250/255f, 34/255f, 48/255f, 1);
        saveMessage.text = "Two copies of the same card maximum!";
        return false;
    }

    bool IsValidDeck(){
        if(currentDeck.spells.Count() == MAX && currentDeck.units.Count() == MAX){
            return true;
        } else {
            saveMessage.gameObject.SetActive(true);
            saveMessage.color = new Color(250/255f, 34/255f, 48/255f, 1);
            saveMessage.text = "Your deck must have 10 units & spells to be saved!";   
            return false;
        }
    }

    void TextUpdate(){
        saveMessage.gameObject.SetActive(false);
    }

    public void Save(){
        TextUpdate();
        if (IsValidDeck()){
            DeckListingMenu dlm = Object.FindObjectOfType<DeckListingMenu>();
            dlm.setDeck(currentDeck);
            saveMessage.gameObject.SetActive(true);
            saveMessage.color = new Color(27/255f, 140/255f, 30/255f, 1);
            saveMessage.text = "Deck saved!";
            AccessDatabase.SaveDeck(PlayerPrefs.GetString("username"), currentDeck);
        }
    }

    public void AddCard(CardBase cb){
        TextUpdate();
        if (currentDeck != null){

            if ( (cb.GetType().IsAssignableFrom(typeof(UnitCard)) || cb.GetType().IsSubclassOf(typeof(UnitCard)) ) && currentDeck.units.Count() < MAX && IsNotTriplicate(cb)){
                for (int i = 0; i < CardUnit.Count(); i++){
                    if(CardUnit[i].gameObject.activeSelf == false){
                        CardUnit[i].card = cb;
                        CardUnit[i].gameObject.SetActive(true);
                        CardUnit[i].transform.localScale = size;
                        currentDeck.AddCard(cb);
                        unitCount++;
                        break;
                    }
                }
            } else if (!cb.GetType().IsAssignableFrom(typeof(UnitCard)) && currentDeck.spells.Count() < MAX && IsNotTriplicate(cb)){
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
        cc.transform.localScale = size;
        cc.transform.localPosition += new Vector3(0,0,30) ;
        if (cc.card.GetType().IsAssignableFrom(typeof(UnitCard)) || cc.card.GetType().IsSubclassOf(typeof(UnitCard))){
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
            Vector3 scale = size;
            CardUnit[i].transform.localScale = scale;
            if (i > 4){
                pos.x = (-375)+((i-5) * spacing);
                pos.z = -20;
                pos.y = 115;
            } else {
                pos.x = (-490)+(i * spacing);
                pos.z = -10;
                pos.y = 265;
            }
            CardUnit[i].transform.localPosition = pos;
        }
        
        for (int i = 0; i<MAX; i++){
            if (ls.Count > i){
                CardSpell[i].card = ls[i];
                CardSpell[i].gameObject.SetActive(true);
            }
            Vector3 pos = CardSpell[i].transform.localPosition;
            Vector3 scale = size;
            CardSpell[i].transform.localScale = scale;
            if (i > 4){
                pos.x = (-375)+((i-5) * spacing);
                pos.z = -20;
                pos.y = -295;
            } else {
                pos.x = (-490)+(i * spacing);
                pos.z = -10;
                pos.y = -145;
            }
            CardSpell[i].transform.localPosition = pos;
        }
    }
}
