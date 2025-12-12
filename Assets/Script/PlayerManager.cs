using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static Enums;

public class PlayerManager : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference fire;
    [SerializeField] private InputActionReference quit;

    [Header("Stats")]
    [SerializeField] private PlayerStats stats;

    private int currentHealth;
    private float fireCooldown;

    [Header("Movement")]
    [SerializeField] Transform LeftLimit;
    [SerializeField] Transform RightLimit;
    [SerializeField] Transform BottomLimit;
    [SerializeField] Transform TopLimit;
    private Vector2 rawMove;

    [Header("Shoot")]
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private GameObject bulletCanva;
    
    [Header("Particles")]
    
    [SerializeField] private ParticleSystem particle;
    
    [Header("Shield")]
    [SerializeField] private GameObject shield;
    [SerializeField] private TMP_Text shieldTimerText;
    [SerializeField] private float shieldTimer;
    private bool isShield;
    private Coroutine shieldRoutine;
    
    public bool IsShield => isShield;
    public int CurrentHealth => currentHealth;
    public PlayerStats Stats => stats;
    
    AudioSource audioSource;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        currentHealth = (int)stats.CurrentMaxHealth.Value;
        
        GameManager.Instance.ResetScore();
    }

    private void OnEnable()
    {
        move.action.Enable();
        fire.action.Enable();
        quit.action.Enable();

        move.action.started += OnMove;
        move.action.performed += OnMove;
        move.action.canceled += OnMove;

        fire.action.started += OnShoot;

        quit.action.started += OnQuit;
    }

    

    private void OnDisable()
    {
        move.action.Disable();
        fire.action.Disable();
        quit.action.Disable();

        move.action.started -= OnMove;
        move.action.performed -= OnMove;
        move.action.canceled -= OnMove;

        fire.action.started -= OnShoot;
        
        quit.action.started -= OnQuit;
    }

    private void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        transform.Translate(rawMove * stats.CurrentSpeed.Value * Time.deltaTime);

        Vector3 pos = transform.position;

        
        pos.x = Mathf.Clamp(pos.x, LeftLimit.position.x, RightLimit.position.x);
        pos.y = Mathf.Clamp(pos.y, BottomLimit.position.y, TopLimit.position.y);

        transform.position = pos;
    }

    private void Fire()
    {
        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }
        else
        {
            bulletCanva.SetActive(true);
        }
    }

    private void Quit()
    {
        GameManager.Instance.ResetScore();
        GameManager.Instance.GetComponent<ScenesManager>().ChangeScene(0);
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        rawMove = ctx.ReadValue<Vector2>().normalized;
    }

    private void OnShoot(InputAction.CallbackContext ctx)
    {
        if (fireCooldown > 0)
            return;
        
        Shoot();
        bulletCanva.SetActive(false);
        fireCooldown = stats.CurrentFireRate.Value;
    }
    
    private void OnQuit(InputAction.CallbackContext ctx)
    {
        Quit();
    }

    private void Shoot()
    {
        int count = (int)stats.CurrentBulletCount.Value;
        float angleStep = 10f;
        float startAngle = -(angleStep * (count - 1) / 2f);

        for (int i = 0; i < count; i++)
        {
            float angle = startAngle + angleStep * i;
            Quaternion rot = transform.rotation * Quaternion.Euler(0, 0, -90 + angle);

            GameObject newBullet = Instantiate(bulletPrefab.gameObject, transform.position, rot);
            newBullet.GetComponent<Bullet>().Init(true, stats.CurrentDamage.Damage, stats.CurrentDamage.BulletSprite);
        }

        audioSource.PlayOneShot(stats.CurrentDamage.BulletSound);

    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void MakeBuff(CollectType collectable)
    {
        switch (collectable)
        {
            case CollectType.Health:
                if (currentHealth < stats.CurrentMaxHealth.Value)
                    currentHealth ++;
                break;
            case CollectType.Shield:
                GetShield();
                break;
            case CollectType.Coins:
                GameManager.Instance.AddCoins(10);
                break;
        }
    }

    private void GetShield()
    {
        isShield = true;
        shield.SetActive(true);
        
        if (shieldRoutine != null)
            StopCoroutine(shieldRoutine);

        shieldRoutine = StartCoroutine(ShieldTimer());
    }

    private IEnumerator ShieldTimer()
    {
        float timer = shieldTimer;
        
        shieldTimerText.gameObject.SetActive(true);
        
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            shieldTimerText.text = Mathf.Ceil(timer).ToString();
            yield return null;
        }

        isShield = false;
        shield.SetActive(false);
        
        shieldTimerText.gameObject.SetActive(false);

        shieldRoutine = null;
    }

    private void Die()
    {
        ParticleSystem ps = Instantiate(particle, transform.position, transform.rotation);
        ps.Play();

        Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
        transform.localScale = Vector3.zero;
        
        StartCoroutine(ChangeScene());
    }
    
    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
    }
}
