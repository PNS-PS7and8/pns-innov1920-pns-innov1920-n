using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hexMap : MonoBehaviour
{
    const float hexWidth = 1.73203f;
    const float hexHeight = 2f;
    public Grid grid;
    // Update is called once per frame
    void OnMouseDown() {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Get a ray from the screen at mouse position
            if (Physics.Raycast(ray, out RaycastHit hitinfo)) { // Did the ray hit something ?
                Vector3 pos = transform.TransformPoint(hitinfo.point);
                float x = pos.x / 1.5f;
                float z = 0f;
                if (Mathf.RoundToInt(pos.x) % 2 == 0){
                    z = pos.z / hexHeight / 1.73f * 2f;
                } else {
                    z = (pos.z - hexHeight + (3)) / hexHeight / 1.73f * 2f;
                }
                //print(Mathf.RoundToInt(x)+"; "+Mathf.RoundToInt(z));
                
            }
    }
}
