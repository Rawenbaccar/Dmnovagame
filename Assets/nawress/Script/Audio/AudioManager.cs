using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    #region private variable
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip enemyDeathSound;
    
    private static AudioManager instance;


    #endregion



    void Awake()
    {
        Audio();
    }

    private void Audio()
    {
        // Singleton pattern to keep music playing between scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Setup audio sources if not assigned
            if (musicSource == null)
                musicSource = gameObject.AddComponent<AudioSource>();

            if (sfxSource == null)
                sfxSource = gameObject.AddComponent<AudioSource>();

            // Configure sources with adjusted volumes
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.volume = 0.3f;

            sfxSource.volume = 0.8f;
            sfxSource.loop = false;

            musicSource.Play();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public static void PlayEnemyDeathSound()
    {
        if (instance != null && instance.enemyDeathSound != null)
        {
            instance.sfxSource.PlayOneShot(instance.enemyDeathSound);
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void FadeMusic(float targetVolume, float duration)
    {
        StartCoroutine(FadeMusicCoroutine(targetVolume, duration));
    }

    private IEnumerator FadeMusicCoroutine(float targetVolume, float duration)
    {
        float startVolume = musicSource.volume;
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsed / duration);
            yield return null;
        }

        musicSource.volume = targetVolume;
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void ResumeMusic()
    {
        musicSource.UnPause();
    }
} 