using UnityEngine;
using BehaviorTree;
public class NodeBlackboard : BlackBoard
{
    [SerializeField] private Transform _origin;
    [SerializeField] private float _distance;
    [SerializeField] private Transform _target;

    void Start()
    {
      Initialize();   
    }
    public override void Initialize()
    {
        SetData("Origin", _origin);
        SetData("Target", _target);
        SetData("Distance", _distance); 
    }
}
