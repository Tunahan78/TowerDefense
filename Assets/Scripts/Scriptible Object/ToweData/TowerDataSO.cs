using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "TD/Tower Data")]
public class TowerDataSO : ScriptableObject
{
        [Header("Stats")]
        public float baseDamage;
        public DamageType damageType;
        public float attackRate;
        public float attackRange; 

        [Header("Behavior Setup")]
        // Hangi somut IAttackBehavior sınıfını yaratacağımızı Factory'ye söyler
        public AttackBehaviorType behaviorType; 
    
    // Mermili kuleler için gereken prefab
        [Header("Projectile/VFX Data")]
        public GameObject projectilePrefab; 

        // Işınlı kuleler için gereken LayerMask (EnemyLayer'ı)
        public LayerMask targetLayer;
}

