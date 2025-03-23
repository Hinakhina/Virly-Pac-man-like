using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [HideInInspector] public Enemy enemy;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text enemyCountText;
    [SerializeField] private Animator animator;
    [SerializeField] KilledEnemyButton killedEnemyButton;
    private int enemyCount;


    private Coroutine powerUpCoroutine;
    public Action OnPowerUpStart;
    public Action OnPowerUpStop;

    private Coroutine isInvinsibleCoroutine;
    public bool isInvinsible = false;

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
        InitEnemyList();
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
                enemyCount -= 1;
                UpdateUI();
                if(enemyCount <= 0)
                {
                    killedEnemyButton.ScreenActive();
                }

            }
        }
    }

    public void Dead()
    {
        health -= 1;
        isInvinsibleCoroutine = StartCoroutine(StartInvinsible());
        if(health > 0){
            transform.position = respawnPoint.position;
        }
        else{
            health = 0;
            Debug.Log("Lose");
            SceneManager.LoadScene("LoseScene");
        }
        UpdateUI();
    }

    public IEnumerator StartInvinsible()
    {
        isInvinsible = true;
        animator.SetBool("isInvinsible", isInvinsible);
        yield return new WaitForSeconds(5);
        isInvinsible = false;
        animator.SetBool("isInvinsible", isInvinsible);
    }

    public void UpdateUI()
    {
        healthText.text = "Health: " + health;
        enemyCountText.text = "Enemy: " + enemyCount;
    }

    private void InitEnemyList()
    {
        Enemy[] enemyList = GameObject.FindObjectsOfType<Enemy>();
        for (int i = 0; i < enemyList.Length; i++)
        {
            enemyCount += 1;
            
        }
    }

}
