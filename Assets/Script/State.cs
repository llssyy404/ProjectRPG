using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATE_TYPE
{
    //STATE_IDLE = 0,
    STATE_PATROL = 0,
    STATE_CHASE,
    STATE_ATTACK,
    //STATE_DEMAGED,
    STATE_DIE,
    MAX_STATE_TYPE
}

public abstract class State<TEntity> {
    protected STATE_TYPE _stateType = STATE_TYPE.STATE_PATROL;

    public STATE_TYPE stateType { get { return _stateType; } set { _stateType = stateType; } }
    public virtual bool StartState(TEntity entity) { return false; }
    public virtual bool ProcessState(TEntity entity) { return false; }
    public virtual bool FinishState(TEntity entity) { return false; }
}

public class EnemyPatrolState : State<EnumyState>
{
    public EnemyPatrolState()
    {
        _stateType = STATE_TYPE.STATE_PATROL;
    }

    public override bool StartState(EnumyState entity)
    {
        return false;
    }

    public override bool ProcessState(EnumyState entity)
    {
        return false;
    }

    public override bool FinishState(EnumyState entity)
    {
        return false;
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
        return false;
    }

    public override bool ProcessState(EnumyState entity)
    {
        return false;
    }

    public override bool FinishState(EnumyState entity)
    {
        return false;
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
        return false;
    }

    public override bool ProcessState(EnumyState entity)
    {
        return false;
    }

    public override bool FinishState(EnumyState entity)
    {
        return false;
    }
}

public class EnemyStateManager
{
    State<EnumyState>[] _arrEnemyState;

    public EnemyStateManager()
    {
        _arrEnemyState = new State<EnumyState>[(int)STATE_TYPE.MAX_STATE_TYPE];
        _arrEnemyState[(int)STATE_TYPE.STATE_PATROL] = new EnemyPatrolState();
        _arrEnemyState[(int)STATE_TYPE.STATE_CHASE] = new EnemyChaseState();
        _arrEnemyState[(int)STATE_TYPE.STATE_ATTACK] = new EnemyAttackState();
    }

    public State<EnumyState> GetEnemyState( STATE_TYPE stateType) { return _arrEnemyState[(int)stateType]; }
}

public class EnemyStateCheckFunc
{
    static bool CheckPatrol(EnumyState enemyState, STATE_TYPE stateType)
    {
        if (stateType != STATE_TYPE.STATE_DIE)
            return false;

        return true;
    }
}

public class StateMachine<TEntity>
{
    private State<TEntity> curState;
    private State<TEntity> prevState;
    private State<TEntity> morePrevState;

    // get
    public STATE_TYPE CurStateType
    {
        get { return curState.stateType; }
    }

    public STATE_TYPE PrevStateType
    {
        get { return prevState.stateType; }
    }

    //
    public bool ChangeState(TEntity tEntity, State<TEntity> changeState)
    {
        if (changeState == null)
            return false;

        return Change(tEntity, changeState);
    }

    public bool Change(TEntity tEntity, State<TEntity> changeState)
    {
        if (changeState == null)
            return false;

        if (curState != null)
            curState.FinishState(tEntity);

        if(!changeState.StartState(tEntity))
        {
            if (curState != null)
                curState.StartState(tEntity);

            return true;
        }

        morePrevState = prevState;
        prevState = curState;
        curState = changeState;

        return true;
    }

    public void ProcessState(TEntity tEntity)
    {
        if (curState == null)
            return;

        curState.ProcessState(tEntity);
    }
}