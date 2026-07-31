using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Scores Variables")]
    public int playerOneScore;
    public int playerTwoScore;
    public int maxScore;
    public int addScore = 1;

    public void IncreaseScore(int scoreZoneId)
    {
        if(scoreZoneId == 1)
        {
            if(addScore == -1)
            {
                playerTwoScore += addScore;
            }
            else
            {
                playerOneScore += addScore;
            }
        }
        else if (scoreZoneId == 2)
        {
            if(addScore == -1)
            {
                playerOneScore  += addScore;
            }
            else
            {
                playerTwoScore += addScore;
            }
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
        GameManager.instance.aiController.SetAiMovementValues();
    }

    public void CheckForWinner()
    {
        if(playerOneScore == maxScore)
        {
            GameManager.instance.uIManger.SetMessage("Player 1 Wins!");
            GameManager.instance.audioManager.PlaySound("win");
            StartCoroutine(GameManager.instance.menuManager.EnableMenuUI());
        }
        else if (playerTwoScore == maxScore)
        {
            GameManager.instance.uIManger.SetMessage("Player 2 Wins!");
            GameManager.instance.audioManager.PlaySound("win");
            StartCoroutine(GameManager.instance.menuManager.EnableMenuUI());
        }
    }

    public void CheckForLoser()
    {
        string message;

        if(playerTwoScore <= 0)
        {
            message = "Game Over!";
            GameManager.instance.audioManager.PlaySound("lose");

            if (PlayerPrefs.GetInt("HIGHSCORE") < playerOneScore)
            {
                PlayerPrefs.SetInt("HIGHSCORE", playerOneScore);
                message += $"\n Your highscore is: {playerOneScore}";
            }

            GameManager.instance.uIManger.SetMessage(message);
            StartCoroutine(GameManager.instance.menuManager.EnableMenuUI());
        }
    }
}
