using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckIfEnemyInRange : ActionNode
{
    [SerializeField] private float _range = 2f;
    [SerializeField] private Transform _target;

    protected override void OnStart()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnStop()
    {
        throw new System.NotImplementedException();
    }

    protected override NodeState OnUpdate()
    {
         if (Vector3.Distance(transform.position, _target.position) < _range)
        {
            return NodeState.Success;
        }
s
        return NodeState.Failure;
    }
}