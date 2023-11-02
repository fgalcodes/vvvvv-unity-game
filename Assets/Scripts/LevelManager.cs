using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public bool isBack;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
