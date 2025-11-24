using UnityEngine;

public interface IAttackBehavior
{
    void Attack(IUnitTarget target, DamageInfo damage);
}
