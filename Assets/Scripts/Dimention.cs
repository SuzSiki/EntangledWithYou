using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimention : MonoBehaviour
{
    public int dimentionID{get{return _dimentionID;} private set{dimentionID = value;}}
    [SerializeField] int _dimentionID;
    TestPlayer myPlayer;
    GameManager gameManager;
    public bool active{get{return _active;}private set{_active = value;}}
    [SerializeField] bool _active;

    void Start()
    {
        myPlayer = GetComponentInChildren<TestPlayer>();
        gameManager = GameManager.instance;
        if(active){
            gameManager.RegisterModule(myPlayer);
        }
    }


    void Update()
    {
        if(active){
            HandleInput();
        }
    }

    void HandleInput(){
        int x = (int)Input.GetAxisRaw("Horizontal");
        int y = (int)Input.GetAxisRaw("Vertical");

        if(x != 0 || y != 0){
            myPlayer.Input(x,y);
        }
    }

    public void SetActive(bool isActive){
        active = isActive;
        if(!isActive){
            gameManager.DisregisterModule(myPlayer);
        }
    }

}
