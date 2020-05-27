public enum GameState {
    NotFinished = 0,
    Draw = 1,
    WinPlayerOne = 2,
    WinPlayerTwo = 4,
    Finished = WinPlayerOne | WinPlayerTwo | Draw
}

public static class GameStateExtension {
    public static bool Finished(this GameState state) {
        return (int)(state & GameState.Finished) != 0;
    }
}

[System.Serializable]
public enum GameModes {
    KillToWin
}

public static class GameModesExtension {
    public static GameMode GameMode(this GameModes mode) {
        switch(mode) {
            case GameModes.KillToWin: return new KillToWin();
            default: return null;
        }
    }
}

[System.Serializable]
public abstract class GameMode {
    public int x;
    public abstract GameState CurrentGameState(GameManager gameManager);
}