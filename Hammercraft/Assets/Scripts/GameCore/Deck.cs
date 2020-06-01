using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Deck {
    public string name {get; set;}
    private List<UnitCard> units;
    private List<SpellCard> spells;

    public Deck(string name) {
        this.name = name;
        this.units = new List<UnitCard>();
        this.spells = new List<SpellCard>();
    }

    public Deck(string name, UnitCard[] units, SpellCard[] spells) {
        this.name = name;
        this.units = new List<UnitCard>(units);
        this.spells = new List<SpellCard>(spells);
    } 

    public Deck(UnitCard[] units, SpellCard[] spells) {
        this.name = "";
        this.units = new List<UnitCard>(units);
        this.spells = new List<SpellCard>(spells);
    }

    public Deck(Deck original) {
        this.name = original.name;
        this.units = new List<UnitCard>(original.units);
        this.spells = new List<SpellCard>(original.spells);
    }

    public void Shuffle() {
        units = units.OrderBy(unit => Random.value).ToList();
        spells = spells.OrderBy(spell => Random.value).ToList();
    }

    public UnitCard DrawUnit() {
        var card = units[0];
        units.RemoveAt(0);
        return card;
    }

    public SpellCard DrawSpell() {
        var card = spells[0];
        spells.RemoveAt(0);
        return card;
    }
}