using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleLoader : MonoBehaviour
{
    GraphicFadeMotion titieFadeMotion;


    void Start()
    {
        titieFadeMotion = GetComponentInChildren<GraphicFadeMotion>();


        titieFadeMotion.Enter().onComplete = () =>
        {
            GameManager.instance.StartGame();
            SEKAIModule.instance.SwapDimention(0);
        };

    }

}
