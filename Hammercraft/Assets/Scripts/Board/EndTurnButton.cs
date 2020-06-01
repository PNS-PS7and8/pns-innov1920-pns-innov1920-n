using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    [SerializeField] private BoardManager BoardManager;
    public void OnMouseDown()
    {
        BoardManager.NextTurn();
    }
}
