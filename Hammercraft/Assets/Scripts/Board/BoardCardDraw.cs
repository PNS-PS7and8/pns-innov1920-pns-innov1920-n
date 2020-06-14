using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//Classe s'occupant de la pioche du deck par le joueur lors du jeu
public class BoardCardDraw : MonoBehaviour {
    public enum Draw {
        Unit, Spell
    }
    
    [SerializeField] private BoardPlayer player = null;
    [SerializeField] private Draw draw = Draw.Spell;
    [SerializeField] private BoardCardDraw[] others = null;
    [SerializeField] private GameObject drawUI = null;
    [SerializeField] private BoardManager BoardManager = null;
    [SerializeField] private GameObject DeckSpell = null;
    [SerializeField] private GameObject DeckUnit = null;

    private bool canDraw = false;

    public void AllowDraw() {
        StartCoroutine(AllowDrawCo());
    }

    IEnumerator AllowDrawCo()
    {
        if (BoardManager.Manager.Turn == 1)
        {
            yield return new WaitUntil(() => BoardManager.ValidateButton.activeInHierarchy);
            yield return new WaitWhile(() => BoardManager.ValidateButton.activeInHierarchy);

        }
        canDraw = true;
        drawUI.SetActive(true);
        if (others != null) {
            foreach(var o in others) {
                o.canDraw = true;
            }
        }
    }

    public void DrawAtStartOfTurn() {
        
        if (BoardManager.Manager.Turn != 1 || !BoardManager.ValidateButton.activeInHierarchy)
        { 
            if (canDraw) {
                if (draw == Draw.Unit) {
                    player.DrawUnit();
                } else {
                    player.DrawSpell();
                }
                canDraw = false;
                drawUI.SetActive(false);
                if (draw == Draw.Unit)
                {
                    if (BoardManager.Manager.CurrentPlayer.Deck.units.Count == 0)
                    {
                        DeckUnit.SetActive(false);
                        drawUI.GetComponentsInChildren<Button>(true)[1].gameObject.SetActive(false);
                    }
                } else
                {
                    if (BoardManager.Manager.CurrentPlayer.Deck.spells.Count == 0)
                    {
                        DeckSpell.SetActive(false);
                        drawUI.GetComponentsInChildren<Button>(true)[0].gameObject.SetActive(false);
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