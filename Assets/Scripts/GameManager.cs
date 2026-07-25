using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Singleton")]
    public static GameManager instance;

    [Header("Refrences")]
    public ScoreManager scoreManager;
    public UIManger uIManger;
    public ToggleController toggleController;
    public AudioManager audioManager;

    private void Awake()
    {
        instance = this;
        PauseGame(0);
    }

    public void PauseGame(int value)
    {
        Time.timeScale = value;
    }
}
