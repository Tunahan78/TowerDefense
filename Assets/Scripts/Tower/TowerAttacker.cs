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
    [SerializeField] private GameObject arrowPrefab; // Arbalet Mermisi Prefab'ı


    private float attackCooldown = 0f;

    private IAttackBehavior currentAttackBehavior;

    private void Start()
    {
        // Örn: Başlangıçta Arbalet davranışını atıyoruz.
        // IAttackBehavior somut sınıfının başlatılması için gereken tüm veriyi sağlamalıyız.
        currentAttackBehavior = new ArrowShotBehavior(firePoint,arrowPrefab);

        attackCooldown = 1f / attackRate;
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
        IUnitTarget target = towerTarget.GetCurrentTarget();
        
        if(attackCooldown <= 0f && target != null)
        {
            ExecuteAttack(target);
            attackCooldown = 1f / attackRate;
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
    }

}

