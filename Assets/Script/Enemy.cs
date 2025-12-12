using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private BoxCollider2D col;
    [SerializeField] private Slider _slider;
    [SerializeField] private ParticleSystem particle;

    private EnemyData data;
    private float currentHealth;
    
    private AudioSource audioSource;
    
    public EnemyData Data => data;
    
    private void Awake()
    {
        col.isTrigger = true;
    }
    
    public void Init(EnemyData newData)
    {
        data = newData;
        
        audioSource = GetComponent<AudioSource>();
        
        currentHealth = data.MaxHealth;
        _slider.value = currentHealth / data.MaxHealth;

        sr.sprite = data.Sprite;
        
        col.size = new Vector3(sr.sprite.bounds.size.y,  sr.sprite.bounds.size.x,  sr.sprite.bounds.size.z);
        StartCoroutine(ShootLoop());
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.left * data.Speed * Time.deltaTime);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        _slider.value = currentHealth/data.MaxHealth;
        if (currentHealth <= 0)Die();
    }
    
    IEnumerator ShootLoop()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(data.ShootInterval);
        }
    }

    private void Shoot()
    {
        audioSource.PlayOneShot(data.BulletSound);
        
        GameObject newBullet = Instantiate(bullet.gameObject, transform.position, transform.rotation * Quaternion.Euler(0, 0, 90));
        
        Bullet newBulletScript = newBullet.GetComponent<Bullet>();
        
        newBulletScript.Init(false, data.Damage, data.BulletSprite, data.BulletSpeed);
    }

    private void Die()
    {
        AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
        
        GameManager.Instance.AddScore(data.ScoreValue);
        ParticleSystem ps = Instantiate(particle, transform.position, transform.rotation);
        ps.Play();

        Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
        
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager pl = other.GetComponent<PlayerManager>();
            if (pl != null)
            {
                if(!pl.IsShield)
                    pl.TakeDamage(data.ContactDamage);
                Destroy(gameObject);
            }
        }
        else if(other.CompareTag("Finish"))
        {
            Instantiate(particle, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
