using System.Collections.Generic;
using UnityEngine;

public class BoardPlayer : BoardBehaviour {
    private Player player;
    
    public override void OnResetBoard(BoardManager boardManager) {
        base.OnResetBoard(boardManager);
        Start();
    }
    
    private void Start() {
        player = manager.LocalPlayer;
        player.DrawUnit();
        player.DrawUnit();
        player.DrawSpell();
        player.DrawSpell();
    }
}