using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hexMap : MonoBehaviour
{
    const float hexWidth = 1.73203f;
    const float hexHeight = 2f;
    public Grid grid;
    // Update is called once per frame
    public Vector3Int selectedHexa;
    public Vector3 realSelectedHexa;

    void Start() {
        selectedHexa.y = 1;
    } 

    void OnMouseDown() {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Get a ray from the screen at mouse position
            if (Physics.Raycast(ray, out RaycastHit hitinfo)) { // Did the ray hit something ?
                realSelectedHexa = transform.TransformPoint(hitinfo.point);
                float x = realSelectedHexa.x / 1.5f;
                float z = 0f;
                if (Mathf.RoundToInt(realSelectedHexa.x) % 2 == 0){
                    z = realSelectedHexa.z / hexHeight / 1.73f * 2f;
                } else {
                    z = (realSelectedHexa.z - hexHeight + (3)) / hexHeight / 1.73f * 2f;
                }
                selectedHexa.x =  Mathf.RoundToInt(x);
                selectedHexa.y = 0;
                selectedHexa.z = Mathf.RoundToInt(z);
                
            }
    }
}
