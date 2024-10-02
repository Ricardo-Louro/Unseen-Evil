using UnityEngine;

public class FakeScarecrow : MonoBehaviour
{
    private bool active;
    private bool lookedAt;

    private PlayerMovement playerMovement;
    private Plane[] planes;
    [SerializeField] private float maxDetectionDistance;
    [SerializeField] private BoxCollider lookCollider;

    [SerializeField] private GameObject firstScarecrow;


    private bool wasSeen;

    // Start is called before the first frame update
    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>(); 
        active = true;
        wasSeen = false;
        lookedAt = false;
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
        }
        else
        {
            planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
            lookedAt = GeometryUtility.TestPlanesAABB(planes, lookCollider.bounds);

            if(lookedAt)
            {
                wasSeen = true;
            }
        }

        if(!lookedAt && wasSeen)
        {
            firstScarecrow.SetActive(true);
            Destroy(gameObject);
        }
        if(lookedAt && !wasSeen)
        {
            wasSeen = true;
        }
    }
    private float CalculateDistanceFromPlayer()
    {
        return Mathf.Abs((transform.position - playerMovement.transform.position).magnitude);
    }
}