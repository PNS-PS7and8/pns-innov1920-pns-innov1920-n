using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System.Collections;
public class MoveUnit : BoardBehaviour
{
    [SerializeField] private BoardClicker boardClicker = null;
    [SerializeField] private HoverCell hoverCell = null;
    [SerializeField] private Color color = Color.white;
    [SerializeField] private Color pathColor = Color.white;
    [SerializeField] private TMP_Text infoUnits = null;
    [SerializeField] private TMP_Text atqText;
    [SerializeField] private TMP_Text counterText;


    private Cell origin;
    private Unit unit;
    private Unit infoUnit;
    private Cell hover;

    private void Update() {
        if(unit != null){
            if (unit.Health <= 0){
                Deselect();
            }
        }
        if(infoUnit != null){
            SetInfo();
        }
    }

    private void SetInfo(){
        string move;
        string atq;
        if(infoUnit.Health <= 0){infoUnits.gameObject.SetActive(false);} else {infoUnits.gameObject.SetActive(true);}
        if (CanMove(infoUnit)){move = "Can move";} else { move = "Movement used";}
        if (CanAtq(infoUnit)){atq = "Can attack";} else { atq = "Attack used";}
        if (infoUnit.Player == PlayersExtension.LocalPlayer()){infoUnits.color = Color.cyan;} else {infoUnits.color = Color.red;}
        if (infoUnit.Card.ResourcePath.Contains("Specials/")){
            infoUnits.text = infoUnit.Card.Name+"\nHP: "+infoUnit.Health;
        } else {
            infoUnits.text = infoUnit.Card.Name+"\nHP: "+infoUnit.Health+"\nATQ: "+infoUnit.Attack+"\n"+move+"\n"+atq;
        }
    }

    private bool CanMove(Unit unit) {
        return manager.History.Find<MovementAction>(action => {
            return
                action.Turn == manager.Turn &&
                action.UnitId == unit.Id;
        }).Count == 0;
    }

    private bool CanAtq(Unit unit) {
        return manager.History.Find<AtqAction>(action => {
            return
                action.Turn == manager.Turn &&
                action.UnitId == unit.Id;
        }).Count == 0;
    }
    
    private void OnEnable() {
        boardClicker.OnHoverUnit += OnInfoUnit;
        boardClicker.OnClickUnit += OnSelectUnit;
        boardClicker.OnClickCell += OnSelectCell;
        boardClicker.OnHoverCell += OnHoverCell;
        boardClicker.OnClickCard += _ => Deselect();
    }

    private void OnDisable() {
        boardClicker.OnHoverUnit -= OnInfoUnit;
        boardClicker.OnClickUnit -= OnSelectUnit;
        boardClicker.OnClickCell -= OnSelectCell;
        boardClicker.OnHoverCell -= OnHoverCell;
        boardClicker.OnClickCard -= _ => Deselect();
    }

    private void OnInfoUnit(Cell HovCell, Unit unit) {
        this.infoUnit = unit;
    }

        private void DisplayAtq(int dmg, Unit unit){
        atqText.gameObject.SetActive(true);
        Vector3 pos = board.LocalPosition(unit);
        pos.z -= 11f;
        pos.y += 2; 
        pos.x -= 1.5f;
        atqText.transform.localPosition = pos;
        
        if (unit.Health <=0){
            atqText.text = "KO";
        } else {
            atqText.text = "-"+dmg;
        }
        StartCoroutine(waitDmg());
    }

    private void DisplayCounterAtq(int dmg, Unit unit){
        counterText.gameObject.SetActive(true);
        Vector3 pos = board.LocalPosition(unit);
        pos.z -= 11f;
        pos.y += 2; 
        pos.x -= 1.5f;
        counterText.transform.localPosition = pos;

        if (unit.Health <=0){
            counterText.text = "KO";
        } else {
            counterText.text = "-"+dmg;
        }
        StartCoroutine(waitDmg());
    }

    private IEnumerator waitDmg(){
            yield return new WaitForSecondsRealtime(2f);
            counterText.gameObject.SetActive(false);
            atqText.gameObject.SetActive(false);
    }

    void OnSelectUnit(Cell cell, Unit unit)
    {
        if (this.unit != null && cell.Distance(origin) <= this.unit.RangeAtq && this.unit != unit && CanAtq(this.unit)){ 
            this.unit.DealDamage(unit);
            DisplayAtq(this.unit.Attack, unit);
            if (unit.RangeAtq>=cell.Distance(origin)){
                unit.DealDamage(this.unit);
                DisplayCounterAtq(unit.Attack, this.unit);
            }
            manager.History.Add(new AtqAction(manager.CurrentPlayer.Role, manager.Turn, this.unit.Id));
            Deselect();
        } else if(!unit.Dead && manager.MyTurn() && unit.Player == manager.PlayerTurn) {
            this.unit = unit;
            this.origin = cell;
            if ((unit.MovementMask & UnitMoveMask.FlyBy) != 0){
                hoverCell.ShowCells(color, UnitMovement.AvailableCells(unit.MovementMask, board, cell, unit.Movement+1));
            } else {
                hoverCell.ShowCells(color, UnitMovement.AvailableCells(unit.MovementMask, board, cell, unit.Movement));
            }
            hoverCell.ShowCells(Color.yellow, UnitMovement.AvailableCells(unit.AtqMask, board, cell, (unit.RangeAtq)/2));
        }
    }

    private void OnSelectCell(Cell cell) {
        if (origin != null && unit != null && cell.position != unit.position) {
            if (UnitMovement.CanMove(unit.MovementMask, board, origin, cell, unit.Movement, out var path) && CanMove(unit)) {
                
                unit.position = cell.position;

                manager.History.Add(new MovementAction(manager.CurrentPlayer.Role, manager.Turn, unit.Id, path.Select(c => c.position).ToArray()));
                boardManager.SubmitManager();

                Deselect();
            } else {
                Deselect();
            }
        }
    }

    private void OnHoverCell(Cell cell) {
        if (hover != cell) {
            hover = cell;
            hoverCell.HideCells(pathColor);
            if (unit == null || origin == null) return;
            if (UnitMovement.CanMove(unit.MovementMask, board, origin, cell, unit.Movement, out var path)) {
                hoverCell.ShowCells(pathColor, path);
            }
        }
    }

    private void Deselect() {
        unit = null;
        origin = null;
        hoverCell.HideCells(Color.yellow);
        hoverCell.HideCells(pathColor);
        hoverCell.HideCells(color);
    }
}
