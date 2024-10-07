using UnityEngine;
using UnityEngine.AI;

public class Scarecrow : MonoBehaviour
{
    public bool active = true;
    private bool lookedAt;

    [SerializeField] private Transform[] spawnPoints;

    private Plane[] planes;
    [SerializeField] private float maxDetectionDistance;
    [SerializeField] private BoxCollider lookCollider;
    private AudioSource audioSource;

    private NavMeshAgent agent;
    private Transform playerTransform;
    private LayerMask whatIsGround;
    private LayerMask whatIsPlayer;

    private float lastTimeSighted;

    private float maxVolume = 1f;
    private float minVolume = 0f;
    private float minAudioDistance = 0f;
    private float maxAudioDistance = 20f;

    [SerializeField] private float killDistance;
    private float distanceToPlayer;

    private Rigidbody rb;

    private bool testLook;

    // Start is called before the first frame update
    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        lastTimeSighted = Time.time;
        distanceToPlayer = CalculateDistanceFromPlayer();

        rb = GetComponent<Rigidbody>();
    }

     private void Update()
    {
        if(active)
        {
            distanceToPlayer = CalculateDistanceFromPlayer();
            AdaptVolumeToDistance();
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
        else
        {
            agent.isStopped = true;
            lastTimeSighted += Time.deltaTime;
        }
    }

    private void TestLookedAt()
    {
        if(distanceToPlayer >= maxDetectionDistance)
        {
            if(lookedAt && distanceToPlayer >= maxAudioDistance)
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
        rb.velocity = Vector3.zero;
    }

    private void OutOfSightBehaviour()
    {
        if(distanceToPlayer <= killDistance)
        {
            Camera.main.GetComponent<Animator>().SetTrigger("Death");
        }

        agent.isStopped = false;
        agent.SetDestination(playerTransform.position);

        if(Time.time - lastTimeSighted >= 60f)
        {
            lastTimeSighted = Time.time;
            TeleportToSpawn();
        }
    }

    private float CalculateDistanceFromPlayer()
    {
        return Mathf.Abs((transform.position - playerTransform.position).magnitude);
    }

    private void TeleportToSpawn()
    {
        Vector3 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

        if (Mathf.Abs((spawnPoint - playerTransform.position).magnitude) >= maxAudioDistance)
        {
            transform.position = spawnPoint;
            lookedAt = false;
        }
    }

    private void AdaptVolumeToDistance()
    {
        float audioVolume;

        audioVolume = (maxVolume - minVolume) / (minAudioDistance - maxAudioDistance) * (distanceToPlayer - maxAudioDistance) + minVolume;

        if (audioVolume < minVolume)
        {
            audioVolume = minVolume;
        }
        else if (audioVolume > maxVolume)
        {
            audioVolume = maxVolume;
        }

        audioSource.volume = audioVolume;
    }
}