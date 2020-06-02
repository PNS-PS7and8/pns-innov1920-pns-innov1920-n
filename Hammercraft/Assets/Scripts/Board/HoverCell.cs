using System.Collections.Generic;
using UnityEngine;

public class HoverCell : BoardBehaviour
{
    private readonly Color transparent = new Color(0,0,0,0); 
    
    [SerializeField] private Sprite sprite;
    [SerializeField] private float offset = 0.01f;

    private Dictionary<Vector2Int, SpriteRenderer> instances;
    private Dictionary<Vector2Int, List<Color>> colors;


    private void Start()
    {
        instances = new Dictionary<Vector2Int, SpriteRenderer>();
        colors = new Dictionary<Vector2Int, List<Color>>();

        foreach(Cell cell in board.Cells()) {
            GameObject go = new GameObject(string.Format("Cell ({0}, {1})", cell.position.x, cell.position.y), typeof(SpriteRenderer));
            
            go.transform.parent = transform;
            go.transform.localPosition = board.LocalPosition(cell) + Vector3.up * offset;
            go.transform.localRotation = Quaternion.Euler(90, 0, 0);
            go.transform.localScale = Vector3.one;
            
            instances[cell.position] = go.GetComponent<SpriteRenderer>();
            instances[cell.position].sprite = sprite;
            instances[cell.position].color = transparent;
            instances[cell.position].enabled = false;

            colors[cell.position] = new List<Color>();
        }
    }

    public void ShowCells(Color color, params Cell[] cells) {
        ShowCells(color, new List<Cell>(cells));
    }

    public void ShowCells(Color color, IEnumerable<Cell> cells) {
        foreach (var cell in cells) {
            if (instances.ContainsKey(cell.position) && !colors[cell.position].Contains(color)) {
                colors[cell.position].Add(color);
                instances[cell.position].color = color;
                instances[cell.position].enabled = true;
            }
        }
    }

    public void HideCells(Color color) {
        foreach (var cell in instances.Keys) {
            colors[cell].Remove(color);
            if (colors[cell].Count > 0)
                instances[cell].color = colors[cell][colors[cell].Count - 1];
            else {
                instances[cell].color = transparent;
                instances[cell].enabled = false;
            }

        }
    }
}
