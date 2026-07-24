using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Scores Variables")]
    public int playerOneScore;
    public int playerTwoScore;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void IncreaseScore(int scoreZoneId)
    {
        if(scoreZoneId == 1)
        {
            playerOneScore++;
        }
        else if (scoreZoneId == 2)
        {
            playerTwoScore++;
        }

        GameManager.instance.uIManger.SetScoresOnBoard(playerOneScore, playerTwoScore);
    }
}
