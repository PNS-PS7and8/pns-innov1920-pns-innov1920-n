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
    [SerializeField] private int movement;
    [SerializeField] private UnitMoveMask movementMask;
    [SerializeField] private PlayerRole player;
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
    public int Cost => card.Cost;
    public bool Dead => dead;
    public int Health => health;
    public int Attack => attack;
    public int Movement => movement;
    public UnitMoveMask MovementMask => movementMask;
    public PlayerRole Player => player;
    public int RangeAtq => rangeAtq;

    private BoardUnit boardUnit;

    /* This should never be used directly when using networking */ 
    public Unit(UnitCard card, Vector2Int position, int id, PlayerRole owner) {
        this.cardResourcePath = card.ResourcePath;
        this.health = card.Health;
        this.attack = card.Attack;
        this.movement = card.Movement;
        this.position = position;
        this.dead = false;
        this.player = owner;
        this.id = id;
        this.rangeAtq = (card.Range) ? 6 : 2;
        this.movementMask = card.MovementMask;
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