using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseHover : BoardBehaviour
{
    [SerializeField] private BoardClicker boardClicker = null;
    [SerializeField] private HoverCell hoverCell = null;

    [SerializeField] private Color friendlyColor = new Color();
    [SerializeField] private Color ennemyColor = new Color();
    [SerializeField] private Color hoverColor = new Color();

    private void OnEnable() {
        boardClicker.OnHoverCell += OnHoverCell;
        boardClicker.OnHoverUnit += OnHoverUnit;
        boardClicker.OnHoverCard += _ => Clear();
    }

    private void OnDisable() {
        boardClicker.OnHoverCell -= OnHoverCell;
        boardClicker.OnHoverUnit -= OnHoverUnit;
        boardClicker.OnHoverCard -= _ => Clear();
    }

    void OnHoverCell(Cell cell)
    {
        Clear();
        hoverCell.ShowCells(hoverColor, cell);
    }

    void OnHoverUnit(Cell cell, Unit unit) {
        Clear();
        if (unit.Player == PlayersExtension.LocalPlayer())
            hoverCell.ShowCells(friendlyColor, cell);
        else
            hoverCell.ShowCells(ennemyColor, cell);
    }

    void Clear() {
        hoverCell.HideCells(friendlyColor);
        hoverCell.HideCells(ennemyColor);
        hoverCell.HideCells(hoverColor);
    }
}
