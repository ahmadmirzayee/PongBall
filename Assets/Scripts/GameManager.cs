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
    
    private void Awake()
    {
        instance = this;
        PauseGame(0);
    }

    private void Start()
    {
        
    }

    public void PauseGame(int value)
    {
        Time.timeScale = value;
    }
}
