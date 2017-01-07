using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick : MonoBehaviour
{
    public static JoyStick instance = null;
    public Transform stick;
    public Vector3 axis;

    float radius;
    Vector3 defaultCenter;
    Touch myTouch;

    private GameManager gameMgr;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        gameMgr = GameManager.GetInstance();
        radius = GetComponent<RectTransform>().sizeDelta.x / 8;
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
        Player.Instance.PS = PlayerState.RUN;

    }
    public void End()
    {
        Player.Instance.PS = PlayerState.IDEL;
        stick.position = defaultCenter;
    }


}
