using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player {
    public Deck originalDeck  { get; private set; }
    public Deck deck { get; private set; }

    [SerializeField] private List<CardBase> hand;
    [SerializeField] private int id;
    [SerializeField] private int gold;
    [SerializeField] private int currentGold;
    public List<CardBase> Hand => hand;
    public int Id => id;
    public int Gold => gold;
    public int CurrentGold => currentGold;

    public Player(Deck deck, int id) {
        this.id = id;
        originalDeck = deck;
        this.gold = 1;
        this.currentGold = gold;
        this.deck = new Deck(deck);
        this.deck.Shuffle();
        hand = new List<CardBase>();
    }

    public void SetGold(int newgold){
        this.gold = newgold;
    }

    public void SetCurrentGold(int newgold){
        currentGold = newgold;
    }
}