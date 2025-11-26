using System.ComponentModel;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance { get; private set; }
    [SerializeField] private Transform[] waypoints;

    public Transform[] GetWaypoints() => waypoints;

    private void Awake()
    {
        if(Instance!= null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
