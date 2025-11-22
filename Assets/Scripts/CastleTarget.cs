using UnityEngine;
using System;

public class CastleTarget : MonoBehaviour
{

    public static event Action OnEnemyReachedCastle;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy reached the castle!");
            OnEnemyReachedCastle?.Invoke();
            Destroy(other.gameObject);
        }
    }
}