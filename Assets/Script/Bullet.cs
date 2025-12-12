using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private int damage;
    private bool ally;
    
    private SpriteRenderer sr;
    private BoxCollider2D col;
    
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        
        col.isTrigger = true;
    }

    public void Init(bool isAlly, int dmg, Sprite sprite)
    {
        ally = isAlly;
        damage = dmg;
        sr.sprite = sprite;

        if (col != null)
            col.size = sr.sprite.bounds.size;

    }
    public void Init(bool isAlly, int dmg, Sprite sprite, float speed)
    {
        ally = isAlly;
        damage = dmg;
        sr.sprite = sprite;
        this.speed = speed;

        if (col != null)
            col.size = sr.sprite.bounds.size;

    }

    private void Update()
    {
        
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!ally)
        {
            PlayerManager pl = other.GetComponent<PlayerManager>();
            
            if (pl != null)
            {
                if(!pl.IsShield)
                    pl.TakeDamage(damage);
                Destroy(gameObject);
            }
        }

        else
        {
            Enemy en = other.GetComponent<Enemy>();
            
            if (en != null)
            {
                en.TakeDamage(damage);
                Destroy(gameObject);
            }
        }

        if (other.CompareTag("Finish"))
        {
            Destroy(gameObject);
        }
        
    }
}
