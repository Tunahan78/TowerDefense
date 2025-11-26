using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "ScriptableObjects/WaveData")]

public class WaveData : ScriptableObject
{
    [System.Serializable]
    public class EnemyGrup
    {
    public GameObject enemyPrefab;
    public int enemyCount;
    public float spawnInterval; // Dalgalar arası zaman
    }
    public float preWaveDelay; // Dalga başlamadan bekleme süresi
    public List<EnemyGrup> enemyGroups;
    
}
