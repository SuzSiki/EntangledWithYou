using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HaisenController : MapManager
{
    List<AnimatedTile> haisenList;
    
    void Start()
    {
        throw new System.NotImplementedException();
    }

    void InitHaisen(){
        foreach(TileBase tile in tileInfo){
            if(tile is AnimatedTile haisen)
            {
                haisenList.Add(haisen);
            }
        }

        
        foreach(var haisen in haisenList){
            
        }
    }
}
