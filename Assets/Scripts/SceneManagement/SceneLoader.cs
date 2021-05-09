using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    [SerializeField] int SceneStartIndex = 1;
    int nowScene = 0;
    public int nowStage{get{return nowScene - SceneStartIndex;}}


    protected override void Awake()
    {
        base.Awake();
        nowScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadTitle(){
        SceneManager.LoadScene(0);
    }

    public void LoadNextScene()
    {
        nowScene++;
        SceneManager.LoadScene(nowScene);
    }

    public void Restart(){
        SceneManager.LoadScene(nowScene);
    }
}
