using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCard : MonoBehaviour
{
    public UnitCard card;
    [HideInInspector] public bool selected = false;

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown() {
        if (selected == false){
            selected = true;
        }
    }

    public void DeployUnit(Vector2 coord){
        Unit u = new Unit(card, coord);
        Destroy(this.gameObject);
    }
}
