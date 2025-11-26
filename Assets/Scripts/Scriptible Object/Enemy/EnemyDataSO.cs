using UnityEngine;



[CreateAssetMenu(fileName = "EnemeyData" , menuName = "TD/EnemeyData")]
public class EnemyDataSO : ScriptableObject
{

    public string enemeyName;
    [Header("Base Stats")]
    public float maxHealth;
    public float baseMovementSpeed;
    public float threatMultiplier;

    [Header("Defense & Rewards")]
    [Range(0f, 1f)] public float physicalResistance ; // 0.75f = %75 direnç
    public int killReward; // Ölünce alınacak ödül
}
