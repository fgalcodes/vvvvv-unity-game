using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private AudioSource soundNewGame;

    public void NewGame()
    {
        soundNewGame.Play();
        
        // call the function after 2s
        Invoke("LoadScene", .5f);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    private void LoadScene()
    {
        StaticData.GameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}