using System.ComponentModel.Design;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    public bool active = true;

    private Rigidbody rb;

    private Vector3 moveDirection;

    private AudioSource audioSource;
    [SerializeField] private AudioClip[] footstepSounds;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(active)
        {
            GetMoveDirection();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        if(active)
        {
            AddSpeed();
        }
    }

    private void GetMoveDirection()
    {
        moveDirection = transform.forward * Input.GetAxis("Vertical")
                      + transform.right * Input.GetAxis("Horizontal");

        if(moveDirection != Vector3.zero)
        {
            PlayFootstepSound();
        }
    }

    private void AddSpeed()
    {
        moveDirection = moveDirection.normalized;
        moveDirection *= moveSpeed;
        moveDirection.y = rb.velocity.y;

        rb.velocity = moveDirection;
    }

    public void SetPlayerRotation(float yValue)
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.y = yValue;
        transform.eulerAngles = rotation;
        transform.eulerAngles = rotation;
    }

    private void PlayFootstepSound()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.pitch = Random.Range(0.5f, 1f);
            audioSource.PlayOneShot(footstepSounds[Random.Range(0, footstepSounds.Length)]);
        }
    }
}