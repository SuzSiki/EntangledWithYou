using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(MotionModule), typeof(SONAR))]
public class Player : MonoBehaviour, IRequireToLoad
{
    public List<ILoad> requireComponentList { get; private set; }
    public bool loaded { get; private set; }

    void Start()
    {
        sonar = GetComponent<SONAR>();
        movementModule = GetComponent<MotionModule>();

        requireComponentList = new List<ILoad>();
        requireComponentList.Add(sonar);
        LoadManager.instance.RegisterLoad(this);
    }


    public void StartLoad()
    {
        sonar.Cast(transform.position);
        state = ModuleState.ready;
        loaded = true;
    }


    Dictionary<Vector2Int, List<IFieldSurface>> shuiKankyo
    {
        get{return sonar.shuiKankyo;}
    }

    public ModuleState state { get; private set; }

    SONAR sonar;
    MotionModule movementModule;

    public bool Input(int x, int y)
    {
        if (state != ModuleState.working)
        {
            if (!(x == 1 && y == 1)) //斜め移動は許さない
            {
                if (Check(x, y))
                {
                    state = ModuleState.working;
                    Move(x, y, () => {
                        sonar.Cast(transform.position);
                        state = ModuleState.compleate;
                    });
                    return true;
                }
            }
        }

        return false;
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

    protected void Move(int x, int y, System.Action action = null)
    {
        var _direction = new Vector2Int(x, y);

        AttractOperation(_direction);

        ContactOperation(_direction);

        movementModule.Move(_direction, action);
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
}