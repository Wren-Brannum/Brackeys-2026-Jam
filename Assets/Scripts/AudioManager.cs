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
    public AudioClip BackroundMusic;
    public AudioClip[] TalkingSounds;

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
    public void PlayBackRoundMusic()
    {
        audioSource.PlayOneShot(BackroundMusic);
    }
    public void PlayTalkingSounds()
    {
        int randomIndex = Random.Range(0, TalkingSounds.Length);
        AudioClip randomTalkingSound = TalkingSounds[randomIndex];
        audioSource.PlayOneShot(randomTalkingSound);
    }
}