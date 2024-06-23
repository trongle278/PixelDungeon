using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public TextMeshProUGUI enemyCounterText;
    private int totalEnemies;
    private int defeatedEnemies;

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
    }

    public void EnemyDefeated()
    {
        defeatedEnemies++;
        UpdateEnemyCounter();
    }

    private void UpdateEnemyCounter()
    {
        enemyCounterText.text = "Enemies Remaining: " + (totalEnemies - defeatedEnemies).ToString();
    }

    public bool AllEnemiesDefeated()
    {
        return defeatedEnemies >= totalEnemies;
    }
}
