using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreCounter : StepCounter
{
    [SerializeField] string highScore = "is HighScore";
    [SerializeField] Text text;

    void Start()
    {
        text = GetComponent<Text>();
        text.text = highScore;

        StartCoroutine(CountRoutine());
    }


    protected override IEnumerator CountRoutine()
    {
        var nowHighscore = SaveManager.instance.GetScore();
        while (nowHighscore > nowCount)
        {
            yield return 5;
        }

        if (nowHighscore < nowCount)
        {
            text.text = "High:" + nowHighscore;
        }
    }

}
