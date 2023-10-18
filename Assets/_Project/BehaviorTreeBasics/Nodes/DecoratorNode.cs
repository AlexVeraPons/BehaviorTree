using UnityEngine;

namespace BehaviorTree
{
    public abstract class Sequencer : Node
    {
        [HideInInspector] public Node child;

        public override Node Clone()
        {
            Sequencer node = Instantiate(this);
            node.child = child.Clone();
            return node;
        }
    }
}

