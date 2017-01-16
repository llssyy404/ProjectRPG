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
    //STATE_DIE,
    MAX_STATE_TYPE
}

public class State<TEntity> {
    protected STATE_TYPE _stateType = STATE_TYPE.STATE_PATROL;

    //STATE_TYPE stateType { get { return _stateType; } set { _stateType = stateType; } }
    protected virtual bool StartState(TEntity entity) { return false; }
    protected virtual bool ProcessState(TEntity entity) { return false; }
    protected virtual bool FinishState(TEntity entity) { return false; }
}

public class EnemyPatrolState : State<EnumyState>
{
    protected override bool StartState(EnumyState entity)
    {
        return false;
    }

    protected override bool ProcessState(EnumyState entity)
    {
        return false;
    }

    protected override bool FinishState(EnumyState entity)
    {
        return false;
    }
}

public class EnemyChaseState : State<EnumyState>
{
    protected override bool StartState(EnumyState entity)
    {
        return false;
    }

    protected override bool ProcessState(EnumyState entity)
    {
        return false;
    }

    protected override bool FinishState(EnumyState entity)
    {
        return false;
    }
}

public class EnemyAttackState : State<EnumyState>
{
    protected override bool StartState(EnumyState entity)
    {
        return false;
    }

    protected override bool ProcessState(EnumyState entity)
    {
        return false;
    }

    protected override bool FinishState(EnumyState entity)
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