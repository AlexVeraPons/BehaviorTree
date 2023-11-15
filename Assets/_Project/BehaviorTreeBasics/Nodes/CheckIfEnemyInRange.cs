using UnityEngine;
using BehaviorTree;

namespace BehaviorTree
{
    public class CheckIfEnemyInRange : ActionNode
    {
        private float _range = 2f;
        private Transform _target;
        private Transform _origin;
        protected override void OnStart()
        {
            _target = (Transform)blackBoard.GetData("PlayerTransform");
            _origin = (Transform)blackBoard.GetData("Origin");
            _range = (float)blackBoard.GetData("Distance");

            state = NodeState.Failure;
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            if (Vector3.Distance(_origin.position, _target.position) <= _range)
            {
                state = NodeState.Success;
                return state;
            }

            state = NodeState.Failure;
            return state;
        }
    }
}
