using ExitGames.Client.Photon;
using UnityEngine;
using Photon.Pun;

[System.Serializable]
public class Unit : ITakeDamage, IDealDamage
{
    [SerializeField] private string cardResourcePath;
    [SerializeField] private int id;
    [SerializeField] private bool dead;
    [SerializeField] private int health;
    [SerializeField] private int attack;
    [SerializeField] private int deplacement;
    [SerializeField] private int player;
    [SerializeField] private int rangeAtq;
    private UnitCard card;
    
    public Vector2Int position;

    
    public UnitCard Card { get {
        if (!card) {
            card = Resources.Load<UnitCard>(cardResourcePath);
        }
        return card;
    }}

    public int Id => id;
    public bool Dead => dead;
    public int Health => health;
    public int Attack => attack;
    public int Deplacement => deplacement;
    public int Player => player;
    public int RangeAtq => rangeAtq;

    private BoardUnit boardUnit;

    /* This should never be used when using networking */ 
    public Unit(UnitCard card, Vector2Int position, int id) {
        this.card = card;
        this.health = Card.Health;
        this.attack = Card.Attack;
        this.deplacement = Card.Deplacement;
        this.position = position;
        this.dead = false;
        this.id = id;
        this.player = PhotonNetwork.LocalPlayer.ActorNumber;
        this.rangeAtq = (card.Range) ? 6 : 2;
    }

    public Unit(string cardResourcePath, Vector2Int position, int id) {
        this.cardResourcePath = cardResourcePath;
        this.card = Resources.Load<UnitCard>(cardResourcePath);
        this.health = card.Health;
        this.attack = card.Attack;
        this.deplacement = card.Deplacement;
        this.position = position;
        this.dead = false;
        this.player = PhotonNetwork.LocalPlayer.ActorNumber-1;
        this.id = id;
        this.rangeAtq = (card.Range) ? 6 : 2;
    }

    public void TakeDamage(int amount) {
        health -= amount;
        if (health <= 0) {
            Die();
        }
    }

    public void DealDamage(ITakeDamage target) {
        target.TakeDamage(attack);
    }

    private void Die() {
        dead = true;
    }
}