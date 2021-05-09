using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class ElementForAllHumankind
{
    public List<SyncModuleBaseBase> key;
    public SyncStateFlag value;

    public ElementForAllHumankind(List<SyncModuleBaseBase> keys,SyncStateFlag flag){
        key = keys;
        value = flag;
    }
}