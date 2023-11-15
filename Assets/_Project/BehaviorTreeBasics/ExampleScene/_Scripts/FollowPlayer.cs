using UnityEngine;
using BehaviorTree;

public class FollowPlayer : ActionNode
{
    private float _speed;
    private Transform _transform;
    private Transform _playerTransform;
    private float _distance;

    protected override void OnStart()
    {
        _distance = (float)blackBoard.GetData("Distance");
        _transform = (Transform)blackBoard.GetData("Origin");
        _playerTransform = (Transform)blackBoard.GetData("PlayerTransform");
        _speed = (float)blackBoard.GetData("Speed");
    }

    protected override void OnStop()
    {
    }

    protected override NodeState OnUpdate()
    {
            _transform.position = Vector2.MoveTowards(_transform.position, _playerTransform.position, _speed * Time.deltaTime);
            return NodeState.Success;
    }
}
