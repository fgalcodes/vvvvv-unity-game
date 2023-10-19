using System;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHighScore : MonoBehaviour
{
    //public GameObject counter;
    public Text highScore;

    // Start is called before the first frame update
    void Start()
    {
        highScore.text = Convert.ToString(StaticData.HighFlipScore);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.IsFlipping)
        {
            highScore.text = Convert.ToString(StaticData.HighFlipScore);

        }
        if (StaticData.GameOver)
        {
            StaticData.HighFlipScore = 0;
        }
    }
}
