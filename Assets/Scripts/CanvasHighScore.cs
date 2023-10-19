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
        highScore.text = Convert.ToString(StaticData.highFlipScore);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.isFlipping)
        {
            highScore.text = Convert.ToString(StaticData.highFlipScore);

        }
        if (StaticData.GameOver)
        {
            StaticData.highFlipScore = 0;
        }
    }
}
