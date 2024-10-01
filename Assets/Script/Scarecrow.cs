using UnityEngine;
using UnityEngine.AI;

public class Scarecrow : MonoBehaviour
{
    private bool active;
    private bool lookedAt;

    private Plane[] planes;
    [SerializeField] private float maxDetectionDistance;
    [SerializeField] private BoxCollider lookCollider;

    private NavMeshAgent agent;
    private Transform playerTransform;
    private LayerMask whatIsGround;
    private LayerMask whatIsPlayer;


    private bool testLook;

    // Start is called before the first frame update
    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
        agent = GetComponent<NavMeshAgent>();
        active = true;
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
                //Teleport away
            }

            lookedAt = false;
        }
        else
        {
            planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
            lookedAt = GeometryUtility.TestPlanesAABB(planes, lookCollider.bounds);
        }
    }

    private void OnSightBehaviour()
    {
        agent.isStopped = true;
    }

    private void OutOfSightBehaviour()
    {
        agent.isStopped = false;
        agent.SetDestination(playerTransform.position);
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
}