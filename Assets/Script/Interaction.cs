using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private float maxRange;
    [SerializeField] private GameObject interactionUI;

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxRange))
        {
            if (hit.collider.GetComponent<Interactive>() != null)
            {
                if(!interactionUI.activeSelf)
                {
                    interactionUI.SetActive(true);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    hit.collider.GetComponent<Interactive>().Interact();
                }
            }
            else
            {
                if (interactionUI.activeSelf)
                {
                    interactionUI.SetActive(false);
                }
            }
        }
        else
        {
            if (interactionUI.activeSelf)
            {
                interactionUI.SetActive(false);
            }
        }
    }
}