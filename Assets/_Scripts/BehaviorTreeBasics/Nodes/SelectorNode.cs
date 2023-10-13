using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class SelectorNode : CompositeNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            foreach (var child in children)
            {
                switch (child.Evaluate())
                {
                    case NodeState.Failure:
                        continue;
                    case NodeState.Running:
                        state = NodeState.Running;
                        return state;
                    case NodeState.Success:
                        state = NodeState.Success;
                        return state;
                }
            }

            state = NodeState.Failure;
            return state;
        }
    }
}
