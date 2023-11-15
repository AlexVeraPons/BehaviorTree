using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using BehaviorTree;
using Tree = BehaviorTree.Tree;
using Node = BehaviorTree.Node;
using System.Collections.Generic;
using System.Linq;
using System;
using Vector2 = UnityEngine.Vector2;

public class BehaviorTreeGraph : GraphView
{
    public Action<NodeView> OnNodeSelected;
    public new class UxmlFactory : UxmlFactory<BehaviorTreeGraph, GraphView.UxmlTraits> { }
    Tree _tree;
    public BehaviorTreeGraph()
    {
        Insert(0, new GridBackground());

        // Manipulators for the graph view to act accordingly
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new RectangleSelector());

        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/_Project/BehaviorTreeBasics/BehaviorTreeEditor/Editor/BehaviorTreeView.uss");
        styleSheets.Add(styleSheet);
    }
    NodeView FindNodeView(Node node)
    {
        return GetNodeByGuid(node.Guid) as NodeView;
    }

    internal void LoadTree(Tree tree)
    {
        _tree = tree;

        if (_tree == null)
        {
            return;
        }

        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;

        if (_tree.rootNode == null)
        {
            tree.rootNode = _tree.CreateNode(typeof(RootNode)) as RootNode;
            EditorUtility.SetDirty(_tree);// what is this?
        }

        // Create node view
        _tree.nodes.ForEach(node => AddElement(CreateNodeView(node)));

        // Create edges
        foreach (var node in _tree.nodes)
        {
            var children = _tree.GetChildren(node);
            children.ForEach(child =>
            {

                NodeView parentView = FindNodeView(node);
                NodeView childView = FindNodeView(child);


                Edge edge = parentView.output.ConnectTo(childView.input);
                AddElement(edge);
            });
        }
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        //Reorganize node lists baeed on position
        if (graphViewChange.movedElements != null)
        {
            ReloadTreeNodePositionsInList();
        }


        // Remove nodes that need removing
        if (graphViewChange.elementsToRemove != null)
        {
            RemoveElements(graphViewChange);
        }


        // Add edges that need adding and create the connection in the tree
        if (graphViewChange.edgesToCreate != null)
        {
            CreateEdges(graphViewChange);
        }

        return graphViewChange;
    }

    private void ReloadTreeNodePositionsInList()
    {
        foreach (var node in _tree.nodes)
        {
            if (node is CompositeNode compositeNode)
            {
                if (compositeNode.children != null)
                {
                    compositeNode.ReorganizeListByPosition();
                }
            }
        }
    }

    private void CreateEdges(GraphViewChange graphViewChange)
    {
        foreach (var edge in graphViewChange.edgesToCreate)
        {
            NodeView parentView = edge.output.node as NodeView;
            NodeView childView = edge.input.node as NodeView;
            _tree.AddChild(parentView.node, childView.node);
        }
    }

    private void RemoveElements(GraphViewChange graphViewChange)
    {
        foreach (var element in graphViewChange.elementsToRemove)
        {
            if (element is NodeView nodeView)
            {
                if (nodeView.node == _tree.rootNode)
                    continue;

                _tree.DeleteNode(nodeView.node);
            }

            Edge edge = element as Edge;

            if (edge != null)
            {
                NodeView parentView = edge.output.node as NodeView;
                NodeView childView = edge.input.node as NodeView;
                _tree.RemoveChild(parentView.node, childView.node);
            }
        }
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        if (_tree == null)
        {
            return;
        }

        var actionNode = TypeCache.GetTypesDerivedFrom<ActionNode>();
        
        Vector2 mousePos = evt.mousePosition;
        mousePos = this.contentViewContainer.WorldToLocal(mousePos);

        foreach (var nodeType in actionNode)
        {
            evt.menu.AppendAction($"Add Node/ActionNode/{nodeType.Name}", (a) => CreateNode(nodeType, mousePos));
        }

        var compositeNode = TypeCache.GetTypesDerivedFrom<BehaviorTree.CompositeNode>();

        foreach (var nodeType in compositeNode)
        {
            evt.menu.AppendAction($"Add Node/CompositeNode/{nodeType.Name}", (a) => CreateNode(nodeType,mousePos));
        }

        var decoratorNode = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
        foreach (var nodeType in decoratorNode)
        {
            evt.menu.AppendAction($"Add Node/DecoratorNode/{nodeType.Name}", (a) => CreateNode(nodeType,mousePos));
        }
    }

    private void CreateNode(System.Type type, Vector2 position)
    {
        Node node = _tree.CreateNode(type);
        node.position = position;
        AddElement(CreateNodeView(node));
    }

    private GraphElement CreateNodeView(BehaviorTree.Node node)
    {
        NodeView nodeView = new NodeView(node);
        nodeView.OnNodeSelected = OnNodeSelected;
        return nodeView;
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(port => port.direction != startPort.direction
        && port.node != startPort.node).ToList();
    }

}
