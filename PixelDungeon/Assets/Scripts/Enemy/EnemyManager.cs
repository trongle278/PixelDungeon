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
    private int i = 0 ;
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
    private void Update()
    {

        if (i == 0)
        {
            if (AllEnemiesDefeated() == true)
            {
                points = points + 100;
                PlayerPrefs.SetInt("PointValue", points);
                PlayerPrefs.Save();
                Debug.Log("Current point: " + points);
                i = 1;
            }
        }
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
        return defeatedEnemies >= totalEnemies;     
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("PointValue", 0);
    }

}
