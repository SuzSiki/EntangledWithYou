using DG.Tweening;
using UnityEngine;


public class TargetSpriteFadeMotion:SpriteFadeMotion
{
    public Sprite sprite{set{panel.sprite = value;}}
    public Vector2 position{set{transform.position = value;}}

}