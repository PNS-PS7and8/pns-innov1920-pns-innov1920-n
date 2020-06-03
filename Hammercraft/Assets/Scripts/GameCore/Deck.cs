using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Deck {
    public string name {get; set;}
    
    [SerializeField] private List<string> serializedUnits;
    [SerializeField] private List<string> serializedSpells;
    
    public List<UnitCard> units => serializedUnits.Select(c => Resources.Load<UnitCard>(c)).ToList();
    public List<SpellCard> spells => serializedSpells.Select(c => Resources.Load<SpellCard>(c)).ToList();
    private int MAX = 5;

    public Deck(string name) {
        this.name = name;
        serializedUnits = new List<string>();
        serializedSpells = new List<string>();
    }

    public Deck(string name, UnitCard[] units, SpellCard[] spells) {
        this.name = name;
        this.serializedUnits = units.Select(u => u.ResourcePath).ToList();
        this.serializedSpells = spells.Select(s => s.ResourcePath).ToList();
    } 

    public Deck(UnitCard[] units, SpellCard[] spells) {
        this.name = "";
        this.serializedUnits = units.Select(u => u.ResourcePath).ToList();
        this.serializedSpells = spells.Select(s => s.ResourcePath).ToList();
    }

    public Deck(Deck original) {
        this.name = original.name;
        this.serializedUnits = new List<string>(original.serializedUnits);
        this.serializedSpells = new List<string>(original.serializedSpells);
    }

    public void Shuffle() {
        serializedUnits = serializedUnits.OrderBy(unit => Random.value).ToList();
        serializedSpells = serializedSpells.OrderBy(spell => Random.value).ToList();
    }

    public UnitCard DrawUnit() {
        var card = serializedUnits[0];
        serializedUnits.RemoveAt(0);
        return Resources.Load<UnitCard>(card);
    }

    public SpellCard DrawSpell() {
        var card = serializedSpells[0];
        serializedSpells.RemoveAt(0);
        return Resources.Load<SpellCard>(card);
    }

    public void AddCard(CardBase card) {
        if (card.GetType().IsAssignableFrom(typeof(UnitCard))) {
            serializedUnits.Add(card.ResourcePath);
        } else {
            serializedSpells.Add(card.ResourcePath);
        }
    }

    public void RemoveCard(CardBase card) {
        if (card.GetType().IsAssignableFrom(typeof(UnitCard))) {
            serializedUnits.Remove(card.ResourcePath);
        } else {
            serializedSpells.Remove(card.ResourcePath);
        }
    }
}