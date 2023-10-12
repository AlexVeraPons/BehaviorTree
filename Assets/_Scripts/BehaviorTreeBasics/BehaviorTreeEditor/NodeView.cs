using System;
using BehaviorTree;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
public class NodeView : UnityEditor.Experimental.GraphView.Node
{
    public Action<NodeView> OnNodeSelected;
    public Port input;
    public Port output;
    public BehaviorTree.Node node;
    public NodeView(BehaviorTree.Node node)
    {
        this.node = node;
        title = node.name;
        this.viewDataKey = node.Guid;

        style.left = node.position.x;
        style.top = node.position.y;

        CreateInputPorts();
        CreateOutputPorts();
    }

    private void CreateInputPorts()
    {
        if (node is CompositeNode)
        {
            input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            input.portName = "Input";
            inputContainer.Add(input);
        }
        else if (node is DecoratorNode)
        {
            input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            input.portName = "Input";
            inputContainer.Add(input);
        }
        else if (node is ActionNode)
        {
            input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            input.portName = "Input";
            inputContainer.Add(input);
        }
        // root node has no input port
    }
    private void CreateOutputPorts()
    {
        if (node is CompositeNode)
        {
            output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
            output.portName = "Output";
            outputContainer.Add(output);
        }
        else if (node is DecoratorNode)
        {
            output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            output.portName = "Output";
            outputContainer.Add(output);
        }
        else if (node is RootNode)
        {
            output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            output.portName = "Output";
            outputContainer.Add(output);
        }
        // ActionNode has no output port
    }


    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);

        node.position.x = newPos.xMin;
        node.position.y = newPos.yMin;
    }

    public override void OnSelected()
    {
        base.OnSelected();

        if (OnNodeSelected != null)
        {
            OnNodeSelected(this);
        }
    }
}
