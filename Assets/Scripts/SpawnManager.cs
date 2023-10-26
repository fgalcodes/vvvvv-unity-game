using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    [SerializeField] private GameObject player;

    private GameObject spawn;
    private Transform position;

    private int startLevel;
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

    private void Start()
    {
        startLevel = SceneManager.GetActiveScene().buildIndex;
        SpawnPlayer("Spawn", 0);
    }
    private void Update()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        if (startLevel < currentLevel)
        {
            SpawnPlayer("PreviousLevel", 4);
            startLevel = currentLevel;
        }
        else if (startLevel > currentLevel)
        {
            SpawnPlayer("NextLevel", -4);
            startLevel = currentLevel;
        } 


    }

    public void SpawnPlayer(string tag, int posX)
    {
        Instantiate(player);
        spawn = GameObject.FindGameObjectWithTag(tag);
        gameObject.transform.position = spawn.transform.position;
        player.transform.position = new Vector3 (gameObject.transform.position.x + posX, gameObject.transform.position.y, gameObject.transform.position.z);

    }


}
