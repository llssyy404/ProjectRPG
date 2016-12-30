using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnumyState : MonoBehaviour {
    enum EnumyBehavState { IDLE = 0, CHASE, PATROL, ATTACK, DEMAGED, DIE };
    EnumyBehavState eEnumyState = EnumyBehavState.IDLE;
    float patrolSpeed = 5.0f;
    float chaseSpeed = 7.0f;
    Transform[] cubeWayPoint;
    int curWayPoint = 0;
    bool IsDestination = true;
    private NavMeshAgent nav;

    public Transform PlayerTrans;
    public Transform wayPoint1;
    public Transform wayPoint2;
    public Transform wayPoint3;
    public Transform wayPoint4;

    // Use this for initialization
    void Start()
    {
        nav = this.gameObject.GetComponent<NavMeshAgent>();
        cubeWayPoint = new Transform[4];
        cubeWayPoint[0] = wayPoint1;
        cubeWayPoint[1] = wayPoint2;
        cubeWayPoint[2] = wayPoint3;
        cubeWayPoint[3] = wayPoint4;
        //for (int i = 0; i < 4; ++i)
        //{
        //    Debug.Log(cubeWayPoint[i].position);
        //}

        eEnumyState = EnumyBehavState.PATROL;
        nav.speed = patrolSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        SetEnumyStateByPlayerPos();         // 적 state 설정
        SetDestinationByEnumyState();       // state에 따른 목표위치 설정

        //Debug.Log(transform.position);
    }

    float GetDistanceByTargetPosition(Vector3 TargetPosition)
    {
        return Vector3.Distance(transform.position, TargetPosition);
    }

    void SetEnumyStateByPlayerPos()
    {
        float distance = GetDistanceByTargetPosition(PlayerTrans.transform.position);
        if (distance > 20.0f)
            eEnumyState = EnumyBehavState.PATROL;
        else if (distance < 5.0f)
            eEnumyState = EnumyBehavState.ATTACK;
        else
            eEnumyState = EnumyBehavState.CHASE;
        //Debug.Log(eEnumyState);
    }

    void SetDestinationByEnumyState()
    {
        switch (eEnumyState)
        {
            case EnumyBehavState.CHASE:
                {
                    nav.SetDestination(PlayerTrans.position);
                }
                break;
            case EnumyBehavState.PATROL:
                {
                    nav.SetDestination(cubeWayPoint[curWayPoint].transform.position);
                    if (GetDistanceByTargetPosition(cubeWayPoint[curWayPoint].transform.position) < 0.5f && false == IsDestination)
                        IsDestination = true;

                    if (false == IsDestination)
                        break;

                    ++curWayPoint;
                    if (4 == curWayPoint)
                        curWayPoint = 0;

                    nav.speed = patrolSpeed;
                    IsDestination = false;
                }
                break;
            default: break;
        }
    }
}
