using UnityEngine;
using System.Collections.Generic;


public class SONAR : GridCheckBase
{
    public Dictionary<Vector2Int, List<IFieldSurface>> shuiKankyo{get{return _shuiKankyo;}}

    Dictionary<Vector2Int, List<IFieldSurface>> _shuiKankyo = new Dictionary<Vector2Int, List<IFieldSurface>>()
    {
        {Vector2Int.down,null},
        {Vector2Int.up,null},
        {Vector2Int.right,null},
        {Vector2Int.left,null},
        {Vector2Int.zero,null}
    };

    /// <summary>
    /// 周囲環境をマー君
    /// </summary>
    public Dictionary<Vector2Int, List<IFieldSurface>> Cast(Vector2 center)
    {
        shuiKankyo[Vector2Int.zero] = TileSearchLazor(center, Vector2Int.zero, shuiKankyo[Vector2Int.zero]);
        shuiKankyo[Vector2Int.right] = TileSearchLazor(center, Vector2Int.right, shuiKankyo[Vector2Int.right]);
        shuiKankyo[Vector2Int.left] = TileSearchLazor(center, Vector2Int.left, shuiKankyo[Vector2Int.left]);
        shuiKankyo[Vector2Int.up] = TileSearchLazor(center, Vector2Int.up, shuiKankyo[Vector2Int.up]);
        shuiKankyo[Vector2Int.down] = TileSearchLazor(center, Vector2Int.down, shuiKankyo[Vector2Int.down]);

        return shuiKankyo;
    }
}