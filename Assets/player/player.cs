using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private float powerUpDuration;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Rigidbody rb;
    private float multiply;
    public bool SneakMode;
    [SerializeField] public Enemy enemy;
    private Coroutine powerUpCoroutine;

    public Action OnPowerUpStart;
    public Action OnPowerUpStop;

    public void PickPowerUp()
    {
        if(powerUpCoroutine != null)
        {
            StopCoroutine(powerUpCoroutine);
        }
        powerUpCoroutine = StartCoroutine(StartPowerUp());
    }

    private IEnumerator StartPowerUp()
    {
        if(OnPowerUpStart != null)
        {
            OnPowerUpStart();
        }
        yield return new WaitForSeconds(powerUpDuration);
        if(OnPowerUpStop != null)
        {
            OnPowerUpStop();
        }
    }

    private void Awake()
    {
        HideAndLockCursor();
    }

    void Update()
    {
        SneakMode = false;

        // Horizontal = left (a) [-] and right (d) [+]
        float horizontal = Input.GetAxis("Horizontal");

        // Vertical = foward (w) [+] and backward (s) [-]
        float vertical = Input.GetAxis("Vertical");

        Vector3 horizontalDirection = horizontal * playerCamera.transform.right;
        Vector3 vericalDirection = vertical * playerCamera.transform.forward;
        horizontalDirection.y = 0;
        vericalDirection.y = 0;

        Vector3 movementDirection = horizontalDirection + vericalDirection;

        multiply = 1.0f;
        // running using LeftShift
        if(Input.GetKey(KeyCode.LeftShift)){
            multiply = 2.0f;
        }

        // sneak using LeftControl
        if(Input.GetKey(KeyCode.LeftControl)){
            multiply = 0.7f;
            SneakMode = true;
        }

        rb.velocity = movementDirection * moveSpeed * multiply * Time.deltaTime;
    }

    private void HideAndLockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
