using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HoverCell : BoardBehaviour
{
    private readonly Color transparent = new Color(0,0,0,0); 
    
    [SerializeField] private Sprite sprite = null;
    [SerializeField] private float offset = 0.01f;

    private Dictionary<Vector2Int, SpriteRenderer> instances;
    private Dictionary<Color, List<Vector2Int>> colorToPos;
    private List<Color> stack;


    private void Start()
    {
        instances = new Dictionary<Vector2Int, SpriteRenderer>();
        colorToPos = new Dictionary<Color, List<Vector2Int>>();
        stack = new List<Color>();

        foreach(Cell cell in board.Cells()) {
            GameObject go = new GameObject(string.Format("Cell ({0}, {1})", cell.position.x, cell.position.y), typeof(SpriteRenderer));
            
            go.transform.parent = transform;
            go.transform.localPosition = board.LocalPosition(cell) + Vector3.up * offset;
            go.transform.localRotation = Quaternion.Euler(90, 0, 0);
            go.transform.localScale = Vector3.one;
            
            instances[cell.position] = go.GetComponent<SpriteRenderer>();
            instances[cell.position].sprite = sprite;
            instances[cell.position].enabled = false;
        }
    }

    private void Update() {
        foreach (var kv in colorToPos)
        {
            foreach (var pos in kv.Value)
            {
                instances[pos].transform.localPosition = board.CellToLocal(pos) + Vector3.up * offset;
                instances[pos].color = kv.Key;
                instances[pos].enabled = true;
            }
        }
    }

    public void ShowCells(Color color, params Cell[] cells) {
        ShowCells(color, new List<Cell>(cells));
    }

    public void ShowCells(Color color, IEnumerable<Cell> cells) {
        if (!stack.Contains(color)) stack.Add(color);
        if (!colorToPos.ContainsKey(color)) {
            colorToPos.Add(color, new List<Vector2Int>());
        }
        foreach (var cell in cells) {
            if (instances.ContainsKey(cell.position)) {
                instances[cell.position].color = color;
                instances[cell.position].enabled = true;
                if (!colorToPos[color].Contains(cell.position))
                    colorToPos[color].Add(cell.position);
            }
        }
    }

    public void HideCells(Color color) {
        if (colorToPos.ContainsKey(color)) {
            foreach (var pos in colorToPos[color])
            {
                instances[pos].enabled = false;
            }
            colorToPos.Remove(color);
        }
    }
}
