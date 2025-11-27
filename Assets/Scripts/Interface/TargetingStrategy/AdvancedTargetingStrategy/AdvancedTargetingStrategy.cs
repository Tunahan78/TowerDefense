using System.Collections.Generic;
using UnityEngine;

public class AdvancedTargetingStrategy : ITargetingStrategy
{
    // Yol ilerleme skoru tehtidin önüne geömesi için yüksek değer veriyoruz
    private const float STABILITY_BONUS = 5f;
    private const float PRIORITY_MULTIPLIER = 1000f;
    public IUnitTarget SelectTarget(Vector3 towerPosition, List<IUnitTarget> potentialTargets , IUnitTarget currentLockedTarget)
    {
        if(potentialTargets == null || potentialTargets.Count == 0)
        {
            return null;
        }

        IUnitTarget bestTarget = null;
        float highestScore = float.NegativeInfinity;

        foreach(IUnitTarget target in potentialTargets)
        {
            GameObject targetGO = target.GetGameObject();
    
            // 1. Düşmanın GO'sunda PathProgress'i ara
            if (!targetGO.TryGetComponent(out IPathProgress pathProgress)) continue;
    
            // 2. Düşmanın GO'sunda ThreatLevel'i ara
            if (!targetGO.TryGetComponent(out IThreatLevel threatLevel)) continue;

            float pathScore = pathProgress.GetPathProgressScore();
            float threatScore = threatLevel.GetThreatScore();

            float totalScore = (pathScore * PRIORITY_MULTIPLIER) + threatScore;
            if(target == currentLockedTarget)
            {
                totalScore += STABILITY_BONUS;
            }

            if (totalScore > highestScore)
            {
                highestScore = totalScore;
                bestTarget = target;
            }
        }
        
         return bestTarget;
    }
}
