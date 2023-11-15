using System.Diagnostics;
using BehaviorTree;
using UnityEngine;
using Debug = UnityEngine.Debug;
public class Inverter : DecoratorNode
{
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override NodeState OnUpdate()
    {
        child.Evaluate();

        if(child.state == NodeState.Failure)
        {
            state = NodeState.Success;
            return state;
        }
        else if(child.state == NodeState.Success)
        {
            state = NodeState.Failure;
            return state;
        }
        else
        {
            state = NodeState.Running;
            return state;
        }
    }
}
