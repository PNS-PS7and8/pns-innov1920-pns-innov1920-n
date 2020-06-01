using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPlayer : BoardBehaviour {
    private Player player;
    [SerializeField] private DeckUnit deckUnit;
    [SerializeField] private DeckSpell deckSpell;

    public override void OnResetBoard(BoardManager boardManager) {
        base.OnResetBoard(boardManager);
        Start();
    }
    
    private void Start() {
        player = manager.LocalPlayer;
        StartCoroutine(CardDraw());
        
    }

    IEnumerator CardDraw()
    {
        
        float time = 3f;
        yield return new WaitForSecondsRealtime(time);
        deckUnit.DrawUnit(manager.CurrentPlayer.DrawUnit());
        yield return new WaitForSecondsRealtime(time);
        deckUnit.DrawUnit(manager.CurrentPlayer.DrawUnit());
        yield return new WaitForSecondsRealtime(time);
        deckUnit.DrawUnit(manager.CurrentPlayer.DrawUnit());
        yield return new WaitForSecondsRealtime(time);
        deckUnit.DrawUnit(manager.CurrentPlayer.DrawUnit());
        yield return new WaitForSecondsRealtime(time);
        deckSpell.DrawSpell(manager.CurrentPlayer.DrawSpell());
        yield return new WaitForSecondsRealtime(time);
        deckSpell.DrawSpell(manager.CurrentPlayer.DrawSpell());
        
        
    }
}