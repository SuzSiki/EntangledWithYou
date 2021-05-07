using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public enum TurnState
{
    notReady,
    turnReady,
    inTurn,
    catchUp
}

public class GameManager : Singleton<GameManager>, IRequireToLoad
{
    public bool loaded { get; private set; }
    public bool turnReady
    {
        get
        {
            if (finishing)
            {
                return false;
            }

            return state == TurnState.turnReady;
        }
    }

    public List<ILoad> requireComponentList { get; private set; }


    public bool finished{get{return finishing;}}
    bool finishing = false;
    List<ITurnModule> turnModules = new List<ITurnModule>();
    List<ITurnModule> oneTimeRegister = new List<ITurnModule>();
    List<ITurnModule> catchUpList = new List<ITurnModule>();
    List<IGoal> goals;

    Coroutine acceptableCatchup = null;

    TurnState state
    {
        get { return _state; }
        set
        {
            Debug.Log(value);
            _state = value;
        }
    }
    TurnState _state;

    protected override void Awake()
    {
        base.Awake();
    }

    public void StartLoad()
    {
        goals = new List<IGoal>();
        foreach (var dimention in Dimention.dimentionList)
        {
            goals.Add(dimention.goal);
        }
        StartCoroutine(GameLoop());

        state = TurnState.turnReady;
        loaded = true;
    }

    void Start()
    {
        requireComponentList = new List<ILoad>();
        Dimention.dimentionList.ForEach(x => requireComponentList.Add(x));
        LoadManager.instance.RegisterLoad(this);
    }

    IEnumerator GameLoop()
    {
        yield return new WaitUntil(() =>
        {
            var accGoal = goals.FindAll(x => x.accomplished);

            if (accGoal.Count == 0)
            {
                return false;
            }
            if (Dimention.dimentionList.All(x => x.goal.accomplished))
            {
                Debug.Log(accGoal.Count);
                return true;
            }
            else
            {

                var dim = Dimention.dimentionList.Find(x => !x.active && !x.goal.accomplished );
                if(dim != null){
                    SEKAIModule.instance.SwapDimention(dim.dimentionID);
                }

                return false;
            }

        });

         Debug.Log("end");

        finishing = true;
    }

    public bool DisregisterModule(ITurnModule module)
    {
        if (!turnModules.Contains(module))
        {
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



    public bool RegisterOnce(ITurnModule module)
    {
        if (state == TurnState.inTurn)
        {

            if (acceptableCatchup == null)
            {
                acceptableCatchup = StartCoroutine(CatchUpRoutine());
            }

            catchUpList.Add(module);


            return true;
        }
        else
        {
            oneTimeRegister.Add(module);

            return true;
        }
    }

    public bool StartTurn()
    {
        if (state == TurnState.turnReady)
        {
            state = TurnState.inTurn;
            StartCoroutine(TurnRoutine());
        }
        return false;
    }

    IEnumerator TurnRoutine()
    {
        foreach (var module in turnModules)
        {
            yield return new WaitUntil(() => module.state == ModuleState.compleate);
        }

        //関係者がここに通知するよりも役割ごとに管理したほうが良いかもね、上の使えばできるし。
        foreach (var module in oneTimeRegister)
        {
            yield return new WaitUntil(() => module.state == ModuleState.compleate);
        }

        oneTimeRegister.Clear();

        state = TurnState.turnReady;
    }

    /// <summary>
    /// ターンに追いつくためのルーチン。
    /// 位置ターンごとに別のものが生成される
    /// </summary>
    /// <returns></returns>
    IEnumerator CatchUpRoutine()
    {
        yield return new WaitUntil(() => turnReady);

        //今回のターンの分はここで消す。
        acceptableCatchup = null;
        var modules = catchUpList;
        catchUpList.Clear();

        foreach (var module in modules)
        {
            yield return new WaitUntil(() => module.state == ModuleState.compleate);
        }
    }


}
