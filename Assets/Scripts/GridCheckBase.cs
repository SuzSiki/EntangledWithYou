using UnityEngine;
using System.Collections.Generic;

public class GridCheckBase : MonoBehaviour, IRequireToLoad
{
    protected UnitGrid _unitGrid;

    public bool loaded { get; private set; }

      RaycastHit2D[] rayHits;


    public List<ILoad> requireComponentList { get { return reqc; } private set { reqc = value; } }
    List<ILoad> reqc = new List<ILoad>();

    public virtual void StartLoad()
    {
        _unitGrid = UnitGrid.instance;
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

    protected void TileSearchLazor(Vector2 centor, Vector2Int direction, ref List<IFieldSurface> fieldObjects)
    {
        if (fieldObjects != null)
        {
            fieldObjects.Clear();
        }
        else
        {
            fieldObjects = new List<IFieldSurface>();
        }

        var rayLine = (Vector2)direction * _unitGrid.grid.cellSize.x;
        Debug.DrawRay(centor, rayLine, Color.red, 2);
        rayHits = Physics2D.RaycastAll(centor, direction, _unitGrid.grid.cellSize.x);

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
