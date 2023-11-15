using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class BlackBoard : MonoBehaviour
    {
        [SerializeField]private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        public void SetData(string key, object value)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext[key] = value;
            }
            else
            {
                _dataContext.Add(key, value);
            }
        }

        public object GetData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                return _dataContext[key];
            }
            else
            {
                Debug.LogError("Key not found in blackboard: " + key);
                return null;
            }
        }
    }
}




