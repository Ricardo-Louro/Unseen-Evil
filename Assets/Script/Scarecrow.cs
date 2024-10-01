using UnityEngine;
using UnityEngine.AI;

public class Scarecrow : MonoBehaviour
{
    private bool active;
    private bool lookedAt;

    [SerializeField] private Transform[] spawnPoints;

    private Plane[] planes;
    [SerializeField] private float maxDetectionDistance;
    [SerializeField] private BoxCollider lookCollider;

    private NavMeshAgent agent;
    private Transform playerTransform;
    private LayerMask whatIsGround;
    private LayerMask whatIsPlayer;

    private float lastTimeSighted;


    private bool testLook;

    // Start is called before the first frame update
    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
        agent = GetComponent<NavMeshAgent>();
        active = true;
        lastTimeSighted = Time.time;
    }

     private void Update()
    {
        if(active)
        {
            TestLookedAt();

            if(lookedAt)
            {
                OnSightBehaviour();
            }
            else
            {
                OutOfSightBehaviour();
            }
        }
    }

    private void TestLookedAt()
    {
        if(CalculateDistanceFromPlayer() >= maxDetectionDistance)
        {
            if(lookedAt)
            {
                TeleportToSpawn();
            }
        }
        else
        {
            planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
            lookedAt = GeometryUtility.TestPlanesAABB(planes, lookCollider.bounds);
        }
    }

    private void OnSightBehaviour()
    {
        lastTimeSighted = Time.time;
        agent.isStopped = true;
    }

    private void OutOfSightBehaviour()
    {
        agent.isStopped = false;
        agent.SetDestination(playerTransform.position);

        if(Time.time - lastTimeSighted >= 60f)
        {
            TeleportToSpawn();
        }
    }

    private float CalculateDistanceFromPlayer()
    {
        return Mathf.Abs((transform.position - playerTransform.position).magnitude);
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerMovement player = collision.collider.GetComponent<PlayerMovement>();

        if(player != null && !lookedAt)
        {
            //player.Die();
        }
    }

    private void TeleportToSpawn()
    {
        Vector3 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

        if (Mathf.Abs((spawnPoint - playerTransform.position).magnitude) >= maxDetectionDistance)
        {
            transform.position = spawnPoint;
            lookedAt = false;
        }
    }
}