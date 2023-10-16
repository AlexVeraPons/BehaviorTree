using System;
using BehaviorTree;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
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
        style.borderTopLeftRadius = 10;
        style.borderTopRightRadius = 10;
        style.borderBottomLeftRadius = 10;
        style.borderBottomRightRadius = 10;

        style.width = 200;
        style.height = 100;
        style.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 1);


        //delete the extension container
        base.inputContainer.RemoveFromHierarchy();
        base.outputContainer.RemoveFromHierarchy();
        titleButtonContainer.RemoveFromHierarchy();
        topContainer.RemoveFromHierarchy();


        SetTitleContainerStyle();

        CreatePortStyle();

        CreateInputPorts();
        CreateOutputPorts();

        node.OnStatusChanged += ChangeBackground;
    }

    private void ChangeBackground(NodeState state)
    {
        if(state == NodeState.Running)
        {
            titleContainer.style.backgroundColor = new Color(1.0f, 0.4784f, 0.3451f, 1.0f);
        }
        else
        {
            SetTitleContainerStyle();            
        }
    }

    private void SetTitleContainerStyle()
    {
        if (node is BehaviorTree.CompositeNode)
        {
            titleContainer.style.backgroundColor = new Color(0.0f, 0.4784f, 0.3451f, 1.0f);
        }
        else if (node is Sequencer)
        {
            titleContainer.style.backgroundColor = new Color(0.2235f, 0.3608f, 0.4196f, 1.0f);
        }
        else if (node is ActionNode)
        {
            titleContainer.style.backgroundColor = new Color(0.7333f, 0.2235f, 0.1451f, 1.0f);
        }
        else if (node is RootNode)
        {
            titleContainer.style.backgroundColor = new Color(0.7765f, 0.1804f, 0.3961f, 1.0f);
        }
        // separate the titlecontainer from the topof the container

    }
    private void CreatePortStyle()
    {
        // make the inputcontainer on the center
        inputContainer.style.alignItems = Align.Center;
        inputContainer.style.justifyContent = Justify.Center;
        inputContainer.style.position = Position.Absolute;
        inputContainer.style.top = -10;
        inputContainer.style.left = 10;
        inputContainer.style.right = 0;

        // Create new output container
        outputContainer.style.alignItems = Align.Center;
        outputContainer.style.justifyContent = Justify.Center;
        outputContainer.style.position = Position.Absolute;
        outputContainer.style.bottom = -10;
        outputContainer.style.left = 0;
        outputContainer.style.right = 10;

        // add the new container to the node
        this.hierarchy.Add(base.inputContainer);
        this.hierarchy.Add(base.outputContainer);

        //refresh the node
        RefreshPorts();
    }

    private void CreateInputPorts()
    {
        if (node is BehaviorTree.CompositeNode)
        {
            input = base.InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
            input.portName = "";
            base.inputContainer.Add(input);
        }
        else if (node is Sequencer)
        {
            input = base.InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
            input.portName = "";
            base.inputContainer.Add(input);
        }
        else if (node is ActionNode)
        {
            input = base.InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
            input.portName = "";
            base.inputContainer.Add(input);
        }
    }
    private void CreateOutputPorts()
    {
        if (node is BehaviorTree.CompositeNode)
        {
            output = base.InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
            output.portName = "";
            base.outputContainer.Add(output);
        }
        else if (node is Sequencer)
        {
            output = base.InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
            output.portName = "";
            base.outputContainer.Add(output);
        }
        else if (node is RootNode)
        {
            output = base.InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
            output.portName = "";
            base.outputContainer.Add(output);
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
