using System.ComponentModel;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PortalPath
{
    // Hangi portal bu yolu kullanacak? (Atama için Inspector'da görünecek)
    public EnemyPortal portalReference; 
    
    // Bu portalın takip etmesi gereken Transform[] dizisi.
    public Transform[] pathWaypoints; 
}

public class WaypointManager : MonoBehaviour
{
    // Singleton Deseni: Tek bir örneği garanti eder
    public static WaypointManager Instance { get; private set; }
    
    [Header("Tüm Portal Yolları")]
    [SerializeField] private List<PortalPath> allPortalPaths; 

    private void Awake()
    {
        // Singleton Mantığı
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    /// <summary>
    /// Belirtilen portala ait Waypoint dizisini döndürür.
    /// Düşmanlar, başlatıldıklarında bu metodu çağırarak kendi yollarını öğrenirler.
    /// </summary>
    /// <param name="portal">Yolu istenen portalın referansı (EnemyPortal bileşeni).</param>
    /// <returns>Portalın takip etmesi gereken Transform[] dizisi.</returns>
    public Transform[] GetPathForPortal(EnemyPortal portal)
    {
        if (portal == null)
        {
            Debug.LogError("Geçersiz Portal referansı alındı.");
            return null;
        }

        // İstenen portala ait PortalPath'i arama
        foreach (var portalPath in allPortalPaths)
        {
            // Eşleşme kontrolü
            if (portalPath.portalReference == portal)
            {
                // Eşleşme bulundu, yolu döndür
                return portalPath.pathWaypoints;
            }
        }
        
        Debug.LogError($"WaypointManager'da {portal.name} için tanımlı bir yol bulunamadı! Lütfen yolu Inspector'da atayın.");
        return null; 
    }
}


