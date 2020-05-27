using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseHover : MonoBehaviour
{
    [SerializeField] private BoardClicker boardClicker = null;
    [SerializeField] private HoverCell hoverCell = null;

    // Update is called once per frame
    void Update()
    {
        if (boardClicker.HoverCell(out var hover)) {
            hoverCell.ShowMouseHover(hover);
        }
    }
}
