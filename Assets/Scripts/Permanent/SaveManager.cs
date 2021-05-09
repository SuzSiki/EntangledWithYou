using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    public void SaveStage(int stageNum,int score)
    {
        PlayerPrefs.SetInt(stageNum + "",score);
        PlayerPrefs.Save();
    }


    public int GetScore(int stageNum){
        var score = PlayerPrefs.GetInt(stageNum+"");

        return score;
    }

    public int GetScore(){
        var stage = SceneLoader.instance.nowStage;
        return GetScore(stage);
    }
}
