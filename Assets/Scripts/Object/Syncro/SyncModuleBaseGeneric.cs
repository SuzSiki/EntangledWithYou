using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//型ごとの処理。
/// <summary>
/// SyncManagerとFieldObjectの仲介。
/// </summary>
public abstract class SyncModuleBase<T> : SyncModuleBaseBase, IObserver<T>
{


    List<InputMemory<T>> _memoryOfAction = new List<InputMemory<T>>();
    List<SyncModuleBase<T>> _syncedList = new List<SyncModuleBase<T>>();
    Dictionary<SyncModuleBase<T>, SyncStateFlag> syncStateDict = new Dictionary<SyncModuleBase<T>, SyncStateFlag>();
    List<ICommandableModule<T>> _selfModules = new List<ICommandableModule<T>>();

    void Start()
    {
        GetComponents<ICommandableModule<T>>(_selfModules);
        myDimention = GetComponentInParent<Dimention>();


        foreach (var module in _selfModules)
        {
            module.AddObserver(this);
        }
    }


    public override bool AddBrother(SyncModuleBaseBase rawModule, SyncStateFlag stateflag)
    {
        if (rawModule is SyncModuleBase<T> module && !_syncedList.Contains(module))
        {
            _syncedList.Add(module);
            syncStateDict[module] = stateflag;
            return true;
        }

        return false;
    }

    public override bool RemoveBrother(SyncModuleBaseBase rawModule)
    {
        if (rawModule is SyncModuleBase<T> module)
        {
            if (_syncedList.Contains(module))
            {
                _syncedList.Remove(module);
                syncStateDict.Remove(module);
                return true;
            }
            return false;
        }

        Debug.LogWarning("Syncronize fatally failed!");
        return false;
    }


    //自分のモジュールが動いたとき
    public virtual bool OnNotice(T arg)
    {
        var memory = new InputMemory<T>(arg, myDimention.dimentionID);
        _memoryOfAction.Add(memory);

        int syncFailure = 0;

        foreach (var module in _syncedList)
        {
            var mem = memory;

            if (syncStateDict[module] != SyncStateFlag.entangled)
            {
                mem = DSDistortion(memory);
            }

            var result = module.Sync(mem);

            if (!result) { syncFailure++; }
        }

        if (syncFailure != 0) { return false; }

        return true;
    }

    public bool Sync(InputMemory<T> memory)
    {
        if (_selfModules.All(x => x.Check(memory.value)))
        {
            _selfModules.ForEach(x => x.Command(memory.value));
            _memoryOfAction.Add(memory);
            return true;
        }

        return false;
    }

    /// <summary>
    /// ここで情報をゆがめる
    /// </summary>
    /// <param name="memory"></param>
    /// <returns></returns>
    protected abstract InputMemory<T> DSDistortion(InputMemory<T> memory);

    public override bool AddConnectionState(SyncModuleBaseBase rawModule, SyncStateFlag stateFlag)
    {
        if (rawModule is SyncModuleBase<T> module)
        {
            if (_syncedList.Contains(module))
            {
                var state = syncStateDict[module as SyncModuleBase<T>];
                FlagManager<SyncStateFlag>.AppendFlag(ref state, stateFlag);
                syncStateDict[module] = state;

                return true;
            }

            Debug.LogWarning("don't have connection!");
        }


        Debug.LogWarning("not same type!");
        return false;
    }

    public override bool SetConnectionState(SyncModuleBaseBase rawModule, SyncStateFlag stateFlag)
    {
        if (rawModule is SyncModuleBase<T> module)
        {
            if (_syncedList.Contains(module))
            {
                syncStateDict[module as SyncModuleBase<T>] = stateFlag;
                return true;
            }

            Debug.LogWarning("don't have connection!");
        }


        Debug.LogWarning("not same type!");
        return false;
    }

    public override bool ClearConnectionState(SyncModuleBaseBase rawModule)
    {
        if (rawModule is SyncModuleBase<T> module)
        {
            if (_syncedList.Contains(module))
            {
                syncStateDict[module as SyncModuleBase<T>] = 0;

                return true;
            }

            Debug.LogWarning("don't have connection!");
        }


        Debug.LogWarning("not same type!");
        return false;
    }
}
