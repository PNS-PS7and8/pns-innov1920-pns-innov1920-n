using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class BoardMesh : BoardBehaviour
{
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;
    private Mesh mesh;
    private Dictionary<int, int> cellCenterIndices;

    public override void OnResetBoard(BoardManager boardManager) {
        base.OnResetBoard(boardManager);
        Start();
    }

    private void Start()
    {
        mesh = new Mesh();
        mesh.MarkDynamic();

        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        cellCenterIndices = new Dictionary<int, int>();

        meshFilter = gameObject.GetComponent<MeshFilter>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshCollider = gameObject.GetComponent<MeshCollider>();

        GenerateMesh();

        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

    private void GenerateMesh() {
        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uv = new List<Vector2>();
        List<List<int>> triangles = new List<List<int>>();
        for (int i = 0; i < 4; i++) triangles.Add(new List<int>());

        List<List<Cell>> cellGroups = new List<List<Cell>>();
        
        foreach (var cell in board.Cells())
        {
            GenerateHexagonTop(cell, vertices, uv, triangles);
        }
        
        foreach (var cell in board.Cells()) {
            foreach(var neighbor in board.Neighbors(cell)) {
                if (cellCenterIndices.ContainsKey(board.CellIndex(neighbor.position.x, neighbor.position.y))) {
                    GenerateHexagonSide(cell, neighbor, vertices, uv, triangles);
                }
            }
        }
        mesh.subMeshCount = triangles.Count;
        mesh.vertices = vertices.ToArray();
        mesh.uv = uv.ToArray();
        for (int i = 0; i < triangles.Count; i++) mesh.SetTriangles(triangles[i], i);
        mesh.RecalculateNormals();
        mesh.MarkModified();
    }

    public void UpdateMesh(List<Cell> changedCells) {
        Vector3[] vertices = mesh.vertices;
        List<List<int>> triangles = new List<List<int>>();
        for (int i = 0; i < mesh.subMeshCount; i++)
        {
            triangles.Add(mesh.GetTriangles(i).ToList());
        }
        foreach (var cell in changedCells)
        {
            UpdateCell(cell, vertices, triangles);
        }
        mesh.vertices = vertices;
        for (int i = 0; i < mesh.subMeshCount; i++)
        {
            mesh.SetTriangles(triangles[i], i);
        }
        mesh.RecalculateNormals();
        mesh.MarkModified();
        meshCollider.sharedMesh = mesh;
    }

    private int[] GetCellVertIndices(Cell cell) {
        int index = board.CellIndex(cell.position.x, cell.position.y);
        
        int c = cellCenterIndices[index];

        return new int[] {
            c,
            c+1, c+2, c+3, c+4, c+5, c+6,
            c+7, c+8,
            c+9, c+10,
            c+11, c+12,
            c+13, c+14,
            c+15, c+16,
            c+17, c+18,
        };
    }

    private void UpdateCell(Cell cell, Vector3[] vertices, List<List<int>> triangles) {
        int[] verts = GetCellVertIndices(cell);
        for (int i = 0; i < verts.Length; i++)
        {
            vertices[verts[i]].y = cell.Height;
        }
        
        int oldSubMesh = triangles.FindIndex(sm => sm.Any(t => t == verts[0]));
        int firstTriangle = triangles[oldSubMesh].FindIndex(t => t == verts[0]);
        triangles[oldSubMesh].RemoveRange(firstTriangle, 18);
        
        int newSubMesh = CellSubMesh(cell);
        GenerateHexagonTopTriangles(verts[0], newSubMesh, triangles);
    }

    private void GenerateHexagonTop(Cell cell, List<Vector3> vertices, List<Vector2> uv, List<List<int>> triangles) {
        int nverts = vertices.Count;
        int subMesh = CellSubMesh(cell);

        Vector3 pos = board.CellToLocal(cell.position);

        cellCenterIndices[board.CellIndex(cell.position.x, cell.position.y)] = vertices.Count;

        vertices.Add(pos);
        vertices.Add(pos + new Vector3(-0.5f, 0, -Mathf.Sqrt(3f)/2f));
        vertices.Add(pos + new Vector3(0.5f, 0, -Mathf.Sqrt(3f)/2f));
        vertices.Add(pos + new Vector3(1f, 0, 0));
        vertices.Add(pos + new Vector3(0.5f, 0, Mathf.Sqrt(3f)/2f));
        vertices.Add(pos + new Vector3(-0.5f, 0, Mathf.Sqrt(3f)/2f));
        vertices.Add(pos + new Vector3(-1f, 0, 0));
        
        vertices.Add(pos + new Vector3(-0.5f, 0, -Mathf.Sqrt(3f)/2f));
        vertices.Add(pos + new Vector3(0.5f, 0, -Mathf.Sqrt(3f)/2f));

        vertices.Add(pos + new Vector3(0.5f, 0, -Mathf.Sqrt(3f)/2f));
        vertices.Add(pos + new Vector3(1f, 0, 0));

        vertices.Add(pos + new Vector3(1f, 0, 0));
        vertices.Add(pos + new Vector3(0.5f, 0, Mathf.Sqrt(3f)/2f));

        vertices.Add(pos + new Vector3(0.5f, 0, Mathf.Sqrt(3f)/2f));
        vertices.Add(pos + new Vector3(-0.5f, 0, Mathf.Sqrt(3f)/2f));

        vertices.Add(pos + new Vector3(-0.5f, 0, Mathf.Sqrt(3f)/2f));
        vertices.Add(pos + new Vector3(-1f, 0, 0));

        vertices.Add(pos + new Vector3(-1f, 0, 0));
        vertices.Add(pos + new Vector3(-0.5f, 0, -Mathf.Sqrt(3f)/2f));


        uv.Add(new Vector2(0.5f,    0.5f));
        uv.Add(new Vector2(0.25f,     0));
        uv.Add(new Vector2(0.75f,     0));
        uv.Add(new Vector2(1,     0.5f));
        uv.Add(new Vector2(0.75f,     1));
        uv.Add(new Vector2(0.25f,     1));
        uv.Add(new Vector2(0,     0.5f));

        uv.Add(new Vector2(0,     0));
        uv.Add(new Vector2(1,     0));

        uv.Add(new Vector2(0,     0));
        uv.Add(new Vector2(1,     0));

        uv.Add(new Vector2(0,     0));
        uv.Add(new Vector2(1,     0));
        
        uv.Add(new Vector2(0,     1));
        uv.Add(new Vector2(1,     1));
        
        uv.Add(new Vector2(0,     1));
        uv.Add(new Vector2(1,     1));

        uv.Add(new Vector2(0,     1));
        uv.Add(new Vector2(1,     1));

        GenerateHexagonTopTriangles(nverts, subMesh, triangles);

    }

    private void GenerateHexagonTopTriangles(int center, int subMesh, List<List<int>> triangles) {

        triangles[subMesh].Add(center); triangles[subMesh].Add(center+2); triangles[subMesh].Add(center+1);
        triangles[subMesh].Add(center); triangles[subMesh].Add(center+3); triangles[subMesh].Add(center+2);
        triangles[subMesh].Add(center); triangles[subMesh].Add(center+4); triangles[subMesh].Add(center+3);
        triangles[subMesh].Add(center); triangles[subMesh].Add(center+5); triangles[subMesh].Add(center+4);
        triangles[subMesh].Add(center); triangles[subMesh].Add(center+6); triangles[subMesh].Add(center+5);
        triangles[subMesh].Add(center); triangles[subMesh].Add(center+1); triangles[subMesh].Add(center+6);
    }

    private void GenerateHexagonSide(Cell cell, Cell neighbor, List<Vector3> vertices, List<Vector2> uv, List<List<int>> triangles) {
        int subMesh = SideSubMesh(cell, neighbor);
        
        Vector3 middle = (board.CellToLocal(cell.position) + board.CellToLocal(neighbor.position)) / 2f;
        Vector3 offset = board.CellToLocal(cell.position) - board.CellToLocal(neighbor.position);
        offset.y = 0;
        Vector3 right = Vector3.Cross(offset, Vector3.up).normalized * 0.5f;
        float height = cell.Height - neighbor.Height;

        Vector3 a = middle - right;
        Vector3 b = middle + right;

        int[] cellVerts = GetCellVertIndices(cell).Skip(7).ToArray();
        int[] neighborVerts = GetCellVertIndices(neighbor).Skip(7).ToArray();

        var vertsCell = vertices.GetRange(cellVerts[0], 12);
        var vertsNeighbor = vertices.GetRange(neighborVerts[0], 12);

        var la = vertsCell.OrderBy(v => (v-a).sqrMagnitude).ToArray();
        var lc = vertsNeighbor.OrderBy(v => (v-a).sqrMagnitude).ToArray();

        int ia = (vertsCell.IndexOf(la[0]))%12;
        int ib = (ia+11)%12;

        int ic = (ia+5)%12;
        int id = (ic+1)%12;

        int na = cellVerts[ia];
        int nb = cellVerts[ib];
        int nc = neighborVerts[ic];
        int nd = neighborVerts[id];

        triangles[subMesh].Add(nb); triangles[subMesh].Add(na); triangles[subMesh].Add(nc);
        triangles[subMesh].Add(nd); triangles[subMesh].Add(nb); triangles[subMesh].Add(nc);
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
        return 3;
    }

}
