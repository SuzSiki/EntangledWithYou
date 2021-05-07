using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpriteMonitor : Singleton<SpriteMonitor>
{
    [SerializeField] int seido = 6;
    [SerializeField] int instances = 10;
    [SerializeField] TurnModuleTracker pref;
    InstantPool<TurnModuleTracker> trackerPool;
    List<ITurnModule> trackedList = new List<ITurnModule>();
    bool active = true;
    void Start()
    {
        trackerPool = new InstantPool<TurnModuleTracker>(transform);
        trackerPool.CreatePool(pref,instances);
        foreach (var dimention in Dimention.dimentionList)
        {
            var turnModules = dimention.GetComponentsInChildren<ITurnModule>();


            foreach(var module in turnModules){
                trackerPool.GetObj().Register(module,dimention);
            }
        }
    }
}
