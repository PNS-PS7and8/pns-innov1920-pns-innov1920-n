public class KillToWin : GameMode
{
    public override GameState CurrentGameState(GameManager gameManager)
    {
        bool winP1 = false;
        bool winP2 = false;
        foreach(var unit in gameManager.Board.Units) {
            if (unit.Dead) {
                winP1 |= unit.Player == PlayerRole.PlayerOne;
                winP2 |= unit.Player == PlayerRole.PlayerTwo;
            }
        }
        if (winP1 && !winP2) return GameState.WinPlayerOne;
        else if (!winP1 && winP2) return GameState.WinPlayerTwo;
        else if (winP1 && winP2) return GameState.Draw;
        else return GameState.NotFinished;
    }
}