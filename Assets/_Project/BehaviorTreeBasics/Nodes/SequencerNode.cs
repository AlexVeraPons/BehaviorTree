using BehaviorTree;
using UnityEngine;
public class SequencerNode : CompositeNode
{
    [HideInInspector] private int _currentChild;
    protected override void OnStart()
    {
        _currentChild = 0;
    }

    protected override void OnStop()
    {
    }

    protected override NodeState OnUpdate()
    {
        var child = children[_currentChild];

        switch (child.Evaluate())
        {
            case NodeState.Failure:
                state = NodeState.Failure;
                return state;
            case NodeState.Running:
                state = NodeState.Running;
                return state;
            case NodeState.Success:
                _currentChild++;
                break;
        }

        if (_currentChild == children.Count)
        {
            state = NodeState.Success;
            return state;
        }

        state = NodeState.Running;
        return state;

    }
}
