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
public abstract class SyncModuleBase : MonoBehaviour
{
    protected Dimention myDimention;

    public abstract bool AddBrother(SyncModuleBase rawModule, SyncStateFlag stateflag);
    public abstract bool RemoveBrother(SyncModuleBase module);
    public abstract bool AddConnectionState(SyncModuleBase rawModule, SyncStateFlag stateFlag);
    public abstract bool SetConnectionState(SyncModuleBase rawModule, SyncStateFlag stateFlag);
    public abstract bool ClearConnectionState(SyncModuleBase rawModule);
}

//型ごとの処理。
/// <summary>
/// SyncManagerとFieldObjectの仲介。
/// </summary>
public abstract class SyncModuleBase<T> : SyncModuleBase, IObserver<T>
{


    List<InputMemory<T>> _memoryOfAction =  new List<InputMemory<T>>();
    List<SyncModuleBase<T>> _syncedList = new List<SyncModuleBase<T>>();
    Dictionary<SyncModuleBase<T>, SyncStateFlag> syncStateDict = new Dictionary<SyncModuleBase<T>, SyncStateFlag>();
    List<IGameModule<T>> _selfModules = new List<IGameModule<T>>();

    void Start()
    {
        GetComponents<IGameModule<T>>(_selfModules);
        myDimention = GetComponentInParent<Dimention>();
        
        
        foreach (var module in _selfModules)
        {
            module.AddObserver(this);
        }
    }


    public override bool AddBrother(SyncModuleBase rawModule, SyncStateFlag stateflag)
    {
        if (rawModule is SyncModuleBase<T> module && !_syncedList.Contains(module))
        {
            _syncedList.Add(module);
            syncStateDict[module] = stateflag;
            return true;
        }

        return false;
    }

    public override bool RemoveBrother(SyncModuleBase rawModule)
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
            
            if(syncStateDict[module] != SyncStateFlag.entangled){
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

    public override bool AddConnectionState(SyncModuleBase rawModule, SyncStateFlag stateFlag)
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

    public override bool SetConnectionState(SyncModuleBase rawModule, SyncStateFlag stateFlag)
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

    public override bool ClearConnectionState(SyncModuleBase rawModule)
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