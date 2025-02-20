using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Camera playerCamera;
    private Rigidbody playerRigidbody;
    
    private bool isOnGround = false;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        HideAndLockCursor();
    }

    // Update is called once per frame
    void Update()
    {
        // move (wasd), camera control
        // playerMovementControl();
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

        playerRigidbody.velocity = movementDirection * moveSpeed * Time.fixedDeltaTime;

        // Debug.Log("Horizontal: " + horizontal);
        // Debug.Log("Vartical: " + vertical);

        // running using LeftShift
        if(Input.GetKey(KeyCode.LeftShift)){
            playerRigidbody.velocity = movementDirection * 2 * moveSpeed * Time.fixedDeltaTime;
        }

        // sneak using LeftControl
        if(Input.GetKey(KeyCode.LeftControl)){
            playerRigidbody.velocity = movementDirection * 0.5f * moveSpeed * Time.fixedDeltaTime;
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
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }     
    }

    private void OnCollisionStay()
    {
        isOnGround = true;
    }

}
