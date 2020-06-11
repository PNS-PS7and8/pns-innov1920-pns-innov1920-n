using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPlayer : BoardBehaviour {
    public Player player { get; private set; }
    [SerializeField] private DeckDraw deckdraw = null;

    public override void OnResetBoard(BoardManager boardManager) {
        base.OnResetBoard(boardManager);
        Start();
    }
    
    private void Start() {
        player = manager.CurrentPlayer;
    }

    public void Mulligan()
    {
        deckdraw.Mulligan(manager.CurrentPlayer.DrawMulligan(manager.PlayerTurn));
    }

    public void DrawUnit() {
        deckdraw.Draw(manager.CurrentPlayer.DrawUnit());
    }

    public void DrawSpell() {
        deckdraw.Draw(manager.CurrentPlayer.DrawSpell());
    }
}