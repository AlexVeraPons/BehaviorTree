using System;
using System.Linq;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using BehaviorTree;
using System.Collections.Generic;
using Vector2 = UnityEngine.Vector2;
using Tree = BehaviorTree.Tree;
using Node = BehaviorTree.Node;

public class BehaviorTreeGraph : GraphView
{
    private const string StyleSheetPath = "Assets/_Project/BehaviorTreeBasics/BehaviorTreeEditor/Editor/BehaviorTreeView.uss";
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

        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(StyleSheetPath);
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
                compositeNode?.ReorganizeListByPosition();
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

        Vector2 mousePos = evt.mousePosition;
        mousePos = this.contentViewContainer.WorldToLocal(mousePos);

        AppendNodeActions<ActionNode>("Add Node", mousePos, evt);
        AppendNodeActions<DecoratorNode>("Add Node", mousePos, evt);
        AppendNodeActions<CompositeNode>("Add Node", mousePos, evt);
    }

    private void AppendNodeActions<T>(string menuPath, Vector2 mousePosition, ContextualMenuPopulateEvent evt)
    {
        var nodeTypes = TypeCache.GetTypesDerivedFrom<T>();
        foreach (var nodeType in nodeTypes)
        {
            evt.menu.AppendAction($"{menuPath}/{nodeType.Name}", (a) => CreateNode(nodeType, mousePosition));
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
