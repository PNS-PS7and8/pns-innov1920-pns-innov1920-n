using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class PlayerTest : MonoBehaviour
{
    [Test]
    public void PlayerDrawUnitCompare_Test(){
        UnitCard u = ScriptableObject.CreateInstance<UnitCard>();
        SpellCard s = ScriptableObject.CreateInstance<DamageSpell>();
        UnitCard[] us = new UnitCard[1];
        SpellCard[] ss = new SpellCard[1];
        us[0] = u;
        ss[0] = s;
        Deck deck = new Deck(us, ss);
        Player p = new Player(deck, 0);
        Assert.That(p.OriginalDeck.DrawUnit(), Is.EqualTo(p.Deck.DrawUnit()));
    }

        [Test]
    public void PlayerDrawSpellCompare_Test(){
        UnitCard u = ScriptableObject.CreateInstance<UnitCard>();
        SpellCard s = ScriptableObject.CreateInstance<DamageSpell>();
        UnitCard[] us = new UnitCard[1];
        SpellCard[] ss = new SpellCard[1];
        us[0] = u;
        ss[0] = s;
        Deck deck = new Deck(us, ss);
        Player p = new Player(deck, 0);
        Assert.That(p.OriginalDeck.DrawSpell(), Is.EqualTo(p.Deck.DrawSpell()));
    }

    [Test]
    public void PlayerUseCardSpell_Test(){
        UnitCard u = ScriptableObject.CreateInstance<UnitCard>();
        SpellCard s = ScriptableObject.CreateInstance<DamageSpell>();
        UnitCard[] us = new UnitCard[1];
        SpellCard[] ss = new SpellCard[1];
        us[0] = u;
        ss[0] = s;
        Deck deck = new Deck(us, ss);
        Player p = new Player(deck, 0);
        SpellCard a = p.DrawSpell();
        p.UseCard(a);
        Assert.True(p.Hand.Count == 0);
    }

    [Test]
    public void PlayerUseCardUnit_Test(){
        UnitCard u = ScriptableObject.CreateInstance<UnitCard>();
        SpellCard s = ScriptableObject.CreateInstance<DamageSpell>();
        UnitCard[] us = new UnitCard[1];
        SpellCard[] ss = new SpellCard[1];
        us[0] = u;
        ss[0] = s;
        Deck deck = new Deck(us, ss);
        Player p = new Player(deck, 0);
        UnitCard a = p.DrawUnit();
        p.UseCard(a);
        Assert.True(p.Hand.Count == 0);
    }

        [Test]
    public void PlayerDrawSpell_Test(){
        UnitCard u = ScriptableObject.CreateInstance<UnitCard>();
        SpellCard s = ScriptableObject.CreateInstance<DamageSpell>();
        UnitCard[] us = new UnitCard[1];
        SpellCard[] ss = new SpellCard[1];
        us[0] = u;
        ss[0] = s;
        Deck deck = new Deck(us, ss);
        Player p = new Player(deck, 0);
        SpellCard a = p.DrawSpell();
        Assert.True(p.Hand.Count == 1);
    }

    [Test]
    public void PlayerDrawUnit_Test(){
        UnitCard u = ScriptableObject.CreateInstance<UnitCard>();
        SpellCard s = ScriptableObject.CreateInstance<DamageSpell>();
        UnitCard[] us = new UnitCard[1];
        SpellCard[] ss = new SpellCard[1];
        us[0] = u;
        ss[0] = s;
        Deck deck = new Deck(us, ss);
        Player p = new Player(deck, 0);
        UnitCard a = p.DrawUnit();
        Assert.True(p.Hand.Count == 1);
    }
}
