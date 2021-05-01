using UnityEngine;
using System.Collections.Generic;

public abstract class InstantTurnObjBase : MonoBehaviour, ITurnModule
{
    public ModuleState state { get; private set; }
    static GameManager gameManager;

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = GameManager.instance;
        }

        gameManager = GameManager.instance;
        state = ModuleState.ready;
    }

    protected void RegisterOnce()
    {
        gameManager.RegisterOnce(this);
    }
}