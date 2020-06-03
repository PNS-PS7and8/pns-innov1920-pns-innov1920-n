using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Point : GameMode
{
    public delegate void OnUnitDie(int cost, PlayerRole player);
    int pointToWin = 2;
    int PointPlayer1 = 0;
    int PointPlayer2 = 0;
    public static event OnUnitDie UnitIsDead;
    

    public Point()
    {
        UnitIsDead += UnitDead;

    }

    public override GameState CurrentGameState(GameManager gameManager)
    {                   
        if (PointPlayer1 > pointToWin) { return GameState.WinPlayerOne; }
        else if (PointPlayer2 > pointToWin) { return GameState.WinPlayerTwo; }
        else return GameState.NotFinished;
        
    }

    void UnitDead(int cost, PlayerRole player)
    {
        PointPlayer2 += 1;
        Debug.Log(PointPlayer2 + "player2");
    }

    


}
