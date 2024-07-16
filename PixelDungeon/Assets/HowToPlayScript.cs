using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayScript : MonoBehaviour
{
    public GameObject howToPlay;
    // Start is called before the first frame update
    void Start()
    {
        howToPlay.SetActive(true);
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    public void StartGame()
    {
        howToPlay.SetActive(false);
        Time.timeScale = 1f;
    }
}
