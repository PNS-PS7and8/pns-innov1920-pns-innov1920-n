using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class GameCardTest : MonoBehaviour
{
    [Test]
    public void GameCardTest_Test(){
        Board board = new Board(10,10);
        Cell cell = board.GetCell(5,5);
        
        //GameCard gc = gameObject.GetComponent<GameCard>();
        
        /*BoardPlayer bp = new BoardPlayer();
        List<GameCard> lgc = new List<GameCard>();
        UnitCard u = ScriptableObject.CreateInstance<UnitCard>();
        SpellCard s = ScriptableObject.CreateInstance<SpellCard>();
        UnitCard[] us = new UnitCard[1];
        SpellCard[] ss = new SpellCard[1];
        us[0] = u;
        ss[0] = s;
        Deck deck = new Deck(us, ss);
        Player p = new Player(deck);
        lgc.Add(gc);
        gc.card = u;
        gc.Use(cell);*/
    }
}
