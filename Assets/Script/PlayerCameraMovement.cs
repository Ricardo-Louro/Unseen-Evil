using Unity.VisualScripting;
using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour
{
    public bool active = true; 
    private PlayerMovement playerMovement;

    [SerializeField] private float heightOffset;
    [SerializeField] private float mouseSensitivity;

    private float rotY;
    private float rotX;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rotX = transform.eulerAngles.x;
        rotY = transform.eulerAngles.y;
    }

    private void Update()
    {
        if(active)
        {
            SetCameraPosition();
            SetCameraRotation();
            playerMovement.SetPlayerRotation(transform.eulerAngles.y);
        }
        
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
