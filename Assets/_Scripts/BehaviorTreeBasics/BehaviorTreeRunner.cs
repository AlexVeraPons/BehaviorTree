using UnityEngine;
using BehaviorTree;
using Tree = BehaviorTree.Tree;
public class BehaviorTreeRunner : MonoBehaviour
{
    public Tree _tree;

    private void Start()
    {
        _tree = _tree.Clone();
    }

    private void Update()
    {
        _tree.Update();
    }
}