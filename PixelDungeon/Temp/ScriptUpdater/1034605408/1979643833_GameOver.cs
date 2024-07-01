using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject pausedButton;
    public GameObject displayText;
    public GameObject healthbar;



    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().SetActive(true);
        PlayerStatus playerStatus = FindObjectOfType<PlayerStatus>(); // Find the PlayerStatus component
        if (playerStatus != null)
        {
            playerStatus.OnPlayerDeath += HandlePlayerDeath; // Subscribe to the OnPlayerDeath event
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void HandlePlayerDeath()
    {
        Debug.Log("Player has died! Responding to death...");
        pausedButton.SetActive(false);
        displayText.SetActive(false);
        healthbar.SetActive(false);
        SceneManager.LoadSceneAsync("GameOver");
    }
}
