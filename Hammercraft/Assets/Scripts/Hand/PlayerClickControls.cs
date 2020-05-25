using System.Collections.Generic;
using UnityEngine;

public class PlayerClickControls : MonoBehaviour {
    [SerializeField] private BoardClicker boardClicker = null;
    private Cell selectedCell = null;
    private GameCard selectedCard = null;
    private List<GameCard> gameCards;
    
    [SerializeField] private Transform hoverEffect = null;

    private void Start() {
        gameCards = new List<GameCard>();
    }

    private void Hover() {
        if (boardClicker.HoverCell(out var hover)) {
            hoverEffect.gameObject.SetActive(true);
            hoverEffect.position = boardClicker.boardManager.transform.TransformPoint(boardClicker.board.CellToLocal(hover.position));
        } else {
            hoverEffect.gameObject.SetActive(false);
        }
    }

    private void Select() {
        if (boardClicker.ClickCell(out var cell)) {
            selectedCell = cell;
        } else {
            if (Input.GetMouseButton(0)) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitinfo)) {
                    if (hitinfo.transform.TryGetComponent<GameCard>(out var gameCard)) {
                        if (selectedCard != null){
                            selectedCard.transform.localScale += new Vector3(-2,-2,-2);
                        }
                        selectedCard = gameCard;
                        gameCard.transform.localScale += new Vector3(2,2,2);
                    }
                }
            }
        }
    }
    
    int radius = 2;
    private void Update() {
        Hover();
        Select();

        if (Input.mouseScrollDelta.y > 0) radius+=2;
        if (Input.mouseScrollDelta.y < 0) radius = Mathf.Max(radius-2, 0);
        if (boardClicker.HoverCell(out var c)) {
            List<Vector3> verts = new List<Vector3>();
            foreach (var cell in c.Disc(radius))
            {
                verts.Add(cell.LocalPosition);
            }
            for (int i = 0; i < verts.Count-1; i++)
            {
                Debug.DrawLine(verts[i], verts[(i+1)%verts.Count], Color.white);
            }
        }

        if (selectedCell != null && selectedCard != null) {
            selectedCard.Use(selectedCell);
            selectedCard = null;
            selectedCell = null;
        }
    }
}