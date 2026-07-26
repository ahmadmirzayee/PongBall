using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Singleton")]
    public static GameManager instance;

    [Header("Refrences")]
    public BallController ballController;
    public ToggleController toggleController;
    public MenuManager menuManager;
    public ScoreManager scoreManager;
    public AudioManager audioManager;
    public UIManger uIManger;
    
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
