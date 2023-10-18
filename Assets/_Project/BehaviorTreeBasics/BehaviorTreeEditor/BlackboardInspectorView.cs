using UnityEditor;
using UnityEngine.UIElements;
using BehaviorTree;
using UnityEngine;

public class BlackboardInspectorView : EditorWindow
{
    private BehaviorTreeView parentEditor;
    private IMGUIContainer imguiContainer;
    Editor editor;

    public static void OpenWindow(BehaviorTreeView parentEditor)
    {
        var window = GetWindow<BlackboardInspectorView>("Blackboard Inspector");
        window.parentEditor = parentEditor;
    }

    public void CreateGUI()
    {

        //load the editor
        imguiContainer = new IMGUIContainer(() =>
        {
            if (editor != null)
            {
                editor.OnInspectorGUI();
            }
        });

        // make spacing consistent with other inspectors
        imguiContainer.style.marginLeft = 10;
        imguiContainer.style.marginRight = 10;
        imguiContainer.style.marginTop = 10;
        imguiContainer.style.marginBottom = 10;

        rootVisualElement.Add(imguiContainer);
    }

    internal void UpdateSelection(BlackBoard view)
    {
        UnityEngine.Object.DestroyImmediate(editor);
        editor = Editor.CreateEditor(view);

        if (imguiContainer != null)
        {
            imguiContainer.MarkDirtyRepaint();
        }
    }
}
