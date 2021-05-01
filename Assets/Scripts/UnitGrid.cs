using UnityEngine;
using UnityEngine.Tilemaps;

//ゲームの基本単位となるグリッドへのインターフェイスを設ける
public class UnitGrid:Singleton<UnitGrid>,ILoad
{
    public Grid grid{get;private set;}
    public bool loaded{get;private set;}
    
    protected override void Awake()
    {
        base.Awake();

        grid = GetComponent<Grid>();

        loaded = true;
    }
}