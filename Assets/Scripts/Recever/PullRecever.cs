using UnityEngine;

//Playerに引っ張られたことを感知し、Moduleに伝える
[RequireComponent(typeof(IReactableModule<Vector2Int>))]
public class PullRecever : MonoBehaviour, IAttractable
{
    IReactableModule<Vector2Int> motionModule;
    public ObjectAttribute attribute { get { return ObjectAttribute.isAttractable; } }

    void Start()
    {
        motionModule = GetComponent<IReactableModule<Vector2Int>>();
    }


    public bool Check(Vector2Int check)
    {
        return motionModule.Check(check);
    }

    public void Attract(AttractContext context, Vector2Int direction)
    {
        if (context == AttractContext.away)
        {
            motionModule.Reaction(direction);
        }
    }
}