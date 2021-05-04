using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Flags]
public enum SyncStateFlag
{
    normal = 0,
    entangled = 1 << 0
}



/// <summary>
/// シンクロさせるすべてのオブジェクトが持つ
/// 一つにまとめるためのインターフェイス。
/// serializeするためにabstractにした。
/// </summary>
public abstract class SyncModuleBaseBase : SubjectBehaviour
{
    protected Dimention myDimention;

    public abstract bool AddBrother(SyncModuleBaseBase rawModule, SyncStateFlag stateflag);
    public abstract bool RemoveBrother(SyncModuleBaseBase module);
    public abstract bool AddConnectionState(SyncModuleBaseBase rawModule, SyncStateFlag stateFlag);
    public abstract bool SetConnectionState(SyncModuleBaseBase rawModule, SyncStateFlag stateFlag);
    public abstract bool ClearConnectionState(SyncModuleBaseBase rawModule);
}

/*
public abstract class SyncModuleBase : SyncModuleBaseBase, IObserver
{
    List<int> _memoryOfDimention = new List<int>();
    List<SyncModuleBase> _syncedList = new List<SyncModuleBase>();
    Dictionary<SyncModuleBase, SyncStateFlag> syncStateDict = new Dictionary<SyncModuleBase, SyncStateFlag>();
    List<ICommandableModule> _selfModules = new List<ICommandableModule>();

    void Start()
    {
        GetComponents<ICommandableModule>(_selfModules);
        myDimention = GetComponentInParent<Dimention>();


        foreach (var module in _selfModules)
        {
            module.AddObserver(this);
        }
    }


    public override bool AddBrother(SyncModuleBaseBase rawModule, SyncStateFlag stateflag)
    {
        if (rawModule is SyncModuleBase module && !_syncedList.Contains(module))
        {
            _syncedList.Add(module);
            syncStateDict[module] = stateflag;
            return true;
        }

        return false;
    }

    public override bool RemoveBrother(SyncModuleBaseBase rawModule)
    {
        if (rawModule is SyncModuleBase module)
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
    public virtual bool OnNotice()
    {
        var memory = myDimention.dimentionID;
        _memoryOfDimention.Add(memory);

        int syncFailure = 0;

        foreach (var module in _syncedList)
        {
            var mem = memory;

            if (syncStateDict[module] != SyncStateFlag.entangled)
            {
                DSDistortion();
            }

            var result = module.Sync(mem);

            if (!result) { syncFailure++; }
        }

        if (syncFailure != 0) { return false; }

        return true;
    }
    public bool Sync(int memory)
    {
        if (_selfModules.All(x => x.Check()))
        {
            _selfModules.ForEach(x => x.Command());
            _memoryOfDimention.Add(memory);
            return true;
        }

        return false;
    }

    /// <summary>
    /// ここで情報をゆがめる
    /// </summary>
    /// <param name="memory"></param>
    /// <returns></returns>
    protected abstract bool DSDistortion();

    public override bool AddConnectionState(SyncModuleBaseBase rawModule, SyncStateFlag stateFlag)
    {
        if (rawModule is SyncModuleBase module)
        {
            if (_syncedList.Contains(module))
            {
                var state = syncStateDict[module as SyncModuleBase];
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
        if (rawModule is SyncModuleBase module)
        {
            if (_syncedList.Contains(module))
            {
                syncStateDict[module as SyncModuleBase] = stateFlag;
                return true;
            }

            Debug.LogWarning("don't have connection!");
        }


        Debug.LogWarning("not same type!");
        return false;
    }

    public override bool ClearConnectionState(SyncModuleBaseBase rawModule)
    {
        if (rawModule is SyncModuleBase module)
        {
            if (_syncedList.Contains(module))
            {
                syncStateDict[module as SyncModuleBase] = 0;

                return true;
            }

            Debug.LogWarning("don't have connection!");
        }


        Debug.LogWarning("not same type!");
        return false;
    }
}
*/

public struct InputMemory<M>
{
    //記憶の中身
    public M value { get; set; }
    //どこの次元の記憶か
    public int dimention { get; private set; }

    public InputMemory(M val, int D)
    {
        value = val;
        dimention = D;
    }
}