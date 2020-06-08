using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPlayer : BoardBehaviour {
    private Player player;
    [SerializeField] private DeckDraw deckdraw;

    public override void OnResetBoard(BoardManager boardManager) {
        base.OnResetBoard(boardManager);
        Start();
    }
    
    private void Start() {
        player = manager.LocalPlayer;
       // deckdraw.Mulligan(manager.LocalPlayer.DrawMulligan(manager.PlayerTurn));
    }

    

    public void DrawUnit() {
        deckdraw.Draw(manager.CurrentPlayer.DrawUnit());
    }

    public void DrawSpell() {
        deckdraw.Draw(manager.CurrentPlayer.DrawSpell());
    }
}