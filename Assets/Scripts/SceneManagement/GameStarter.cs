using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    Dimention dimention;
    GraphicFadeMotion titleFadeMotion;

    void Start()
    {
        
        titleFadeMotion = GetComponentInChildren<GraphicFadeMotion>();
        StartCoroutine(WaitForStart());
    }

    IEnumerator WaitForStart()
    {
        dimention = Dimention.dimentionList[0];
        
        yield return new WaitUntil(() => dimention.loaded);
        yield return new WaitUntil(() => dimention.goal.accomplished);


        titleFadeMotion.Exit().onComplete += () =>
        {
            SEKAIModule.instance.DarkenTheWorld(SceneLoader.instance.LoadNextScene);
        };
    }
}
