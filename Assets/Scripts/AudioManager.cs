using UnityEngine;
using UnityEngine.InputSystem;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip eyeSound;
    public AudioClip HeartBeatSound;
    public AudioClip InhaleBreathing;
    public AudioClip ExhaleBreathing;
    public AudioClip[] TypingSounds;
    public AudioClip[] TalkingSounds;
    public AudioClip BackroundMusic;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            PlayClickSound();
        }
        PlayBackroundMusic();
    }

    public void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
    public void PlayEyeSound()
    {
        audioSource.PlayOneShot(eyeSound);
    }
    public void PlayHeartBeatSound()
    {
        audioSource.PlayOneShot(HeartBeatSound);
    }
    public void PlayInhale()
    {
        audioSource.PlayOneShot(InhaleBreathing);
    }
    public void PlayExhale()
    {
        audioSource.PlayOneShot(ExhaleBreathing);
    }
    public void PlayRandomTypingSound()
    {
        int randomIndex = Random.Range(0, TypingSounds.Length);
        AudioClip randomTypingSound = TypingSounds[randomIndex];
        audioSource.PlayOneShot(randomTypingSound);
    }
    public void PlayRandomTalkingSounds()
    {
        int randomIndex = Random.Range(0, TalkingSounds.Length);
        AudioClip randomTalkingSounds = TalkingSounds[randomIndex];
        audioSource.PlayOneShot(randomTalkingSounds);
    }
    public void PlayBackroundMusic()
    {
        audioSource.PlayOneShot(BackroundMusic);
    }
}