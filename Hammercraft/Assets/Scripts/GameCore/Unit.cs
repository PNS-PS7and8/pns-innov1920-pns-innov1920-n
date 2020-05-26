using ExitGames.Client.Photon;
using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

[System.Serializable]
public class Unit : ITakeDamage, IDealDamage
{
    private UnitCard card;
    [SerializeField] private int health;
    [SerializeField] private int attack;
    [SerializeField] private int deplacement;
    [SerializeField] private int player;
    public Vector2Int position;

    public UnitCard Card => card;
    public int Health => health;
    public int Attack => attack;
    public int Deplacement => deplacement;
    public int Player => player;

    private BoardUnit boardUnit;

    public Unit(UnitCard card, Vector2Int position) {
        this.card = card;
        this.health = card.Health;
        this.attack = card.Attack;
        this.deplacement = card.Deplacement;
        this.position = position;
        
        GameObject gameObject = Object.Instantiate(card.Model);
        this.boardUnit = gameObject.GetComponent<BoardUnit>();
        boardUnit.unit = this;
    }

    public void Move(Cell cell) {

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
    }
}