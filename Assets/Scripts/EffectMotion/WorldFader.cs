using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WorldFader : EnterExitModule
{
    List<SpriteRenderer> graphicList = new List<SpriteRenderer>();

    [SerializeField] float enterDuration = 1.5f;
    [SerializeField] float exitDuration = 0.5f;
    [SerializeField] Ease enterEase = Ease.InBounce;
    [SerializeField] Ease exitEase = Ease.OutExpo;


    public override Sequence Enter(float timescale = 1, bool activate = true)
    {
        var ENTER = EnterInitiailzer(timescale, activate);

        foreach(var graphic in graphicList){
            ENTER.Join(graphic.DOFade(1,enterDuration));
        }

        ENTER.SetEase(enterEase);

        if(activate){
            ENTER.Play();
        }

        return ENTER;
    }

    protected override void Initialize()
    {
        var syncMods = GetComponentsInChildren<SyncModuleBase>();
        foreach(var mod in syncMods){
            graphicList.Add(mod.gameObject.GetComponent<SpriteRenderer>());
        }

        //Exit();
    }

    public override Sequence Exit(float timescale = 1, bool activate = true)
    {
        var EXIT = base.ExitInitializer(timescale, activate);

        foreach(var graphic in graphicList){
            EXIT.Join(graphic.DOFade(0,exitDuration));
        }

        EXIT.SetEase(exitEase);

        if(activate){
            EXIT.Play();
        }

        return EXIT;
    }
}
