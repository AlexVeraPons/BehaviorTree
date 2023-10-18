using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using BehaviorTree;
using Tree = BehaviorTree.Tree;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using System;

public class BehaviorTreeView : EditorWindow
{
    BehaviorTreeGraph _treeGraphView;
    BehaviorInspectorView _inspectorView;
    BlackboardInspectorView _blackboardInspectorView;


    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;


    [MenuItem("BehaviorTree/BehaviorTreeView")]
    public static void ShowExample()
    {
        BehaviorTreeView wnd = GetWindow<BehaviorTreeView>();
        wnd.titleContent = new GUIContent("BehaviorTreeView");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Instantiate UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/_Project/BehaviorTreeBasics/BehaviorTreeEditor/Editor//BehaviorTreeView.uxml");
        m_VisualTreeAsset.CloneTree(root);

        // Add stylesheet
        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/_Project/BehaviorTreeBasics/BehaviorTreeEditor/Editor//BehaviorTreeView.uss");
        root.styleSheets.Add(styleSheet);

        _treeGraphView = root.Q<BehaviorTreeGraph>();
        _inspectorView = root.Q<BehaviorInspectorView>();
        _treeGraphView.OnNodeSelected = OnNodeSelectionChanged;

        OnSelectionChange();
    }

    private void OnNodeSelectionChanged(NodeView view)
    {
        _inspectorView.UpdateSelection(view);
    }

    private void OnSelectionChange()
    {
        if (Selection.activeObject == null)
        {
            return;
        }

        if (Selection.activeObject is Tree)
        {
            Tree tree = Selection.activeObject as Tree;
            if (tree && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
            {
                _treeGraphView.LoadTree(tree);
                return;
            }
        }

        if (Selection.activeGameObject is not GameObject) //without this line, the editor will crash when selecting a gameobject
            return;

        Debug.Log(Selection.activeGameObject.name);
        
        if (Selection.activeGameObject.GetComponent<BehaviorTreeRunner>())
        {
            BehaviorTreeRunner runner = Selection.activeGameObject.GetComponent<BehaviorTreeRunner>();
            {
                _treeGraphView.LoadTree(runner.tree);
            }
        }

        if (Selection.activeGameObject.GetComponent<BlackBoard>() != null)
        {
            BlackBoard blackboard = Selection.activeGameObject.GetComponent<BlackBoard>();
            if (blackboard)
            {
                _blackboardInspectorView = GetWindow<BlackboardInspectorView>();
                _blackboardInspectorView.Show();
                _blackboardInspectorView.UpdateSelection(blackboard);
                return;
            }
        }

      

    }
}
