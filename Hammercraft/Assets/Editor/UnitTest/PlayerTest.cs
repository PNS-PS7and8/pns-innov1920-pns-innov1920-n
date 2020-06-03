using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class PlayerTest : MonoBehaviour
{
    [Test]
    public void Player1_Test(){
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
    public void Player2_Test(){
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
}
