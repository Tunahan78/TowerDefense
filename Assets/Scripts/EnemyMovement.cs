
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
   [SerializeField] private Transform[] waypoints;
   private int waypointIndex = 0; 
   private NavMeshAgent agent;
   private float rotationSpeed = 10f;
    private void Awake()
    {
        if(TryGetComponent<NavMeshAgent>(out NavMeshAgent navMeshAgent))
        {
            agent = navMeshAgent;
        }
        else
          Debug.LogError("NavMeshAgent component not found on " + gameObject.name);

        agent.updateRotation = false;
        agent.avoidancePriority = Mathf.RoundToInt(agent.speed * 10);
    }

    private void Start()
    {

        if(waypoints.Length > 0)
        {
            SetNextDestination();
        }
        if(WaypointManager.Instance != null)
        {
            waypoints = WaypointManager.Instance.GetWaypoints();
        }
    }
    private void Update()
    {
        FaceTarget(agent.steeringTarget);
        // NavMeshAgent'ın hedefe yaklaştığını kontrol et
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            // Eğer hedefe ulaştıysa, bir sonraki hedefi ayarla
            SetNextDestination();
        }
    } 
    private void FaceTarget(Vector3 newTarget)
    {
       Vector3 direactionTarget = (newTarget - transform.position).normalized; 
       direactionTarget.y = 0; // Y eksenindeki farkı sıfırla, böylece sadece yatay düzlemde döner
       Quaternion newRoatation = Quaternion.LookRotation(direactionTarget);
       transform.rotation = Quaternion.Lerp(transform.rotation, newRoatation, Time.deltaTime * rotationSpeed);

    }

    private void SetNextDestination()
    {
        
        Vector3 targetPosition = waypoints[waypointIndex].position;
        agent.SetDestination(targetPosition);
        waypointIndex++;
    }
}