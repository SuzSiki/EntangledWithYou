using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IToggleMotion))]
public class ButtonModule : SubjectBehaviour<bool>, ITurnModule, IReactableModule, ICommandableModule<bool>
{
    [SerializeField] bool defaultState = true;
    protected IToggleMotion toggleButton;
    public ModuleState state { get; protected set; }



    protected virtual void Start()
    {
        toggleButton = GetComponent<IToggleMotion>();
        if (toggleButton.nowState != defaultState)
        {
            Reaction();
        }

        state = ModuleState.ready;
    }



    public bool Check()
    {
        return true;
    }

    public bool Check(bool isOn)
    {
        return true;
    }

    public virtual bool Reaction(System.Action onCompleate = null)
    {
        state = ModuleState.working;

        GameManager.instance.RegisterOnce(this);

        toggleButton.Toggle(onCompleate: () =>
        {
            state = ModuleState.compleate;
            if (onCompleate != null)
            {
                onCompleate();
            }
            Notice(toggleButton.nowState);
        });

        return true;
    }

    public virtual bool Command(bool isOn, System.Action onCompleate = null)
    {
        if (toggleButton.nowState != isOn)
        {
            state = ModuleState.working;
            GameManager.instance.RegisterOnce(this);
            
            toggleButton.Toggle(onCompleate: () =>
            {
                if (onCompleate != null)
                {
                    onCompleate();
                }
                state = ModuleState.compleate;
            });
        }

        //なんもしてなくても失敗ではない
        return true;
    }

}