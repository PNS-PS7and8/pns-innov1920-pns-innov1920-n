using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Unit : ITakeDamage, IDealDamage
{
    private UnitCard card;
    private int health;
    private int attack;

    public int Health => health;
    public int Attack => attack;
    public UnitCard Card => card;

    private BoardUnit boardUnit;
    private Vector2 coord;

    public Unit(UnitCard card, Vector2 coord) {
        this.card = card;
        this.health = card.Health;
        this.attack = card.Attack;
        this.coord = coord;
        GameObject gameObject = Object.Instantiate(card.Model);
        this.boardUnit = gameObject.GetComponent<BoardUnit>();
        boardUnit.unit = this;
        boardUnit.transform.position = GameObject.Find("hexMap").transform.InverseTransformPoint(new Vector3(coord.x, 0, coord.y))+Vector3.up*2;
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

    public void Die() {
        GameObject.Destroy(boardUnit.gameObject);
    }
}