using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState
{
    notReady,
    turnReady,
    inTurn
}

public class GameManager : Singleton<GameManager>,ILoad
{
    public bool loaded{get;private set;}
    public bool turnReady { get{return state == TurnState.turnReady;}}

    List<ITurnModule> turnModules = new List<ITurnModule>();
    List<ITurnModule> oneTimeRegister = new List<ITurnModule>();

    TurnState state;

    protected override void Awake()
    {
        base.Awake();

        state = TurnState.turnReady;
        loaded = true;
    }

    public bool DisregisterModule(ITurnModule module){
        if(!turnModules.Contains(module)){
            turnModules.Remove(module);
            return true;
        }

        return false;
    }

    public bool RegisterModule(ITurnModule module)
    {
        if (!turnModules.Contains(module))
        {
            turnModules.Add(module);
            return true;
        }
        return false;
    }

    public void RegisterOnce(ITurnModule module){
        oneTimeRegister.Add(module);
    }


    public bool StartTurn()
    {
        if(state == TurnState.turnReady){
            state = TurnState.inTurn;
            StartCoroutine(TurnRoutine());
        }
        return false;
    }

    IEnumerator TurnRoutine()
    {
        foreach(var module in turnModules){
            yield return new WaitUntil(()=>module.state == ModuleState.compleate);
        }
        
        //関係者がここに通知するよりも役割ごとに管理したほうが良いかもね、上の使えばできるし。
        foreach(var module in oneTimeRegister){
            yield return new WaitUntil(()=>module.state == ModuleState.compleate);
        }

        oneTimeRegister.Clear();

        state = TurnState.turnReady;
    }


}
