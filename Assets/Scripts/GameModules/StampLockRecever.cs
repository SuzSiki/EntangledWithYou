using UnityEngine;
using System;
using Sirenix.OdinInspector;

[RequireComponent(typeof(IToggleMotion))]
public class StampLockRecever : SubjectBehaviour<bool>, ICommandableModule<bool>,ITurnModule,IContactable
{
    [SerializeField] bool defaultLockState = true;
    public ObjectAttribute attribute{get{return ObjectAttribute.isContactable;}}
    public ModuleState state{get;private set;}
    [SerializeField]IToggleMotion togMotion;
    bool locked;

    void Start()
    {
        locked = defaultLockState;

        togMotion = GetComponent<IToggleMotion>();
        
        if (locked == true != togMotion.nowState)
        {
            togMotion.Toggle();
        }
    }

    
    public bool Check(Vector2Int direction){
        if(locked){
            return false;
        }
        else{
            return true;
        }
    }

    public void Contact(ContactContext context,Vector2Int direction){}
    
    public bool Check(bool isLocked){
        return true;
    }

    /// <summary
    /// ここではLockだけを同期させる
    /// </summary>
    /// <param name="isOn"></param>
    /// <param name="onCompleate"></param>
    /// <returns></returns>
    public bool Command(bool isOn, Action onCompleate = null)
    {
        if(isOn != locked){
            locked = isOn;
            togMotion.Toggle();
        }

        return true;
    }

}