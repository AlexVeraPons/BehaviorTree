using BehaviorTree;

namespace BehaviorTree
{
    public class RepeatNode : Sequencer
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