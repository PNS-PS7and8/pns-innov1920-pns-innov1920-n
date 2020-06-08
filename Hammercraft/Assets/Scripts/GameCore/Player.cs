using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Player {
    [SerializeField] private Deck originalDeck;
    [SerializeField] private Deck deck;
    
    public Deck OriginalDeck => originalDeck;
    public Deck Deck => deck;

    [SerializeField] private List<string> serializedHand;
    [SerializeField] private PlayerRole role;
    [SerializeField] private int gold;
    [SerializeField] private int currentGold;
    public List<CardBase> Hand => serializedHand.Select(c => Resources.Load<CardBase>(c)).ToList();
    public PlayerRole Role => role;


    public int Gold => gold;
    public int CurrentGold => currentGold;

    public Player(Deck deck, PlayerRole role) {
        this.role = role;
        originalDeck = deck;
        this.gold = 1;
        this.currentGold = gold;
        this.deck = new Deck(deck);
        this.Deck.Shuffle();
        serializedHand = new List<string>();
    }

    public void SetGold(int newgold){
        this.gold = newgold;
    }

    public void SetCurrentGold(int newgold){
        currentGold = newgold;
    }

    public UnitCard DrawUnit() {
        UnitCard t = Deck.DrawUnit();
        if (serializedHand.Count <= 10)
            serializedHand.Add(t.ResourcePath);
        return t;
    }

    public void addCard(CardBase card){
        if (serializedHand.Count <= 10)
            serializedHand.Add(card.ResourcePath);
    }
    
    public Tuple<List<UnitCard>,List<SpellCard>> DrawMulligan(PlayerRole playerRole)
    {
        List<UnitCard> U = new List<UnitCard>();
        List<SpellCard> S = new List<SpellCard>();
        U.Add(Deck.DrawUnit());
        U.Add(Deck.DrawUnit());
        S.Add(Deck.DrawSpell());
        if (playerRole.Equals(PlayerRole.PlayerTwo))
        {
            S.Add(Deck.DrawSpell());
        }

        return Tuple.Create(U,S);
    }
    

    public SpellCard DrawSpell() {
        SpellCard t = Deck.DrawSpell();
        if (serializedHand.Count <= 10)
            serializedHand.Add(t.ResourcePath);
        return t;
    }

    public void UseCard(CardBase card) {
        serializedHand.Remove(card.ResourcePath);
    }
}