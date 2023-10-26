using UnityEngine;

public class ShowFlipCounter : MonoBehaviour
{
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.IsGrounded && PlayerController.CounterFlips > 0)
        {
            panel.SetActive(true);
        } else
        {
            panel.SetActive(false);
        }
    }

}
