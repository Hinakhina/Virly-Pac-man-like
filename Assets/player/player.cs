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
    [SerializeField] private Transform RespawnPoint;
    private float multiply;
    public bool SneakMode;
    [HideInInspector] public Enemy enemy;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text enemyCountText;
    [SerializeField] private Animator animator;
    [SerializeField] KilledEnemyButton killedEnemyButton;
    [SerializeField] ButtonManager ButtonManager;
    private float rotationTime = 0.1f;
    private float rotationVelocity;
    private float velocityAnim;
    
    [HideInInspector] public int enemyCount;


    private Coroutine powerUpCoroutine;
    public Action OnPowerUpStart;
    public Action OnPowerUpStop;

    private Coroutine isRespawnCoroutine;
    public bool isRespawn = false;

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
        AudioManager.Instance.PlaySFX1("powerUp");
        if(OnPowerUpStart != null)
        {
            OnPowerUpStart();
        }
        yield return new WaitForSeconds(powerUpDuration);
        isPowerUpActive = false;
        AudioManager.Instance.PlaySFX1("powerDown");
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

        Vector3 movementDirection = new Vector3(horizontal, 0, vertical);
 
        if (movementDirection.magnitude >= 0.1)
        {
            float rotationAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg + playerCamera.transform.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref rotationVelocity, rotationTime);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
            movementDirection = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;
        }
        

        // running using LeftShift
        if(Input.GetKey(KeyCode.LeftShift)){
            multiply = 2.0f;
            animator.SetBool("isRun", true);
        }
        else if(!Input.GetKey(KeyCode.LeftShift)){
            animator.SetBool("isRun", false);

        }

        // sneak using LeftControl
        if(Input.GetKey(KeyCode.LeftControl)){
            multiply = 0.7f;
            SneakMode = true;
        }

        if(!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
        {
            multiply = 1.0f;

        }

        rb.velocity = movementDirection * moveSpeed * multiply * Time.deltaTime;

        velocityAnim = rb.velocity.magnitude;
        animator.SetFloat("Velocity", velocityAnim);
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
                Debug.Log("Killed Ghost...");
                animator.SetBool("isAttack", true);
                collision.gameObject.GetComponent<Enemy>().Dead();
                animator.SetBool("isAttack", false);
                AudioManager.Instance.PlaySFX2("kill");
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
        AudioManager.Instance.PlaySFX2("dead");
        if(health > 0){
            isRespawnCoroutine = StartCoroutine(StartRespawn());
        }
        else{
            health = 0;
            Debug.Log("Lose");
            ButtonManager.loseScreen();
        }
        UpdateUI();
    }

    public IEnumerator StartRespawn()
    {
        isRespawn = true;
        animator.SetBool("isDead", isRespawn);
        yield return new WaitForSeconds(1);
        transform.position = RespawnPoint.position;
        isRespawn = false;
        animator.SetBool("isDead", isRespawn);
    }

    public void UpdateUI()
    {
        healthText.text = ": " + health;
        enemyCountText.text = ": " + enemyCount;
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
