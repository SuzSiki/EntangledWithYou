using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimention : MonoBehaviour,IRequireToLoad
{
    public List<ILoad> requireComponentList{get{return _req;} private set{_req = value;}}
    public bool loaded{get;private set;}
    public int dimentionID { get { return _dimentionID; } private set { dimentionID = value; } }
    public int myLayerMask{get;private set;}
    public Player player{get{return myPlayer;}}
    public IGoal goal{get;private set;}
    public bool active { get { return _active; } private set { _active = value; } }
    [SerializeField] int _dimentionID;
    List<ILoad> _req = new List<ILoad>();
    Player myPlayer;
    GameManager gameManager;
    [SerializeField] bool _active;
    public static List<Dimention> dimentionList = new List<Dimention>();


    void OnEnable()
    {
        dimentionList.Add(this);
    }

    void Start()
    {
        myPlayer = GetComponentInChildren<Player>();
        goal = GetComponentInChildren<IGoal>();
        myLayerMask = 1 << LayerMask.NameToLayer("Dimention"+dimentionID);
        gameManager = GameManager.instance;

        requireComponentList.Add(myPlayer);
        LoadManager.instance.RegisterLoad(this);
    }

    public void StartLoad(){
        loaded = true;
    }


    void Update()
    {
        if (active && gameManager.turnReady)
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        int x = (int)Input.GetAxisRaw("Horizontal");
        int y = (int)Input.GetAxisRaw("Vertical");

        if (x != 0 || y != 0)
        {
            if (myPlayer.Input(x, y))
            {

                gameManager.StartTurn();
            }
        }

        if(Input.GetKeyDown(KeyCode.R)){
            SEKAIModule.instance.DarkenTheWorld(SceneLoader.instance.Restart);
        }
    }

    public void SetActive(bool isActive)
    {
        active = isActive;
        myPlayer.Remap();
    }

    void OnDisable()
    {
        dimentionList.Remove(this);
    }
}
