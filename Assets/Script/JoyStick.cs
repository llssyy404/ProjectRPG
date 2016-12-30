using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick : MonoBehaviour
{

    public Transform stick;
    public Vector3 axis;

    float radius;
    Vector3 defaultCenter;
    Touch myTouch;

    void Start()
    {
        radius = GetComponent<RectTransform>().sizeDelta.x / 4;
        defaultCenter = stick.position;
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

        Player.Instance.PlayMove(axis);
    }
    public void End()
    {
        stick.position = defaultCenter;
    }


}
