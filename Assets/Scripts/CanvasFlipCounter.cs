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
            counterFlip.text = "Flip X" + Convert.ToString(PlayerController.ContadorFlips);
            
            if (PlayerController.ContadorFlips > StaticData.HighFlipScore)
            {
                StaticData.HighFlipScore = PlayerController.ContadorFlips;
            }

        }
        else
        {
            counterFlip.text = "0";
        }
    }
}
