using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Classe gérant le clique du joueur sur le plateau afin de récupéré la cellule ou l'unité cliqué
[RequireComponent(typeof(Collider))]
public class BoardClicker : BoardBehaviour
{
    [SerializeField] private Camera Camera = null;
    public delegate void OnClickCardAction(GameCard card);
    public delegate void OnClickCellAction(Cell cell);
    public delegate void OnClickUnitAction(Cell cell, Unit unit);
    
    public event OnClickCardAction OnHoverCard;
    public event OnClickCardAction OnClickCard;
    public event OnClickCellAction OnHoverCell;
    public event OnClickCellAction OnClickCell;
    public event OnClickUnitAction OnHoverUnit;
    public event OnClickUnitAction OnClickUnit;

    void Update()
    {
        if (Raycast(out var hit)) {
            if (HoverCard(hit, out var card)) {
                if (Input.GetMouseButtonDown(0))
                    OnClickCard?.Invoke(card);
                else
                    OnHoverCard?.Invoke(card);
            }
            else if (HoverCell(hit, out var cell)) {
                Unit unit = board.GetUnit(cell);
                if (unit != null && !unit.Dead) {
                    if (Input.GetMouseButtonDown(0))
                        OnClickUnit?.Invoke(cell, unit);
                    else
                        OnHoverUnit?.Invoke(cell, unit);
                } else {
                    if (Input.GetMouseButtonDown(0))
                        OnClickCell?.Invoke(cell);
                    else 
                        OnHoverCell?.Invoke(cell);
                }
            }
        }
    }

    private bool HoverCell(RaycastHit hit, out Cell cell) {
        if (hit.transform == boardManager.transform) {
            Vector2Int cellPos = board.LocalToCell(transform.InverseTransformPoint(hit.point));
            if (board.HasCell(cellPos)) {
                cell = board.GetCell(cellPos);
                if (cell != null && cell.cellType != Cell.CellType.None)
                    return true;
            }
        }
        cell = null;
        return false;
    }

    private bool HoverCard(RaycastHit hit, out GameCard card) {
        card = hit.transform.GetComponent<GameCard>();
        return card != null;
    }

    private bool Raycast(out RaycastHit hit) {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit);
    }

  
}