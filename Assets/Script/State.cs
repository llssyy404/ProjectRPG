using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum STATE_TYPE
{
    STATE_IDLE = 0,
    STATE_PATROL,
    STATE_CHASE,
    STATE_ATTACK,
    STATE_DEMAGED,
    STATE_DIE,
    MAX_STATE_TYPE
}

public abstract class State<TEntity> {
    protected STATE_TYPE _stateType = STATE_TYPE.STATE_PATROL;

    public STATE_TYPE stateType { get { return _stateType; } set { _stateType = stateType; } }
    public virtual bool StartState(TEntity entity) { return false; }
    public virtual bool ProcessState(TEntity entity) { return false; }
    public virtual bool FinishState(TEntity entity) { return false; }
    //public virtual bool CheckState(TEntity entity) { return false; }
}

public class EnemyIdleState : State<EnumyState>
{
    public EnemyIdleState()
    {
        _stateType = STATE_TYPE.STATE_IDLE;
    }

    public override bool StartState(EnumyState entity)
    {
        return true;
    }

    public override bool ProcessState(EnumyState entity)
    {
        return true;
    }

    public override bool FinishState(EnumyState entity)
    {
        return true;
    }
}

public class EnemyPatrolState : State<EnumyState>
{
    public EnemyPatrolState()
    {
        _stateType = STATE_TYPE.STATE_PATROL;
    }

    public override bool StartState(EnumyState entity)
    {
        entity.StateTransition(STATE_TYPE.STATE_PATROL, 5.0f, false, false, "Walk");

        return true;
    }

    public override bool ProcessState(EnumyState entity)
    {
        entity.Patroll();

        return true;
    }

    public override bool FinishState(EnumyState entity)
    {
        return true;
    }
}

public class EnemyChaseState : State<EnumyState>
{
    public EnemyChaseState()
    {
        _stateType = STATE_TYPE.STATE_CHASE;
    }

    public override bool StartState(EnumyState entity)
    {
        entity.StateTransition(STATE_TYPE.STATE_CHASE, 7.0f, true, false, "Run");

        return true;
    }

    public override bool ProcessState(EnumyState entity)
    {
        Transform playerTrans = GameManager.GetInstance().GetPlayer().transform;
        NavMeshAgent nav = entity.gameObject.GetComponent<NavMeshAgent>();
        nav.SetDestination(playerTrans.position);

        return true;
    }

    public override bool FinishState(EnumyState entity)
    {
        return true;
    }
}

public class EnemyAttackState : State<EnumyState>
{
    public EnemyAttackState()
    {
        _stateType = STATE_TYPE.STATE_ATTACK;
    }

    public override bool StartState(EnumyState entity)
    {
        entity.StateTransition(STATE_TYPE.STATE_ATTACK, 0.0f, false, true, "Attack");

        return true;
    }

    public override bool ProcessState(EnumyState entity)
    {
        return true;
    }

    public override bool FinishState(EnumyState entity)
    {
        return true;
    }
}

public class EnemyDemagedState : State<EnumyState>
{
    public EnemyDemagedState()
    {
        _stateType = STATE_TYPE.STATE_DEMAGED;
    }

    public override bool StartState(EnumyState entity)
    {
        return true;
    }

    public override bool ProcessState(EnumyState entity)
    {
        return true;
    }

    public override bool FinishState(EnumyState entity)
    {
        return true;
    }
}

public class EnemyDieState : State<EnumyState>
{
    public EnemyDieState()
    {
        _stateType = STATE_TYPE.STATE_DIE;
    }

    public override bool StartState(EnumyState entity)
    {
        entity.StateTransition(STATE_TYPE.STATE_DIE, 0.0f, false, false, "Die");

        NavMeshAgent nav = entity.gameObject.GetComponent<NavMeshAgent>();
        nav.Stop();

        return true;
    }

    public override bool ProcessState(EnumyState entity)
    {
        return true;
    }

    public override bool FinishState(EnumyState entity)
    {
        return true;
    }
}