using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private AudioClip startFx;
    [SerializeField] private AudioClip closeFx;

    public void NewGame()
    {
        SoundManager.Instance.PlaySound(startFx);
        StaticData.GameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void CloseGame()
    {
        SoundManager.Instance.PlaySound(closeFx);
        Application.Quit();
    }
}