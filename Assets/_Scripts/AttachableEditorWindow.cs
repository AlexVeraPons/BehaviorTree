// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using BehaviorTree;
// using UnityEditor.PackageManager.UI;
// using System;
// using System.Numerics;
// using Vector2 = UnityEngine.Vector2;
// public class AttachableEditorWindow
// {
//     public Rect windowSize => _windowSize;
//     public int id = 1;
//     private Rect _windowSize;
//     static Rect _defaultWindowSize = new Rect(10, 10, 100, 100);

//     private BasicAttachable _basicAttachable;
    
//     public GameObject gameObject => _gameObject;
//     private GameObject _gameObject;
//     internal Vector2 position;

//     public AttachableEditorWindow(BasicAttachable basicAttachable,GameObject gameObj)
//     {
//         _basicAttachable = basicAttachable;
//         _windowSize = _defaultWindowSize;
//         _gameObject = gameObj;
//     }

//     public AttachableEditorWindow(BasicAttachable basicAttachable)
//     {
//         _basicAttachable = basicAttachable;
//         _windowSize = _defaultWindowSize;
//     }

//      public void DrawWindow()
//     {
//         _windowSize = GUI.Window(id, _windowSize, DrawNodeWindow, _gameObject.name);
//         position = _windowSize.position;
//     }

//     void DrawNodeWindow(int id)
//     {
//         // Draw the contents of the window here
//         // button that attaches another basic attachable to this one
//         if (GUI.Button(new Rect(10, 20, 80, 20), "Attach"))
//         {
//             // get the selected game object
//             // get the basic attachable component from the selected game object
//             // attach the selected object to this one
//             _basicAttachable.Attach();
//         }

//         // Allow the window to be dragged
//         GUI.DragWindow();
//     }
// }

