using UnityEngine;

public class FakeScarecrow : MonoBehaviour
{
    private bool active;
    private bool lookedAt;

    private PlayerMovement playerMovement;
    private Plane[] planes;
    [SerializeField] private float maxDetectionDistance;
    [SerializeField] private BoxCollider lookCollider;


    private bool wasSeen;

    // Start is called before the first frame update
    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>(); 
        active = true;
        wasSeen = false;
        lookedAt = false;

        Debug.Log("Here we go!");
    }

     private void Update()
    {
        if(active)
        {
            TestLookedAt();
        }
    }

    private void TestLookedAt()
    {
        if(CalculateDistanceFromPlayer() >= maxDetectionDistance)
        {
            lookedAt = false;
            Debug.Log("I'm too far!");
        }
        else
        {
            Debug.Log(Camera.main.name);
            planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
            lookedAt = GeometryUtility.TestPlanesAABB(planes, lookCollider.bounds);

            if(lookedAt)
            {
                Debug.Log("You see me!");
                wasSeen = true;
            }
        }

        if(!lookedAt && wasSeen)
        {
            Debug.Log("Bye bye!");
            Destroy(gameObject);
        }
        if(lookedAt && !wasSeen)
        {
            Debug.Log("You're looking straight at me!");
            wasSeen = true;
        }
    }
    private float CalculateDistanceFromPlayer()
    {
        return Mathf.Abs((transform.position - playerMovement.transform.position).magnitude);
    }
}