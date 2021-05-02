using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimention : MonoBehaviour
{
    public int dimentionID { get { return _dimentionID; } private set { dimentionID = value; } }
    [SerializeField] int _dimentionID;
    Player myPlayer;
    GameManager gameManager;
    public bool active { get { return _active; } private set { _active = value; } }
    [SerializeField] bool _active;

    void Start()
    {
        myPlayer = GetComponentInChildren<Player>();
        gameManager = GameManager.instance;
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
    }

    public void SetActive(bool isActive)
    {
        active = isActive;
    }

}
