using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    // Start is called before the first frame update
    public hexMap Map;
    public int currentSelected = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < this.transform.childCount; i++){
            bool select = this.transform.GetChild(i).GetComponent<HandCard>().selected;
            if (currentSelected != i && select == true){
                if (currentSelected != -1){
                    this.transform.GetChild(currentSelected).GetComponent<HandCard>().selected = false;
                }
                currentSelected = i;
            }
        }

        if (this.transform.childCount > 0){
            HandCard carte = this.transform.GetChild(currentSelected).GetComponent<HandCard>();
            if(carte.selected == true && Map.selectedHexa.y == 0){
                carte.selected = false;
                Map.selectedHexa.y = 1;
                currentSelected = 0;
                //Vector3 middleCoord = Map.s
                Vector2 coord = new Vector2(Map.realSelectedHexa.x, Map.realSelectedHexa.z);
                carte.DeployUnit(coord);
            }

            if (carte.selected == false){
                Map.selectedHexa.y = 1;
            }
        }

    }
}
