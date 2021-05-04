using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    Tilemap _tilemap;

    protected Tilemap tilemap{get{return _tilemap;}}

    protected Tile[,] tileInfo { get { return _tileInfo; } private set { _tileInfo = value; } }

    Tile[,] _tileInfo;

    void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
        Debug.Log(_tilemap.cellBounds);

        Vector3Int placeVector = new Vector3Int();
        var origin = _tilemap.cellBounds.position;
        var size = _tilemap.cellBounds.size;

        //Foreachでxの配列を回したいので[y][x]という順番になっている。
        _tileInfo = new Tile[size.x, size.y];

        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                placeVector.Set(x + origin.x, y + origin.y, origin.z);
                var tile = _tilemap.GetTile<Tile>(placeVector);
                try
                {
                    _tileInfo[x, y] = tile;
                }
                catch (System.NullReferenceException)
                {
                    _tileInfo[x, y] = null;
                }
            }
        }

        LogTiles();
    }


    void LogTiles()
    {
        string result = "";

        for (int y = 0; y < _tileInfo.GetLength(1); y++)
        {
            for (int x = 0; x < _tileInfo.GetLength(0); x++)
            {
                try
                {
                    result += _tileInfo[x, y].name[0];
                }
                catch (System.NullReferenceException)
                {
                    result += "n";
                }
            }
            result += "\n";
        }
            
        Debug.Log(result);
    }
}
