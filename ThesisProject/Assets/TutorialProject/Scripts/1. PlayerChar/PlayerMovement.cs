using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody characterRB;
    private Vector3 movementInput;
    private Vector3 movementVector;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float crouchMultiplier;
    [SerializeField] private float runMultiplier;
    private float startingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        characterRB = GetComponent<Rigidbody>();
        startingSpeed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        ApplyMovement();
    }

    private void OnMovement(InputValue input)
    {
        movementInput = new Vector3(input.Get<Vector2>().x, 0, input.Get<Vector2>().y);
    }

    private void OnCrouch()
    {
        movementSpeed *= crouchMultiplier;
    }

    private void OnRun()
    {
        movementSpeed *= runMultiplier;
    }

    private void ApplyMovement()
    {
        if (movementInput != Vector3.zero)
        {
            movementVector = movementInput.x * transform.right + movementInput.z * transform.forward;
            movementVector.y = 0;
        }
        characterRB.velocity = movementVector * movementSpeed * Time.fixedDeltaTime;
    }

    private void OnMovementStop()
    {
        movementVector = Vector3.zero;
    }

    private void OnCrouchStop()
    {
        movementSpeed = startingSpeed;
    }

    private void OnRunStop()
    {
        movementSpeed = startingSpeed;
    }
}
