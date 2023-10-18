using UnityEngine;
using BehaviorTree;
using System.ComponentModel;

public class WaitNode : ActionNode
{
    public float waitTime;
    private float timer;

    protected override void OnStart()
    {
        timer = 0;
    }

    protected override void OnStop()
    {
        timer = 0;
    }

    protected override NodeState OnUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= waitTime)
        {
            return NodeState.Success;
        }
        
        return NodeState.Running;
    }
}