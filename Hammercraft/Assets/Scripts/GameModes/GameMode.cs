public enum GameState {
    NotFinished = 0,
    Draw = 1,
    WinPlayerOne = 2,
    WinPlayerTwo = 4,
    Finished = WinPlayerOne | WinPlayerTwo | Draw
}

//Classe gérant les modes de jeu, permettant de définir quel joueur gagne et quel joueur perd la partie
public static class GameStateExtension {
    public static bool Finished(this GameState state) {
        return (int)(state & GameState.Finished) != 0;
    }

    public static PlayerRole Winner(this GameState state) {
        if ((state & GameState.WinPlayerOne) != 0) return PlayerRole.PlayerOne;
        else if ((state & GameState.WinPlayerTwo) != 0) return PlayerRole.PlayerTwo;
        else return PlayerRole.Spectator;
    }
}

[System.Serializable]
public enum GameModes {
 //   KillToWin,
    Point,
    DestroyTheBase
}

public static class GameModesExtension {
    public static GameMode GameMode(this GameModes mode) {
        switch(mode) {
       //     case GameModes.KillToWin: return new KillToWin();
            case GameModes.Point: return new Point();
            case GameModes.DestroyTheBase: return new DestroyTheBase();
            default: return null;
        }
    }
}

[System.Serializable]
public abstract class GameMode {
    public abstract GameState CurrentGameState(GameManager gameManager);
    public abstract int GetScore(PlayerRole player, GameManager gameManager);
}