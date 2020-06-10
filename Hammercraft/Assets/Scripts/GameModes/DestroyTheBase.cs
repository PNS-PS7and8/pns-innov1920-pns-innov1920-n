using System.Linq;
using UnityEngine;

public class DestroyTheBase : GameMode {
    public override GameState CurrentGameState(GameManager gameManager)
    {
        Unit b1 = GetBase(PlayerRole.PlayerOne, gameManager);
        Unit b2 = GetBase(PlayerRole.PlayerTwo, gameManager);
        if (b1 == null) {
            AddBase(PlayerRole.PlayerOne, gameManager);
            b1 = GetBase(PlayerRole.PlayerOne, gameManager);
        }
        if (b2 == null) {
            AddBase(PlayerRole.PlayerTwo, gameManager);
            b2 = GetBase(PlayerRole.PlayerTwo, gameManager);
        }
        if (b1.Dead && b2.Dead) return GameState.Draw;
        else if (b1.Dead) return GameState.WinPlayerTwo;
        else if (b2.Dead) return GameState.WinPlayerOne;
        else return GameState.NotFinished;
    }

    public override int GetScore(PlayerRole player, GameManager gameManager) {
        if (player == PlayerRole.Spectator) {
            return Resources.Load<UnitCard>("Cards/Specials/PlayerBaseP1").Health;
        }
        var unit = GetBase(player, gameManager);
        if (unit != null) return unit.Health;
        else return 1;
    }

    private Unit GetBase(PlayerRole player, GameManager gameManager) {
        var unit = gameManager.Board.GetUnit(gameManager.Board.GetCell(GetBasePosition(player, gameManager)), true);
        return unit;
    }

    private void AddBase(PlayerRole player, GameManager gameManager) {
        UnitCard card;
        if (player == PlayerRole.PlayerOne) card = Resources.Load<UnitCard>("Cards/Specials/PlayerBaseP1");
        else card = Resources.Load<UnitCard>("Cards/Specials/PlayerBaseP2");
        Cell cell = gameManager.Board.GetCell(GetBasePosition(player, gameManager));
        gameManager.Board.AddUnit(card, cell, player);
    }

    private Vector2Int GetBasePosition(PlayerRole player ,GameManager gameManager) {
        if (player == PlayerRole.PlayerOne) {
            return new Vector2Int(23, 20);
        } else {
            return new Vector2Int(29, 28);
        }
    }
}