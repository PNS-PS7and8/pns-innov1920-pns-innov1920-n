using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player {
    public Deck originalDeck  { get; private set; }
    public Deck deck { get; private set; }

    [SerializeField] private List<CardBase> hand;
    [SerializeField] private int id;

    public List<CardBase> Hand => hand;

    public Player(Deck deck, int id) {
        this.id = id;
        originalDeck = deck;
        this.deck = new Deck(deck);
        this.deck.Shuffle();
        hand = new List<CardBase>();
    }
}