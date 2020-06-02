using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DeckCardList : MonoBehaviour
{
    [SerializeField] private CollectionCard cardPrefab;
    public CollectionCard[] Cards { get; private set; }

    private Deck currentDeck;
    private int MAX = 5;
    private float spacing = 190f;

    void Start()
    {
        Cards = new CollectionCard[10];
        for (int i = 0; i < Cards.Length; i++)
        {
            GameObject cardObject = Instantiate(cardPrefab.gameObject, transform);
            Cards[i] = cardObject.GetComponent<CollectionCard>();
            cardObject.SetActive(false);
        }
    }

    public void AddCard(CardBase cb){
        string type = cb.GetType().ToString();
        if (type == "UnitCard"){
            currentDeck.units.Add((UnitCard)cb);
        } else {
            currentDeck.spells.Add((SpellCard)cc);
        }
        LoadDeck(currentDeck);
    }

    public void DeleteCard(CollectionCard cc){
        string type = cc.card.GetType().ToString();
        Cards = Cards.Where(x=>x != cc).ToArray();
        if (type == "UnitCard"){
            currentDeck.units.Remove((UnitCard)cc.card);
        } else {
            currentDeck.spells.Remove((SpellCard)cc.card);
        }
        Destroy(cc.gameObject);
        LoadDeck(currentDeck);
    }

    public void LoadDeck(Deck deck){
        currentDeck = deck;
        List<UnitCard> lu = deck.units;
        List<SpellCard> ls = deck.spells;
        
        for (int i = 0; i<lu.Count && i<MAX; i++){
            Cards[i].card = lu[i];
            Cards[i].gameObject.SetActive(true);
            Vector3 pos = Cards[i].transform.localPosition;
            Vector3 scale = new Vector3(1800,1800,1800);
            Cards[i].transform.localScale = scale;
            pos.x = (-370)+(i * spacing);
            pos.z = -10;
            pos.y = 200;
            Cards[i].transform.localPosition = pos;
        }
        for (int i = 0; i<ls.Count && i<MAX; i++){
            Cards[lu.Count+i].card = ls[i];
            Cards[lu.Count+i].gameObject.SetActive(true);
            Vector3 pos = Cards[i].transform.localPosition;
            Vector3 scale = new Vector3(1800,1800,1800);
            Cards[lu.Count+i].transform.localScale = scale;
            pos.x = (-370)+(i * spacing);
            pos.z = -10;
            pos.y = -200;
            Cards[lu.Count+i].transform.localPosition = pos;
        }
    }
}
