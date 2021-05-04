using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// うんち
/// </summary>
[RequireComponent(typeof(Dimention))]
public class GhostShowingMan : MonoBehaviour, IObserver<Vector2Int>
{

    Dimention dimention;
    ICommandableModule<Vector2Int>[] movingModules;
    GhostBlinkMotion blinker;
    void Start()
    {
        dimention = GetComponent<Dimention>();
        movingModules = dimention.GetComponentsInChildren<ICommandableModule<Vector2Int>>();
        blinker = dimention.GetComponentInChildren<GhostBlinkMotion>();

        foreach (var mod in movingModules)
        {
            mod.AddObserver(this);
        }
    }

    bool blinking = false;

    public bool OnNotice(Vector2Int direction)
    {
        if (!blinking)
        {
            blinking = true;
            blinker.Enter().onComplete = () =>
            {
                blinker.Exit().onComplete = () =>{
                    blinking = false;
                };
            };
        }

        return true;
    }


}
