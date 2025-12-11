// CameraShaker.cs (CameraController'ın yanına eklenir)

using UnityEngine;
using System.Collections; // Coroutine için

public class CameraShaker : MonoBehaviour
{
    private Transform cameraTransform; // Sallanacak Transform
    private Vector3 originalPosition;  // Sarsıntının başladığı yer (stabil pozisyon)
    
    private float shakeAmplitude = 0f; // Sarsıntı şiddeti (piksel)
    private float shakeDuration = 0f;  // Sarsıntı süresi
    private float shakeTimer = 0f;     // Geçen zaman
    
    private Coroutine shakeCoroutine;

    private void Awake()
    {
        // Sallanacak Transform'u bul (Bu script'in bağlı olduğu obje)
        cameraTransform = transform;
        // Kamera hareket etse bile bu değerin güncel olması gerekiyor.
        // Shake sadece ofset ekleyeceği için, orijinal pozisyonu her zaman güncel tutmalıyız.
        originalPosition = cameraTransform.localPosition; 
    }

    private void OnEnable()
    {
        // DIP: Olayı dinlemeye başla
        CameraEvents.OnCameraShakeRequest += StartShake;
    }

    private void OnDisable()
    {
        // Temizlik
        CameraEvents.OnCameraShakeRequest -= StartShake;
    }

    private void StartShake(float amplitude, float duration)
    {
        // Eğer zaten bir sarsıntı varsa, yenisiyle değiştir (veya şiddeti topla)
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
        }
        
        shakeAmplitude = amplitude;
        shakeDuration = duration;
        shakeTimer = duration; // Timer'ı sürenin başına ayarla
        
        // Sarsıntıyı başlatan Coroutine'i kaydet
        shakeCoroutine = StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float timeElapsed = 0f;
        
        // Perlin Gürültüsü için rastgele başlangıç noktaları
        // Bu, farklı sarsıntılar için farklı desenler üretir.
        float randomX = UnityEngine.Random.Range(0f, 100f);
        float randomY = UnityEngine.Random.Range(0f, 100f);

        while (timeElapsed < shakeDuration)
        {
            // Orijinal pozisyonu her zaman güncel kamera pozisyonundan al.
            // Bu, kamera WASD ile hareket etse bile sarsıntının doğru yere uygulanmasını sağlar.
            originalPosition = cameraTransform.localPosition;

            // 1. Perlin Gürültüsü ile Eşzamanlı Rastgele Ofset Hesaplama
            // Time.time ile Perlin Gürültüsünü kaydırarak titreşim deseni oluştururuz.
            float percentComplete = timeElapsed / shakeDuration;
            
            // X ve Y ofsetini hesapla
            float xOffset = (Mathf.PerlinNoise(randomX + timeElapsed, 0f) * 2f - 1f) * shakeAmplitude;
            float yOffset = (Mathf.PerlinNoise(0f, randomY + timeElapsed) * 2f - 1f) * shakeAmplitude;
            
            // 2. Sönümleme (Fade Out)
            // Sarsıntının süresi dolarken şiddeti azaltmak için kullanılır.
            float falloff = 1f - percentComplete;
            
            // 3. Ofseti Uygulama
            Vector3 offset = new Vector3(xOffset, yOffset, 0f) * falloff;
            
            // Kameranın yerel pozisyonuna ofseti ekle
            cameraTransform.localPosition = originalPosition + offset;

            timeElapsed += Time.deltaTime;
            yield return null; // Bir sonraki karede devam et
        }

        // Bitiş: Kamerayı temiz konuma geri getir
        cameraTransform.localPosition = originalPosition;
        shakeCoroutine = null;
    }
}