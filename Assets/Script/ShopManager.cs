using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Enums;

public class ShopManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerManager player;
    [SerializeField] private Button damageBtn;
    [SerializeField] private Button fireRateBtn;
    [SerializeField] private Button bulletsBtn;
    [SerializeField] private Button healthBtn;
    [SerializeField] private Button speedBtn;
    
    [Header("Levels")]
    [SerializeField] private TMP_Text damageLvl;
    [SerializeField] private TMP_Text fireRateLvl;
    [SerializeField] private TMP_Text bulletsLvl;
    [SerializeField] private TMP_Text healthLvl;
    [SerializeField] private TMP_Text speedLvl;

    [Header("Price Texts")]
    [SerializeField] private TMP_Text damagePriceText;
    [SerializeField] private TMP_Text fireRatePriceText;
    [SerializeField] private TMP_Text bulletsPriceText;
    [SerializeField] private TMP_Text healthPriceText;
    [SerializeField] private TMP_Text speedPriceText;
    
    
    private GameManager gameManager;

    private void Start()
    {
        player.Stats.ResetAllStats();
        
        gameManager = GameManager.Instance;
        gameManager.SetShop(this);
        UpdateUI();
    }

    private void TryUpgrade(Stats stat)
    {
        int price = player.Stats.GetPrice(stat);

        if (price < 0)
            return;

        if (gameManager.CoinsCollected < price)
            return;

        gameManager.SpendCoins(price);

        player.Stats.LevelUp(stat);

        if (stat == Stats.Health)
            player.MakeBuff(CollectType.Health);
        
        UpdateUI();
    }
    
    public void UpgradeDamage()      => TryUpgrade(Stats.Damage);
    public void UpgradeFireRate()    => TryUpgrade(Stats.FireRate);
    public void UpgradeBullets()     => TryUpgrade(Stats.BulletCount);
    public void UpgradeHealth()      => TryUpgrade(Stats.Health);
    public void UpgradeSpeed()       => TryUpgrade(Stats.Speed);
    
    public void UpdateUI()
    {
        UpdatePriceUI(Stats.Damage, damageLvl, damagePriceText, damageBtn);
        UpdatePriceUI(Stats.FireRate, fireRateLvl, fireRatePriceText, fireRateBtn);
        UpdatePriceUI(Stats.BulletCount, bulletsLvl, bulletsPriceText, bulletsBtn);
        UpdatePriceUI(Stats.Health, healthLvl, healthPriceText, healthBtn);
        UpdatePriceUI(Stats.Speed, speedLvl, speedPriceText, speedBtn);
    }

    private void UpdatePriceUI(Stats stat, TMP_Text levelText, TMP_Text priceText, Button button)
    {
        int price = player.Stats.GetPrice(stat);

        if (price < 0)
        {
            priceText.text = "MAX";
            button.interactable = false;
        }
        else
        {
            priceText.text = price.ToString();
            button.interactable = gameManager.CoinsCollected >= price;

            switch (stat)
            {
                case Stats.Damage:
                    levelText.text = "Lvl."+(player.Stats.Damage + 1);
                    break;
                case Stats.FireRate:
                    levelText.text = "Lvl."+(player.Stats.FireRate + 1);
                    break;
                case Stats.BulletCount:
                    levelText.text = "Lvl."+(player.Stats.BulletCount + 1);
                    break;
                case Stats.Health:
                    levelText.text = "Lvl."+(player.Stats.Health + 1);
                    break;
                case Stats.Speed:
                    levelText.text = "Lvl."+(player.Stats.Speed + 1);
                    break;
                    
            }
        }
    }
    
}
