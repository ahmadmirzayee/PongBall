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

    // This function is used to set the scores on the UI board
    public void SetScoresOnBoard(int playerOneScore, int playerTwoScore)
    {
        playerOneScoreText.text = playerOneScore.ToString();
        playerTwoScoreText.text = playerTwoScore.ToString();
    }

    // This function is used to set the max score text on the UI board
    public void SetMaxScoreText(string maxScore)
    {
        maxScoreText.text = maxScore;
    }

    // This function is used to set the message text on the UI board and make it visible
    public void SetMessage(string message)
    {
        messageText.gameObject.SetActive(true);
        messageText.text = message;
    }
}
