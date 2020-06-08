using System.Collections;
using UnityEngine;

public class BoardCardDraw : MonoBehaviour {
    public enum Draw {
        Unit, Spell
    }
    
    [SerializeField] private BoardPlayer player;
    [SerializeField] private Draw draw;
    [SerializeField] private BoardCardDraw[] others;
    [SerializeField] private GameObject arrow1;
    [SerializeField] private GameObject arrow2;
    [SerializeField] private BoardManager BoardManager;
    [SerializeField] private GameObject DeckSpell;
    [SerializeField] private GameObject DeckUnit;

    private bool canDraw = false;

    public void AllowDraw() {
        canDraw = true;
        arrow1.SetActive(true);
        arrow2.SetActive(true);
        if (others != null) {
            foreach(var o in others) {
                o.canDraw = true;
            }
        }
    }

    private void OnMouseDown() {
        
        if (BoardManager.Manager.Turn != 1 || !BoardManager.ValidateButton.activeInHierarchy)
        { 
            if (canDraw) {
                if (draw == Draw.Unit) {
                    player.DrawUnit();
                } else {
                    player.DrawSpell();
                }
                canDraw = false;
                arrow1.SetActive(false);
                arrow2.SetActive(false);
                if (draw == Draw.Unit)
                {
                    if (BoardManager.Manager.CurrentPlayer.Deck.units.Count == 0)
                    {
                        DeckUnit.SetActive(false);
                    }
                } else
                {
                    if (BoardManager.Manager.CurrentPlayer.Deck.spells.Count == 0)
                    {
                        DeckSpell.SetActive(false);
                    }
                }
                if (others != null) {
                    foreach(var o in others) {
                        o.canDraw = false;
                
                    }
                }
            }

        }
    }

}