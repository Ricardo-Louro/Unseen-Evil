using UnityEngine;

public class Scarecrow : MonoBehaviour
{
    private bool active;
    private bool lookedAt;

    private PlayerMovement playerMovement;
    private Plane[] planes;
    [SerializeField] private float maxDetectionDistance;
    [SerializeField] private BoxCollider lookCollider;


    private bool testLook;

    // Start is called before the first frame update
    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();  
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
        //STAY PUT
    }

    private void OutOfSightBehaviour()
    {
        //APPROACH PLAYER
        //DISAPPEAR TO REAPPEAR AT A LATER TIME
    }

    private float CalculateDistanceFromPlayer()
    {
        return Mathf.Abs((transform.position - playerMovement.transform.position).magnitude);
    }
}