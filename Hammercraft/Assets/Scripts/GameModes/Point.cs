using System.Linq;
using UnityEngine;

public class Point : GameMode
{
    public override GameState CurrentGameState(GameManager gameManager)
    {                   
        int p1 = gameManager.Board.Units.Where(u=>u.Dead && u.Player == PlayerRole.PlayerOne).Select(u => u.Cost).Sum();
        int p2 = gameManager.Board.Units.Where(u=>u.Dead && u.Player == PlayerRole.PlayerTwo).Select(u => u.Cost).Sum();
        Debug.Log(p1 +" " + p2);
        if (p1 > 2) { return GameState.WinPlayerTwo; }
        else if (p2 > 2) { return GameState.WinPlayerOne; }
        else return GameState.NotFinished;
    }
}
