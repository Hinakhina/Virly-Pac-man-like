using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private float powerUpDuration;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int health;
    [SerializeField] private Transform respawnPoint;
    private float multiply;
    public bool SneakMode;
    [SerializeField] public Enemy enemy;
    private Coroutine powerUpCoroutine;
    [SerializeField] private TMP_Text healthText;

    public Action OnPowerUpStart;
    public Action OnPowerUpStop;
    private bool isPowerUpActive = false;

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
        isPowerUpActive = true;
        if(OnPowerUpStart != null)
        {
            OnPowerUpStart();
        }
        yield return new WaitForSeconds(powerUpDuration);
        isPowerUpActive = false;
        if(OnPowerUpStop != null)
        {
            OnPowerUpStop();
        }
    }

    private void Awake()
    {
        HideAndLockCursor();
    }

    private void Start()
    {
        UpdateUI();
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

    private void OnCollisionEnter(Collision collision)
    {
        if(isPowerUpActive)
        {
            if(collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().Dead();
            }
        }
    }

    public void Dead()
    {
        health -= 1;
        if(health > 0){
            transform.position = respawnPoint.position;
        }
        else{
            health = 0;
            Debug.Log("Lose");
            Time.timeScale = 0;
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        healthText.text = "Health: " + health;
    }

}
