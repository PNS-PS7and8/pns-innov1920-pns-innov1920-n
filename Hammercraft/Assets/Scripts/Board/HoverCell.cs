using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HoverCell : BoardBehaviour
{
    private readonly Color transparent = new Color(0,0,0,0); 
    
    [SerializeField] private Sprite sprite;
    [SerializeField] private float offset = 0.01f;

    private Dictionary<Vector2Int, SpriteRenderer> instances;
    private Dictionary<Color, List<Vector2Int>> colorToPos;


    private void Start()
    {
        instances = new Dictionary<Vector2Int, SpriteRenderer>();
        colorToPos = new Dictionary<Color, List<Vector2Int>>();

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
        }
    }

    public void ShowCells(Color color, params Cell[] cells) {
        ShowCells(color, new List<Cell>(cells));
    }

    public void ShowCells(Color color, IEnumerable<Cell> cells) {
        if (!colorToPos.ContainsKey(color)) {
            colorToPos.Add(color, cells.Select(c => c.position).ToList());
        }
        foreach (var cell in cells) {
            if (instances.ContainsKey(cell.position)) {
                instances[cell.position].color = color;
                instances[cell.position].enabled = true;
            }
        }
    }

    public void HideCells(Color color) {
        if (colorToPos.ContainsKey(color)) {
            foreach (var cell in colorToPos[color]) {
                var colors = colorToPos.Where(kv => kv.Key != color && kv.Value.Contains(cell)).Select(kv => kv.Key).ToArray();
                if (colors.Length > 0) {
                    instances[cell].color = colors[0];
                } else {
                    instances[cell].enabled = false;
                }
            }
            colorToPos.Remove(color);
        }
    }
}
