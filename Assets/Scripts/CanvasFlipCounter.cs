using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFlipCounter : MonoBehaviour
{
    //public GameObject counter;
    public Text counterFlip;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.isFlipping)
        {
            counterFlip.text = Convert.ToString(PlayerController.contadorFlips);

            if (PlayerController.contadorFlips > StaticData.highFlipScore)
            {
                StaticData.highFlipScore = PlayerController.contadorFlips;
            }

        }
        else
        {
            counterFlip.text = "0";
        }
    }
}
