using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.Rendering;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Transform enemySpwanPoint;
    [SerializeField] private List<WaveData> waves;

    private float currentWaveIndex = 0;
    private bool isSpawning = false; 

    // İleride buraya UI güncellemek için event eklnebilir

    private void Start()
    {
        // ToDo : Oyun başlamadan buraya bir bekleme süresi koyulabilir
        StartNextWave();
    }

    private void StartNextWave()
    {
        if(currentWaveIndex >= waves.Count)
        {
            Debug.Log("Tüm Dalgalar Tamamlandı");
            // ToDo: Oyun bitme işlemleri buraya eklenebilir
            return;
        }

        WaveData currentWave = waves[(int)currentWaveIndex];
        // ToDo: UI güncellemeleri burada yapılır
        if (!isSpawning)
        {
            StartCoroutine(SpawnWaveCoroutine(currentWave));
        }
    }

    private System.Collections.IEnumerator SpawnWaveCoroutine(WaveData currentWave)
    {
        isSpawning = true;
        Debug.Log($"Dalga {currentWaveIndex + 1} Başlıyor!");
        // Dalga Öncesi Gecikme
        yield return new WaitForSeconds(currentWave.preWaveDelay);

        foreach(var grup in currentWave.enemyGroups)
        {
            for(int i = 0; i < grup.enemyCount; i++)
            {
                // Burada ileride Enemy Factory kullanılabilir
                Instantiate(grup.enemyPrefab, enemySpwanPoint.position, Quaternion.identity);
                yield return new WaitForSeconds(grup.spawnInterval);
            }
        }
        isSpawning = false;
        currentWaveIndex++;
        StartCoroutine(StartNextWaveDelayed(15f)); // Örnek bekleme süresi
    }

    private System.Collections.IEnumerator StartNextWaveDelayed(float spawnInterval)
    {
        yield return new WaitForSeconds(spawnInterval);
        StartNextWave();
    }
}
