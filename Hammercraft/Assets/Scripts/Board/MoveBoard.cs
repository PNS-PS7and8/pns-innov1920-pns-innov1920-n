using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoard : MonoBehaviour
{
    [SerializeField] private float maxZoom = 0.03f;
    [SerializeField] private float minZoom = 0.013f;
    private float CurrentZoom = 0;
    [SerializeField] private float Sensitivity = 0.002f;
    [SerializeField] private float distance = -0.1f;
    [SerializeField] private float decalage = 0.05f;
    [SerializeField] private float magni = 0.2f;
    Vector3 p = Vector3.zero;
    Vector3 s = Vector3.zero;
    bool buttonDown = false;

    private void Start()
    {
        CurrentZoom = transform.localScale.x;
    }

    void FixedUpdate()
    {
        
        transform.localScale = Vector3.one * Mathf.Clamp((CurrentZoom + (Input.mouseScrollDelta.y * Time.deltaTime * Sensitivity)), minZoom, maxZoom);
        CurrentZoom = transform.localScale.x;
        magni = ((CurrentZoom * 450f) / 17f) - 117f / 340f;
        decalage = ((CurrentZoom * -1f) / 17f) + 18359375f/6640625000f;
        

    }

    private void OnMouseOver()
    {
        
        if (Input.GetMouseButtonDown(1))
        {
            
            p = Input.mousePosition;
            s = Vector3.zero;
            buttonDown = true;
        }

        if (Input.GetMouseButton(1) && buttonDown)
        {
            
            s = Input.mousePosition;
            Vector3 normal = Vector3.Normalize(s - p);
            normal = new Vector3(normal.x,0,normal.y);
           
            transform.position = Vector3.ClampMagnitude(((s - p).magnitude * normal * decalage),magni)-new Vector3(0,0.1f,0);

        }

        if (Input.GetMouseButtonUp(1))
        {
            buttonDown = false;
        }

    }







}
