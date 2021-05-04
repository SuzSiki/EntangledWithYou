using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

public class SEKAIModule : SerializedSingleton<SEKAIModule>, ITurnModule, IRequireToLoad
{
    public ModuleState state { get; private set; }
    [SerializeField] int? defaultDimention = null;
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

        if (defaultDimention != null)
        {
            SwapDimention(defaultDimention.Value);
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
        GameManager.instance.StartTurn();


        Dimention.dimentionList.Find(x => x.active).SetActive(false);

        fadeMotion.Enter().onComplete = () =>
        {
            swapper.ShowWorld(dimentionID);
            fadeMotion.Exit().onComplete += () =>
            {
                state = ModuleState.compleate;
            };
        };

        Dimention.dimentionList.Find(x => x.dimentionID == dimentionID).SetActive(true);

        return true;
    }

}
