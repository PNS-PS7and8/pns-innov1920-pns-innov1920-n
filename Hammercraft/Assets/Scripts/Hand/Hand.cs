using System.Collections.Generic;
using UnityEngine;

public class Hand : BoardBehaviour {
    [SerializeField] private float spacing = 0.1f;
    [SerializeField] private GameCard cardPrefab;
    [SerializeField] private PlayerRole role;
    private GameCard[] cards;

    private void Start() {
        cards = new GameCard[10];
        for (int i = 0; i < cards.Length; i++)
        {
            GameObject cardObject = Instantiate(cardPrefab.gameObject, transform);
            cards[i] = cardObject.GetComponent<GameCard>();
            cardObject.SetActive(false);
        }
    }

    private void Update() {
        Player player = manager.LocalPlayer;
        for (int i = 0; i < cards.Length; i++)
        {
            if (i < player.Hand.Count) {
                cards[i].card = player.Hand[i];
                if (cards[i] != null) {
                    cards[i].gameObject.SetActive(true);
                    Vector3 pos = cards[i].transform.localPosition;
                    pos.x = ((float) i - ((float) player.Hand.Count / 2f)) * spacing;
                    cards[i].transform.localPosition = pos;
                }
            } else {
                cards[i].card = null;
                cards[i].gameObject.SetActive(false);
            }
        }
    }
}