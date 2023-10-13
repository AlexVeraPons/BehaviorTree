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
        //make the background not a rectangle
        style.borderTopLeftRadius = 10;
        style.borderTopRightRadius = 10;
        style.borderBottomLeftRadius = 10;
        style.borderBottomRightRadius = 10;

        style.width = 200;
        style.height = 100;
        style.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 1);


        //delete the extension container
        inputContainer.RemoveFromHierarchy();
        outputContainer.RemoveFromHierarchy();
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
        if (node is CompositeNode)
        {
            titleContainer.style.backgroundColor = new Color(0.0f, 0.4784f, 0.3451f, 1.0f);
        }
        else if (node is DecoratorNode)
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


        // Create new input container
        var newInputContainer = new VisualElement();
        // make the inputcontainer on the center
        newInputContainer.style.alignItems = Align.Center;
        newInputContainer.style.justifyContent = Justify.Center;
        newInputContainer.style.height = 0;
        newInputContainer.style.position = Position.Absolute;
        newInputContainer.style.top = 0;
        newInputContainer.style.left = 10;
        newInputContainer.style.right = 0;

        // Create new output container
        var newOutputContainer = new VisualElement();
        newOutputContainer.style.alignItems = Align.Center;
        newOutputContainer.style.justifyContent = Justify.Center;
        newOutputContainer.style.height = 0;
        newOutputContainer.style.position = Position.Absolute;
        newOutputContainer.style.bottom = 0;
        newOutputContainer.style.left = 0;
        newOutputContainer.style.right = 10;

        // Add new containers to node
        newInputContainer.Add(inputContainer);
        newOutputContainer.Add(outputContainer);
        this.Add(newInputContainer);
        this.Add(newOutputContainer);
    }

    private void CreateInputPorts()
    {
        if (node is CompositeNode)
        {
            input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
            input.portName = "";
            inputContainer.Add(input);
        }
        else if (node is DecoratorNode)
        {
            input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
            input.portName = "";
            inputContainer.Add(input);
        }
        else if (node is ActionNode)
        {
            input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
            input.portName = "";
            inputContainer.Add(input);
        }
    }
    private void CreateOutputPorts()
    {
        if (node is CompositeNode)
        {
            output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
            output.portName = "";
            outputContainer.Add(output);
        }
        else if (node is DecoratorNode)
        {
            output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
            output.portName = "";
            outputContainer.Add(output);
        }
        else if (node is RootNode)
        {
            output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
            output.portName = "";
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
