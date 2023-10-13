using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace BehaviorTree
{
    public enum NodeState
    {
        Running,
        Success,
        Failure
    }
    public abstract class Node : ScriptableObject
    {
        public Tree tree;
        protected BlackBoard blackBoard => GetBlackbaord();

        private BlackBoard GetBlackbaord()
        {
            if (tree == null)
            {
                return null;
            }

            return tree.blackboard;
        }

        public NodeState state = NodeState.Running;
        private bool _started = false;
        [HideInInspector] public string Guid;
        [HideInInspector] public Vector2 position;

        public virtual NodeState Evaluate()
        {
            if (!_started)
            {
                OnStart();
                _started = true;
            }

            state = OnUpdate();

            if (state == NodeState.Failure || state == NodeState.Success)
            {
                OnStop();
                _started = false;
            }

            return state;
        }

        public virtual Node Clone()
        {
            return Instantiate(this);
        }


        protected abstract NodeState OnUpdate();
        protected abstract void OnStart();
        protected abstract void OnStop();
    }

}
