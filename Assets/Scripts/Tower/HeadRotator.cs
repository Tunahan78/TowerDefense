using UnityEngine;

public class HeadRotator : MonoBehaviour
{
    [SerializeField] private Transform towerHead; 
    [SerializeField] private float rotationSpeed ; 
    
    private int rotationTolerance = 2; // Kaç derecelik açı farkıyla hazır sayılacak
    private Transform currentTarget; 

    public void SetTarget(Transform newTarget)
    {
        currentTarget = newTarget;
    }

    private void Update()
{
    // 1. Hedefin null olup olmadığını kontrol et (Zaten mevcut)
    if (currentTarget == null)
    {
        return;
    }
    
    // YENİ KONTROL: Hedefin hala sahnede var olup olmadığını kontrol et
    // Bu kontrol, özellikle düşman yok edildiği anda kritik önem taşır.
    if (currentTarget.gameObject.activeInHierarchy == false) 
    {
        currentTarget = null;
        return;
    }

    // Hedefe bakma ve yumuşak dönüşü uygulama
    FaceTarget(currentTarget.position);
}

    private void FaceTarget(Vector3 targetPosition)
    {
        // 1. Hedefe doğru yön vektörünü hesapla
        Vector3 direction = (targetPosition - towerHead.position).normalized; 
        
        // 3. Hedefe bakacak Quaternion (rotasyon) hesapla
        // Eğer yön vektörü sıfır değilse (hedef tam kule pozisyonundaysa hata vermemek için)
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            
            // 4. Mevcut rotasyon ile hedef rotasyon arasına yumuşak geçiş yap (Lerp)
            towerHead.rotation = Quaternion.Lerp(
                towerHead.rotation, 
                targetRotation, 
                Time.deltaTime * rotationSpeed
            );
        }
    }

    public bool IsFaceingTarget()
    {
        if(currentTarget == null)
        {
            return false;
        }
        Vector3 diractionToTarget = (currentTarget.position - towerHead.position).normalized;

        float angel = Vector3.Angle(towerHead.forward, diractionToTarget);
        return angel <= rotationTolerance;
    }
}