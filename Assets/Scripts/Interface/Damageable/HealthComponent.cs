using UnityEngine;

public class HealthComponent : MonoBehaviour , IDamageable
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth;
    private float currentHealth;
    [Header("Resistance Settings (Yüzde olarak)")]
    // Kulelerin Fiziksel hasarını ne kadar keseceğimizi belirler
    [SerializeField, Range(0f, 1f)] private float physicalResistance = 0f; 
    // Diğer hasar türleri için dirençler eklenebilir
    // [SerializeField, Range(0f, 1f)] private float fireResistance = 0f;
 
    private bool isAlive => currentHealth > 0;

    public bool IsAlive => throw new System.NotImplementedException();

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(DamageInfo damage)
    {
        if(!isAlive) return;
        // 1. Hasar Direnci Hesaplaması
        float effectiveDamage = damage.DamageAmount;

        // Hasar türüne göre direnci uygula
        if (damage.DamageType == DamageType.Physical)
        {
            // EffectiveDamage = BaseDamage * (1 - Direnç Yüzdesi)
            effectiveDamage *= (1f - physicalResistance); 
        }
        // else if (damage.DamageType == DamageType.Fire) { ... }

        // 2. Canı Düşürme
        currentHealth -= effectiveDamage;
        
        Debug.Log($"{gameObject.name} hasar aldı: {effectiveDamage} (Kalan Can: {currentHealth})");

        // 3. Ölüm Kontrolü
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Ölüm mantığı:
        // 1. Düşman tipine göre oyuncuya para/kaynak ver.
        // 2. Ölüm animasyonunu/efektini oynat.
        // 3. GameObject'i sahneden yok et.

        Debug.Log($"{gameObject.name} yok edildi!");
        Destroy(gameObject);
        
        // Bu noktada WaveManager'a düşmanın öldüğünü bildirmeliyiz.
    }

    

}
