using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SplashScreenHandler : MonoBehaviour
{
    [Header("Video Player")]
    public VideoPlayer videoPlayer;

    // This function is called when the video player finishes playing the video and it loads the next scene
    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }
    void OnVideoFinished(VideoPlayer vp)
    {
        EndVideo();
    }

    // This function loads the next scene when the video finishes playing
    void EndVideo()
    {
        SceneManager.LoadScene("GameScene");
    }
}
