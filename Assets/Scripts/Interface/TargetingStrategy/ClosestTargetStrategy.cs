using System.Collections.Generic;
using UnityEngine;

public class ClosestTargetStrategy : ITargetingStrategy
{
    public IUnitTarget SelectTarget(Vector3 towerPosition, List<IUnitTarget> potentialTargets)
    {
        // Liste boşsa null döner
        if(potentialTargets == null || potentialTargets.Count == 0)
        {
            return null;
        }

        IUnitTarget closestTarget = null;
        float shortestDistanceSqr = float.MaxValue; // Başlangıçta sonsuz büyük bir mesafe

        // 3. Her bir potansiyel hedefi döngü ile kontrol et.
        foreach (IUnitTarget target in potentialTargets)
        {
            // Hedefin Transform'unu alıyoruz
            Transform targetTransform = target.GetTransform();
            
            if (targetTransform == null) continue;

            
            
            // sadece karşılaştırma yaptığımız için karekök almamıza gerek yok.
            float currentDistanceSqr = (targetTransform.position - towerPosition).sqrMagnitude;

            // 4. Eğer bu mesafe, bulduğumuz en kısa mesafeden kısaysa, hedefi güncelle.
            if (currentDistanceSqr < shortestDistanceSqr)
            {
                shortestDistanceSqr = currentDistanceSqr;
                closestTarget = target;
            }
        }

        
        return closestTarget;
    }
}
