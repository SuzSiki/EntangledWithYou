using UnityEngine;
using System.Collections.Generic;

public class GridCheckBase : MonoBehaviour, IRequireToLoad
{
    protected UnitGrid _unitGrid;

    public bool loaded { get; private set; }

    RaycastHit2D[] rayHits;

    //何でこいつだけ知ってんの！？！？
    Dimention dimention;


    public List<ILoad> requireComponentList { get { return reqc; } private set { reqc = value; } }
    List<ILoad> reqc = new List<ILoad>();

    public virtual void StartLoad()
    {
        _unitGrid = UnitGrid.instance;
        dimention = GetComponentInParent<Dimention>();
        loaded = true;
    }

    protected virtual void Start()
    {
        requireComponentList.Add(UnitGrid.instance);
        LoadManager.instance.RegisterLoad(this);
    }


    protected List<IFieldSurface> TileSearchLazor(Vector2 centor, Vector2Int direction, List<IFieldSurface> fieldObjects)
    {
        TileSearchLazor(centor, direction, ref fieldObjects);
        return fieldObjects;
    }

    KeyValuePair<Vector2, List<IFieldSurface>> centerObj = new KeyValuePair<Vector2, List<IFieldSurface>>();
    public void TileSearchLazor(Vector2 center, Vector2Int direction, ref List<IFieldSurface> fieldObjects)
    {
        if (fieldObjects == null)
        {
            fieldObjects = new List<IFieldSurface>();
        }
        else if (fieldObjects.Count != 0)
        {
            fieldObjects.Clear();
        }


        if (direction == Vector2Int.zero)
        {
            ResetCenter(center);
        }
        else if (centerObj.Key != center)
        {
            //中心を再設定。
            ResetCenter(center);
            fieldObjects.Clear();
        }

        var rayLine = (Vector2)direction * _unitGrid.grid.cellSize.x;
        Debug.DrawRay(center, rayLine, Color.red, 2);

        CastRay(center, direction, ref fieldObjects);

        if (direction != Vector2Int.zero)
        {
            foreach (var obj in centerObj.Value)
            {
                fieldObjects.Remove(obj);
            }
        }

    }

    void ResetCenter(Vector2 center)
    {
        List<IFieldSurface> objList = new List<IFieldSurface>();
        CastRay(center, Vector2Int.zero, ref objList);
        centerObj = new KeyValuePair<Vector2, List<IFieldSurface>>(center, objList);
    }


    void CastRay(Vector2 centor, Vector2Int direction, ref List<IFieldSurface> fieldObjects)
    {
        rayHits = Physics2D.RaycastAll(centor, direction, _unitGrid.grid.cellSize.x,dimention.myLayerMask);
        Debug.DrawRay(centor,(Vector2)direction,Color.red,2);

        foreach (var hit in rayHits)
        {
            try
            {
                foreach (IFieldSurface obj in hit.collider.gameObject.GetComponents<IFieldSurface>())
                {
                    fieldObjects.Add(obj);
                }
            }
            catch (System.NullReferenceException)
            {

            }
        }
    }
}
