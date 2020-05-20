using UnityEngine;

[CreateAssetMenu]
public class UnitCard : CardBase {
    [SerializeField] private GameObject model = null;
    [SerializeField] private int health = 1;
    [SerializeField] private int attack = 1;

    public GameObject Model => model;
    public int Health => health;
    public int Attack => attack;

}