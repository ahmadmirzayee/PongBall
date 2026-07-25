using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("User Max Score Inputfield")]
    public TMP_InputField maxScoreInputfield;

    [Header("Sliders For Volumes")]
    public Slider musicVolumeSlider;
    public TextMeshProUGUI musicVolumePercent;
    public Slider sfxVolumeSlider;
    public TextMeshProUGUI sfxVolumePercent;

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
            disableMenuUI();
            GameManager.instance.uIManger.SetMaxScoreText(GameManager.instance.scoreManager.maxScore.ToString());
        }
        else if(GameManager.instance.toggleController.GetComponent<Toggle>().isOn == false)
        {
            GameManager.instance.scoreManager.maxScore = int.MaxValue;
            GameManager.instance.scoreManager.playerTwoScore = 3;
            GameManager.instance.uIManger.SetScoresOnBoard(GameManager.instance.scoreManager.playerOneScore, GameManager.instance.scoreManager.playerTwoScore);
            GameManager.instance.uIManger.SetMaxScoreText("∞");
            disableMenuUI();
        }
    }

    private void disableMenuUI()
    {
        activePanel.SetActive(false);
        transform.root.gameObject.SetActive(false);
        GameManager.instance.PauseGame(1);
    }

    public void SetMusicVolume()
    {
        GameManager.instance.audioManager.musicAudioSource.volume = musicVolumeSlider.value;
        musicVolumePercent.text = $"{(Mathf.Round(musicVolumeSlider.value * 100))}%";
    }
    public void SetSfxVolume()
    {
        GameManager.instance.audioManager.sfxAudioSource.volume = sfxVolumeSlider.value;
        sfxVolumePercent.text = $"{(Mathf.Round(sfxVolumeSlider.value * 100))}%";
    }
}
