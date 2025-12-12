using System;
using UnityEngine;
using static Enums;


[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    [Header("Niveles de daño en cada mejora")]
    [SerializeField] private BulletClass[] damageLevels;
    [SerializeField] private int damageLevel = 0;

    [Header("Fire rate por nivel")]
    [SerializeField] private StatLevel[] fireRateLevels;
    [SerializeField] private int fireRateLevel = 0;

    [Header("Balas por nivel")]
    [SerializeField] private StatLevel[] bulletCountLevels;
    [SerializeField] private int bulletCountLevel = 0;

    [Header("Vida por nivel")]
    [SerializeField] private StatLevel[] healthLevels;
    [SerializeField] private int healthLevel = 0;
    
    [Header("Velocidad")]
    [SerializeField] private StatLevel[] speedLevels;
    [SerializeField] private int speedLevel = 0;


    public void ResetStat(Stats stats)
    {
        switch (stats)
        {
            case Stats.Damage:
                damageLevel = 0;
                break;
            case Stats.FireRate:
                fireRateLevel = 0;
                break;
            case Stats.BulletCount:
                bulletCountLevel = 0;
                break;
            case Stats.Health:
                healthLevel = 0;
                break;
            case Stats.Speed:
                speedLevel = 0;
                break;
        }
    }
    
    public void ResetAllStats()
     {
         damageLevel = 0;
         fireRateLevel = 0;
         bulletCountLevel = 0;
         healthLevel = 0;
         speedLevel = 0;
     }
    
    public void LevelUp(Stats stats)
    {
        switch (stats)
        {
            case Stats.Damage:
                damageLevel++;
                break;
            case Stats.FireRate:
                fireRateLevel++;
                break;
            case Stats.BulletCount:
                bulletCountLevel++;
                break;
            case Stats.Health:
                healthLevel++;
                break;
            case Stats.Speed:
                speedLevel++;
                break;
        }
    }

    

    public int GetPrice(Stats stat)
    {
        return stat switch
        {
            Stats.Damage      => damageLevel      < damageLevels.Length - 1 ? damageLevels[damageLevel + 1].Price : -1,
            Stats.FireRate    => fireRateLevel    < fireRateLevels.Length - 1 ? fireRateLevels[fireRateLevel + 1].Price : -1,
            Stats.BulletCount => bulletCountLevel < bulletCountLevels.Length - 1 ? bulletCountLevels[bulletCountLevel + 1].Price : -1,
            Stats.Health      => healthLevel      < healthLevels.Length - 1 ? healthLevels[healthLevel + 1].Price : -1,
            Stats.Speed       => speedLevel       < speedLevels.Length - 1 ? speedLevels[speedLevel + 1].Price : -1,    
            _ => -1
        };
    }
    
    
    [Serializable]
    public class BulletClass
    {
        [SerializeField] private int damage;
        [SerializeField] private Sprite bulletSprite;
        [SerializeField] private AudioClip bulletSound;
        [SerializeField] private int price;
        
        public int Damage => damage;
        public Sprite BulletSprite => bulletSprite;
        public AudioClip BulletSound => bulletSound;
        public int Price => price;
    }
    [Serializable]
    public class StatLevel
    {
        [SerializeField] private float value;
        [SerializeField] private int price;
        
        public float Value => value;
        public int Price => price;
        
    }
    
    public int DamageLevels => damageLevels.Length;
    public float FireRateLevels => fireRateLevels.Length;
    public int BulletCountLevels => bulletCountLevels.Length;
    public int HealthLevels => healthLevels.Length;
    public float SpeedLevels => speedLevels.Length;
    
    public int Damage => damageLevel;
    public float FireRate => fireRateLevel;
    public int BulletCount => bulletCountLevel;
    public int Health => healthLevel;
    public float Speed => speedLevel;

    public BulletClass CurrentDamage => damageLevels[damageLevel];
    public StatLevel CurrentFireRate => fireRateLevels[fireRateLevel];
    public StatLevel CurrentBulletCount => bulletCountLevels[bulletCountLevel];
    public StatLevel CurrentMaxHealth => healthLevels[healthLevel];
    public StatLevel CurrentSpeed => speedLevels[speedLevel]; 
}