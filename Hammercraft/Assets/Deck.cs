using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    // Start is called before the first frame update
    public hexMap Map;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.childCount > 0){
            HandCard carte = this.transform.GetChild(0).GetComponent<HandCard>();
            if(carte.selected == true && Map.selectedHexa.y == 0){
                carte.selected = false;
                Map.selectedHexa.y = 1;
                Vector2 coord = new Vector2(Map.realSelectedHexa.x, Map.realSelectedHexa.z);
                carte.DeployUnit(coord);
            }

            if (carte.selected == false){
                Map.selectedHexa.y = 1;
            }
        }

    }
}
