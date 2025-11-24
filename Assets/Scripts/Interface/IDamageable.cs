
public interface IDamageable 
{
    void TakeDamage(DamageInfo damage);

    // Canlı olup olmadığını kontrol etmek için eklenebilir.
    bool IsAlive { get; }
}
