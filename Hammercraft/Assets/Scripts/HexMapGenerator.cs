using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMapGenerator : MonoBehaviour
{
    public GameObject hexTile;
    
    int mapWidth = 25;
    int mapHeight = 12;
    public float xoffset = 1.8f;
    public float zoffset = 1.8f;
    // Start is called before the first frame update
    void Start()
    {
        CreateHexMap();
    }

    // Update is called once per frame
    void CreateHexMap()
    {
        for (int x = 0; x <= mapWidth; x++)
        {
            for (int z = 0; z <= mapHeight; z++)
            {
                GameObject Go = Instantiate(hexTile);
                if (z % 2 == 0)
                {
                    Go.transform.position = new Vector3(x * xoffset, 0, z * zoffset); 
                }
                else 
                {
                    Go.transform.position = new Vector3(x * xoffset + xoffset/2, 0, z * zoffset+zoffset/2);
                }
            }
        }
    }
}
