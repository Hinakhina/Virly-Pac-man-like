using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAddForce : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Camera playerCamera;
    [SerializeField] float dragValue;
    [SerializeField] Rigidbody rb;

    private float multiply;
    
    private bool isOnGround = false;

    private void Awake()
    {
        HideAndLockCursor();
    }

    void Update()
    {
        rb.drag = dragValue;
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


        // running using LeftShift
        multiply = 1.0f;
        if(Input.GetKey(KeyCode.LeftShift)){
            rb.drag = 0.5f * dragValue;
        }

        // sneak using LeftControl
        if(Input.GetKey(KeyCode.LeftControl)){
            rb.drag = 2f * dragValue;
        }

        rb.AddForce(movementDirection * moveSpeed * multiply * Time.deltaTime);
        rb.velocity = movementDirection * moveSpeed * multiply * Time.deltaTime;
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
    }

}
