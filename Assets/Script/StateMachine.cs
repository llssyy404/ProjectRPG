using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<TEntity>
{
    private State<TEntity> _curState;
    private State<TEntity> _prevState;
    private State<TEntity> _morePrevState;

    // get
    public STATE_TYPE GetCurStateType()
    {
        if (_curState == null)
            return STATE_TYPE.MAX_STATE_TYPE;

        return _curState.stateType;
    }

    public STATE_TYPE GetPrevStateType()
    {
        if (_prevState == null)
            return STATE_TYPE.MAX_STATE_TYPE;

        return _prevState.stateType;
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

        if (_curState != null)
            _curState.FinishState(tEntity);


        if (!changeState.StartState(tEntity))
        {
            //if (_curState != null)
            //    _curState.StartState(tEntity);

            return true;
        }

        _morePrevState = _prevState;
        _prevState = _curState;
        _curState = changeState;

        return true;
    }

    public void ProcessState(TEntity tEntity)
    {
        if (_curState == null)
            return;

        _curState.ProcessState(tEntity);
    }
}