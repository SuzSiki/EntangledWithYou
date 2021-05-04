using UnityEngine;
using DG.Tweening;

public class LockableToggleAnimMotion : ToggleAnimMotion, ILockableToggleMotion
{
    public bool locked { get; private set; }



    public void Lock(bool ifLock, bool activate,System.Action onCompleate)
    {
        locked = ifLock;
        Sequence seq;

        if (ifLock)
        {
            seq = InitAnimSequence("Lock",onCompleate);
        }
        else{
            seq = InitAnimSequence("Unlock",onCompleate);
        }

        if(activate){
            seq.Play();
        }
    }


}