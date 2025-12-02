// PathProgressCalculator.cs (Managers/Helpers klasöründe)

using UnityEngine;
using UnityEngine.AI;

public static class PathProgressCalculator
{
    
    /// <param name="agent">Düşmanın NavMeshAgent bileşeni.</param>
    /// <param name="waypoints">Tüm Waypoint noktaları.</param>
    /// <param name="waypointIndex">Düşmanın hedeflediği bir sonraki Waypoint'in indeksi.</param>

    public static float CalculateScore(NavMeshAgent agent, Transform[] waypoints, int waypointIndex)
    {
        // Temel Kontrol: Ajanın varlığı ve Waypoint dizisinin geçerliliği.
        if (agent == null || waypoints == null || waypoints.Length == 0) return 0f;
        
        // Dayanıklılık Kontrolü (Hata Giderme): Ajanın aktif ve geçerli bir yolda olduğundan emin ol.
        if (!agent.gameObject.activeInHierarchy || agent.pathPending || !agent.isOnNavMesh || agent.isStopped)
        {
            // Ajan yolu hesaplıyorsa veya geçersiz durumdaysa çok düşük bir skor döndür.
            return 0.001f; 
        }
        
        // --- 1. Kalan Statik Yol Uzunluğunu Hesapla ---
        // Şu anki Waypoint'ten (waypointIndex) yolun sonuna kadar kalan Waypoint mesafesi
        float remainingStaticPath = 0f;
        
        // Waypoint'leri şu anki hedef Waypoint'ten başlayarak yolun sonuna kadar dolaşır.
        for (int i = waypointIndex; i < waypoints.Length; i++)
        {
            // i-1 ile i arasındaki mesafeyi hesaplamak için döngü 1. indisten başlamalıdır.
            // i > 0 kontrolü, Waypoint'ler arası mesafeyi doğru hesaplamayı sağlar.
            if (i > 0 && waypoints[i - 1] != null && waypoints[i] != null)
            {
                // Waypointler arasındaki mesafeyi ekle
                remainingStaticPath += Vector3.Distance(waypoints[i - 1].position, waypoints[i].position);
            }
        }

        // --- 2. NavMesh Kalan Mesafeyi Ekle ---
        // NavMeshAgent'ın bir sonraki aktif Waypoint'e kalan fiziksel mesafesi
        float distanceToNextWaypoint = agent.remainingDistance;

        // --- 3. Toplam Kalan Yolu Hesapla ---
        // Kalan yol = (Aktif hedefe kalan NavMesh mesafesi) + (Hedef Waypoint'ten sonraki statik yol)
        float totalDistanceRemaining = distanceToNextWaypoint + remainingStaticPath;
        
        // --- 4. Priorite Puanı Hesapla ---
        // Amaç: Daha kısa kalan yol, daha büyük skor demektir. (Ters ilişki)
        
        if (totalDistanceRemaining <= 0.1f)
        {
            // Düşman neredeyse yolun sonundaysa veya yola ulaşmışsa en yüksek önceliği ver.
            return float.MaxValue; 
        }
        
        // Kalan mesafenin tersini döndürerek, daha kısa mesafeleri daha yüksek puan yaparız.
        return 1.0f / totalDistanceRemaining; 
    }
}