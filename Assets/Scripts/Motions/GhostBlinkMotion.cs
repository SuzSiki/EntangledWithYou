using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class GhostBlinkMotion : EnterExitModule
{
    [SerializeField] float inDuration = 1;
    [SerializeField] float outDuration = 1;
    [SerializeField] Ease inEase = Ease.InExpo;
    [SerializeField] Ease outEase = Ease.OutExpo;
    [SerializeField] Color defaultColor = Color.black;

    SpriteRenderer sprite;

    protected override void Initialize()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = defaultColor;
    }

    public override Sequence Exit(float timescale = 1, bool activate = true)
    {
        var ENTER = base.ExitInitializer(timescale, activate);

        ENTER.Append(sprite.DOFade(1, inDuration).SetEase(inEase));

        if (activate)
        {
            ENTER.Play();
        }

        return ENTER;
    }

    public override Sequence Enter(float timescale = 1, bool activate = true)
    {
        var EXIT = base.EnterInitiailzer(timescale, activate);
        EXIT.Append(sprite.DOFade(0, outDuration).SetEase(outEase));

        if (activate)
        {
            EXIT.Play();
        }

        return EXIT;
    }


}
