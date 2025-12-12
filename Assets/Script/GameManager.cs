using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    private int score = 0;
    private int maxScore;
    private int coinsCollected;
    private ShopManager shopManager;
    
    public int ScoreValue => score;
    public int MaxScore => maxScore;
    public int CoinsCollected => coinsCollected;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);
    }

    public void ResetScore()
    {
        score = 0;
        coinsCollected = 0;
    }
    
    public void AddScore(int addScore)
    {
        score += addScore;
        if (score > maxScore) maxScore = score;
        AddCoins(addScore/10);
    }

    public void AddCoins(int addCoins)
    {
        coinsCollected += addCoins;
        shopManager.UpdateUI();
    }

    public void SpendCoins(int price)
    {
        coinsCollected -= price;
        shopManager.UpdateUI();
    }

    public void SetShop(ShopManager shop)
    {
        shopManager = shop;
    }
    
}
