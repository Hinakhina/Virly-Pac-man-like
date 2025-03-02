using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player1 : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Camera playerCamera;
    [SerializeField] float dragValue;
    [SerializeField] CharacterController control;
    [SerializeField] Rigidbody rb;
    
    private bool isOnGround = false;

    private void Awake()
    {
        HideAndLockCursor();
    }

    void Update()
    {
        // move (wasd), camera control
        // Horizontal = left (a) [-] and right (d) [+]
        float horizontal = Input.GetAxis("Horizontal");

        // Vertical = foward (w) [+] aand backward (s) [-]
        float vertical = Input.GetAxis("Vertical");

        Vector3 horizontalDirection = horizontal * playerCamera.transform.right;
        Vector3 vericalDirection = vertical * playerCamera.transform.forward;
        horizontalDirection.y = 0;
        vericalDirection.y = 0;

         //vector3 (x, y, z)
        Vector3 movementDirection = horizontalDirection + vericalDirection;

        control.Move(movementDirection * moveSpeed * Time.deltaTime);

        // running using LeftShift
        if(Input.GetKey(KeyCode.LeftShift)){
            control.Move(movementDirection * 1.5f * moveSpeed * Time.deltaTime);
        }

        // sneak using LeftControl
        if(Input.GetKey(KeyCode.LeftControl)){
            control.Move(movementDirection * 0.2f * moveSpeed * Time.deltaTime);
        }

        jumpInput();
   
    }

    private void HideAndLockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void jumpInput()
    {
        // Slow Falling?
        if(Input.GetButtonDown("Jump") && isOnGround){
            // rb.drag = 0;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }     
    }

    private void OnCollisionStay()
    {
        isOnGround = true;
        // rb.drag = dragValue;
    }

}
