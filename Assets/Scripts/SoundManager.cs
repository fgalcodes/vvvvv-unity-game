using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource bgmSource, effectSource;

    private void Awake()
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

    public void PlayBGM(AudioClip bgMusic)
    {
        bgmSource.clip = bgMusic;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void PlaySound(AudioClip soundFX)
    {
        effectSource.PlayOneShot(soundFX);
    }
}