using UnityEngine;

public class BeamAttackBehavior : IAttackBehavior
{
    private Transform firePoint;
    private LineRenderer lineRenderer;
    private LayerMask enemyLayer; // Raycast için düşmanların layer'ı
    private BeamVFXController beamVFXController;

    // Constructor'a ekleme: EnemyLayer'ı da alalım.
    public BeamAttackBehavior(Transform fp, LineRenderer lr, BeamVFXController vfx , LayerMask el ) 
    {
        firePoint = fp;
        lineRenderer = lr;
        beamVFXController = vfx;
        enemyLayer = el; // Enemy layer'ını Inspector'dan atayacağız
        lineRenderer.enabled = false;
    }

    public void Attack(IUnitTarget target, DamageInfo damage)
    {
        if (target == null || target.GetTransform() == null) return;
        
        // Görsel hedef yükseltilmiş pivot
        Transform targetTransform = target.GetTransform();
        // mekanik hedef kök pivot
        GameObject rootGameObject = target.GetGameObject();

        // Hasarı anında uygula
        if (rootGameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
        
         damageable.TakeDamage(damage); 
        } 
        else
        {
         Debug.LogError($"IDamageable arayüzü {rootGameObject.name} üzerinde bulunamadı!");
        }


        // Raycast ile çarpan noktayı bul burada yükselilmiş pivota vurmasını sağlıyoruz 
        DrawBeamWithRaycast(targetTransform); 
    }

    private void DrawBeamWithRaycast(Transform targetTransform)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePoint.position);

        Vector3 endPoint = targetTransform.position; // Varsayılan olarak merkeze
        RaycastHit hit;
        
        // Atış noktasından hedefe doğru bir ışın gönder
        Vector3 direction = (targetTransform.position - firePoint.position).normalized;
        float distance = Vector3.Distance(firePoint.position, targetTransform.position);

        if (Physics.Raycast(firePoint.position, direction, out hit, distance, enemyLayer))
        {
            // Eğer ışın bir düşmana çarparsa, bitiş noktası çarpma noktası olsun
            endPoint = hit.point;
            
            // Buraya bir "Hit VFX" (çarpma görsel efekti) eklenebilir.
            // Örn: VfxManager.Play("LazerHit", hit.point);
        }
        else
        {
            // Eğer düşmanın tam çarpma noktasını bulamazsa (bazen olabilir) 
            // veya düşmanın collider'ı küçükse, yine de hedef pozisyonunu kullan.
            endPoint = targetTransform.position;
        }

        lineRenderer.SetPosition(1, endPoint);
    }
}