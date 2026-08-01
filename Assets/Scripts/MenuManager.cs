using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("User Max Score Inputfield")]
    public TMP_InputField maxScoreInputfield;

    [Header("User Highscore Text")]
    public TextMeshProUGUI highscoreText;
    
    [Header("Last Panel")]
    private GameObject activePanel;

    // This function is used to show the panel that is passed as a parameter and set it as the active panel
    private void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
        activePanel = panel;
    }

    // This function is used to quit the game, if the game is running in the Unity Editor, it will stop the play mode, otherwise it will quit the application
    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // This function is used to close the active panel
    private void ClosePanel()
    {
        activePanel.SetActive(false);
    }

    // This function is used to start the game, it checks if the toggle is on or off, if it's on, it sets the max score to the value in the input field or 3 if it's empty,
    // if it's off, it sets the max score to int.MaxValue and player two's score to 3, then it disables the menu UI and updates the scores on the board
    private void OnStartGameButtonClicked()
    {
        if (GameManager.instance.toggleController.GetComponent<Toggle>().isOn == true)
        {
            if (maxScoreInputfield.text == "")
            {
                GameManager.instance.scoreManager.maxScore = 3;
            }
            else
            {
                GameManager.instance.scoreManager.maxScore = int.Parse(maxScoreInputfield.text);
            }
            GameManager.instance.scoreManager.playerOneScore = 0;
            GameManager.instance.scoreManager.playerTwoScore = 0;
            DisableMenuUI();
            GameManager.instance.uIManger.SetMaxScoreText(GameManager.instance.scoreManager.maxScore.ToString());
        }
        else if(GameManager.instance.toggleController.GetComponent<Toggle>().isOn == false)
        {
            GameManager.instance.scoreManager.playerOneScore = 0;
            GameManager.instance.scoreManager.maxScore = int.MaxValue;
            GameManager.instance.scoreManager.playerTwoScore = 3;
            GameManager.instance.uIManger.SetScoresOnBoard(GameManager.instance.scoreManager.playerOneScore, GameManager.instance.scoreManager.playerTwoScore);
            GameManager.instance.uIManger.SetMaxScoreText("∞");
            DisableMenuUI();
        }

        GameManager.instance.uIManger.SetScoresOnBoard(GameManager.instance.scoreManager.playerOneScore, GameManager.instance.scoreManager.playerTwoScore);
    }

    // This function is used to enable the menu UI after a delay of 1.5 seconds, it sets the root game object of the transform to active and pauses the game
    public IEnumerator EnableMenuUI()
    {
        yield return new WaitForSeconds(1.5f);

        transform.root.gameObject.SetActive(true);
        GameManager.instance.PauseGame(0);
    }

    // This function is used to disable the menu UI, it sets the active panel and the root game object of the transform to inactive, pauses the game, invokes the moveBall event, and hides the message text
    private void DisableMenuUI()
    {
        activePanel.SetActive(false);
        transform.root.gameObject.SetActive(false);
        GameManager.instance.PauseGame(1);
        GameManager.instance.ballController.moveBall?.Invoke();
        GameManager.instance.uIManger.messageText.gameObject.SetActive(false);
    }

    // This function is used to update the highscore text when the menu is enabled, it gets the highscore from PlayerPrefs and sets it to the highscore text
    private void OnEnable()
    {
        highscoreText.text = $"Highscore is: {PlayerPrefs.GetInt("HIGHSCORE")}";
    }
}
