using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioSource musicSFX;
    public AudioSource soundSFX;
    public AudioSource ButtonSFX;
    public AudioSource BonusSFX;

    public Sprite musicOn;
    public Sprite musicOff;
    public Sprite soundOn;
    public Sprite soundOff;

    public Image SoundButton;
    public Image MusicButton;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicSFX.volume = PlayerPrefs.GetFloat("MusicVolume");
        }

        if (PlayerPrefs.HasKey("SoundVolume"))
        {
            soundSFX.volume = PlayerPrefs.GetFloat("SoundVolume");
        }
    }

    private void Start()
    {
        if(musicSFX.volume >= 0.1f)
        {
            MusicButton.GetComponent<Image>().sprite = musicOn;
        }
        else
        {
            MusicButton.GetComponent<Image>().sprite = musicOff;
        }

        if (soundSFX.volume >= 0.1f)
        {
            SoundButton.GetComponent<Image>().sprite = soundOn;
        }
        else
        {
            SoundButton.GetComponent<Image>().sprite = soundOff;
        }
    }

    public void OnClickMusicButton()
    {
        if(musicSFX.volume >=0.1f)
        {
            musicSFX.volume = 0;
            MusicButton.GetComponent<Image>().sprite = musicOff;
        }
        else
        {
            musicSFX.volume += 0.5f;
            MusicButton.GetComponent<Image>().sprite = musicOn;
        }
        PlayerPrefs.SetFloat("MusicVolume", musicSFX.volume);
    }

    public void OnClickSoundButton()
    {
        if (soundSFX.volume >= 0.1f)
        {
            soundSFX.volume = 0;
            ButtonSFX.volume = 0;
            BonusSFX.volume = 0;
            SoundButton.GetComponent<Image>().sprite = soundOff;
        }
        else
        {
            soundSFX.volume += 0.65f;
            ButtonSFX.volume = 0.6f;
            BonusSFX.volume = 0.65f;
            SoundButton.GetComponent<Image>().sprite = soundOn;
        }
        PlayerPrefs.SetFloat("SoundVolume", soundSFX.volume);
    }

}
