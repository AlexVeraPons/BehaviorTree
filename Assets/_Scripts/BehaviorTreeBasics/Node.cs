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
        public event Action<NodeState> OnStatusChanged;
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

        private NodeState _prevState;
         public NodeState state;
        public bool _started = false;
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

            if (_prevState != state)
            {
                OnStatusChanged?.Invoke(state);
            }

            if (state == NodeState.Failure || state == NodeState.Success)
            {
                OnStop();
                _started = false;
            }

            _prevState = state;

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
