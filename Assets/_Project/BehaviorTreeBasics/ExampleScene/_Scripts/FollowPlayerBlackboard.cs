using UnityEngine;
using BehaviorTree;
public class FollowPlayerBlackboard : BlackBoard
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _speed;
    [SerializeField] private float _distance;
    [SerializeField] private Transform _point1;
    [SerializeField] private Transform _point2;

    [SerializeField] private Sprite _alertSprite;
    [SerializeField] private Sprite _originalSprite;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        SetData("Origin", this.transform);
        SetData("PlayerTransform", _playerTransform);
        SetData("Speed", _speed);
        SetData("Distance", _distance);

        SetData("Point1", _point1);
        SetData("Point2", _point2);

        SetData("SpriteRenderer", _spriteRenderer);
        SetData("AlertSprite", _alertSprite);
        SetData("OriginalSprite", _originalSprite);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _distance);
    }
}
