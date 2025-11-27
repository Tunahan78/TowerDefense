using UnityEngine;


public class TowerAttacker : MonoBehaviour
{
    // Bağımlılıklar: Artık veriye değil, SO'ya ve diğer Komponentlere bağımlıyız.
    [Header("Core Dependencies")]
    [SerializeField] private TowerDataSO towerData; // Tek SO referansımız
    [SerializeField] private TowerTarget towerTarget;
    [SerializeField] private HeadRotator headRotator;
    [SerializeField] private Transform firePoint;

    [Header("VFX Dependencies")]
    [SerializeField] private ChargeVFXController chargeVFXController;
    [SerializeField] private BeamVFXController beamVFXController;
    [SerializeField] private LineRenderer lineRenderer;
    
    
    private float attackCooldown = 0f;
    private IAttackBehavior currentAttackBehavior;

    private void Start()
    {
        if (towerData == null)
        {
            Debug.LogError("Tower Data SO is missing on " + gameObject.name, this);
            enabled = false;
            return;
        }

        // 1. Statik Veriyi SO'dan Yükle
        towerTarget.SetRange(towerData.attackRange); // TargetingComponent menzili SO'dan alır
        attackCooldown = 1f / towerData.attackRate;

        // 2. Saldırı Davranışını Factory ile Enjekte Et (DIP/Strategy Pattern)
        // Factory, SO'daki behaviorType'a göre doğru sınıfı yaratır.
        currentAttackBehavior = TowerFactoryBehavior.CreateAttackBehavior(
            towerData, 
            firePoint, 
            lineRenderer, 
            beamVFXController // BeamVFXController, Beam davranışına enjekte edilir
        );
    }

    private void Update()
    {
        // 1. Cooldown Yönetimi
        attackCooldown -= Time.deltaTime;
        
        // 2. Hedefi TargetingComponent'ten al
        IUnitTarget target = towerTarget.GetCurrentTarget();

        // 3. Şarj Görsel Efektini Güncelle
        float maxCooldown = 1f / towerData.attackRate;
        if (chargeVFXController != null)
        { 
            chargeVFXController.UpdateCharge(attackCooldown, maxCooldown);
        }
        
        // 4. Atış Kontrolü
        if(attackCooldown <= 0f && target != null && headRotator.IsFaceingTarget())
        {
            ExecuteAttack(target);
            attackCooldown = maxCooldown;

            // 5. Atıştan Sonra Görsel Efekti Sıfırla
            if (chargeVFXController != null)
            {
                chargeVFXController.ResetCharge();
            }
        } 
    }

    private void ExecuteAttack(IUnitTarget target)
    {
        // 1. Hasar Bilgisini Oluştur (SO verisine göre)
        DamageInfo damageInfo = new DamageInfo 
        { 
            DamageAmount = towerData.baseDamage, 
            DamageType = towerData.damageType 
        };

        // 2. Saldırı Davranışını Tetikle (IAttackBehavior'a devret)
        currentAttackBehavior.Attack(target, damageInfo);

        // 3. Işın Görsel Efektini Başlat (Sadece Işın Kulelerinde anlamlıdır)
        // Bu, BeamAttackBehavior içinde de yapılabilir, ancak hızlı ışınlar için burada tutmak da yaygındır.
        if (beamVFXController != null)
        {
            beamVFXController.EnableBeamForDuration(0.05f); // Kısa bir süre görünür yap
        }
    }
}

