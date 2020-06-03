using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : GameMode
{
    int pointToWin = 4;
    int[] PointPlayer = new int[2] { 0, 0 };
   
    public override GameState CurrentGameState(GameManager gameManager)
    {
        foreach(Unit unit in gameManager.Board.Units)
        {
            if (unit.Dead)
            {
                PointPlayer[(unit.Player == PlayerRole.PlayerOne) ? 0 : 1]+=unit.Cost; 
            }
        }

        if (PointPlayer[0] > pointToWin) { return GameState.WinPlayerOne; }
        else if (PointPlayer[1] > pointToWin) { return GameState.WinPlayerTwo; }
        else return GameState.NotFinished;
        
    }

   
}
