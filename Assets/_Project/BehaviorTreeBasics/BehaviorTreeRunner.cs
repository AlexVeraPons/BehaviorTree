using UnityEngine;

namespace BehaviorTree
{
    public class BehaviorTreeRunner : MonoBehaviour
    {
        public Tree tree;
        public BlackBoard _blackBoard;
        private void Start()
        {
            tree.Clone();
            tree.blackboard = _blackBoard;
        }
        private void Update()
        {
            tree.Update();
        }
    }
}