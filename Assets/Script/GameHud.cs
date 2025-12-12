using TMPro;
using UnityEngine;

public class GameHud : MonoBehaviour
{
    [SerializeField] private PlayerManager player;
    
    [SerializeField] private TMP_Text life;
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text coins;
    
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.Instance;
    }
    
    void Update()
    {
        life.text = "x" + player.CurrentHealth;
        score.text = "Score " + gameManager.ScoreValue;
        coins.text = "Coins: " + gameManager.CoinsCollected;
    }
}
