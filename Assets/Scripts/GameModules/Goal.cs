using UnityEngine;

[RequireComponent(typeof(ICommandableModule<bool>))]
public class Goal:MonoBehaviour,IGoal,IObserver<bool>
{
    public bool accomplished{get;private set;}

    ICommandableModule<bool> buttonModule;

    void Start()
    {
        buttonModule = GetComponent<ICommandableModule<bool>>();
        buttonModule.AddObserver(this);
    }


    public bool OnNotice(bool state){
        Debug.LogWarning("called:" + state);
        accomplished = state;
        
        return true;
    }
}