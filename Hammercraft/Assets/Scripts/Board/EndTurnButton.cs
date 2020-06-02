using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class EndTurnButton : MonoBehaviour
{
    [SerializeField] private BoardManager BoardManager;
    public void OnMouseDown()
    {
        BoardManager.NextTurn();
    }
}
