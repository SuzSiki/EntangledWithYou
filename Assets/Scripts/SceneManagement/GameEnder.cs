using UnityEngine;
using System.Collections;

public class GameEnder:MonoBehaviour
{
    Dimention dimention;

    void Start()
    {
        StartCoroutine(WaitForEnd());
    }

    IEnumerator WaitForEnd()
    {
        dimention = Dimention.dimentionList[0];

        yield return new WaitUntil(()=>dimention.loaded);
        yield return new WaitUntil(()=>dimention.goal.accomplished);


        SEKAIModule.instance.DarkenTheWorld(() => {
            SceneLoader.instance.LoadTitle();
        });
    }
}