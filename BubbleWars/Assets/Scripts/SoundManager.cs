using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Header("----------- Audio Source ----------")]
    [SerializeField] AudioSource SFXSource;

    [Header("----------- Audio Clips ----------")]
    public AudioClip bubbleCharge;
    public AudioClip footsteps;
    public AudioClip slingshot;
    public AudioClip bubblePop;
    public AudioClip wind;
    public AudioClip score;
    public AudioClip death;
    public AudioClip win;
    public AudioClip btnPop1;

    [Header("----------- Audio UI ----------")]
    [SerializeField] Image soundOnIcon;
    [SerializeField] Image soundOffIcon;
    private bool muted = false;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    void Start()
    {
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            LoadToggle();
        }
        else
        {
            LoadToggle();
        }

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVol();
            SetSFXVol();
        }

        UpdateButtonIcon();
        AudioListener.pause = muted;
    }

    public void OnButtonPress()
    {
        if(muted == false)
        {
            muted = true;
            AudioListener.pause = true;
        }

        else
        {
            muted = false;
            AudioListener.pause = false;
        }

        SaveToggle();
        UpdateButtonIcon();
    }

    private void UpdateButtonIcon()
    {
        if(muted == false)
        {
            soundOnIcon.enabled = true;
            soundOffIcon.enabled = false;
        }
        else
        {
            soundOnIcon.enabled = false;
            soundOffIcon.enabled = true;
        }
    }

    private void LoadToggle()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void SaveToggle()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }

    public void SetMusicVol()
    {
        float mvolume = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(mvolume) * 20);
        PlayerPrefs.SetFloat("musicVolume", mvolume);
    }

    public void SetSFXVol()
    {
        float sfxvolume = SFXSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(sfxvolume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", sfxvolume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("sfxVolume");

        SetMusicVol();
        SetSFXVol();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip, float scaledVolume)
    {
        SFXSource.PlayOneShot(clip, scaledVolume);
    }
}
