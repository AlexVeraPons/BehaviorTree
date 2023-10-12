// using UnityEngine;
// using UnityEditor;
// using System.Collections.Generic;
// using BehaviorTree;
// using Tree = BehaviorTree.Tree;
// using UnityEditor.PackageManager.UI;
// using System;

// public class ExampleWindow : EditorWindow
// {
//     private List<Node> nodes = new List<Node>();
//     private List<Rect> windows = new List<Rect>();
//     private Node _root;

//     [MenuItem("Window/Node editor")]

//     static void ShowEditor()
//     {
//         ExampleWindow editor = EditorWindow.GetWindow<ExampleWindow>();
//     }

//     void OnGUI()
//     {
//         BeginWindows();

//         for (int i = 0; i < nodes.Count; i++)
//         {
//             windows[i] = GUI.Window(i, windows[i], DrawNodeWindow, nodes[i].GetType().ToString());
//         }

//         for (int i = 0; i < nodes.Count; i++)
//         {
//             Node node = nodes[i];
//             List<Node> children = node.GetChildren();

//             for (int j = 0; j < children.Count; j++)
//             {
//                 DrawNodeCurve(windows[i], windows[nodes.IndexOf(children[j])]);
//             }

//         }


//         EndWindows();
//     }

//     private void DrawNodeWindow(int id)
//     {
//         GUI.DragWindow();
//     }

//     void OnSelectionChange()
//     {
//         GameObject selected = Selection.activeGameObject;

//         nodes.Clear();
//         windows.Clear();

//         if (selected == null)
//         {
//             return;
//         }

//         if (selected.GetComponent<Tree>() != null)
//         {
//             Tree tree = selected.GetComponent<Tree>();
//             _root = tree.GetRoot();

//             nodes.Add(_root);
//             windows.Add(new Rect(10, 10, 200, 100));

//             AddChildrenToList(_root);

//             for (int i = 0; i < nodes.Count; i++)
//             {
//                 windows.Add(new Rect(10, 10, 200, 100));
//             }
//         }

//         Repaint();
//     }

//     private void AddChildrenToList(Node node)
//     {
//         if (node == null)
//         {
//             return;
//         }

//         if (node.GetChildren().Count > 0)
//         {
//             for (int i = 0; i < node.GetChildren().Count; i++)
//             {
//                 nodes.Add(node.GetChildren()[i]);
//                 AddChildrenToList(node.GetChildren()[i]);
//             }
//         }
   
//     }

//     void DrawNodeCurve(Rect start, Rect end)
//     {
//         Vector3 startPos = new Vector3(start.x + start.width / 2, start.y + start.height, 0);
//         Vector3 endPos = new Vector3(end.x + end.width / 2, end.y, 0);
//         Vector3 startTan = startPos + Vector3.down * 50;
//         Vector3 endTan = endPos + Vector3.up * 50;
//         Color shadowCol = new Color(0, 0, 0, 0.06f);
//         for (int i = 0; i < 3; i++) // Draw a shadow
//             Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
//         Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
//     }
// }


