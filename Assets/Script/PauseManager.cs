using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pauseMenuComponents;
    private PlayerMovement playerMovement;
    private PlayerCameraMovement playerCameraMovement;
    private Scarecrow[] scarecrows;
    private bool pauseActive;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerCameraMovement = FindObjectOfType<PlayerCameraMovement>();
        scarecrows = FindObjectsOfType<Scarecrow>(true);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        pauseActive = !pauseActive;

        playerMovement.active = !pauseActive;
        playerCameraMovement.active = !pauseActive;

        foreach(Scarecrow scarecrow in scarecrows)
        {
            scarecrow.active = !pauseActive;
        }

        foreach(GameObject component in pauseMenuComponents)
        {
            component.SetActive(pauseActive);
        }

        if(pauseActive)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
