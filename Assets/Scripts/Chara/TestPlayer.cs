using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
/*
[System.Obsolete("you heard \"use it wisely!\" ")]
public class TestPlayer : GridMoveBase, ITurnModule
{
    Dictionary<Vector2Int, List<IFieldSurface>> shuiKankyo = new Dictionary<Vector2Int, List<IFieldSurface>>()
    {
        {Vector2Int.down,null},
        {Vector2Int.up,null},
        {Vector2Int.right,null},
        {Vector2Int.left,null},
        {Vector2Int.zero,null}
    };


    /// <summary>
    /// かっこいい時間…ではなく、次の入力までの時間
    /// </summary>
    /// <value></value>
    [SerializeField] float coolTime = 1;

    public ModuleState state { get; private set; }


    GameManager gameManager;

    public override void StartLoad()
    {
        gameManager = GameManager.instance;
        base.StartLoad();

        SONAR(transform.position);
        state = ModuleState.ready;
    }

    protected override void Start()
    {
        requireComponentList.Add(GameManager.instance);
        base.Start();
    }

    public void Input(int x, int y)
    {
        if (gameManager.turnReady && state != ModuleState.working)
        {
            if (!(x == 1 && y == 1)) //斜め移動は許さない
            {
                if (Check(x, y))
                {
                    state = ModuleState.working;
                    Move(x, y);
                }
            }
        }

    }


    bool Check(int x, int y)
    {
        var _direction = new Vector2Int(x, y);
        var contact = shuiKankyo[_direction];
        var attract = shuiKankyo[_direction * -1];

        if (contact == null && attract == null) { return true; }

        if (contact.Any(con => con == null || con.attribute == ObjectAttribute.isSolid))
        {
            return false;
        }

        foreach (var con in contact)
        {
            if (con is IContactable acc)
            {
                var res = acc.Check(_direction);
                if (!res) { return false; }
            }
        }

        foreach (var atr in attract)
        {
            if (atr is IAttractable acc)
            {
                var res = acc.Check(_direction);
                if (!res) { return false; }
            }
        }

        return true;
    }


    /// <summary>
    /// 周囲環境をマー君
    /// </summary>
    void SONAR(Vector2 center)
    {
        shuiKankyo[Vector2Int.right] = TileSearchLazor(center, Vector2Int.right, shuiKankyo[Vector2Int.right]);
        shuiKankyo[Vector2Int.left] = TileSearchLazor(center, Vector2Int.left, shuiKankyo[Vector2Int.left]);
        shuiKankyo[Vector2Int.up] = TileSearchLazor(center, Vector2Int.up, shuiKankyo[Vector2Int.up]);
        shuiKankyo[Vector2Int.down] = TileSearchLazor(center, Vector2Int.down, shuiKankyo[Vector2Int.down]);
        shuiKankyo[Vector2Int.zero] = TileSearchLazor(center, Vector2Int.zero, shuiKankyo[Vector2Int.zero]);

        return;
    }




    void AttractOperation(Vector2Int direction)
    {

        var leaveBlock = shuiKankyo[direction * -1];
        if (leaveBlock != null)
        {
            var attracts = leaveBlock.FindAll(x => x is IAttractable);

            if (attracts.Count != 0)
            {
                foreach (var attract in attracts)
                {
                    var atr = (IAttractable)attract;
                    atr.Attract(AttractContext.away, direction);
                }
            }

        }

        leaveBlock = shuiKankyo[Vector2Int.zero];
        if (leaveBlock != null)
        {
            var attracts = leaveBlock.FindAll(x => x is IAttractable);
            if (attracts != null)
            {
                foreach (var attract in attracts)
                {
                    var atr = (IAttractable)attract;
                    atr.Attract(AttractContext.remove, direction);
                }
            }
        }

    }

    protected override void Move(int x, int y, System.Action action = null)
    {


        var _direction = new Vector2Int(x, y);

        AttractOperation(_direction);

        ContactOperation(_direction);

        gameManager.StartTurn();

        base.Move(x, y, action);
    }

    void ContactOperation(Vector2Int direction)
    {
        var contactBlock = shuiKankyo[direction];
        if (contactBlock != null)
        {
            var contacts = contactBlock.FindAll(x => x is IContactable);
            if (contacts != null)
            {
                foreach (var contact in contacts)
                {
                    var atr = (IContactable)contact;
                    atr.Contact(ContactContext.stand, direction);
                }
            }
        }
    }

    protected override IEnumerator MoveCoroutine(Vector2 moveVector, System.Action action)
    {
        yield return StartCoroutine(base.MoveCoroutine(moveVector, action));
        yield return new WaitForSeconds(coolTime);
        SONAR(transform.position);
        state = ModuleState.compleate;
    }
}
*/