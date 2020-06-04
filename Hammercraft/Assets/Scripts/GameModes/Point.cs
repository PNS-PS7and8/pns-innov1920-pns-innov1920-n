using System.Linq;
using UnityEngine;

public class Point : GameMode
{

    [SerializeField] private int p1 = 0;
    [SerializeField] private int p2 = 0;
    [SerializeField] private int ScoreToWin = 2;
    public override GameState CurrentGameState(GameManager gameManager)
    {      
        countDeath(gameManager);
        if (p1 >= ScoreToWin) { return GameState.WinPlayerTwo; }
        else if (p2 >= ScoreToWin) { return GameState.WinPlayerOne; }
        else return GameState.NotFinished;
    }

    public override int GetScore(int player, GameManager gameManager){
        countDeath(gameManager);
        if (player==1){
            return p2;
        }
        if (player == 2){
            return p1;
        }
        return ScoreToWin;
    }

    private void countDeath(GameManager gameManager){
        p1 = gameManager.Board.Units.Where(u=>u.Dead && u.Player == PlayerRole.PlayerOne).Select(u => u.Cost).Sum();
        p2 = gameManager.Board.Units.Where(u=>u.Dead && u.Player == PlayerRole.PlayerTwo).Select(u => u.Cost).Sum();
    }


}
