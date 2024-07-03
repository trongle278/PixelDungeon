using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public TextMeshProUGUI enemyCounterText;
    private int totalEnemies;
    private int defeatedEnemies;
    public int points;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        totalEnemies = FindObjectsOfType<Enemy>().Length;
        defeatedEnemies = 0;
        UpdateEnemyCounter();
        points = PlayerPrefs.GetInt("PointValue");
    }

    public void EnemyDefeated()
    {
        defeatedEnemies++;
        UpdateEnemyCounter();
        points = points + 10;
        PlayerPrefs.SetInt("PointValue", points);
        PlayerPrefs.Save();
        Debug.Log("Current point: " + points);
    }

    private void UpdateEnemyCounter()
    {
        enemyCounterText.text = "Enemies Remaining: " + (totalEnemies - defeatedEnemies).ToString();
    }
    

    public bool AllEnemiesDefeated()
    {
        points = points + 100;
        PlayerPrefs.SetInt("PointValue", points);
        PlayerPrefs.Save();
        Debug.Log("Current point: " + points);
        return defeatedEnemies >= totalEnemies;     
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("PointValue", 0);
    }

}
