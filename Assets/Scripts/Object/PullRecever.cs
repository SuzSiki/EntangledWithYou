using UnityEngine;

//Playerに引っ張られたことを感知し、Moduleに伝える
[RequireComponent(typeof(MotionModule))]
public class PullRecever : MonoBehaviour, IAttractable
{
    MotionModule motionModule;
    public ObjectAttribute attribute { get { return ObjectAttribute.isAttractable; } }

    void Start()
    {
        motionModule = GetComponent<MotionModule>();
    }


    public bool Check(Vector2Int check)
    {
        return motionModule.Check(check);
    }

    public void Attract(AttractContext context, Vector2Int direction)
    {
        if (context == AttractContext.away)
        {
            motionModule.Move(direction);
        }
    }
}