using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;


public class MotionModule : GridMoveBase, ITurnModule, ISubject<Vector2Int>, IReactableModule<Vector2Int>, ICommandableModule<Vector2Int>
{
    [SerializeField] bool allowOtoOInteraction = true;
    public ObjectAttribute attribute { get; protected set; }

    public ModuleState state { get; protected set; }
    protected GameManager gameManager;

    protected override void Start()
    {
        base.Start();

        if (gameManager == null)
        {
            gameManager = GameManager.instance;
        }

        gameManager = GameManager.instance;
        state = ModuleState.ready;
    }

    protected void RegisterOnce()
    {
        gameManager.RegisterOnce(this);
    }

    public bool Command(Vector2Int direction, System.Action onCompleate = null)
    {
        if (Check(direction))
        {
            state = ModuleState.working;

            Move(direction, onCompleate, true);
            return true;
        }

        return false;
    }

    public bool Reaction(Vector2Int direction, System.Action onCompleate = null)
    {
        Move(direction, onCompleate);
        return true;
    }

    protected void Move(Vector2Int direction, System.Action onCompleate = null, bool doNotNotice = false)
    {
        state = ModuleState.working;
        //我こそがTurnModule
        RegisterOnce();

        Move(direction.x, direction.y, () =>
        {
            if (onCompleate != null)
            {
                onCompleate();
            }
            state = ModuleState.compleate;
            if (!doNotNotice) { Notice(direction); }
        });
    }

    List<IFieldSurface> objList = new List<IFieldSurface>();

    /// <summary>
    /// overrideしない状態ではdirectionの方向にisSolidのobjがある場合
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public virtual bool Check(Vector2Int direction)
    {
        TileSearchLazor(this.transform.position, direction, ref objList);

        if (objList.Any(x => x.attribute == ObjectAttribute.isSolid))
        {
            return false;
        }

        //当初の目的以外に、これを使うと向こう側のplayerも箱を押せるようになる
        
        if (allowOtoOInteraction)
        {
            objList.FindAll(x => x is IContactable)
            .ForEach(x =>
            {
                var con = x as IContactable;
                con.Contact(ContactContext.stand,direction);
            });

        }

        return true;
    }

    List<IObserver<Vector2Int>> observerList = new List<IObserver<Vector2Int>>();
    public bool AddObserver(IObserver<Vector2Int> observer)
    {
        if (!observerList.Contains(observer))
        {
            observerList.Add(observer);
            return true;
        }
        return false;
    }

    public bool RemoveObserver(IObserver<Vector2Int> observer)
    {
        if (observerList.Contains(observer))
        {
            observerList.Remove(observer);
            return true;
        }

        return false;
    }

    public bool Notice(Vector2Int direction)
    {
        observerList.ForEach(x => x.OnNotice(direction));
        return true;
    }
}