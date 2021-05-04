using UnityEngine;

//直接命令が出せる。
//一つのコンポーネントに同じ型のmoduleは一つの想定。
//内部向けのインターフェイス
public interface ICommandableModule<T> : ICheckable<T>, ISubject<T>
{
    bool Command(T arg, System.Action onCompleate = null);
}

//GameModuleの外部向け
public interface IReactableModule<T> : ICheckable<T>
{
    bool Reaction(T arg, System.Action onCompleate = null);
}

public interface ICommandableModule: ICheckable, ISubject
{
    bool Command(System.Action onCompleate = null);
}

//GameModuleの外部向け
public interface IReactableModule : ICheckable
{
    bool Reaction(System.Action onCompleate = null);
}