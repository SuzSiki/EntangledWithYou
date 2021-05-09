using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class SyncronizeManager : SerializedSingleton<SyncronizeManager>, ILoad
{
    [SerializeField] DictionaryForAllHumankind _syncronizeList;
    public bool loaded { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        foreach (var bros in _syncronizeList.dictionary)
        {
            SyncronizeAll(bros.key, bros.value);
        }

        loaded = true;
    }

    public void Syncronize(SyncModuleBaseBase moduleA, SyncModuleBaseBase moduleB, SyncStateFlag stateFlag)
    {
        moduleA.AddBrother(moduleB, stateFlag);
        moduleB.AddBrother(moduleA, stateFlag);
    }

    protected void SyncronizeAll(List<SyncModuleBaseBase> modules, SyncStateFlag stateFlag)
    {
        foreach (var moduleA in modules)
        {
            foreach (var moduleB in modules)
            {
                if (moduleA != moduleB)
                {
                    Syncronize(moduleA, moduleB, stateFlag);
                }
            }
        }
    }

}
