using UnityEngine;
using System.Collections.Generic;

public class SyncronizeManager : SerializedSingleton<SyncronizeManager>,ILoad
{
    [SerializeField] Dictionary<List<SyncModuleBase>, SyncStateFlag> syncronizeList;
    public bool loaded{get;private set;}

    void Start()
    {
        foreach (var bros in syncronizeList)
        {
            SyncronizeAll(bros.Key,bros.Value);
        }
        loaded = true;
    }

    public void Syncronize(SyncModuleBase moduleA, SyncModuleBase moduleB, SyncStateFlag stateFlag)
    {
        moduleA.AddBrother(moduleB, stateFlag);
        moduleB.AddBrother(moduleA, stateFlag);
    }

    protected void SyncronizeAll(List<SyncModuleBase> modules, SyncStateFlag stateFlag)
    {
        foreach(var moduleA in modules){
            foreach(var moduleB in modules){
                if(moduleA != moduleB){
                    Syncronize(moduleA,moduleB,stateFlag);                                                                                                                                                                         
                }
            }
        }
    }

}

