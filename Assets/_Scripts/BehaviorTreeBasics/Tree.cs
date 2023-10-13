using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(menuName = "BehaviorTree/Tree", fileName = "NewTree")]
    public class Tree : ScriptableObject
    {
        public BlackBoard blackboard;
        public Node rootNode = null;
        public NodeState treeState = NodeState.Running;
        public Dictionary<string, object> dataContext = new Dictionary<string, object>();
        public List<Node> nodes = new List<Node>();

        public NodeState Update()
        {
            if (rootNode == null)
            {
                treeState = NodeState.Failure;
                return treeState;
            }

            if (treeState == NodeState.Running)
            {
                treeState = rootNode.Evaluate();
            }

            
            return treeState;
        }

        public Node CreateNode(System.Type type)
        {
            Node node = ScriptableObject.CreateInstance(type) as Node;
            node.name = type.Name;
            node.Guid = Guid.NewGuid().ToString();
            node.tree = this;
            nodes.Add(node);

            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();

            return node;
        }

        public void DeleteNode(Node node)
        {
            nodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }

        public void AddChild(Node parent, Node child)
        {
            DecoratorNode decoratorNode = parent as DecoratorNode;
            if (decoratorNode != null)
            {
                decoratorNode.child = child;
            }

            CompositeNode compositeNode = parent as CompositeNode;
            if (compositeNode != null)
            {
                compositeNode.children.Add(child);
            }

            RootNode rootNode = parent as RootNode;
            if (rootNode != null)
            {
                rootNode.child = child;
            }
        }

        public void RemoveChild(Node parent, Node child)
        {
            CompositeNode compositeNode = parent as CompositeNode;
            if (compositeNode != null)
            {
                compositeNode.children.Remove(child);
            }

            RootNode rootNode = parent as RootNode;
            if (rootNode != null)
            {
                rootNode.child = null;
            }

            DecoratorNode decoratorNode = parent as DecoratorNode;
            if (decoratorNode != null)
            {
                decoratorNode.child = null;
            }
        }

        public List<Node> GetChildren(Node parent)
        {
            List<Node> children = new List<Node>();

            DecoratorNode decorator = parent as DecoratorNode;
            if(decorator && decorator.child != null)
            {
                children.Add(decorator.child);
            }

            CompositeNode composite = parent as CompositeNode;

            if(composite)
            {
                return composite.children;
            }

            RootNode root = parent as RootNode;

            if(root && root.child != null)
            {
                children.Add(root.child);
            }

            return children;
        }

        public Tree Clone()
        {
            Tree tree = Instantiate(this);
            tree.rootNode = rootNode.Clone();
            return tree;
        }
    }
}
