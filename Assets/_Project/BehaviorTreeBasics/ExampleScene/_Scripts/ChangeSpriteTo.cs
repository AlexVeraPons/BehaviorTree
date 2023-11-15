using UnityEngine;
using BehaviorTree;
public class ChangeSpriteTo : ActionNode
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] string _spriteToChangeName;
    private Sprite _spriteToChange;
    protected override void OnStart()
    {
        _spriteRenderer = (SpriteRenderer)blackBoard.GetData("SpriteRenderer");
        _spriteToChange = (Sprite)blackBoard.GetData(_spriteToChangeName);
    }

    protected override void OnStop()
    {
    }

    protected override NodeState OnUpdate()
    {
        _spriteRenderer.sprite = _spriteToChange;
        return NodeState.Success;
    }
}
