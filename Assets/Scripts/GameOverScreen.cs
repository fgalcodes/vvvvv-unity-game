using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void TryAgainGame()
    {
        StaticData.GameOver = false;
        SceneManager.LoadScene(StaticData.CurrentLevel);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}