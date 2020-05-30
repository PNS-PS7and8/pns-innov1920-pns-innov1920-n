using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player {
    public Deck originalDeck  { get; private set; }
    public Deck deck { get; private set; }

    [SerializeField] private List<CardBase> hand;
    [SerializeField] private PlayerRole role;
    [SerializeField] private int gold;
    [SerializeField] private int currentGold;
    public List<CardBase> Hand => hand;
    public PlayerRole Role => role;
    public int Gold => gold;
    public int CurrentGold => currentGold;

    public Player(Deck deck, PlayerRole role) {
        this.role = role;
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

    public void DrawUnit() {
        hand.Add(deck.DrawUnit());
    }

    public void DrawSpell() {
        hand.Add(deck.DrawSpell());
    }

    public void UseCard(CardBase card) {
        hand.Remove(card);
    }
}