using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// プレイヤーにPushされたことを感知し、モジュールに伝える
/// </summary>
[RequireComponent(typeof(IReactableModule<Vector2Int>))]
public class PushRecever :MonoBehaviour, IContactable
{
    IReactableModule<Vector2Int> motionModule;
    public ObjectAttribute attribute{get{return ObjectAttribute.isContactable;}}

    void Start()
    {
        motionModule = GetComponent<IReactableModule<Vector2Int>>();
    }


    public bool Check(Vector2Int check){
        return motionModule.Check(check);
    }

    public void Contact(ContactContext context, Vector2Int direction)
    {
        if (context == ContactContext.stand)
        {
            motionModule.Reaction(direction);
        }
    }
}
