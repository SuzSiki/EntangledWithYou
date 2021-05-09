using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Linq;

public class ResultManager : Singleton<ResultManager>
{
    GraphicFadeMotion[] fadeMotions;
    [SerializeField] float StopSec = 3;
    [SerializeField] float showDelay = 1;


    void Start()
    {
        fadeMotions = GetComponentsInChildren<GraphicFadeMotion>();
        fadeMotions = fadeMotions.Reverse().ToArray();
        
        StartCoroutine(WaitForGameEnd());
    }

    IEnumerator WaitForGameEnd()
    {

        yield return new WaitUntil(() => GameManager.instance.finished);

        var seq = ShowAll();

        seq.Play();

        yield return seq.WaitForKill();
        Debug.Log("compleated");

        var nowStage = SceneLoader.instance.nowStage;

        var highscore = SaveManager.instance.GetScore(nowStage);


        if (GameManager.instance.StepCount > highscore)
        {
            SaveManager.instance.SaveStage(SceneLoader.instance.nowStage, GameManager.instance.StepCount);
        }

        yield return new WaitForSeconds(StopSec);


        HideAll().onComplete = () =>
        {
            SEKAIModule.instance.DarkenTheWorld(SceneLoader.instance.LoadNextScene);
        };
    }

    Sequence ShowAll()
    {
        var seq = DOTween.Sequence();

        foreach (var motion in fadeMotions)
        {
            seq.Join(motion.Enter()).SetDelay(showDelay);
        }

        return seq;
    }

    Sequence HideAll()
    {
        var seq = DOTween.Sequence();

        foreach (var motio in fadeMotions)
        {
            seq.Join(motio.Exit());
        }

        seq.Play();

        return seq;
    }
}