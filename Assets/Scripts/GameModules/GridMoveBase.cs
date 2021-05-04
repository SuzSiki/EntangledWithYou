using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

[RequireComponent(typeof(IMoveMotion))]
public abstract class GridMoveBase : GridCheckBase
{
    IMoveMotion motion;

    protected override void Start()
    {
        base.Start();
        motion = GetComponent<IMoveMotion>();
    }

    protected virtual void Move(int x, int y, System.Action onMoveEnd = null)
    {
        var _direction = new Vector2Int(x, y);

        var moveLine = GetMoveVector(_direction);


        Move(moveLine, onMoveEnd);
    }

    //ここで詳しい動き方を実装する
    //今はただ瞬間移動するだけ
    void Move(Vector2 movevec, System.Action onMoveEnd)
    {
        motion.Move(movevec).onComplete += ()=>onMoveEnd();
    }

    protected Vector2 GetMoveVector(Vector2Int direction)
    {
        return (Vector2)direction * _unitGrid.grid.cellSize.x;
    }
}