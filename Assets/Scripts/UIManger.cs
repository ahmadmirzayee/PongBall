using TMPro;
using UnityEngine;

public class UIManger : MonoBehaviour
{
    [Header("Score UI")]
    public TextMeshProUGUI playerOneScoreText;
    public TextMeshProUGUI playerTwoScoreText;
    public TextMeshProUGUI maxScoreText;

    [Header("Messages Text")]
    public TextMeshProUGUI messageText;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetScoresOnBoard(int playerOneScore, int playerTwoScore)
    {
        playerOneScoreText.text = playerOneScore.ToString();
        playerTwoScoreText.text = playerTwoScore.ToString();
    }

    public void SetMaxScoreText(string maxScore)
    {
        maxScoreText.text = maxScore;
    }

    public void SetMessage(string message)
    {
        messageText.gameObject.SetActive(true);
        messageText.text = message;
    }
}
