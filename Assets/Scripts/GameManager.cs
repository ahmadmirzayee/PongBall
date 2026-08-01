using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Singleton")]
    public static GameManager instance;

    [Header("Refrences")]
    public BallController ballController;
    public ToggleController toggleController;
    public AiController aiController;
    public MenuManager menuManager;
    public ScoreManager scoreManager;
    public AudioManager audioManager;
    public UIManger uIManger;
    public PowerupManager powerupManager;

    // Pause the game at the start of the game
    private void Awake()
    {
        instance = this;
        PauseGame(0);
    }

    // This function is used to pause the game by setting the time scale to 0 or 1
    public void PauseGame(int value)
    {
        Time.timeScale = value;
    }
}
