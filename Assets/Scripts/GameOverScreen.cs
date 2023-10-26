using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private AudioClip tryAgainFx;
    [SerializeField] private AudioClip quitFx;

    public void TryAgainGame()
    {
        SoundManager.Instance.PlaySound(tryAgainFx);
        StaticData.GameOver = false;
        SceneManager.LoadScene(StaticData.CurrentLevel);
    }

    public void RestartGame()
    {
        SoundManager.Instance.PlaySound(quitFx);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}