using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToBeContinued : MonoBehaviour
{
    [SerializeField] string message = "To be Continued‥";
    TypingModule toBe;

    void Start()
    {
        toBe = GetComponentInChildren<TypingModule>();
        toBe.Type(message);
        StartCoroutine(WaitForWord());
    }

    IEnumerator  WaitForWord()
    {
        yield return new WaitUntil(()=>toBe.state == ModuleState.working);
        yield return new WaitUntil(()=>toBe.state == ModuleState.ready);

        SEKAIModule.instance.SwapDimention(0);
        GameManager.instance.StartGame();
    }
}
