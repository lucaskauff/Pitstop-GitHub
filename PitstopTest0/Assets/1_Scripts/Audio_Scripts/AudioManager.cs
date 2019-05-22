using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Struct which store a sfx linked to his name.
/// </summary>
[System.Serializable]
public struct SfxElement
{
    public string sfxName;
    public AudioClip sfxClip;
}

/// <summary>
/// Struct which store a sfx linked to his name.
/// </summary>
[System.Serializable]
public struct BgmData
{
    public AudioClip clip;
    public float startLoopTime;
    public float endLoopTime;
}

/// <summary>
/// Audio manager of the game.
/// </summary>
public class AudioManager : MonoBehaviour
{
    [Header("Music")]
    public AudioSource bgmSource;
    public AudioClip mainMusic;

    [Header("Sounds")]
    public AudioSource sfxSource; // Sound sources with various pitch.
    public List<SfxElement> allSfx;

    public static AudioManager instance;
    private AudioMixer musicMixer;

    private Dictionary<string, AudioClip> sfxDictionnary;

    private BgmData actualBgm;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        SetupAudio();

        AudioClipsToDictionnary();
        allSfx.Clear(); // Free memory.
    }

    private void Update()
    {
        if (!bgmSource.isPlaying || actualBgm.clip == null || actualBgm.endLoopTime < 0)
        {
            return;
        }

        if (bgmSource.time > actualBgm.endLoopTime)
        {
            bgmSource.time = actualBgm.startLoopTime;
        }
    }

    private void SetupAudio()
    {
        musicMixer = bgmSource.outputAudioMixerGroup.audioMixer;

        if (PlayerPrefs.HasKey("MusicVolume"))
            SetVolume("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));

        if (PlayerPrefs.HasKey("SoundVolume"))
            SetVolume("SoundVolume", PlayerPrefs.GetFloat("SoundVolume"));
    }

    /// <summary>
    /// Play the specified music.
    /// </summary>
    /// <param name="clip"></param>
    public void PlayMusic(AudioClip bgm)
    {
        if (bgm == null)
        {
            Debug.Log("No music to play");
        }

        bgmSource.clip = bgm;
        bgmSource.Play();
    }

    /// <summary>
    /// Update the current music data.
    /// </summary>
    /// <param name="bgmData"></param>
    public void SetBgmData(BgmData bgmData)
    {
        actualBgm = bgmData;
    }


    /// <summary>
    /// Play the specified sound on the hmd with a random pitch between two specified values.
    /// </summary>
    /// <param name="sfx"></param>
    public void PlaySfx(string sfxName)
    {
        AudioClip sfx = GetSound(sfxName);

        if (sfx != null)
        {
            sfxSource.PlayOneShot(sfx);
        }
    }

    /// <summary>
    /// Play the specified sound on the hmd with a random pitch between two specified values.
    /// </summary>
    /// <param name="sfx"></param>
    public float PlaySfx(string sfxName, float volume)
    {
        AudioClip sfx = GetSound(sfxName);

        if (sfx != null)
        {
            sfxSource.pitch = Random.Range(0.9f, 1.1f);
            sfxSource.PlayOneShot(sfx, volume);

            return sfx.length;
        }

        return 0f;
    }

    // Return the specified audio clip from dictionnary.
    public AudioClip GetSound(string sfxName)
    {
        AudioClip sfx;

        if (!sfxDictionnary.TryGetValue(sfxName, out sfx))
        {
            Debug.LogWarning("Sound " + sfxName + " is missing !");
        }

        if (sfx == null)
        {
            Debug.LogWarning("Sound " + sfxName + " is present but not filled !");
        }

        return sfx;
    }

    /// <summary>
    /// Update the volume of the music.
    /// </summary>
    /// <param name="newVolume"></param>
    public void SetVolume(string exposedParameter, float newVolume)
    {
        musicMixer.SetFloat(exposedParameter, newVolume);
    }

    #region Utility methods

    // Convert an array of AudioClips to a dictionnary.
    private void AudioClipsToDictionnary()
    {
        sfxDictionnary = new Dictionary<string, AudioClip>();

        foreach (SfxElement entry in allSfx)
        {
            sfxDictionnary.Add(entry.sfxName, entry.sfxClip);
        }
    }

    #endregion
}