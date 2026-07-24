using TMPro;
using UnityEngine;

public class UIManger : MonoBehaviour
{
    [Header("Score UI")]
    public TextMeshProUGUI PlayerOneScoreText;
    public TextMeshProUGUI PlayerTwoScoreText;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetScoresOnBoard(int playerOneScore, int playerTwoScore)
    {
        PlayerOneScoreText.text = playerOneScore.ToString();
        PlayerTwoScoreText.text = playerTwoScore.ToString();
    }
}
