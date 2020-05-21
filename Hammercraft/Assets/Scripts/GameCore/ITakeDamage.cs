public interface ITakeDamage {
    void TakeDamage(int amount);
    void Die();
    int Health { get; }
}