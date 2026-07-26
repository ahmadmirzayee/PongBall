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

    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
        activePanel = panel;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ClosePanel()
    {
        activePanel.SetActive(false);
    }

    public void OnStartGameButtonClicked()
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

    public IEnumerator EnableMenuUI()
    {
        yield return new WaitForSeconds(1.5f);

        transform.root.gameObject.SetActive(true);
        GameManager.instance.PauseGame(0);
    }

    private void DisableMenuUI()
    {
        activePanel.SetActive(false);
        transform.root.gameObject.SetActive(false);
        GameManager.instance.PauseGame(1);
        GameManager.instance.ballController.moveBall?.Invoke();
        GameManager.instance.uIManger.messageText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        highscoreText.text = $"Highscore is: {PlayerPrefs.GetInt("HIGHSCORE")}";
    }
}
