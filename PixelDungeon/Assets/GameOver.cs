using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject pausedButton;
    public GameObject displayText;
    public GameObject gameoverScreen;
    public GameObject healthbar;
    public TextMeshProUGUI scoreCounterText;
    private int points;

    // Start is called before the first frame update
    void Start()
    {
        PlayerStatus playerStatus = FindObjectOfType<PlayerStatus>(); // Find the PlayerStatus component
        gameoverScreen.SetActive(false);
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
        gameoverScreen.SetActive(true);
        pausedButton.SetActive(false);
        displayText.SetActive(false);
        healthbar.SetActive(false);
        points = PlayerPrefs.GetInt("PointValue");
        scoreCounterText.text = "Your Score: " + (points).ToString();
    }
}
