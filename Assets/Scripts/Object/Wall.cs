using UnityEngine;
using UnityEngine.Tilemaps;

public class Wall:MonoBehaviour,IFieldSurface
{
    public ObjectAttribute attribute{get{return ObjectAttribute.isSolid;}}

}