using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class DeckTest : MonoBehaviour
{
    [Test]
    public void Shuffle_Test(){
        UnitCard u = new UnitCard();
        SpellCard s = new DamageSpell();
        UnitCard[] us = new UnitCard[1];
        SpellCard[] ss = new DamageSpell[1];
        us[0] = u;
        ss[0] = s;
        Deck deck = new Deck(us, ss);
        deck.Shuffle();
        Assert.True(us[0] == u);
    }

    [Test]
    public void DrawUnit_Test(){
        UnitCard u = Resources.Load<UnitCard>("Cards/Unit/Noob");
        SpellCard s = Resources.Load<SpellCard>("Cards/Spell/Fireball");
        UnitCard[] us = new UnitCard[1];
        SpellCard[] ss = new SpellCard[1];
        us[0] = u;
        ss[0] = s;
        Deck deck = new Deck(us, ss);
        Assert.That(deck.DrawUnit().resourcePath, Is.EqualTo(u.resourcePath));
    }

    [Test]
    public void DrawSpell_Test(){
        UnitCard u = Resources.Load<UnitCard>("Cards/Unit/Noob");
        SpellCard s = Resources.Load<SpellCard>("Cards/Spell/Fireball");
        UnitCard[] us = new UnitCard[1];
        SpellCard[] ss = new SpellCard[1];
        us[0] = u;
        ss[0] = s;
        Deck deck = new Deck(us, ss);
        Assert.That(deck.DrawSpell().resourcePath, Is.EqualTo(s.resourcePath));
    }

    [Test]
    public void AddCardUnit_Test(){
        UnitCard u1 = Resources.Load<UnitCard>("Cards/Unit/Noob");
        SpellCard s1 = Resources.Load<SpellCard>("Cards/Spell/Fireball");
        UnitCard[] u = new UnitCard[] {u1};
        SpellCard[] s = new SpellCard[] {s1};
        Deck deck = new Deck(u, s);
        UnitCard u2 = Resources.Load<UnitCard>("Cards/Unit/Pro");
        deck.AddCard(u2);
        Assert.True(deck.units.Count == 2);
    }

    [Test]
    public void AddCardSpell_Test(){
        UnitCard u1 = Resources.Load<UnitCard>("Cards/Unit/Noob");
        SpellCard s1 = Resources.Load<SpellCard>("Cards/Spell/Fireball");
        UnitCard[] u = new UnitCard[] {u1};
        SpellCard[] s = new SpellCard[] {s1};
        Deck deck = new Deck(u, s);
        SpellCard s2 = Resources.Load<SpellCard>("Cards/Spell/Heal");
        deck.AddCard(s2);
        Assert.True(deck.spells.Count == 2);
    }

    [Test]
    public void RemoveCardUnit_Test(){
        UnitCard u1 = Resources.Load<UnitCard>("Cards/Unit/Noob");
        SpellCard s1 = Resources.Load<SpellCard>("Cards/Spell/Fireball");
        UnitCard[] u = new UnitCard[] {u1};
        SpellCard[] s = new SpellCard[] {s1};
        Deck deck = new Deck(u, s);
        deck.RemoveCard(u1);
        Assert.True(deck.units.Count == 0);
    }

    [Test]
    public void RemoveCardSpell_Test(){
        UnitCard u1 = Resources.Load<UnitCard>("Cards/Unit/Noob");
        SpellCard s1 = Resources.Load<SpellCard>("Cards/Spell/Fireball");
        UnitCard[] u = new UnitCard[] {u1};
        SpellCard[] s = new SpellCard[] {s1};
        Deck deck = new Deck(u, s);
        deck.RemoveCard(s1);
        Assert.True(deck.spells.Count == 0);
    }
}
