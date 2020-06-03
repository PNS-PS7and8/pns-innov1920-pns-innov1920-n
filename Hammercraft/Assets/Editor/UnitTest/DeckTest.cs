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
}
