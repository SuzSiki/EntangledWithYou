using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// プレイヤーにPushされたことを感知し、モジュールに伝える
/// </summary>
[RequireComponent(typeof(MotionModule))]
public class PushRecever :MonoBehaviour, IContactable
{
    MotionModule motionModule;
    public ObjectAttribute attribute{get{return ObjectAttribute.isContactable;}}

    void Start()
    {
        motionModule = GetComponent<MotionModule>();
    }


    public bool Check(Vector2Int check){
        return motionModule.Check(check);
    }

    public void Contact(ContactContext context, Vector2Int direction)
    {
        if (context == ContactContext.stand)
        {
            motionModule.Move(direction);
        }
    }
}
