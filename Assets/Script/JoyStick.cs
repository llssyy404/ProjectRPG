using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick : MonoBehaviour
{
    public Transform stick;
    public Transform stickPad;
    public Vector3 axis;

    float radius;
    Vector3 defaultCenter;
    Touch myTouch;

    private GameManager gameMgr;

    private Player _player;

    void Start()
    {
        gameMgr = GameManager.GetInstance();
        //radius = GetComponent<RectTransform>().sizeDelta.x / 4;
        radius = stickPad.GetComponent<RectTransform>().sizeDelta.x / 2;
        defaultCenter = stick.position;

    }
    void Update()
    {
        
    }
    public void Move()
    {

        Vector3 touchPos = Input.mousePosition;
        axis = (touchPos - defaultCenter).normalized;

        float Distance = Vector3.Distance(touchPos, defaultCenter);

        if (Distance > radius)
        {
            stick.position = defaultCenter + axis * radius;
        }
        else
        {
            stick.position = defaultCenter + axis * Distance;
        }

        //Debug.Log("조이스틱 : " + stick.position + " ... " + radius);
        gameMgr.SetJoystickVector(false, axis);
       // _player.PS = PlayerState.RUN;
        // Player.Instance.PS = PlayerState.RUN;

    }
    public void End()
    {
        gameMgr.SetJoystickVector(true, axis);
        //_player.PS = PlayerState.IDEL;
        //Player.Instance.PS = PlayerState.IDEL;
        stick.position = defaultCenter;
    }


}
