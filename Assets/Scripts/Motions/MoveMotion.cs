using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveMotion : MonoBehaviour,IMoveMotion
{
    [SerializeField] float duration = 0.5f;
    [SerializeField] Ease ease = Ease.OutBounce;

    void Awake()
    {
        DOTween.defaultAutoPlay = AutoPlay.None;
    }

    public Tween Move(Vector2 vector,bool acitivate = true){
        var MOVE = transform.DOMove((Vector2)transform.position + vector,duration).SetEase(ease);

        if(acitivate){
            MOVE.Play();
        }

        return MOVE;
    }
}
