using UnityEngine;
using System.Collections.Generic;
public interface ITargetingStrategy
{
    // Kule pozisyonunu ve menzil içindeki tüm potansiyel hedefleri alır.
    IUnitTarget SelectTarget(Vector3 towerPosition, List<IUnitTarget> potentialTargets, IUnitTarget currentLockedTarget);
}

