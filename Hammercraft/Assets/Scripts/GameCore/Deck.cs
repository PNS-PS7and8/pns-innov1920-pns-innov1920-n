using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using UnityEngine.Serialization;

//Classe représentant le deck du joueur
[System.Serializable]
public class Deck {
   // [SerializeField] private string owner = null;
    [SerializeField] private string name = null;
    public string Name => name;
    
    [SerializeField, FormerlySerializedAs("units")] private List<string> serializedUnits = null;
    [SerializeField, FormerlySerializedAs("spells")] private List<string> serializedSpells = null;
    public List<string> SerializedUnits => serializedUnits;
    public List<string> SerializedSpells => serializedSpells;
    
    public List<UnitCard> units => serializedUnits.Select(c => Resources.Load<UnitCard>(c)).ToList();
    public List<SpellCard> spells => serializedSpells.Select(c => Resources.Load<SpellCard>(c)).ToList();
    //private int MAX = 10;

    public Deck(string name) {
        this.name = name;
        serializedUnits = new List<string>();
        serializedSpells = new List<string>();
    }

    public Deck(string name, string[] unitResources, string[] spellResources) {
        this.name = name;
        this.serializedUnits = new List<string>(unitResources);
        this.serializedSpells = new List<string>(spellResources);
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

    //Pioche une carte du deck Unité et l'ajoute à la main du joueur
    public UnitCard DrawUnit() {
        var card = serializedUnits[0];
        serializedUnits.RemoveAt(0);
        return Resources.Load<UnitCard>(card);
    }

    //Pioche une carte du deck Sorts et l'ajoute à la main du joueur
    public SpellCard DrawSpell() {
        var card = serializedSpells[0];
        serializedSpells.RemoveAt(0);
        return Resources.Load<SpellCard>(card);
    }

    //Ajoute une carte au deck
    public void AddCard(CardBase card) {
        if (card.GetType().IsAssignableFrom(typeof(UnitCard)) || card.GetType().IsSubclassOf(typeof(UnitCard)) ) {
            serializedUnits.Add(card.ResourcePath);
        } else {
            serializedSpells.Add(card.ResourcePath);
        }
    }

    //Supprime une carte du deck
    public void RemoveCard(CardBase card) {
        if (card.GetType().IsAssignableFrom(typeof(UnitCard)) || card.GetType().IsSubclassOf(typeof(UnitCard)) ) {
            serializedUnits.Remove(card.ResourcePath);
        } else {
            serializedSpells.Remove(card.ResourcePath);
        }
    }

    public static byte[] Serialize(object deck) {
        string json = JsonUtility.ToJson(deck);
        return Encoding.UTF8.GetBytes(json);
    }

    public static object Deserialize(byte[] data) {
        string json = Encoding.UTF8.GetString(data);
        return JsonUtility.FromJson<Deck>(json);
    }
}