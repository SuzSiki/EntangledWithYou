using System;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ボタンのロックができ、ロックとその解除ができる
/// ただし、ボタン自体は指定できない
/// プレイヤーがトグルすることはできる。
/// </summary>
[RequireComponent(typeof(ILockableToggleMotion))]
public class LockablePushButton : ButtonModule
{
    [SerializeField] bool defaultLockState = true;
    bool locked;
    ILockableToggleMotion lockableThing;

    protected override void Start()
    {
        locked = defaultLockState;
        base.Start();
        lockableThing = (ILockableToggleMotion)toggleButton;
        if (locked == true)
        {
            lockableThing.Lock(true);
        }
    }

    public override bool Reaction(System.Action onCompleate = null)
    {
        if (locked)
        {
            if (onCompleate != null)
            {
                onCompleate();
            }
            return true;
        }

        base.Reaction(onCompleate);

        return true;
    }

    /// <summary
    /// ここではLockだけを同期させる
    /// </summary>
    /// <param name="isOn"></param>
    /// <param name="onCompleate"></param>
    /// <returns></returns>
    public override bool Command(bool isOn, Action onCompleate = null)
    {

        if (locked != isOn)
        {
            state = ModuleState.working;
            GameManager.instance.RegisterModule(this);
            locked = isOn;
            var toggle = toggleButton as ILockableToggleMotion;


            toggle.Lock(locked, true,
            () =>
            {
                state = ModuleState.compleate;
                if (onCompleate != null)
                {
                    onCompleate();
                }
            });
        }

        return true;
    }
}