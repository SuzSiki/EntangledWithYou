using DG.Tweening;
using UnityEngine;


public class ImageSlideInMotion:GraphicFadeMotion
{
    [SerializeField] Vector2 targetPosition;

    //あんま変えることないかも、初期化もしません
    [SerializeField] Vector2 homePosition = Vector2.zero;

    public override Sequence Enter(float timescale = 1, bool activate = true)
    {
        var ENTER = base.Enter(timescale, false);
        ENTER.Join(panel.transform.DOMove(targetPosition,inDuration).SetEase(inEase));

        if(activate){
            ENTER.Play();
        }

        return ENTER;
    }

    public override Sequence Exit(float timescale = 1, bool activate = true)
    {
        var EXIT = base.Exit(timescale, false);
        EXIT.Join(panel.transform.DOMove(homePosition,outDuration).SetEase(outEase));


        if(activate){
            EXIT.Play();
        }

        return EXIT;
    }
}