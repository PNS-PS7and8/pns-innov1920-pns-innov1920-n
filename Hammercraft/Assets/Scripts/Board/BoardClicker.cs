using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BoardClicker : BoardBehaviour
{
    public bool ClickCell(out Cell cell) {
        if (Input.GetMouseButtonDown(0)) {
            return HoverCell(out cell);
        }
        cell = new Cell();
        return false;
    }

    public bool HoverCell(out Cell cell) {
        cell = new Cell();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitinfo) && hitinfo.transform == transform) {
            Vector2Int cellPos = board.LocalToCell(transform.InverseTransformPoint(hitinfo.point));
            cell = board.GetCell(cellPos);
            return true;
        }
        return false;
    }
}