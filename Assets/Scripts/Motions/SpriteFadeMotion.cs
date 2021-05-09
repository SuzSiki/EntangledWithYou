using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SpriteFadeMotion : EnterExitModule
{

    [SerializeField] float inDuration = 1;
    [SerializeField] float outDuration = 1;
    [SerializeField] Ease inEase = Ease.InExpo;
    [SerializeField] Ease outEase = Ease.OutExpo;
    [SerializeField] float defaultAlpha = 1;

    [SerializeField] float maxFade = 1;
    protected SpriteRenderer panel;

    protected override void Initialize()
    {
        panel = GetComponentInChildren<SpriteRenderer>();
        var color = panel.color;
        color.a = defaultAlpha;
        panel.color = color;
    }

    public override Sequence Enter(float timescale = 1, bool activate = true)
    {
        var ENTER = base.EnterInitiailzer(timescale, activate);

        ENTER.Append(panel.DOFade(maxFade, inDuration).SetEase(inEase));

        if (activate)
        {
            ENTER.Play();
        }

        return ENTER;
    }


    public override Sequence Exit(float timescale = 1, bool activate = true)
    {
        var EXIT = base.ExitInitializer(timescale, activate);
        EXIT.Append(panel.DOFade(0, outDuration).SetEase(outEase));

        if (activate)
        {
            EXIT.Play();
        }

        return EXIT;
    }
}
