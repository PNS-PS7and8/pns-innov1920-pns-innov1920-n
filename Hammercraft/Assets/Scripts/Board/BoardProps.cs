using System.Collections.Generic;
using UnityEngine;

public class BoardProps : BoardBehaviour {
    [SerializeField, Range(0, 1)] private float spawnRate;
    
    [SerializeField] private List<GameObject> waterProps;
    [SerializeField] private List<GameObject> fieldProps;
    [SerializeField] private List<GameObject> mountainProps;

    private void Start() {
        AddProps();
    }

    private void AddProps() {
        foreach(var cell in board.Cells()) {
            if (Random.value > spawnRate) {
                if (cell.cellType == Cell.CellType.Water) SpawnProp(waterProps, cell);
                if (cell.cellType == Cell.CellType.Field) SpawnProp(fieldProps, cell);
                if (cell.cellType == Cell.CellType.Mountain) SpawnProp(mountainProps, cell);
            }
        }
    }

    private void SpawnProp(List<GameObject> pool, Cell cell) {
        GameObject prop = pool[Random.Range(0, pool.Count)];
        Instantiate(prop,
            transform.TransformPoint(board.CellToLocal(cell.position)),
            Quaternion.Euler(transform.TransformDirection(new Vector3(0, Random.Range(0f, 360f), 0))),
            transform);
    }
}