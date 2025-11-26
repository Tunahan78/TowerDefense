using UnityEngine;

public static class TowerFactoryBehavior 
{
    public static IAttackBehavior CreateAttackBehavior(TowerDataSO data, 
        Transform firePoint, 
        LineRenderer lr,
        BeamVFXController vfx)
    {
        switch (data.behaviorType)
        {
            case AttackBehaviorType.Projectile:
                return new ArrowShotBehavior(firePoint, data.projectilePrefab);
            
            case AttackBehaviorType.Beam:
                return new BeamAttackBehavior(firePoint, lr, vfx, data.targetLayer);
            
            default:
                Debug.LogWarning("Bilinmeyen saldırı davranışı: " + data.behaviorType);
                return null;
        }
    }
}