using DG.Tweening;
using UnityEngine;
public interface IMoveMotion
{
    Tween Move(Vector2 vector,bool acitivate = true);
}