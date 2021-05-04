public interface IObserver<T>
{
    bool OnNotice(T arg);
}

public interface IObserver
{
    bool OnNotice();
}