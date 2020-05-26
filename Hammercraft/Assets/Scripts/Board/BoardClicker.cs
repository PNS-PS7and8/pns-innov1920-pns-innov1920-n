using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BoardClicker : BoardBehaviour
{
    public bool ClickCell(out Cell cell) {
        if (Input.GetMouseButtonDown(0)) {
            return HoverCell(out cell);
        }
        cell = null;
        return false;
    }

    public bool HoverCell(out Cell cell) {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitinfo) && hitinfo.transform == transform) {
            Vector2Int cellPos = board.LocalToCell(transform.InverseTransformPoint(hitinfo.point));
            if (board.HasCell(cellPos)) {
                cell = board.GetCell(cellPos);
                if (cell.cellType != Cell.CellType.None)
                    return true;
            }
        }
        cell = null;
        return false;
    }
}