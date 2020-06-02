using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DeckCardList : MonoBehaviour
{
    [SerializeField] private CollectionCard cardPrefab;
    public CollectionCard[] Cards { get; private set; }

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadDeck(Deck deck){
        List<UnitCard> lu = deck.units;
        List<SpellCard> ls = deck.spells;
        //List<CardBase> lu = deck.units.Cast<CardBase>().ToList();
        float spacing = 190f;
        for (int i = 0; i<lu.Capacity; i++){
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
        for (int i = 0; i<ls.Capacity; i++){
            Cards[lu.Capacity+i].card = ls[i];
            Cards[lu.Capacity+i].gameObject.SetActive(true);
            Vector3 pos = Cards[i].transform.localPosition;
            Vector3 scale = new Vector3(1800,1800,1800);
            Cards[lu.Capacity+i].transform.localScale = scale;
            pos.x = (-370)+(i * spacing);
            pos.z = -10;
            pos.y = -200;
            Cards[lu.Capacity+i].transform.localPosition = pos;
        }
    }
}
