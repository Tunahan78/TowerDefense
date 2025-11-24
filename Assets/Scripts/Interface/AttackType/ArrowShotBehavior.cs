using UnityEngine;

public class ArrowShotBehavior : IAttackBehavior
{
    private Transform firePoint;
    private GameObject projectilePrefab;

    public ArrowShotBehavior(Transform fp, GameObject prefab)
    {
        firePoint = fp;
        projectilePrefab = prefab;
    }

    public void Attack(IUnitTarget target, DamageInfo damage)
    {
       // Hedef geçerli değilse atış yapma
        if (target == null || target.GetTransform() == null) return;
        
        // 1. Mermiyi Oluşturma
        GameObject projectileGO = GameObject.Instantiate(
            projectilePrefab,
            firePoint.position,
            Quaternion.identity // Başlangıç rotasyonu önemli değil, Projectile script'i hedefi takip edecek
        );

        // 2. Mermi Görsel Efekti (Muzzle Flash): Buraya eklenebilir.
        // Örneğin: VfxManager.Play("MuzzleFlash", firePoint.position);

        // 3. Mermiyi Başlatma (Hasar ve Hedef Bilgisini Enjekte Etme)
        if (projectileGO.TryGetComponent<Projectile>(out Projectile projectile))
        {
            // Mermiyi hedef ve hasar bilgisi ile başlat
            projectile.Initialize(target.GetTransform(), damage);
        }
        else
        {
            Debug.LogError("Projectile prefab'da 'Projectile' script'i bulunamadı!");
        }
    
    }
}
