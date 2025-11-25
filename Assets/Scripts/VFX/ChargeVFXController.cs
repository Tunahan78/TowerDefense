using UnityEngine;

public class ChargeVFXController : MonoBehaviour
{
    [SerializeField] private MeshRenderer emissionRenderer; // Parlayacak olan parçanın Renderer'ı
    [SerializeField] private  string emissionPropertyName = "_EmissionColor"; // Parlaklık parametresi
    [SerializeField]private Color maxChargeColor = Color.cyan; // Maksimum parlaklık rengi

    private Material materialInstance;
    private Color baseColor;

    private void Awake()
    {
        if(emissionRenderer == null) return;

        materialInstance = emissionRenderer.material; // Materyalin bir kopyasını alıyoruz 
        // Hata burada alınıyordu: Artık doğru parametreyi deneyecek.
        if (materialInstance.HasProperty(emissionPropertyName))
        {
           baseColor = materialInstance.GetColor(emissionPropertyName);
        }
        else
        {
          // Güvenlik: Eğer hala bulamazsa bir uyarı ver
          Debug.LogError($"Shader {materialInstance.shader.name} içinde {emissionPropertyName} bulunamadı. Lütfen Inspector'daki parametre adını kontrol edin.");
        }
    }

    public void UpdateCharge(float timeRemaining, float maxCooldown)
    {
        if(materialInstance == null) return;

        
        // 1. Şarj Yüzdesini Hesapla (0.0'dan 1.0'a)
        float chargePercentage = 1f - (timeRemaining / maxCooldown); 
        chargePercentage = Mathf.Clamp01(chargePercentage); // 0-1 arasına sıkıştır

        // 2. Parlaklık Rengini Hesapla
        // Temel renk ile MaxCharge rengi arasında geçiş yap
        Color currentEmission = Color.Lerp(baseColor, maxChargeColor, chargePercentage);

        float baseIntensity = 1f;
        float maxExtraIntensity = 4f; // Toplamda 1 + 4 = 5 kat parlaklık
    
        // Yüzdeye göre ekstra parlaklığı hesapla
        float finalIntensity = baseIntensity + (chargePercentage * maxExtraIntensity);
        // Rengi, bu yoğunluk ile çarparak uygula
        materialInstance.SetColor(emissionPropertyName, currentEmission * finalIntensity);

        
    }

    public void ResetCharge()
    {
       materialInstance.SetColor(emissionPropertyName, baseColor); 
    }
}
