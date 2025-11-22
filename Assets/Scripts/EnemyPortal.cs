using UnityEngine;

public class EnemyPortal : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    [Header("Spawn Settings")]
    [SerializeField] private GameObject basicEnemyPrefab;
    [SerializeField] private float spawnCooldown;
    private float spawnTimer;

    void Awake()
    {
        spawnTimer = spawnCooldown;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if(spawnTimer <= 0f)
        {
            SpawnEnemy();
            spawnTimer = spawnCooldown;
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(basicEnemyPrefab,spawnPoint.position,Quaternion.identity);
    }

}
