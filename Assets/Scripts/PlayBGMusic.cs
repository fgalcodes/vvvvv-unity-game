using UnityEngine;

public class PlayBGMusic : MonoBehaviour
{
    [SerializeField] private AudioClip bgMusic;

    private void Start()
    {
        SoundManager.Instance.PlayBGM(bgMusic);
    }
}
