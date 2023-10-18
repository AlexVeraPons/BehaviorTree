using UnityEngine;

namespace BehaviorTree
{
    class DebugLogNode : ActionNode
    {
        public string message;

        protected override void OnStart()
        {
            state = NodeState.Running;
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            Debug.Log(message);
            state = NodeState.Success;
            return state;
        }
    }
}