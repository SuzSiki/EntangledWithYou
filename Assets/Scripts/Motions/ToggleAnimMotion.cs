using UnityEngine;
using DG.Tweening;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer),typeof(Animator))]
public class ToggleAnimMotion:MonoBehaviour,IToggleMotion
{
    public bool nowState{get{return _nowState;}}
    protected Animator animator;
    new SpriteRenderer renderer;
    bool _nowState = false;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>(); 
        animator = GetComponent<Animator>();
    }

    public void Toggle(bool activate = true,System.Action onCompleate = null)
    {
        var seq = InitAnimSequence("Trigger",onCompleate);

        _nowState = !nowState;

        if(activate){
            seq.Play();
        }
    }

    public virtual Sequence InitAnimSequence(string triggerString,System.Action onCompleate){
        var seq = DOTween.Sequence();
        seq.onPlay += () => StartCoroutine(WaitForAnimeEnd(seq,triggerString,onCompleate));

        return seq;
    }

    IEnumerator WaitForAnimeEnd(Sequence seq,string triggerString,System.Action onCompleate)
    {
        animator.SetTrigger(triggerString);
        var info = animator.GetCurrentAnimatorStateInfo(0);

        yield return new WaitWhile(()=>info.fullPathHash == animator.GetCurrentAnimatorStateInfo(0).fullPathHash );


        if(onCompleate != null){
            onCompleate();
        }
    }
}