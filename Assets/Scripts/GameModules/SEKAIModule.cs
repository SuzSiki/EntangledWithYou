using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;

public class SEKAIModule : SerializedSingleton<SEKAIModule>, ITurnModule, IRequireToLoad
{
    public ModuleState state { get; private set; }
    [SerializeField] int defaultDimention = 0;
    [SerializeField] bool setDefault = true;

    public bool loaded { get; private set; }
    public List<ILoad> requireComponentList { get { return requires; } }
    List<ILoad> requires = new List<ILoad>();
    CameraSwapper swapper;
    WorldFadeMotion fadeMotion;


    public void StartLoad()
    {
        swapper = CameraSwapper.instance;
        fadeMotion = WorldFadeMotion.instance;


        loaded = true;
        state = ModuleState.ready;

        if (setDefault)
        {
            SwapDimention(defaultDimention);
        }
    }


    void Start()
    {
        Dimention.dimentionList.ForEach(x => requires.Add(x));
        LoadManager.instance.RegisterLoad(this);
    }

    [Button]
    public bool SwapDimention(int dimentionID)
    {
        GameManager.instance.RegisterOnce(this);


        var activDim = Dimention.dimentionList.Find(x => x.active);

        if (activDim != null)
        {
            activDim.SetActive(false);
        }

        fadeMotion.Enter().onComplete += () =>
        {
            swapper.ShowWorld(dimentionID);
            fadeMotion.Exit().onComplete += () =>
            {
                state = ModuleState.compleate;
                Dimention.dimentionList.Find(x => x.dimentionID == dimentionID).SetActive(true);
            };
        };

        return true;
    }

    public void DarkenTheWorld(System.Action onCompleate = null)
    {
        Dimention.dimentionList.Find(x => x.active).SetActive(false);
        fadeMotion.Enter().onComplete += () =>
        {
            if (onCompleate != null)
            {
                onCompleate();
            }
        };

    }

}
