using System.Collections.Generic;
using UnityEngine;

public class Hand : BoardBehaviour {
    [SerializeField] private float spacing = 0.1f;
    [SerializeField] private GameCard cardPrefab = null;
  //  [SerializeField] private PlayerRole role = PlayerRole.PlayerOne;
    public Player player { get; private set; }
    public GameCard[] Cards { get; private set; }

    public float Spacing { get { return spacing; } }

    private void Start() {
        Cards = new GameCard[10];
        for (int i = 0; i < Cards.Length; i++)
        {
            GameObject cardObject = Instantiate(cardPrefab.gameObject, transform);
            Cards[i] = cardObject.GetComponent<GameCard>();
            cardObject.SetActive(false);
        }
    }

    private void Update() {
        UpdateHand();
    }

    public void UpdateHand() {
        player = manager.LocalPlayer;
        for (int i = 0; i < Cards.Length; i++)
        {
            if (i < player.Hand.Count) {
                Cards[i].card = player.Hand[i];
                if (Cards[i] != null) {
                    Cards[i].gameObject.SetActive(true);
                    Vector3 pos = Cards[i].transform.localPosition;
                    pos.x = ((float) i - ((float) player.Hand.Count / 2f)) * spacing;
                    Cards[i].transform.localPosition = pos;
                }
            } else {
                Cards[i].card = null;
                Cards[i].gameObject.SetActive(false);
            }
        }
    }
}