using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCard : MonoBehaviour
{
    private GameCard _selectedCard = null;
    public GameCard SelectedCard { get{return _selectedCard;} }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out RaycastHit hitinfo) && hitinfo.transform.TryGetComponent<GameCard>(out var gameCard)) {
            if (_selectedCard == null) {
                _selectedCard = gameCard;
                gameCard.transform.localScale = new Vector3(5,5,5);
            } else if (_selectedCard == gameCard) {
                _selectedCard = null;
                gameCard.transform.localScale = new Vector3(3,3,3);
            } else {
                _selectedCard.transform.localScale = new Vector3(3,3,3);
                _selectedCard = gameCard;
                gameCard.transform.localScale = new Vector3(5,5,5);
            }
        }
    }
}
