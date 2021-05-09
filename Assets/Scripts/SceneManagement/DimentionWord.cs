using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimentionWord : MonoBehaviour
{
    [SerializeField] string message;
    TypingModule word;
    Dimention dimention;

    void Start()
    {
        dimention = GetComponentInParent<Dimention>();
        word = GetComponentInChildren<TypingModule>();
        StartCoroutine(WaitForActivate());
    }

    IEnumerator  WaitForActivate()
    {
        yield return new WaitUntil(()=>dimention.active);

        word.Type(message);
    }
}
