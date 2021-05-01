
//直接命令が出せる。
public interface IGameModule<T>:ICheckable<T>,ISubject<T>
{
    bool Command(T arg);
}

public interface IGameModule:ICheckable
{
    bool Command();
}