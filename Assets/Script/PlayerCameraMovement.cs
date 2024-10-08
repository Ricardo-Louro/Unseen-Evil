using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour
{
    public bool active = true; 
    private PlayerMovement playerMovement;

    [SerializeField] private float heightOffset;
    [SerializeField] private float mouseSensitivity;
    public float MouseSensitivity => mouseSensitivity;

    private float rotY;
    private float rotX;

    [SerializeField] private AudioSource heartbeatAudioSource;
    private float maxVolume = 1f;
    private float minVolume = 0f;
    private float minAudioDistance = 0f;
    private float maxAudioDistance = 25f;

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
            AdaptVolumeToDistance();
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

    public void UpdateLookSensitivity(float value)
    {
        mouseSensitivity = value;
    }

    private void AdaptVolumeToDistance()
    {
        if (heartbeatAudioSource != null)
        {
            float audioVolume;
            audioVolume = (maxVolume - minVolume) / (minAudioDistance - maxAudioDistance) * ((float)PortraitPiece.closestPage[1] - maxAudioDistance) + minVolume;

            if (audioVolume < minVolume)
            {
                audioVolume = minVolume;
            }
            else if (audioVolume > maxVolume)
            {
                audioVolume = maxVolume;
            }

            heartbeatAudioSource.volume = audioVolume;
        }
    }
}
