using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Unit : ITakeDamage, IDealDamage
{
    private UnitCard card;
    private int health;
    private int attack;
    private Cell cell;

    public int Health => health;
    public int Attack => attack;
    public UnitCard Card => card;
    public Cell Cell => cell;

    private BoardUnit boardUnit;

    public Unit(UnitCard card, Cell cell) {
        this.card = card;
        this.health = card.Health;
        this.attack = card.Attack;
        this.cell = cell;
        this.cell.unit = this;
        
        GameObject gameObject = Object.Instantiate(card.Model);
        this.boardUnit = gameObject.GetComponent<BoardUnit>();
        boardUnit.unit = this;
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
        cell.unit = null;
    }
}