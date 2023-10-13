using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
  public abstract class CompositeNode : Node
  {
    [HideInInspector] public List<Node> children = new List<Node>();
    public void AddChild(Node child)
    {
      children.Add(child);
    }

    public override Node Clone()
    {
      CompositeNode node = Instantiate(this);
      node.children = children.ConvertAll(c => c.Clone());
      return node;
    }
  }
}

