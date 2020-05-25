using UnityEngine;

[CreateAssetMenu]
public class UnitCard : CardBase {
    [SerializeField] private GameObject model = null;
    [SerializeField] private int health = 1;
    [SerializeField] private int attack = 1;
    [SerializeField] private int deplacement = 5;

    public GameObject Model => model;
    public int Health => health;
    public int Attack => attack;
    public int Deplacement => deplacement;

    protected override void CardEffect(Cell target)
    {
        Unit unit = new Unit(this, target);
    }
}