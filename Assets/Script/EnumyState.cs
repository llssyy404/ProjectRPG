using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnumyState : MonoBehaviour
{
    enum EnemyBehavState { IDLE = 0, CHASE, PATROL, ATTACK, DEMAGED, DIE };
    EnemyBehavState eEnemyState = EnemyBehavState.IDLE;
    Vector3[] wayPoint;
    int curWayPoint = 0;
    bool isDestination = true;
    int wayPointCount = 4;
    private Transform playerTrans;
    private NavMeshAgent nav;

    private Animator anim;

    private int _hp = 100;
    private int _demage = 10;
    private const int maxHP = 100;
    private const float patrolSpeed = 5.0f;
    private const float chaseSpeed = 7.0f;
    private const float chaseDistance = 50.0f;
    private const float attackDistance = 5.0f;
    private const float destDistance = 2.0f;
    private const float wayPointWidth = 112.0f;
    private const float wayPointHeight = 97.0f;
    private const string strIsChase = "IsChase";
    private const string strIsAttack = "IsAttack";
    private const string strDie = "Die";
    private const string strWalk = "Wake";
    private const string strRun = "Run";
    private const string strAttack = "Attack";

    // Use this for initialization
    void Start()
    {
        playerTrans = GameManager.GetInstance().GetPlayer().transform;
        wayPoint = new Vector3[wayPointCount];
        eEnemyState = EnemyBehavState.PATROL;
        anim = GetComponent<Animator>();
        nav = this.gameObject.GetComponent<NavMeshAgent>();
        WayPointInit();
        Init();
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            SphereCollider scoll = GetComponent<SphereCollider>();
            ResourceManager.GetInstance().MakeParticle(scoll.transform.position, "Effect_02", 2.0f);
            SoundManager.GetInstance().PlayOneshotClip("sword");
            Player player = GameManager.GetInstance().GetPlayer();
            player.DamageHp(_demage);
            Damaged(_demage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetEnemyStateByPlayerPos();         // 적 state 설정
        SetDestinationByEnemyState();       // state에 따른 목표위치 설정
    }

    //
    void SetEnemyStateByPlayerPos()
    {
        if (eEnemyState == EnemyBehavState.DIE)
            return;

        float distance = GetDistanceByTargetPosition(playerTrans.position);
        if (distance > chaseDistance)
        {
            StateTransition(EnemyBehavState.PATROL, patrolSpeed, false, false, strWalk);
        }
        else if (distance < attackDistance)
        {
            StateTransition(EnemyBehavState.ATTACK, 0.0f, false, true, strAttack);
        }
        else
        {
            StateTransition(EnemyBehavState.CHASE, chaseSpeed, true, false, strRun);
        }
    }

    void SetDestinationByEnemyState()
    {
        if (eEnemyState == EnemyBehavState.DIE)
            return;

        switch (eEnemyState)
        {
            case EnemyBehavState.CHASE: nav.SetDestination(playerTrans.position); break;
            case EnemyBehavState.PATROL: Patroll(); break;
            default: break;
        }
    }

    //
    void Init()
    {
        isDestination = true;
        eEnemyState = EnemyBehavState.PATROL;
        nav.speed = patrolSpeed;
        _hp = maxHP;
        curWayPoint = 0;
    }

    void WayPointInit()
    {
        wayPoint[0] = transform.position;
        wayPoint[1] = new Vector3(transform.position.x, transform.position.y, transform.position.z + wayPointHeight);
        wayPoint[2] = new Vector3(transform.position.x + wayPointWidth, transform.position.y, transform.position.z + wayPointHeight);
        wayPoint[3] = new Vector3(transform.position.x + wayPointWidth, transform.position.y, transform.position.z);
    }

    float GetDistanceByTargetPosition(Vector3 TargetPosition)
    {
        return Vector3.Distance(transform.position, TargetPosition);
    }

    void SetAnimation(bool isChase, bool isAttack, string strPlayAnim)
    {
        anim.SetBool(strIsChase, isChase);
        anim.SetBool(strIsAttack, isAttack);
        anim.Play(strPlayAnim);
    }

    void StateTransition(EnemyBehavState enemyState, float speed, bool isChase, bool isAttack, string strPlayAnim)
    {
        if (eEnemyState == enemyState)
            return;

        nav.speed = speed;
        SetAnimation(isChase, isAttack, strPlayAnim);
        eEnemyState = enemyState;

    }

    void Patroll()
    {
        nav.SetDestination(wayPoint[curWayPoint]);
        if (GetDistanceByTargetPosition(wayPoint[curWayPoint]) < destDistance && isDestination == false)
            isDestination = true;

        if (isDestination == false)
            return;

        ++curWayPoint;
        if (curWayPoint == wayPointCount)
            curWayPoint = 0;

        isDestination = false;
    }

    void Damaged(int damage)
    {
        _hp -= damage;
        if (0 > _hp)
            _hp = 0;

        _objectUI.DamageHp(damage);

        if (_hp <= 0 && eEnemyState != EnemyBehavState.DIE)
        {
            SetAnimation(false, false, strDie);
            nav.Stop();
            eEnemyState = EnemyBehavState.DIE;
        }
    }

    // get set
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