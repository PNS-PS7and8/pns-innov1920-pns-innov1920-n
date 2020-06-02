using UnityEngine;

public class BoardCardDraw : MonoBehaviour {
    public enum Draw {
        Unit, Spell
    }
    
    [SerializeField] private BoardPlayer player;
    [SerializeField] private Draw draw;
    [SerializeField] private BoardCardDraw[] others;
    private bool canDraw = false;

    public void AllowDraw() {
        canDraw = true;
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
            if (others != null) {
            foreach(var o in others) {
                o.canDraw = false;
            }
        }
        }
    }
}