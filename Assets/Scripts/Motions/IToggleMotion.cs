using DG.Tweening;

public interface IToggleMotion
{
    bool nowState{get;}
    void Toggle(bool activate = true,System.Action onCompleate = null);
}

public interface ILockableToggleMotion:IToggleMotion
{
    bool locked{get;}

    void Lock(bool ifLock,bool activate = true,System.Action onConpleate = null);
}