public class SyncModuleBool:SyncModuleBase<bool>
{
    protected override InputMemory<bool> DSDistortion(InputMemory<bool> memory)
    {
        memory.value = !memory.value;
        return memory;
    }
}