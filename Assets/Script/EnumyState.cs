using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using ConstNameSpace;

public class EnumyState : MonoBehaviour
{
    private Vector3[] _wayPoint;
    private int _curWayPoint = 0;
    private bool _isDestination = true;
    private int _wayPointCount = 4;
    private Player player;
    private NavMeshAgent nav;
    private Animator anim;
    private StateMachine<EnumyState> _stateMachine;

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


    public ObjectInfo Info;

    // Use this for initialization
    void Start()
    {
        player = GameManager.GetInstance().GetPlayer();
        _wayPoint = new Vector3[_wayPointCount];
        anim = GetComponent<Animator>();
        nav = this.gameObject.GetComponent<NavMeshAgent>();
        Init();
        WayPointInit();
        _stateMachine = new StateMachine<EnumyState>();
        _stateMachine.Change(this, GameManager.GetInstance().enemyStateManager.GetEnemyState(STATE_TYPE.STATE_PATROL));

        Info = InfoManager.GetInstance().Enemy;

        StartCoroutine("PlaySound");
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            SphereCollider scoll = GetComponent<SphereCollider>();
            ResourceManager.GetInstance().MakeParticle(coll.transform.position, "Hits/CFXM_GroundHit+Text", 2.0f);
            SoundManager.GetInstance().PlayOneshotClip("Exertions/Ed_Attack_1");
            Player player = GameManager.GetInstance().GetPlayer();
            player.DamageHp(_demage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetEnemyStateByPlayerPos();         // 적 state 설정
        _stateMachine.ProcessState(this);
    }

    //
    void SetEnemyStateByPlayerPos()
    {
        if (_stateMachine.GetCurStateType() == STATE_TYPE.STATE_DIE)
            return;

        float distance = GetDistanceByTargetPosition(player.transform.position);
        if (distance > chaseDistance || player.GetHP() <= 0)
        {
            _stateMachine.Change(this, GameManager.GetInstance().enemyStateManager.GetEnemyState(STATE_TYPE.STATE_PATROL));
        }
        else if (distance < attackDistance)
        {
            _stateMachine.Change(this, GameManager.GetInstance().enemyStateManager.GetEnemyState(STATE_TYPE.STATE_ATTACK));
        }
        else
        {
            _stateMachine.Change(this, GameManager.GetInstance().enemyStateManager.GetEnemyState(STATE_TYPE.STATE_CHASE));
            
        }
    }

    //
    void Init()
    {
        _isDestination = true;
        nav.speed = patrolSpeed;
        _hp = maxHP;
        _curWayPoint = 0;
    }

    void WayPointInit()
    {
        _wayPoint[0] = transform.position;
        _wayPoint[1] = new Vector3(transform.position.x, transform.position.y, transform.position.z + wayPointHeight);
        _wayPoint[2] = new Vector3(transform.position.x + wayPointWidth, transform.position.y, transform.position.z + wayPointHeight);
        _wayPoint[3] = new Vector3(transform.position.x + wayPointWidth, transform.position.y, transform.position.z);
    }

    public float GetDistanceByTargetPosition(Vector3 TargetPosition)
    {
        return Vector3.Distance(transform.position, TargetPosition);
    }

    void SetAnimation(bool isChase, bool isAttack, string strPlayAnim)
    {
        anim.SetBool(strIsChase, isChase);
        anim.SetBool(strIsAttack, isAttack);
        anim.Play(strPlayAnim);
    }

    public void StateTransition(STATE_TYPE enemyState, float speed, bool isChase, bool isAttack, string strPlayAnim)
    {
        if (_stateMachine.GetCurStateType() == enemyState)
            return;

        nav.speed = speed;
        SetAnimation(isChase, isAttack, strPlayAnim);
    }

    public void Patroll()
    {
        nav.SetDestination(_wayPoint[_curWayPoint]);
        if (GetDistanceByTargetPosition(_wayPoint[_curWayPoint]) < destDistance && _isDestination == false)
            _isDestination = true;

        if (_isDestination == false)
            return;

        ++_curWayPoint;
        if (_curWayPoint == _wayPointCount)
            _curWayPoint = 0;

        _isDestination = false;
    }

    void Damaged(int damage)
    {
        _hp -= damage;
        if (0 > _hp)
            _hp = 0;

        _objectUI.DamageHp(damage);

        if (_hp <= 0 )
        {
            _stateMachine.Change(this, GameManager.GetInstance().enemyStateManager.GetEnemyState(STATE_TYPE.STATE_DIE));
        }
    }

    // get set
    public int hp
    {
        get { return _hp; }
    }

    public StateMachine<EnumyState> stateMachine
    {
        get { return _stateMachine; }
    }

    private ObjectUI _objectUI;
    public void SetHpBar(ObjectUI objectUI)
    {
        _objectUI = objectUI;
        _objectUI.Init(_hp);
    }

    //public Vector3 GetWayPoint(int wayPointCount)
    //{
    //    return _wayPoint[wayPointCount];
    //}

    //public int curWayPoint
    //{
    //    get { return _curWayPoint; }
    //    set { _curWayPoint = curWayPoint; }
    //}

    //public bool isDestination
    //{
    //    get { return _isDestination; }
    //    set { _isDestination = isDestination; }
    //}

    //public int wayPointCount
    //{
    //    get { return _wayPointCount; }
    //}

    private IEnumerator PlaySound()
    {
        while(true)
        {

            if (_stateMachine.GetCurStateType() != STATE_TYPE.STATE_CHASE)
                yield return null;
            else
            {
                SoundManager.GetInstance().PlayOneshotClip("discover");
                yield return new WaitForSeconds(2.0f);
            }
        }
    }

}