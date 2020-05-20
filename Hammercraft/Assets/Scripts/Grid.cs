using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    const float hexWidth = 1.73203f;
    const float hexHeight = 2f;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    public Material material;
    private Mesh mesh;

    public Vector2Int gridSize;
    [Range(0,1)] public float thickness = 0.1f;
    public float height;

    void Start()
    {
        mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = material;
        meshFilter.mesh = mesh;
    }

    public void GenerateMesh() {
        if (!mesh) { Start(); }
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        for(int x=0; x<gridSize.x; x++) {
            for(int z=0; z<gridSize.y; z++) {
                Vector3 pos = GetHexPos(x, z);
                if (Physics.Raycast(transform.TransformPoint(pos), -transform.up, out var hit)) {
                    pos.y = - hit.distance + height;
                    GenerateHexagon(pos, vertices, triangles);
                }
                
            }
        }
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }

    void GenerateHexagon(Vector3 position, List<Vector3> vertices, List<int> triangles) {
        int nverts = vertices.Count;

        vertices.Add(position + new Vector3(0, 0, -hexHeight/2f));
        vertices.Add(position + new Vector3(hexWidth/2f, 0, -0.5f));
        vertices.Add(position + new Vector3(hexWidth/2f, 0, 0.5f));
        vertices.Add(position + new Vector3(0, 0, hexHeight/2f));
        vertices.Add(position + new Vector3(-hexWidth/2f, 0, 0.5f));
        vertices.Add(position + new Vector3(-hexWidth/2f, 0, -0.5f));

        vertices.Add(position + new Vector3(0, 0, -hexHeight/2f) * (1 - thickness));
        vertices.Add(position + new Vector3(hexWidth/2f, 0, -0.5f) * (1 - thickness));
        vertices.Add(position + new Vector3(hexWidth/2f, 0, 0.5f) * (1 - thickness));
        vertices.Add(position + new Vector3(0, 0, hexHeight/2f) * (1 - thickness));
        vertices.Add(position + new Vector3(-hexWidth/2f, 0, 0.5f) * (1 - thickness));
        vertices.Add(position + new Vector3(-hexWidth/2f, 0, -0.5f) * (1 - thickness));


        triangles.Add(nverts + 0); triangles.Add(nverts + 6); triangles.Add(nverts + 1);
        triangles.Add(nverts + 1); triangles.Add(nverts + 6); triangles.Add(nverts + 7);

        triangles.Add(nverts + 1); triangles.Add(nverts + 7); triangles.Add(nverts + 2);
        triangles.Add(nverts + 2); triangles.Add(nverts + 7); triangles.Add(nverts + 8);

        triangles.Add(nverts + 2); triangles.Add(nverts + 8); triangles.Add(nverts + 3);
        triangles.Add(nverts + 3); triangles.Add(nverts + 8); triangles.Add(nverts + 9);

        triangles.Add(nverts + 3); triangles.Add(nverts + 9); triangles.Add(nverts + 4);
        triangles.Add(nverts + 4); triangles.Add(nverts + 9); triangles.Add(nverts + 10);

        triangles.Add(nverts + 4); triangles.Add(nverts + 10); triangles.Add(nverts + 5);
        triangles.Add(nverts + 5); triangles.Add(nverts + 10); triangles.Add(nverts + 11);

        triangles.Add(nverts + 5); triangles.Add(nverts + 11); triangles.Add(nverts + 0);
        triangles.Add(nverts + 0); triangles.Add(nverts + 11); triangles.Add(nverts + 6);
    }

    public Vector3 GetHexPos(int x, int z) {
        float y = hexHeight * (z/2) * 1.5f;
        if (z%2 == 0) {
            return new Vector3(x * hexWidth, 0, y);
        } else {
            return new Vector3(x * hexWidth + hexWidth / 2f, 0, y + hexHeight*0.75f);
        }
    }
}
