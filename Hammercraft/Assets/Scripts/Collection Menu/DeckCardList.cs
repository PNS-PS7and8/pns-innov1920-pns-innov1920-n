using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DeckCardList : MonoBehaviour
{
    [SerializeField] private GameCard cardPrefab;
    public GameCard[] Cards { get; private set; }

    void Start()
    {
        Cards = new GameCard[10];
        for (int i = 0; i < Cards.Length; i++)
        {
            GameObject cardObject = Instantiate(cardPrefab.gameObject, transform);
            Cards[i] = cardObject.GetComponent<GameCard>();
            cardObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadDeck(Deck deck){
        List<UnitCard> lu = deck.units;
        //List<CardBase> lu = deck.units.Cast<CardBase>().ToList();
        //Debug.Log(lu[0].Name);
        //List<SpellCard> ls = deck.spells;
        float spacing = 100f;
        for (int i = 0; i<Cards.Length; i++){
            Cards[i].card = lu[1];
            Cards[i].gameObject.SetActive(true);
            Vector3 pos = Cards[i].transform.localPosition;
            Vector3 scale = new Vector3(2000,2000,2000);
            Cards[i].transform.localScale = scale;
            pos.x = (-500)+(i * spacing);
            pos.z = -10;
            Cards[i].transform.localPosition = pos;
        }
    }
}
