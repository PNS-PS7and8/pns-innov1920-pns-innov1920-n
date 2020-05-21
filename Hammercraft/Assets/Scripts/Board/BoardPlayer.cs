using System.Collections.Generic;
using UnityEngine;

public class BoardPlayer : BoardBehaviour {
    [SerializeField] private GameObject gameCardPrefab;
    [SerializeField] private List<UnitCard> units = null;
    [SerializeField] private List<SpellCard> spells = null;
    [SerializeField] private Transform hand;

    private List<GameCard> gameCards;
    
    private Player player;
    
    protected override void Start() {
        base.Start();
        gameCards = new List<GameCard>();
        player = new Player(
            new Deck(units.ToArray(), spells.ToArray())
        );
        DrawUnit();
        DrawUnit();
        DrawSpell();
        DrawSpell();
    }

    private void Update() {
        RepositionCards();
    }

    private void DrawUnit() {
        UnitCard card = player.deck.DrawUnit();
        SpawnGameCard(card);
    }

    private void DrawSpell() {
        SpellCard card = player.deck.DrawSpell();
        SpawnGameCard(card);
    }

    private void SpawnGameCard(CardBase card) {
        GameObject go = Instantiate(gameCardPrefab, hand);
        GameCard gameCard = go.GetComponent<GameCard>();
        gameCards.Add(gameCard);
        gameCard.card = card;
        gameCard.player = this;
    }

    public float spacing;
    private void RepositionCards() {
        int i=0;
        foreach (var card in gameCards) {
            card.transform.localPosition = Vector3.right * i * (spacing);
            i++;
        }
    }

    public void RemoveCard(GameCard card) {
        gameCards.Remove(card);
    }
}