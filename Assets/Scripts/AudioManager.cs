using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[System.Serializable]
public class SoundEffects
{
    public string name;
    public AudioClip track;
}

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;

    [Header("Audio Clips")]
    public SoundEffects[] soundEffects;

    [Header("Sliders For Volumes")]
    public Slider musicVolumeSlider;
    public TextMeshProUGUI musicVolumePercent;
    public Slider sfxVolumeSlider;
    public TextMeshProUGUI sfxVolumePercent;

    // This function play an audio clip which name is {soundname} that called from other functions
    public void PlaySound(string soundName)
    {
        foreach (SoundEffects sound in soundEffects)
        {
            if (sound.name == soundName)
            {
                sfxAudioSource.PlayOneShot(sound.track);
                return;
            }
        }
    }

    // This function load the settings' value and set them on the sliders at the start of the game
    private void Start()
    {
        GameManager.instance.audioManager.musicAudioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        musicVolumePercent.text = $"{Mathf.Round(PlayerPrefs.GetFloat("MusicVolume") * 100)}%";

        GameManager.instance.audioManager.sfxAudioSource.volume = PlayerPrefs.GetFloat("SoundVolume");
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        sfxVolumePercent.text = $"{Mathf.Round(PlayerPrefs.GetFloat("SoundVolume") * 100)}%";
    }

    // This function get the settings' value from the sliders and save them when the game quits
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("SoundVolume", sfxVolumeSlider.value);
        PlayerPrefs.Save();
    }

    // This function is called by the musicVolumeSlider that changes the musicAudioSource volume.
    private void SetMusicVolume()
    {
        GameManager.instance.audioManager.musicAudioSource.volume = musicVolumeSlider.value;
        musicVolumePercent.text = $"{(Mathf.Round(musicVolumeSlider.value * 100))}%";
    }

    // This function is called by the sfxVolumeSlider that changes the sfxAudioSource volume.
    private void SetSfxVolume()
    {
        GameManager.instance.audioManager.sfxAudioSource.volume = sfxVolumeSlider.value;
        sfxVolumePercent.text = $"{(Mathf.Round(sfxVolumeSlider.value * 100))}%";
    }
}
