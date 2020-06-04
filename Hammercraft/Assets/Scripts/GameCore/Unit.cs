using ExitGames.Client.Photon;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Events;
using System;

[System.Serializable]
public class Unit : ITakeDamage, IDealDamage
{
    [SerializeField] private string cardResourcePath;
    [SerializeField] private int id;
    [SerializeField] private bool dead;
    [SerializeField] private int health;
    [SerializeField] public int attack;
    [SerializeField] public int movement;
    [SerializeField] public int cost;
    [SerializeField] private UnitMoveMask movementMask;
    [SerializeField] private UnitMoveMask atqMask;
    [SerializeField] private PlayerRole player;
    [SerializeField] public int rangeAtq;
    private UnitCard card;
    
    public Vector2Int position;

    
    public UnitCard Card { get {
        if (!card) {
            card = Resources.Load<UnitCard>(cardResourcePath);
        }
        return card;
    }}

    public int Id => id;
    public int Cost => cost;
    public bool Dead => dead;
    public int Health => health;
    public int Attack => attack;
    public int Movement => movement;
    public UnitMoveMask MovementMask => movementMask;
    public UnitMoveMask AtqMask => atqMask;
    public PlayerRole Player => player;
    public int RangeAtq => rangeAtq;

    private BoardUnit boardUnit;

    /* This should never be used directly when using networking */ 
    public Unit(UnitCard card, Vector2Int position, int id, PlayerRole owner) {
        this.cardResourcePath = card.ResourcePath;
        this.health = card.Health;
        this.attack = card.Attack;
        this.movement = card.Movement;
        this.cost = card.Cost;
        this.position = position;
        this.dead = false;
        this.player = owner;
        this.id = id;
        this.rangeAtq = (card.Range) ? 6 : 2;
        this.movementMask = card.MovementMask;
        this.atqMask = card.AtqMask;
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