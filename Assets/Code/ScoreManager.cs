using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text hiScoreText;
    public static int scoreCount;
    public static int hiCount;
    // Start is called before the first frame update
    void Start()
    {
       scoreCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreCount > hiCount)
        {
            hiCount = scoreCount;
        }
        scoreText.text = scoreCount.ToString();
        hiScoreText.text = "High score " + hiCount.ToString();
    }
}
