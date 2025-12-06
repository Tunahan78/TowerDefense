using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "WaveData", menuName = "TD/Wave Data")]
public class WaveData : ScriptableObject
{
    // ------------------------------------------------------------------
    // HEDEF 1: GRUPLAMAYI (CLUSTERING) SAĞLAMAK
    // Bu yapı, bir kerede kaç düşmanın spawn edileceğini tanımlar.
    // ------------------------------------------------------------------
    
    [System.Serializable]
    public class EnemyGroup
    {
        public GameObject enemyPrefab;
        public int enemiesPerBatch ; // YENİ: Bir seferde (aynı karede) kaç düşman spawn edilecek.
        public int batchCount ;     // YENİ: Kaç tane grup (batch) spawn edilecek.
        public float timeBetweenBatches; // YENİ: İki grup arasındaki bekleme süresi (SpawnInterval'ın yeni anlamı).
    }

    // ------------------------------------------------------------------
    // HEDEF 2: ÇOKLU PORTAL DESTEĞİ
    // Bu yapı, hangi portalın kullanılacağını belirler.
    // ------------------------------------------------------------------
    
    [System.Serializable]
    public class SpawnGroup
    {
        // Birinci Sorun Çözümü: Tek bir Transform yerine, doğrudan Portal objesinin referansı.
        // WaveManager bu objenin pozisyonunu spawn için kullanacaktır.
        public EnemyPortal portalRefarence; 
        
        // Bu portaldan çıkacak düşman grupları.
        public List<EnemyGroup> groupsToSpawn;
    }

    // ------------------------------------------------------------------
    // ANA VERİ
    // ------------------------------------------------------------------

    public float preWaveDelay = 5f; // Dalga başlamadan önceki bekleme süresi
    
    // Ana liste artık SpawnGroup'ları tutar. Bu sayede bir dalga, birden fazla portaldan gelebilir.
    public List<SpawnGroup> spawnGroups; 
}
