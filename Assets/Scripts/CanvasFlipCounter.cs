using System;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFlipCounter : MonoBehaviour
{
    public Text counterFlip;

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.IsFlipping)
        {
            counterFlip.text = "Flip x" + Convert.ToString(PlayerController.CounterFlips);
            
            if (PlayerController.CounterFlips > StaticData.HighFlipScore)
            {
                StaticData.HighFlipScore = PlayerController.CounterFlips;
            }

        }
        else
        {
            counterFlip.text = "0";
        }
    }
}
