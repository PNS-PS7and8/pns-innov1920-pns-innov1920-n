using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(BoxCollider))]
public class BoardMesh : BoardBehaviour
{
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;
    private Mesh mesh;

    [Range(0,1)] public float thickness = 0.1f;
    [Range(0,1)] public float spacing = 0.1f;

    void Start()
    {
        mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        meshFilter = gameObject.GetComponent<MeshFilter>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        boxCollider = gameObject.GetComponent<BoxCollider>();

        meshFilter.mesh = mesh;
        GenerateMesh();

        boxCollider.center = mesh.bounds.center;
        boxCollider.size = mesh.bounds.extents * 2f;
    }

    public void GenerateMesh() {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        for(int x=0; x < board.size.x; x++) {
            for(int z=0; z < board.size.y; z++) {
                Vector3 pos = board.CellToLocal(x, z);
                GenerateHexagon(pos, vertices, triangles);
            }
        }
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }

    void GenerateHexagon(Vector3 position, List<Vector3> vertices, List<int> triangles) {
        int nverts = vertices.Count;

        Vector3 s = Vector3.one * (1 + spacing);
        Vector3 t = Vector3.one * (1 - thickness);

        vertices.Add(position + Vector3.Scale(s, new Vector3(-0.5f, 0, -0.866f)));
        vertices.Add(position + Vector3.Scale(s, new Vector3(0.5f, 0, -0.866f)));
        vertices.Add(position + Vector3.Scale(s, new Vector3(1f, 0, 0)));
        vertices.Add(position + Vector3.Scale(s, new Vector3(0.5f, 0, 0.866f)));
        vertices.Add(position + Vector3.Scale(s, new Vector3(-0.5f, 0, 0.866f)));
        vertices.Add(position + Vector3.Scale(s, new Vector3(-1f, 0, 0)));
        
        vertices.Add(position + Vector3.Scale(t, new Vector3(-0.5f, 0, -0.866f)));
        vertices.Add(position + Vector3.Scale(t, new Vector3(0.5f, 0, -0.866f)));
        vertices.Add(position + Vector3.Scale(t, new Vector3(1f, 0, 0)));
        vertices.Add(position + Vector3.Scale(t, new Vector3(0.5f, 0, 0.866f)));
        vertices.Add(position + Vector3.Scale(t, new Vector3(-0.5f, 0, 0.866f)));
        vertices.Add(position + Vector3.Scale(t, new Vector3(-1f, 0, 0)));

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
}
