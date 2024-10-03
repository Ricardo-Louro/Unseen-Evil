using System.ComponentModel.Design;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    public bool active = true;

    private Rigidbody rb;

    private Vector3 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
}