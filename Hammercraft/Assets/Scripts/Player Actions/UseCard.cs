using System.Collections;
using System.Linq;
using UnityEngine;
using TMPro;
public class UseCard : BoardBehaviour
{
    [SerializeField] private BoardClicker boardClicker = null;
    [SerializeField] private HoverCell hoverCell = null;

   // [SerializeField] private float growDuration = 0.2f;
   // [SerializeField] private float growAmount = 1.5f;
    [SerializeField] private int cardCost = 0;
    [SerializeField] private TMP_Text atqText;
    [SerializeField] private TMP_Text healText;
    private GameCard card;

    private void OnEnable() {
        boardClicker.OnClickCard += OnClickCard;
        boardClicker.OnClickCell += OnClickCell;
        boardClicker.OnClickUnit += (c, u) => OnClickCell(c);
    }

    private void OnDisable() {
        boardClicker.OnClickCard -= OnClickCard;
        boardClicker.OnClickCell -= OnClickCell;
        boardClicker.OnClickUnit -= (c, u) => OnClickCell(c);
    }

    void OnClickCard(GameCard card)
    {
        
        if (manager.MyTurn()) {
            if (manager.CurrentPlayer.CurrentGold >= card.card.Cost) {
                this.card = card;
                this.cardCost = card.card.Cost;
                hoverCell.HideCells(Color.green);
                
                hoverCell.ShowCells(Color.green, 
                    board.Cells().Where(cell => CardCast.CanCast(card.card.castMask, board, cell))
                );
            }
        }
    }

    private void DisplayAtq(int dmg, Unit unit){
        atqText.gameObject.SetActive(true);
        Vector3 pos = board.LocalPosition(unit);
        pos.z -= 11f;
        pos.y += 2; 
        pos.x -= 1.5f;
        atqText.transform.localPosition = pos;
        if (unit.Health-dmg <=0){
            atqText.text = "KO";
        } else {
            atqText.text = "-"+dmg;
        }
        StartCoroutine(waitDmg());
    }

    private void DisplayHeal(int dmg, Unit unit){
        healText.gameObject.SetActive(true);
        Vector3 pos = board.LocalPosition(unit);
        pos.z -= 11f;
        pos.y += 2; 
        pos.x -= 1.5f;
        healText.transform.localPosition = pos;
        healText.text = "+"+dmg;
        StartCoroutine(waitDmg());
    }

    private IEnumerator waitDmg(){
            yield return new WaitForSecondsRealtime(2f);
            atqText.gameObject.SetActive(false);
            healText.gameObject.SetActive(false);
    }

    private void DisplayDmg(Cell cell){
        if (card.card.GetType().IsAssignableFrom(typeof(DamageSpell)) && board.GetUnit(cell) != null ){
            DamageSpell ds = (DamageSpell)this.card.card;
            if (ds.Damage > 0) {
                DisplayAtq(ds.Damage , board.GetUnit(cell));
            } else {
                DisplayHeal(ds.Damage , board.GetUnit(cell));
            }
        }
    }

    void OnClickCell(Cell cell) {
        if (this.card != null && manager.MyTurn()) {
            if (manager.CurrentPlayer.CurrentGold >= this.card.card.Cost) {
                DisplayDmg(cell);
                if (card.Use(cell)) {
                    manager.CurrentPlayer.SetCurrentGold(manager.CurrentPlayer.CurrentGold - this.cardCost);
                    this.card = null;
                    this.cardCost = 0;
                }
            }
        }
        hoverCell.HideCells(Color.green);
        card = null;
    }
}
