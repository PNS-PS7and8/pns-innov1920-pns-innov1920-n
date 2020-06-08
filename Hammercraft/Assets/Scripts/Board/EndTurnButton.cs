using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class EndTurnButton : MonoBehaviour
{
    [SerializeField] private BoardManager BoardManager;
    public void OnMouseDown()
    {
        if (BoardManager.Manager.Turn != 1 || !BoardManager.ValidateButton.activeInHierarchy)
        {
            BoardManager.NextTurn();
        }
    }     

    
}
