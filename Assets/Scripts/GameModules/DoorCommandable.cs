using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

[RequireComponent(typeof(IToggleMotion), typeof(GridCheckBase))]
public class DoorCommandable : SubjectBehaviour<bool>, ICommandableModule<bool>, IObserver, IReactableModule
{

    IToggleMotion toggleMotion;
    GridCheckBase checker;
    List<IFieldSurface> mySurfaceList = new List<IFieldSurface>();

    void Start()
    {
        checker = GetComponent<GridCheckBase>();
        toggleMotion = GetComponent<IToggleMotion>();
        StartCoroutine(Initialize());
    }

    IEnumerator Initialize()
    {

        yield return new WaitUntil(() => checker.loaded);
        checker.TileSearchLazor(this.transform.position, Vector2Int.zero, ref mySurfaceList);
    }

    public bool Command(bool isOn, System.Action onCompleate)
    {
        if (isOn != toggleMotion.nowState)
        {
            toggleMotion.Toggle(onCompleate: onCompleate);
        }

        return true;
    }

    /// <summary>
    /// 内部処理向け
    /// </summary>
    /// <param name="isOn"></param>
    /// <returns></returns>
    public bool Check(bool isOn)
    {
        List<IFieldSurface> surfaces = new List<IFieldSurface>();
        checker.TileSearchLazor(this.transform.position, Vector2Int.zero, ref surfaces);
        surfaces = surfaces.Except(mySurfaceList).ToList();
        if (surfaces.Count == 0)
        {
            return true;
        }
        Debug.Log("found something");
        return false;
    }

    public bool Reaction(System.Action onCompleate = null)
    {
        if (onCompleate != null)
        {
            onCompleate();
        }

        return true;
    }

    public bool Check()
    {
        if (toggleMotion.nowState)
        {
            return true;
        }
        return false;
    }


    public bool OnNotice()
    {
        throw new System.NotImplementedException();
    }

}