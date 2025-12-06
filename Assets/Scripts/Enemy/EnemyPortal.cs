using UnityEngine;

public class EnemyPortal : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    public Vector3 GetSpawnPoint()
    {
        return spawnPoint.position;
    }
}
