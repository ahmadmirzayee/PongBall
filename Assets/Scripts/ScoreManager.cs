using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Scores Variables")]
    public int playerOneScore;
    public int playerTwoScore;
    public int maxScore;

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
        CheckForWinner();
    }

    public void DecreaseScore(int scoreZoneId)
    {
        if (scoreZoneId == 1)
        {
            playerOneScore++;
        }
        else if (scoreZoneId == 2)
        {
            playerTwoScore--;
        }

        GameManager.instance.uIManger.SetScoresOnBoard(playerOneScore, playerTwoScore);
        CheckForLoser();
    }

    public void CheckForWinner()
    {
        if(playerOneScore == maxScore)
        {
            print("p1 wins");
        }
        else if (playerTwoScore == maxScore)
        {
            print("p2 wins");
        }
    }

    public void CheckForLoser()
    {
        if(playerTwoScore <= 0)
        {
            print("p1 lost");
        }
    }
}
