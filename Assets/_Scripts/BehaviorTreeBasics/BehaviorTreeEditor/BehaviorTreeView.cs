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
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/_Scripts/BehaviorTreeBasics/BehaviorTreeEditor/BehaviorTreeView.uxml");
        m_VisualTreeAsset.CloneTree(root);

        // Add stylesheet
        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/_Scripts/BehaviorTreeBasics/BehaviorTreeEditor/BehaviorTreeView.uss");
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
        Tree tree = Selection.activeObject as Tree;
        if (tree && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
        {
            _treeGraphView.LoadTree(tree);
        }
    }
}
