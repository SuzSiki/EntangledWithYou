using UnityEngine;

/// <summary>
/// 特定のことが可能か調べられる
/// </summary>
public interface ICheckable<T>
{
    bool Check(T direction);
}

public interface ICheckable
{
    bool Check();
}