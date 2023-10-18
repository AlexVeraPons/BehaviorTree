using BehaviorTree;

namespace BehaviorTree
{
    public class RepeatNode : RootNode
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
}