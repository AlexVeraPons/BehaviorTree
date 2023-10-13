using UnityEditor;
using UnityEngine.UIElements;
using BehaviorTree;
using UnityEngine;

public class BlackboardInspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<BlackboardInspectorView, VisualElement.UxmlTraits> { }

        Editor editor;
        public BlackboardInspectorView()
        {
        }

        internal void UpdateSelection(BlackBoard view)
        {
            Clear();
            
            UnityEngine.Object.DestroyImmediate(editor);
            editor = Editor.CreateEditor(view);
            IMGUIContainer container = new IMGUIContainer(() =>
                {
                    editor.OnInspectorGUI();
                });
            Add(container);
        }
    }
