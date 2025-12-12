using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    
    
    [Header("Final")]
    [SerializeField] private TMP_Text actualScore;
    [SerializeField] private TMP_Text maxScore;
    [SerializeField] private TMP_Text nwRecord;
    [SerializeField] private Button button;
    void Start()
    {
        if (startButton != null)
        {
            startButton.onClick.AddListener(() =>
                GameManager.Instance.GetComponent<ScenesManager>().ChangeScene(1)
            );
            quitButton.onClick.AddListener(() =>
                GameManager.Instance.GetComponent<ScenesManager>().QuitApp()
            );
        }
        
        if (actualScore != null)
        {
            actualScore.text = GameManager.Instance.ScoreValue.ToString();
            maxScore.text = GameManager.Instance.MaxScore.ToString();
            if (actualScore.text == maxScore.text && actualScore.text != "0")
                nwRecord.text = "NEW RECORD";
            
            button.onClick.AddListener(() =>
                GameManager.Instance.GetComponent<ScenesManager>().ChangeScene(0)
            );
            button.onClick.AddListener(() =>
                GameManager.Instance.ResetScore()
            );
        }
    }

}
