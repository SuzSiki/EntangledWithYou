using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 近づかれたら南下するやつ
/// </summary>
public interface IContactable:IFieldSurface,ICheckable<Vector2Int>
{
    /// <summary>
    /// 接触を通知
    /// </summary>
    /// <param name="context"></param>
    /// <param name="direction"></param>
    /// <returns>その行動が可能か？(実行の有無ではないので注意)</returns>
    void Contact(ContactContext context,Vector2Int direction);
}

/// <summary>
/// 接近の種類
/// </summary>
public enum ContactContext{
    none,
    stand  //同じマスに来た
}
