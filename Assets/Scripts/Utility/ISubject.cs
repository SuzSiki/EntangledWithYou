public interface ISubject<T>
{
    bool AddObserver(IObserver<T> observer);
    bool RemoveObserver(IObserver<T> observer);

    bool Notice(T args);
} 