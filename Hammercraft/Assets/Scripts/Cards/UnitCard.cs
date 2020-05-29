using UnityEngine;

[CreateAssetMenu]
public class UnitCard : CardBase {
    [SerializeField] private GameObject model = null;
    [SerializeField] private int health = 1;
    [SerializeField] private int attack = 1;
    [SerializeField] private int deplacement = 5;
    [SerializeField] protected bool range;

    public GameObject Model => model;
    public int Health => health;
    public int Attack => attack;
    public int Deplacement => deplacement;
    public bool Range => range;

    protected override void CardEffect(Board board, Cell target, PlayerRole player)
    {
        board.AddUnit(resourcePath, target, player);
    }
}