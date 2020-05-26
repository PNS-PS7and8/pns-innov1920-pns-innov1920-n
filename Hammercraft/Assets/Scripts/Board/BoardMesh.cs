using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class BoardMesh : BoardBehaviour
{
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;
    private Mesh mesh;

    public override void OnResetBoard(BoardManager boardManager) {
        base.OnResetBoard(boardManager);
        Start();
    }

    private void Start()
    {
        mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        meshFilter = gameObject.GetComponent<MeshFilter>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshCollider = gameObject.GetComponent<MeshCollider>();

        GenerateMesh();

        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

    public void GenerateMesh() {
        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uv = new List<Vector2>();
        List<List<int>> triangles = new List<List<int>>();
        for (int i = 0; i < 6; i++)triangles.Add(new List<int>());

        List<List<Cell>> cellGroups = new List<List<Cell>>();
        
        for(int x=0; x < board.size.x; x++) {
            for(int z=0; z < board.size.y; z++) {
                Cell cell = board.GetCell(x, z);
                if (cell.cellType != Cell.CellType.None) {
                    GenerateHexagon(cell, vertices, uv, triangles);
                }
            }
        }

        mesh.Clear();
        mesh.subMeshCount = triangles.Count;
        mesh.vertices = vertices.ToArray();
        mesh.uv = uv.ToArray();
        for (int i = 0; i < triangles.Count; i++) mesh.SetTriangles(triangles[i], i);
        mesh.RecalculateNormals();
    }

    void GenerateHexagon(Cell cell, List<Vector3> vertices, List<Vector2> uv, List<List<int>> triangles) {
        GenerateHexagonTop(cell, vertices, uv, triangles);
        foreach(var neighbor in board.Neighbors(cell)) {
            if (CellSubMesh(cell) > CellSubMesh(neighbor)) {
                GenerateHexagonSide(cell, neighbor, vertices, uv, triangles);
            }
        }
    }

    private void GenerateHexagonTop(Cell cell, List<Vector3> vertices, List<Vector2> uv, List<List<int>> triangles) {
        int nverts = vertices.Count;
        int subMesh = CellSubMesh(cell);

        Vector3 pos = board.CellToLocal(cell.position);

        vertices.Add(pos);
        vertices.Add(pos + new Vector3(-0.5f, 0, -Mathf.Sqrt(3f)/2f));
        vertices.Add(pos + new Vector3(0.5f, 0, -Mathf.Sqrt(3f)/2f));
        vertices.Add(pos + new Vector3(1f, 0, 0));
        vertices.Add(pos + new Vector3(0.5f, 0, Mathf.Sqrt(3f)/2f));
        vertices.Add(pos + new Vector3(-0.5f, 0, Mathf.Sqrt(3f)/2f));
        vertices.Add(pos + new Vector3(-1f, 0, 0));

        uv.Add(new Vector2(0.5f,    0.5f));
        uv.Add(new Vector2(0.25f,     0));
        uv.Add(new Vector2(0.75f,     0));
        uv.Add(new Vector2(1,     0.5f));
        uv.Add(new Vector2(0.75f,     1));
        uv.Add(new Vector2(0.25f,     1));
        uv.Add(new Vector2(0,     0.5f));

        triangles[subMesh].Add(nverts); triangles[subMesh].Add(nverts+2); triangles[subMesh].Add(nverts+1);
        triangles[subMesh].Add(nverts); triangles[subMesh].Add(nverts+3); triangles[subMesh].Add(nverts+2);
        triangles[subMesh].Add(nverts); triangles[subMesh].Add(nverts+4); triangles[subMesh].Add(nverts+3);
        triangles[subMesh].Add(nverts); triangles[subMesh].Add(nverts+5); triangles[subMesh].Add(nverts+4);
        triangles[subMesh].Add(nverts); triangles[subMesh].Add(nverts+6); triangles[subMesh].Add(nverts+5);
        triangles[subMesh].Add(nverts); triangles[subMesh].Add(nverts+1); triangles[subMesh].Add(nverts+6);
    }

    private void GenerateHexagonSide(Cell high, Cell low, List<Vector3> vertices, List<Vector2> uv, List<List<int>> triangles) {
        int subMesh = SideSubMesh(high, low);
        
        Vector3 middle = (board.CellToLocal(high.position) + board.CellToLocal(low.position)) / 2f;
        Vector3 offset = board.CellToLocal(high.position) - board.CellToLocal(low.position);
        offset.y = 0;
        Vector3 right = Vector3.Cross(offset, Vector3.up).normalized * 0.5f;
        float height = high.Height - low.Height;

        Vector3 a = middle - right + Vector3.up * height / 2f;
        Vector3 b = middle + right + Vector3.up * height / 2f;
        Vector3 c = middle - right - Vector3.up * height / 2f;
        Vector3 d = middle + right - Vector3.up * height / 2f;

        int nverts = vertices.Count;

        vertices.Add(a);
        vertices.Add(b);
        vertices.Add(c);
        vertices.Add(d);

        uv.Add(new Vector2(1, 1));
        uv.Add(new Vector2(0, 1));
        uv.Add(new Vector2(1, 0));
        uv.Add(new Vector2(0, 0));

        triangles[subMesh].Add(nverts); triangles[subMesh].Add(nverts+2); triangles[subMesh].Add(nverts+1);
        triangles[subMesh].Add(nverts+2); triangles[subMesh].Add(nverts+3); triangles[subMesh].Add(nverts+1);

    }

    private int CellSubMesh(Cell cell) {
        if (cell.cellType == Cell.CellType.Water)
            return 0;
        else if (cell.cellType == Cell.CellType.Field)
            return 1;
        else
            return 2;
    }

    private int SideSubMesh(Cell high, Cell low) {
        int h = CellSubMesh(high);
        int l = CellSubMesh(low);
        if (h==1 && l==0) return 3;
        else if (h==2 && l==1) return 4;
        else if (h==2 && l==0) return 5;
        else return 3;
    }

}
