using UnityEngine;

[RequireComponent(typeof(IToggleMotion))]
public class DoorCommandable : SubjectBehaviour<bool>, ICommandableModule<bool>,IObserver,IFieldSurface
{
    public ObjectAttribute attribute{get{return _attribute;}}
    ObjectAttribute _attribute;

    IToggleMotion toggleMotion;

    void Start()
    {
        toggleMotion = GetComponent<IToggleMotion>();
    }

    public bool Command(bool isOn, System.Action onCompleate)
    {
        if(isOn){
            FlagManager<ObjectAttribute>.AppendFlag(ref _attribute,ObjectAttribute.isSolid);
        }
        else{
            FlagManager<ObjectAttribute>.RemoveFlag(ref _attribute,ObjectAttribute.isSolid);
        }

        if(isOn != toggleMotion.nowState){
            toggleMotion.Toggle();
        }
        
        return true;
    }

    /// <summary>
    /// 内部処理向け
    /// </summary>
    /// <param name="isOn"></param>
    /// <returns></returns>
    public bool Check(bool isOn)
    {
        return true;
    }

    public bool OnNotice(){
        throw new System.NotImplementedException();
    }

}