using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public partial class DragAndDropWindow : EditorWindow
{
    [MenuItem("Window/Test Window")]
    static void ShowEditor()
    {
        DragAndDropWindow editor = EditorWindow.GetWindow<DragAndDropWindow>();
    }

    
    private void OnGUI()
    {
        GUILayout.BeginVertical(GUI.skin.box);
        // Section 1
        GUILayout.Label("Section 1", EditorStyles.boldLabel);
        

        GUILayout.EndVertical();

        GUILayout.BeginScrollView(Vector2.zero, false, true, GUILayout.Width(200), GUILayout.Height(200));

        // Section 2
        GUILayout.Label("Section 2", EditorStyles.boldLabel);
        // ... Other GUI elements for section 2

        GUILayout.EndScrollView();

        // ... And so on for other sections
    }

}
