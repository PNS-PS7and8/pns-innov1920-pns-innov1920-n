using System.Collections.Generic;
using UnityEngine;

public class BoardPlayer : BoardBehaviour {
    private Player player;
    [SerializeField] private DeckUnit deckUnit;
    
    public override void OnResetBoard(BoardManager boardManager) {
        base.OnResetBoard(boardManager);
        Start();
    }
    
    private void Start() {
        player = manager.LocalPlayer;
        deckUnit.DrawUnit(player.DrawUnit());
        deckUnit.DrawUnit(player.DrawUnit());
       // deckUnit.DrawUnit(player.DrawUnit());
       // deckUnit.DrawUnit(player.DrawUnit());
        player.DrawSpell();
        player.DrawSpell();
    }
}