using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pausePanel; // Reference to your UI pause panel

    private void Start()
    {
        pausePanel.SetActive(false);
    }
    void Update()
    {
        // Check for pause input, e.g., the "P" key
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        // Pause or unpause the game
        if (isPaused)
        {
            Time.timeScale = 0; // Freeze game time
            pausePanel.SetActive(true); // Show the pause UI panel
        }
        else
        {
            Time.timeScale = 1; // Resume normal time flow
            pausePanel.SetActive(false); // Hide the pause UI panel
        }
    }
}
