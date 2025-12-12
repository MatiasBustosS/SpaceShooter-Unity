using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemies/New Enemy")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float speed = 2f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float bulletSpeed = 5;
    [SerializeField] private float shootInterval = 1;
    [SerializeField] private int contactDamage = 1;
    [SerializeField] private int scoreValue = 100;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Sprite bulletSprite;
    [SerializeField] private AudioClip bulletSound;
    [SerializeField] private float spawnTime;
    
    public int MaxHealth => maxHealth;
    public float Speed => speed;
    public int Damage => damage;
    public float BulletSpeed => bulletSpeed;
    public float ShootInterval => shootInterval;
    public int ContactDamage => contactDamage;
    public int ScoreValue => scoreValue;
    public Sprite Sprite => sprite;
    public Sprite BulletSprite => bulletSprite;
    public AudioClip BulletSound => bulletSound;
    public float SpawnTime => spawnTime;
}
