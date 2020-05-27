public class KillToWin : GameMode
{
    public override GameState CurrentGameState(GameManager gameManager)
    {
        bool winP1 = false;
        bool winP2 = false;
        foreach(var unit in gameManager.Board.Units) {
            if (unit.Dead) {
                winP1 |= unit.Player == 0;
                winP2 |= unit.Player == 1;
            }
        }
        if (winP1 && !winP2) return GameState.WinPlayerOne;
        else if (!winP1 && winP2) return GameState.WinPlayerTwo;
        else if (winP1 && winP2) return GameState.Draw;
        else return GameState.NotFinished;
    }
}