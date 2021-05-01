using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;


public abstract class GridMoveBase : MonoBehaviour, IRequireToLoad
{
    protected UnitGrid _unitGrid;
    RaycastHit2D[] rayHits;

    public bool loaded { get; private set; }
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

    

    protected virtual void Move(int x, int y, System.Action onMoveEnd = null)
    {
        var _direction = new Vector2Int(x, y);

        var moveLine = GetMoveVector(_direction);


        StartCoroutine(MoveCoroutine(moveLine, onMoveEnd));
    }

    //ここで詳しい動き方を実装する
    //今はただ瞬間移動するだけ
    protected virtual IEnumerator MoveCoroutine(Vector2 movevec, System.Action onMoveEnd)
    {
        transform.position += (Vector3)movevec;
        yield return null;

        if (onMoveEnd != null)
        {
            onMoveEnd();
        }
    }

    protected Vector2 GetMoveVector(Vector2Int direction)
    {
        return (Vector2)direction * _unitGrid.grid.cellSize.x;
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
            catch(System.NullReferenceException)
            {
                
            }
        }
    }

}