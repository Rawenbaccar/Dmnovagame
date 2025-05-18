using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [Header(" Music Settings")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip[] levelMusicTracks;
    [Range(0f, 1f)] [SerializeField] private float musicVolume = 1f;

    [Header(" Sound Effects")]
    [SerializeField] private AudioSource sfxSource;

    [Header(" Level Change Sound")]
    [SerializeField] private AudioClip levelChangeSound;
    [Range(0f, 1f)] [SerializeField] private float levelChangeVolume = 1f;

    [Header(" Game Over Sound")]
    [SerializeField] private AudioClip gameOverSound;
    [Range(0f, 1f)] [SerializeField] private float gameOverVolume = 1f;

    [Header(" Button Click Sound")]
    [SerializeField] private AudioClip buttonClickSound;
    [Range(0f, 1f)] [SerializeField] private float buttonClickVolume = 1f;


    private static AudioManager instance;

    void Awake()
    {
        SetupAudio();
    }

    private void SetupAudio()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Setup sources
            if (musicSource == null) musicSource = gameObject.AddComponent<AudioSource>();
            if (sfxSource == null) sfxSource = gameObject.AddComponent<AudioSource>();

            // Music setup
            musicSource.loop = true;
            musicSource.volume = musicVolume;

            // Play first track
            if (levelMusicTracks.Length > 0)
            {
                musicSource.clip = levelMusicTracks[0];
                musicSource.Play();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ============= PUBLIC FUNCTIONS TO PLAY SOUND EFFECTS =============

    public static void PlayButtonClickSound()
    {
        if (instance != null && instance.buttonClickSound != null)
        {
            instance.sfxSource.PlayOneShot(instance.buttonClickSound, instance.buttonClickVolume);
        }
    }



    public static void PlayLevelChangeSound()
    {
        if (instance != null && instance.levelChangeSound != null)
        {
            instance.sfxSource.PlayOneShot(instance.levelChangeSound, instance.levelChangeVolume);
        }
    }

    public static void PlayGameOverSound()
    {
        if (instance != null && instance.gameOverSound != null)
        {
            instance.sfxSource.PlayOneShot(instance.gameOverSound, instance.gameOverVolume);
        }
    }

    // ============= MUSIC CONTROL =============

    public void ChangeMusicForLevel(int levelNumber)
    {
        int index = (levelNumber - 1) / 3;
        if (index >= levelMusicTracks.Length) index = levelMusicTracks.Length - 1;

        AudioClip newMusic = levelMusicTracks[index];

        if (musicSource.clip != newMusic)
        {
            musicSource.clip = newMusic;
            musicSource.Play();
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicSource.volume = musicVolume;
    }

    // ============= GET INSTANCE =============

    public static AudioManager Instance => instance;

    // Add method to stop background music
    public static void StopBackgroundMusic()
    {
        if (instance != null && instance.musicSource != null)
        {
            instance.musicSource.Stop();
        }
    }
}
