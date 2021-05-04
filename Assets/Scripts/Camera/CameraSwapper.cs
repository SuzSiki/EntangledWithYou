using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// カメラが映す世界を変える
/// </summary>
[RequireComponent(typeof(Camera))]
public class CameraSwapper : Singleton<CameraSwapper>
{
    int defaultLayers;
    new Camera camera;
    string dimNameBase = "Dimention";
    PostProcessLayer PPLayer;

    protected override void Awake()
    {
        base.Awake();
        camera = GetComponent<Camera>();
        PPLayer = GetComponent<PostProcessLayer>();

        defaultLayers = camera.cullingMask;
    }

    public void ShowWorld(int dimention, bool hideElse = true)
    {
        if (hideElse)
        {
            camera.cullingMask = defaultLayers;
        }
        var DLM = 1 << LayerMask.NameToLayer(dimNameBase + dimention);
        camera.cullingMask |= DLM;
        PPLayer.volumeLayer = DLM;
    }

    public void HideWorld(int dimention)
    {
        camera.cullingMask &= ~1 << LayerMask.NameToLayer(dimNameBase + dimention);
    }
}
