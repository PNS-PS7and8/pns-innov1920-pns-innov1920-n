using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Unit : ITakeDamage, IDealDamage
{
    private UnitCard card;
    private int health;
    private int attack;
    private int deplacement;
    private Cell cell;

    public UnitCard Card => card;
    public int Health => health;
    public int Attack => attack;
    public int Deplacement => deplacement;
    public Cell Cell { get => cell; set {
        cell.unit = null;
        cell = value;
        cell.unit = this;
    }}

    private BoardUnit boardUnit;

    public Unit(UnitCard card, Cell cell) {
        this.card = card;
        this.health = card.Health;
        this.attack = card.Attack;
        this.deplacement = card.Deplacement;
        this.cell = cell;
        this.cell.unit = this;
        
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
        cell.cellState = Cell.CellState.Free;
        cell.unit = null;
    }
}