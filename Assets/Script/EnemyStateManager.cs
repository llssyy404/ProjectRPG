using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager
{
    private State<EnumyState>[] _arrEnemyState;

    public EnemyStateManager()
    {
        _arrEnemyState = new State<EnumyState>[(int)STATE_TYPE.MAX_STATE_TYPE];
        _arrEnemyState[(int)STATE_TYPE.STATE_IDLE] = new EnemyIdleState();
        _arrEnemyState[(int)STATE_TYPE.STATE_PATROL] = new EnemyPatrolState();
        _arrEnemyState[(int)STATE_TYPE.STATE_CHASE] = new EnemyChaseState();
        _arrEnemyState[(int)STATE_TYPE.STATE_ATTACK] = new EnemyAttackState();
        _arrEnemyState[(int)STATE_TYPE.STATE_DEMAGED] = new EnemyDemagedState();
        _arrEnemyState[(int)STATE_TYPE.STATE_DIE] = new EnemyDieState();
    }

    public State<EnumyState> GetEnemyState(STATE_TYPE stateType) { return _arrEnemyState[(int)stateType]; }
}