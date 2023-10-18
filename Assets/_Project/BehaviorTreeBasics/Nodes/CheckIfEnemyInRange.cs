using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

namespace BehaviorTree
{
public class CheckIfEnemyInRange : ActionNode
{
    [SerializeField] private float _range = 2f;
    private Transform _target;
    private Transform _origin;
    protected override void OnStart()
    {
        _target = (Transform)blackBoard.GetData("Target");
        _origin = (Transform)blackBoard.GetData("Origin");
    }

    protected override void OnStop()
    {
    }

    protected override NodeState OnUpdate()
    {
        if (Vector3.Distance(_origin.position, _target.position) <= _range)
        {
            state = NodeState.Success;
            return state;
        }
        else
        {
            state = NodeState.Failure;
            return state;
        }
    }
}}
