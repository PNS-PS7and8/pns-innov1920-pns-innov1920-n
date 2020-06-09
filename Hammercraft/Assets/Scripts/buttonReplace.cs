using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class buttonReplace : MonoBehaviour
{
    [SerializeField] private GameObject replaceCross = null;
    [SerializeField] private GameObject card = null;

    public void OnMouseDown()
    {
        replaceCross.SetActive(!replaceCross.activeInHierarchy);
        DeckDraw.eventReplaceCards.Invoke(card);
    }
}
