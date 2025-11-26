using UnityEngine;

public class EnemyPortal : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }
}
