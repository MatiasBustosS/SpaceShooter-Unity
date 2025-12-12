using UnityEngine;


public class Collectable : MonoBehaviour
{
    [SerializeField] private CollectableData[] collectableData;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private BoxCollider2D col;
    [SerializeField] private AudioSource collectSound;
    [SerializeField] private float speed;
    
    CollectableData data;
    
    public CollectableData Data => data;

    public void Init(CollectableData ColData)
    {
        data = ColData;
        
        sr.sprite = data.Sprite;
        
        col.size = new Vector3(sr.sprite.bounds.size.y,  sr.sprite.bounds.size.x,  sr.sprite.bounds.size.z);
        
    }
    
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager ph = other.GetComponent<PlayerManager>();
            if (ph != null)
                ph.MakeBuff(data.Type);
            AudioSource.PlayClipAtPoint(collectSound.clip, transform.position);
            Destroy(gameObject);
        }
    }
}
