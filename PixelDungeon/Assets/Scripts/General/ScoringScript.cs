using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoringScript : MonoBehaviour
{
    public TextMeshProUGUI scoreCounterText;
    private int points;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        points = PlayerPrefs.GetInt("PointValue");
        scoreCounterText.text = "Score: " + (points).ToString();
    }
}
