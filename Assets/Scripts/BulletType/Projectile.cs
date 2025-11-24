using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 20f; // Mermi hızı
    [SerializeField] private float lifeTime = 5f; // Merminin yok olma süresi

    private Transform targetTransform;
    private DamageInfo damageInfo;

    // Kule tarafından çağrılan başlatma metodu
    public void Initialize(Transform target, DamageInfo damage)
    {
        targetTransform = target;
        damageInfo = damage;
        
        // Mermiyi belirli bir süre sonra yok et (sonsuza gitmesini engeller)
        Destroy(gameObject, lifeTime); 
    }

    private void Update()
    {
        if(targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);

        if(transform.position == targetTransform.position)
        {
            ApplyDamage();
        }
    }

    private void ApplyDamage()
    {
        // 1. Hedefin IDamageable arayüzünü al
        if (targetTransform.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
           
            damageable.TakeDamage(damageInfo); 
            
            // Şimdilik sadece loglayalım:
            Debug.Log($"Hedefe {damageInfo.DamageAmount} {damageInfo.DamageType} hasarı verildi!");
        }

        // Darbe görsel efektini (VFX) oynat.
        // VfxManager.Play("Impact", transform.position);

        Destroy(gameObject);
    }
    
}
