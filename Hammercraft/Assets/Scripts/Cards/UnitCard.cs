using UnityEngine;

[CreateAssetMenu(menuName="Cards/Units/Default")]
public class UnitCard : CardBase {
    [SerializeField] private GameObject model = null;
    [SerializeField] public int health = 1;
    [SerializeField] public int attack = 1;
    [SerializeField] private int movement = 5;
    [SerializeField] private UnitMoveMask movementMask = UnitMoveMask.NeutralCells;
    [SerializeField] private UnitMoveMask atqMask = UnitMoveMask.NeutralCells;
    [SerializeField] protected bool range = false;
    [SerializeField] protected float modelScale = 1f;

    public GameObject Model => model;
    public int Health => health;
    public int Attack => attack;
    public int Movement => movement;
    public UnitMoveMask MovementMask => movementMask;
    public UnitMoveMask AtqMask => atqMask;
    public bool Range => range;
    public float ModelScale => modelScale;

    protected override bool CardEffect(Board board, Cell target, PlayerRole player, Player objPlayer)
    {
        board.AddUnit(this, target, player);
        return true;
    }
}