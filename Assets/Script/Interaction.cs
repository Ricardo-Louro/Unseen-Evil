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
            Interactive interactive = hit.collider.GetComponent<Interactive>();
            if (interactive != null && interactive.enabled)
            {
                if(!interactionUI.activeSelf)
                {
                    interactionUI.SetActive(true);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    interactive.Interact();
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