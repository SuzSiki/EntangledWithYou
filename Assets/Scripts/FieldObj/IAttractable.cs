using UnityEngine;

//遠ざかれたら南下するやつ
public interface IAttractable:IFieldSurface,ICheckable<Vector2Int>{
    void Attract(AttractContext context,Vector2Int direction);
}

public enum AttractContext
{
    none,
    away, 　　//正面方向にいなくなった
    remove　 　//同じマスにいなくなった
}