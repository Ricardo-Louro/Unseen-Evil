using Unity.VisualScripting;
using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour
{
    private PlayerMovement playerMovement;

    [SerializeField] private float heightOffset;
    [SerializeField] private float mouseSensitivity;

    private float rotY = 0;
    private float rotX = 0;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        SetCameraPosition();
        SetCameraRotation();
        playerMovement.SetPlayerRotation(transform.eulerAngles.y);
        
    }

    private void SetCameraPosition()
    {
        Vector3 position = playerMovement.transform.position;
        position.y = heightOffset;
        transform.position = position;
    }

    private void SetCameraRotation()
    {
        rotX -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotY += Input.GetAxis("Mouse X") * mouseSensitivity;

        rotX = Mathf.Clamp(rotX, -85f, 85f);

        transform.eulerAngles = new Vector3(rotX, rotY, 0);
    }
}
