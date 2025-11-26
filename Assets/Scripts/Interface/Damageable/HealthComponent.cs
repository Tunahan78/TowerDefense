using UnityEngine;

public class HealthComponent : MonoBehaviour , IDamageable , IThreatLevel
{
    [Header("Data")]
    [SerializeField] private EnemyDataSO enemyData;
    // Diğer hasar türleri için dirençler eklenebilir
    // [SerializeField, Range(0f, 1f)] private float fireResistance = 0f;

    private float currentHealth;
 
    private bool isAlive => currentHealth > 0;

    public bool IsAlive => throw new System.NotImplementedException();

    private void Start()
    {
        if (enemyData == null) 
        {
            Debug.LogError("EnemyData SO is missing!", this);
            enabled = false;
            return;
        }
        // Max Health değerini SO'dan okuyarak başlat
        currentHealth = enemyData.maxHealth;
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
            effectiveDamage *= (1f - enemyData.physicalResistance); 
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

    public float GetThreatScore()
    {
        if (enemyData == null) return 0f;

    // Tehdit Skoru: Max Can (Tehlike Potansiyeli) * SO'daki Tehdit Çarpanı
       // Bu, aynı mesafedeki düşmanlar için daha sağlam (yüksek can/zırh) olanı seçer.
       return enemyData.maxHealth * enemyData.threatMultiplier;
    } 

}
