using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Scores Variables")]
    public int playerOneScore;
    public int playerTwoScore;
    public int maxScore;
    public int addScore = 1;

    // This function is used to increase the score of the player based on the scoreZoneId, if the scoreZoneId is 1, it increases player one's score, if it's 2, it increases player two's score
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

    // This function is used to decrease the score of the player based on the scoreZoneId, if the scoreZoneId is 1, it increase player one's score, if it's 2, it decreases player two's score
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

    // This function is used to check if any player has reached the max score, if player one has reached the max score, it sets the message to "Player 1 Wins!", plays the win sound and enables the menu UI
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

    // This function is used to check if any player has lost the game, if player two's score is less than or equal to 0, it sets the message to "Game Over!", plays the lose sound,
    // checks if the player's score is higher than the highscore and updates it if it is, and enables the menu UI
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
