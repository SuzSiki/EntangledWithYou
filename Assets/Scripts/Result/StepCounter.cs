using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepCounter : MonoBehaviour
{
    [SerializeField] string baseString = "Steps:";
    Text text;
    protected int nowCount;

    void Start()
    {
        text = GetComponent<Text>();
        StartCoroutine(CountRoutine());
    }

    protected virtual IEnumerator CountRoutine()
    {
        while(!GameManager.instance.finished){
            yield return new WaitWhile(()=>nowCount == GameManager.instance.StepCount);

            nowCount++;

            text.text = baseString + nowCount;
        }
    }
}
