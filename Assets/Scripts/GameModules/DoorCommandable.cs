using UnityEngine;

[RequireComponent(typeof(IToggleMotion))]
public class DoorCommandable : SubjectBehaviour<bool>, ICommandableModule<bool>,IObserver,IReactableModule<Vector2Int>
{

    IToggleMotion toggleMotion;

    void Start()
    {
        toggleMotion = GetComponent<IToggleMotion>();
    }

    public bool Command(bool isOn, System.Action onCompleate)
    {
        if(isOn != toggleMotion.nowState){
            toggleMotion.Toggle(onCompleate:onCompleate);
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

    public bool Reaction(Vector2Int direction,System.Action onCompleate = null){
        if(onCompleate!=null){
            onCompleate();
        }
        
        return true;
    }

    public bool Check(Vector2Int direction){
        if(toggleMotion.nowState){
            return true;
        }
        return false;
    }


    public bool OnNotice(){
        throw new System.NotImplementedException();
    }

}