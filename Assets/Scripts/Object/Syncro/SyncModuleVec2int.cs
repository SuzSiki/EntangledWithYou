using UnityEngine;

public class SyncModuleVec2int:SyncModuleBase<Vector2Int>
{
    protected override InputMemory<Vector2Int> DSDistortion(InputMemory<Vector2Int> memory)
    {
        memory.value = memory.value*-1;
        return memory;
    }
}