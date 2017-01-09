using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnumyState : MonoBehaviour
{
    //private EnemyInfo _enemyInfo;
    enum EnumyBehavState { IDLE = 0, CHASE, PATROL, ATTACK, DEMAGED, DIE };
    EnumyBehavState eEnumyState = EnumyBehavState.IDLE;
    float patrolSpeed = 5.0f;
    float chaseSpeed = 7.0f;
    Vector3[] m_wayPoint;
    int curWayPoint = 0;
    bool IsDestination = true;
    private NavMeshAgent nav;

    private Animator anim;

    public Transform PlayerTrans;
    //public Transform wayPointFirst;
    Vector3 wayPointFirst;
    private int _hp = 100;
    private int _demage = 10;
    private const float m_wayPointWidth = 112.0f;
    private const float m_wayPointHeight = 97.0f;
    private const string m_strIsChase = "IsChase";
    private const string m_strIsAttack = "IsAttack";
    private const string m_strDie = "Die";
    private const string m_strWalk = "Wake";
    private const string m_strRun = "Run";
    private const string m_strAttack = "Attack";

    // Use this for initialization
    void Start()
    {
        PlayerTrans = GameManager.GetInstance().GetPlayer().transform;
        wayPointFirst = transform.position;
        anim = GetComponent<Animator>();
        nav = this.gameObject.GetComponent<NavMeshAgent>();
        m_wayPoint = new Vector3[4];
        m_wayPoint[0] = wayPointFirst;
        m_wayPoint[1] = new Vector3(wayPointFirst.x, wayPointFirst.y, wayPointFirst.z + m_wayPointHeight);
        m_wayPoint[2] = new Vector3(wayPointFirst.x + m_wayPointWidth, wayPointFirst.y, wayPointFirst.z + m_wayPointHeight);
        m_wayPoint[3] = new Vector3(wayPointFirst.x + m_wayPointWidth, wayPointFirst.y, wayPointFirst.z);
        eEnumyState = EnumyBehavState.PATROL;
        nav.speed = patrolSpeed;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Player" )
        {
            SphereCollider scoll = GetComponent<SphereCollider>();
            ResourceManager.GetInstance().MakeParticle(scoll.transform.position, "Effect_02", 2.0f);
            SoundManager.GetInstance().PlayOneshotClip("sword");
            Player player = GameManager.GetInstance().GetPlayer();
            player.DamageHp(_demage);
            Damaged(_demage);
            //Debug.Log(player.hp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (0 >= _hp && EnumyBehavState.DIE != eEnumyState)
        {
            anim.SetBool("IsChase", false);
            anim.Play("Die");
            eEnumyState = EnumyBehavState.DIE;
            nav.Stop();
            return;
        }

        SetEnumyStateByPlayerPos();         // 적 state 설정
        SetDestinationByEnumyState();       // state에 따른 목표위치 설정
        //Debug.Log(transform.position);
    }

    void Init()
    {
        IsDestination = true;
        nav.speed = patrolSpeed;
        _hp = 100;
        curWayPoint = 0;
    }

    void WayPointInit()
    {
        m_wayPoint[0] = wayPointFirst;
        m_wayPoint[1] = new Vector3(wayPointFirst.x, wayPointFirst.y, wayPointFirst.z + m_wayPointHeight);
        m_wayPoint[2] = new Vector3(wayPointFirst.x + m_wayPointWidth, wayPointFirst.y, wayPointFirst.z + m_wayPointHeight);
        m_wayPoint[3] = new Vector3(wayPointFirst.x + m_wayPointWidth, wayPointFirst.y, wayPointFirst.z);
    }

    float GetDistanceByTargetPosition(Vector3 TargetPosition)
    {
        return Vector3.Distance(transform.position, TargetPosition);
    }

    void SetAnimation(bool isChase, bool isAttack, string strPlayAnim)
    {
        anim.SetBool(m_strIsChase, isChase);
        anim.SetBool(m_strIsAttack, isAttack);
        anim.Play(strPlayAnim);
    }

    void SetEnumyStateByPlayerPos()
    {
        if (EnumyBehavState.DIE == eEnumyState)
            return;

        float distance = GetDistanceByTargetPosition(PlayerTrans.transform.position);
        if (distance > 50.0f)
        {
            if (EnumyBehavState.PATROL == eEnumyState)
                return;

            SetAnimation(false, false, m_strWalk);
            eEnumyState = EnumyBehavState.PATROL;
        }
        else if (distance < 5.0f)
        {
            if (EnumyBehavState.ATTACK == eEnumyState)
                return;

            SetAnimation(false, true, m_strAttack);
            nav.speed = 0.0f;
            eEnumyState = EnumyBehavState.ATTACK;
        }
        else
        {
            if (EnumyBehavState.CHASE == eEnumyState)
                return;

            SetAnimation(true, false, m_strRun);
            eEnumyState = EnumyBehavState.CHASE;
        }
    }

    void Patroll()
    {
        nav.SetDestination(m_wayPoint[curWayPoint]);
        nav.speed = patrolSpeed;
        if (GetDistanceByTargetPosition(m_wayPoint[curWayPoint]) < 2.0f && false == IsDestination)
            IsDestination = true;

        if (false == IsDestination)
            return;

        ++curWayPoint;
        if (4 == curWayPoint)
            curWayPoint = 0;

        IsDestination = false;
    }

    void SetDestinationByEnumyState()
    {
        if (EnumyBehavState.DIE == eEnumyState)
            return;

        switch (eEnumyState)
        {
            case EnumyBehavState.CHASE:
                {
                    nav.speed = chaseSpeed;
                    nav.SetDestination(PlayerTrans.position);
                }
                break;
            case EnumyBehavState.PATROL:
                {
                    Patroll();
                }
                break;
            default: break;
        }
    }

    void Damaged(int damage)
    {
        _hp -= damage;
        _objectUI.DamageHp(damage);
        if (0 > _hp)
            _hp = 0;
    }

    public int hp
    {
        get { return _hp; }
    }

    private ObjectUI _objectUI;
    public void SetHpBar(ObjectUI objectUI)
    {
        _objectUI = objectUI;
        _objectUI.Init(_hp);
    }
}
