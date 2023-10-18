using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

public class BehaviorInspectorView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<BehaviorInspectorView, VisualElement.UxmlTraits> { }

    Editor editor;
    public BehaviorInspectorView()
    {
    }

    internal void UpdateSelection(NodeView view)
    {
        Clear();

        UnityEngine.Object.DestroyImmediate(editor);
        editor = Editor.CreateEditor(view.node);
        IMGUIContainer container = new IMGUIContainer(() =>
            {
                editor.OnInspectorGUI();
            });
        Add(container);
    }
}
