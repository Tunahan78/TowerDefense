using UnityEngine;
using System.Collections.Generic;
using System.Collections; // Coroutine için gerekli

public class WaveManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private List<WaveData> waves;

    // ToDo: Enemy Factory'ye ihtiyacımız olacak. Şimdilik Instantiate kullanıyoruz.

    private int currentWaveIndex = 0;
    private bool isSpawning = false;
    private List<Coroutine> activeSpawnCoroutines = new List<Coroutine>(); // Aktif görevleri takip etmek için
    
    // ToDo: UI ve Can yönetimi için olaylar (Events) buraya eklenecek.

    private void Start()
    {
        // ToDo: Oyun başı gecikmesi eklenebilir.
        StartNextWave();
    }

    public void StartNextWave()
{
    if (currentWaveIndex >= waves.Count)
    {
        Debug.Log("Tüm Dalgalar Tamamlandı! Oyun Kazanıldı.");
        // ToDo: Oyun bitiş işlemleri buraya.
        return;
    }

    WaveData currentWave = waves[currentWaveIndex];
    
    // YENİ KONTROL: Önceki dalganın tamamen bittiğinden emin ol.
    if (!isSpawning)
    {
        StartCoroutine(ManageWaveLifecycle(currentWave));
    }
}

private IEnumerator ManageWaveLifecycle(WaveData currentWave)
{
    isSpawning = true;
    activeSpawnCoroutines.Clear(); // Önceki görevleri temizle

    Debug.Log($"Dalga {currentWaveIndex + 1} Başlıyor!");
    yield return new WaitForSeconds(currentWave.preWaveDelay);

    // --- 1. Paralel Spawn Görevlerini Başlatma ---
    foreach (var spawnGroup in currentWave.spawnGroups)
    {
        // HER BİR SPAWN GRUBU İÇİN YENİ BİR COROUTINE BAŞLAT
        // Bu, farklı portallardan aynı anda spawn yapmamızı sağlar.
        Coroutine spawnTask = StartCoroutine(SpawnGroupCoroutine(spawnGroup));
        activeSpawnCoroutines.Add(spawnTask);
    }

    // --- 2. Tüm Spawn Görevlerinin Bitmesini Bekleme (KRİTİK) ---
    // Dalga, tüm düşmanlar yaratılana kadar bitmemelidir.
    // Tüm aktif görevler bitene kadar bekle.
    foreach (Coroutine task in activeSpawnCoroutines)
    {
        yield return task; 
    }
    
    // --- 3. Temizlik ve Sonraki Dalga ---
    Debug.Log($"Dalga {currentWaveIndex + 1} Tamamlandı.");
    currentWaveIndex++;
    isSpawning = false;
    
    // ToDo: Dalga bittikten sonra kalan düşmanları da öldürme kontrolü yapılmalı.

    // Bir sonraki dalga için bekleme süresi
    StartCoroutine(StartNextWaveDelayed(15f)); // Örnek bekleme süresi
}

private IEnumerator SpawnGroupCoroutine(WaveData.SpawnGroup spawnGroup)
{
    // Portal referansı eksikse atla
    if (spawnGroup.portalRefarence == null)
    {
        Debug.LogError("SpawnGroup'ta portal atanmamış!");
        yield break; 
    }
    
    Vector3 spawnPosition = spawnGroup.portalRefarence.GetSpawnPoint();

    // Her bir düşman grubunu dolaş (Örn: Basic Düşmanlar, sonra Hızlı Düşmanlar)
    foreach (var grup in spawnGroup.groupsToSpawn)
    {
        // Batch (Grup) sayısını dolaş
        for (int batch = 0; batch < grup.batchCount; batch++)
        {
            // --- BATCH SPAWN MANTIĞI (Aynı Karede Spawn) ---
            for(int i = 0; i < grup.enemiesPerBatch; i++)
            {
               GameObject enemyGO = Instantiate(grup.enemyPrefab, spawnPosition, Quaternion.identity);
    
             // KRİTİK: Düşmanı başlattığınız yer
            if (enemyGO.TryGetComponent<EnemyMovement>(out EnemyMovement movement))
            {
                // Düşmana, hangi portal tarafından yaratıldığını söyle
                movement.Initialize(spawnGroup.portalRefarence); 
             }

            }
            
            // --- GRUP ARASI BEKLEME ---
            // Bu, AoE kulelerinin grupları ayırması için gereken stratejik beklemedir.
            yield return new WaitForSeconds(grup.timeBetweenBatches);
        }
    }
}
private IEnumerator StartNextWaveDelayed(float delay)
{
    yield return new WaitForSeconds(delay);
    StartNextWave();
}

}
