using BehaviorTree;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class RepeatNode : DecoratorNode
{
    protected override void OnStart()
    {
        state = NodeState.Running;
    }

    protected override void OnStop()
    {
        state = NodeState.Running;
    }

    protected override NodeState OnUpdate()
    {
        child.Evaluate();

        state = NodeState.Running;
        return state;
    }
}
