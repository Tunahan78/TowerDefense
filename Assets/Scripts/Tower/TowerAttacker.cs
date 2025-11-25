using System;
using UnityEngine;

public class TowerAttacker : MonoBehaviour
{
    [SerializeField] private TowerTarget towerTarget;
    [SerializeField] private Transform firePoint;

    [Header("Attack Settings")]
    [SerializeField] private float attackRate = 1f; // saniyede atış

    // Kulemizin hasar verisini Scriptable Object'ten almalıyız, ancak şimdilik manuel tanımlayalım.
    [Header("Tower Data (Placeholder)")]
    [SerializeField] private float baseDamage = 25f;
    [SerializeField] private DamageType damageType = DamageType.Physical;
     // [SerializeField] private GameObject arrowPrefab; // Arbalet Mermisi Prefab'ı
    [SerializeField] private BeamVFXController beamVFXController; // Işın Controlcüsü
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LayerMask layerMask;
    
    [Header("VFX Settings")]
    [SerializeField] private ChargeVFXController chargeVFXController; // Şarj VFX Kontrolcüsü

    


    private float attackCooldown = 0f;

    private IAttackBehavior currentAttackBehavior;

    private void Start()
    {
        // Örn: Başlangıçta Arbalet davranışını atıyoruz.
        // IAttackBehavior somut sınıfının başlatılması için gereken tüm veriyi sağlamalıyız.
        currentAttackBehavior = new BeamAttackBehavior(firePoint, lineRenderer, layerMask);

        attackCooldown = 1f / attackRate;
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
        IUnitTarget target = towerTarget.GetCurrentTarget();

        float maxCooldown = 1f / attackRate;
        if (chargeVFXController != null)
        {  
           chargeVFXController.UpdateCharge(attackCooldown, maxCooldown);
        }

        
        if(attackCooldown <= 0f && target != null)
        {
            ExecuteAttack(target);
            attackCooldown = 1f / attackRate;

            // 5. Atıştan Sonra Görsel Efekti Sıfırla (YENİ EKLENTİ)
            if (chargeVFXController != null)
            {
               chargeVFXController.ResetCharge();
            }
        } 
    }

    private void ExecuteAttack(IUnitTarget target)
    {

        DamageInfo damageInfo = new DamageInfo 
        { 
        DamageAmount = baseDamage, 
        DamageType = damageType 
        };

        currentAttackBehavior.Attack(target,damageInfo);

        if(beamVFXController != null)
        {
           beamVFXController.EnableBeamForDuration(0.05f); 
        }
    }

}

