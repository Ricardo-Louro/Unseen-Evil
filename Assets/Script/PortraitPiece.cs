using UnityEngine;

public class PortraitPiece : Interactive
{
    [SerializeField] private AudioSource pageAudio;
    private Inventory inventory;
    private Transform playerTransform;

    public static object[] closestPage = new object[2];

    private void Awake()
    {
        closestPage[0] = null;
        closestPage[1] = null;
    }
    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
        inventory = FindObjectOfType<Inventory>();
        inventory.AddTotalPage();
    }

    public override void Interact()
    {
        pageAudio.Play();
        inventory.ObtainPage(this);

        if((PortraitPiece)closestPage[0] == this)
        {
            closestPage[0] = null;
            closestPage[1] = null;
        }
        
        Destroy(gameObject);
    }

    private void Update()
    {
        FindClosestPageToPlayer();
    }

    private void FindClosestPageToPlayer()
    {
        if (playerTransform == null)
        {
            playerTransform = FindObjectOfType<PlayerMovement>().transform;
        }
        else
        {
            float distanceFromPlayer = Mathf.Abs((playerTransform.position - transform.position).magnitude);
            if (closestPage[0] == null)
            {
                closestPage[0] = this;
                closestPage[1] = distanceFromPlayer;
            }
            else
            {
                if (distanceFromPlayer < (float)closestPage[1])
                {
                    closestPage[0] = this;
                    closestPage[1] = distanceFromPlayer;
                }
                else if ((PortraitPiece)closestPage[0] == this)
                {
                    closestPage[1] = distanceFromPlayer;
                }
            }
        }
    }
}