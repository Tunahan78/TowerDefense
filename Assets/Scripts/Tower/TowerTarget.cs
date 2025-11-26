using UnityEngine;
using System.Collections.Generic;

public class TowerTarget : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private HeadRotator headRotator;

    private IUnitTarget currenttarget;

    private ITargetingStrategy currentStrategy;

    private void Start()
    {
        currentStrategy = new AdvancedTargetingStrategy();
    }

    private void Update()
    {
        // 1. Menzil Taraması
        List<IUnitTarget> targetsInRange = FindTargetsInRange();
        
        // 2. Stratejiyi Uygulama
        IUnitTarget target = currentStrategy.SelectTarget(transform.position, targetsInRange,currenttarget);
        currenttarget = target;

        // 3. HeadRotator'e Bildirme
        if (target != null)
        {
            currenttarget =target;
            headRotator.SetTarget(target.GetTransform());
        }
        else
        {
            headRotator.SetTarget(null);
        }
    }

    private List<IUnitTarget> FindTargetsInRange()
    {
        // Unity'nin fizik sistemini kullanarak menzil içindeki düşmanları bulur
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);
        List<IUnitTarget> targets = new List<IUnitTarget>();

        foreach (Collider col in colliders)
        {
            // Düşmanların IUnitTarget arayüzünü uyguladığını varsayıyoruz.
            if (col.TryGetComponent<IUnitTarget>(out IUnitTarget target))
            {
                targets.Add(target);
            }
        }
        return targets;
    }

    private void OnDrawGizmos()
    {
        // Kule seçili değilse bir şey yapma
        // if (!enabled) return;
        
        // 1. Gizmos rengini belirleme (Örn: Sarı)
        Gizmos.color = Color.yellow; 
        
        // 2. Kule pozisyonundan (Transform.position) attackRange yarıçapında bir tel küre çizdirme.
        // Bu, kule menzilini gösterir.
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public IUnitTarget GetCurrentTarget()
    {
        return currenttarget;
    }

    public void SetRange(float newRange)
    {
        attackRange = newRange;
    }
}
