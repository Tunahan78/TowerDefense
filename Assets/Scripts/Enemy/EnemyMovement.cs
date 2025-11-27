
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour , IUnitTarget , IPathProgress
{
   [Header("Test")]
   [SerializeField] private float debugPathScore;

   [Header("Data")]
   [SerializeField] private EnemyDataSO enemyData;
   [SerializeField] private Transform[] waypoints;

   [Header("Pathing")]
   [SerializeField] private float pathOffset; 

   [Header("Target Positon")]
   [SerializeField] private Transform targetPosition;
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
        if(enemyData == null)
        {
            Debug.LogError("EnemyData SO is missing!", this);
            enabled = false;
            return;
        }
        agent.speed = enemyData.baseMovementSpeed;
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
        if(waypointIndex >= waypoints.Length)
        {
            return; // Tüm waypoint'lere ulaşıldı
        }
        
        Vector3 targetPosition = waypoints[waypointIndex].position;

        if(pathOffset != 0)
        {
            Vector3 startPosition ;
            if(waypointIndex == 0)
            {
                startPosition = transform.position;
            }
            else
            {
                // Önceki waypoint'in pozisyonunu al
                startPosition = waypoints[waypointIndex - 1].position;
            }

            Vector3 direction = (targetPosition- startPosition).normalized;
            Vector3 perpendicular = new Vector3(-direction.z, 0, direction.x);

            targetPosition += perpendicular * pathOffset;
        }

        agent.SetDestination(targetPosition);
        waypointIndex++;
    }

    

    public Transform GetTransform()
    {
        return targetPosition;
    }

    private void OnDrawGizmosSelected()
{
    // 1. Gerekli Kontroller
    // Waypoint dizisi tanımlanmamışsa veya boşsa çizim yapma
    if (waypoints == null || waypoints.Length == 0) return;
    if (pathOffset == 0f) return; // Offset yoksa çizime gerek yok

    // 2. Çizim Rengi ve Kalınlığı
    Gizmos.color = Color.cyan; // Şerit hedeflerini mavi (cyan) yapalım

    // 3. Her bir Waypoint'i dolaş
    for (int i = 0; i < waypoints.Length; i++)
    {
        if (waypoints[i] == null) continue;
        
        Vector3 targetPosition = waypoints[i].position;
        Vector3 startPosition;

        // Başlangıç pozisyonunu belirle (SetNextDestination mantığına benzer)
        if (i == 0)
        {
            // İlk Waypoint için, düşmanın o anki Editör pozisyonunu başlangıç kabul et
            startPosition = transform.position; 
        }
        else
        {
            // Sonraki Waypoint'ler için, bir önceki Waypoint'i başlangıç kabul et
            if (waypoints[i - 1] == null) continue;
            startPosition = waypoints[i - 1].position;
        }

        // Yön Vektörünü Hesapla
        Vector3 direction = (targetPosition - startPosition).normalized;
        
        // Dikey Vektörü Hesapla (Yana Kaydırma Yönü)
        Vector3 perpendicular = new Vector3(-direction.z, 0, direction.x); 
        
        // Offset uygulanmış hedef pozisyonu
        Vector3 offsetTarget = targetPosition + (perpendicular * pathOffset);

        // 4. Offset'li Hedefi Çiz
        Gizmos.DrawSphere(offsetTarget, 0.2f); // 0.2f yarıçapında küçük bir küre çiz

        // (Opsiyonel) Düşmanın mevcut pozisyonundan offset'li hedefe giden yolu da çiz
        if (i == 0)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, offsetTarget);
        }
    }
}

    public float GetPathProgressScore()
    {
        float score = PathProgressCalculator.CalculateScore(
            agent, 
            waypoints, 
            waypointIndex);

            // SCORE'U INSPECTOR'DA GÖSTER
        debugPathScore = score; 

        return score;
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
}