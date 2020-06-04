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
        if (canDraw) {
            if (draw == Draw.Unit) {
                player.DrawUnit();
            } else {
                player.DrawSpell();
            }
            canDraw = false;
            arrow1.SetActive(false);
            arrow2.SetActive(false);
            if (others != null) {
            foreach(var o in others) {
                o.canDraw = false;
                
            }
        }
        }
    }
}