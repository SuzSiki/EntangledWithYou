public interface IObserver<T>
{
    bool OnNotice(T arg);
}