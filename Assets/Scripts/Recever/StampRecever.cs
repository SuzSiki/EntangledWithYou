using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ボタンギミックの実装に使える
/// </summary>
[RequireComponent(typeof(IReactableModule))]
public class StampRecever : MonoBehaviour, IContactable
{
    public ObjectAttribute attribute { get { return ObjectAttribute.isContactable; } }
    IReactableModule StampModule;

    void Start()
    {
        StampModule = GetComponent<IReactableModule>();
    }

    public bool Check(Vector2Int direction)
    {
        return StampModule.Check();
    }

    public void Contact(ContactContext context, Vector2Int direction)
    {
        if (context == ContactContext.stand)
        {
            StampModule.Reaction();
        }
    }
}
