using UnityEngine;

[CreateAssetMenu(menuName="Cards/Units/Default")]
public class UnitCard : CardBase {
    [SerializeField] private GameObject model = null;
    [SerializeField] private int health = 1;
    [SerializeField] private int attack = 1;
    [SerializeField] private int movement = 5;
    [SerializeField] private UnitMoveMask movementMask;
    [SerializeField] private UnitMoveMask atqMask;
    [SerializeField] protected bool range;

    public GameObject Model => model;
    public int Health => health;
    public int Attack => attack;
    public int Movement => movement;
    public UnitMoveMask MovementMask => movementMask;
    public UnitMoveMask AtqMask => atqMask;
    public bool Range => range;

    protected override void CardEffect(Board board, Cell target, PlayerRole player, Player objPlayer)
    {
        board.AddUnit(this, target, player);
    }
}