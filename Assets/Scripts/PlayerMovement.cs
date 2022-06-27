using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    [Header("Movement")]
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;

    [Header("Gravity")]
    public float gravity;
    public float currentGravity;
    public float constantGravity;

    public float maxGravity;

    private Vector3 _gravityDirection;
    private Vector3 _currentGravityMovement;
    private Vector3 _currentPlayerMovement;
    private float _turnSmoothVelocity;

    private void Awake()
    {
        _gravityDirection = Vector3.down;
    }

    void Update()
    {
        CalculateGravity();
        CalculateMovement();
        controller.Move(_currentPlayerMovement + _currentGravityMovement);
    }

    private bool IsGrounded()
    { 
        return controller.isGrounded;
    }

    private void CalculateMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _currentPlayerMovement = moveDir.normalized * speed * Time.deltaTime;
        }
        else
        {
            _currentPlayerMovement = new Vector3(0f, 0f, 0f);
        }
    }

    private void CalculateGravity()
    {
        if (IsGrounded())
        {
            currentGravity = constantGravity;
        }
        else
        {
            if(currentGravity < maxGravity)
            {
                currentGravity -= gravity * Time.deltaTime;
            }
        }

        currentGravity -= gravity * Time.deltaTime;
        _currentGravityMovement = _gravityDirection * -currentGravity;
    }
}
