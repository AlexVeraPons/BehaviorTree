# User Manual

## What are Behavior Trees 

A behavior tree is a framework designed to guide the decision-making process of AI characters (NPCs) in games. It arranges and sequences a series of tasks or decisions in a tiered layout. This tree consists of various nodes, each outlining specific behaviors and actions for the AI to follow.

further reading: 
-[Behavior trees for AI: How they work (gamedeveloper.com)](https://www.gamedeveloper.com/programming/behavior-trees-for-ai-how-they-work)
[Introduction to behavior trees - Robohub](https://robohub.org/introduction-to-behavior-trees/)

# How to setup a GameObject to use a behavior tree

1. **Create or Prepare Your GameObject**:
    - In the Unity Editor, either create a new GameObject or select an existing GameObject that you want to control using a behavior tree. You can do this by right-clicking in the Hierarchy window and selecting `Create > GameObject`.
2. **Add the Behavior Tree Runner Component**:
    - With your GameObject selected, go to the Inspector window, click `Add Component`, and search for `BehaviorTreeRunner` (or the equivalent in the behavior tree system you are using). Add this component to your GameObject.
3. **Assign the Behavior Tree to the Runner**:
    - Once you have your behavior tree runner, drag and drop it into the `Tree` slot of the `BehaviorTreeRunner` component in your GameObject. This links your tree to the runner, which will manage the execution of the behaviors.
4. **Add the Blackboard Component**:
    - Similarly, add a `Blackboard` component to your GameObject (this might be called `Node Blackboard` or simply `Blackboard`, depending on the system). This component stores data that the behavior tree nodes will use to make decisions.
5. **Link the Blackboard to the Tree Runner**:
    - Drag and drop the blackboard component from your GameObject onto the `Black Board` slot in the `BehaviorTreeRunner`. This connects the data component to the tree execution component.
# How to Create Your Own Behavior Tree

1. **Start by Creating a Tree**: Begin in your project window. Right-click and navigate to `Create/BehaviorTree/Tree`. This will generate a new Scriptable Object named 'Tree'.
    
2. **Accessing the Tree**: To open and view your created tree, go to `BehaviorTree/BehaviorTreeView`. This action will display a window showcasing the root node of your new tree.
    
3. **Adding Nodes**: Right-click within the tree window to add nodes. This lets you build your behavior tree with various node types, each representing different actions or decisions.
    
4. **Connecting Nodes**: Connect your root node to other nodes to define the tree's flow. Simply drag from one node to another. This sets the path your AI will follow when making decisions.
    
5. **Nodes Execution Order**: Nodes in your behavior tree are executed from left to right. This order is important for determining how your AI evaluates and acts on different decisions or actions.
    
6. **Inspecting Nodes**: Select a node to view its properties. The inspector panel on the left side of the window will display settings and options for the selected node.
# How to create custome nodes
## Step 1: Determine the Type of Node

First, decide what type of node you need:

- **Action Node**: Performs specific tasks or actions.
- **Composite Node**: Manages multiple child nodes.
- **Decorator Node**: Modifies the behavior of its child node.
## Step 2: Create a New C# Script in Unity

- Create a new C# script.
- Implement `using BehaviorTree;`
- Name the script to reflect the node's purpose (e.g., `FindPlayerAction`, `SequenceManager`, `RepeatDecorator`).
## Step 3: Define Your Node Class

- Open the script in your C# editor.
- Define a new class that inherits from `ActionNode`, `CompositeNode`, or `DecoratorNode`, depending on your need.
## Step 4: Implement the Node’s Functionality

 Implement methods like `OnStart()`, `OnStop()`, and `OnUpdate()`. 
### OnStart()
- **When It's Called**: `OnStart` is invoked when the node begins its execution. This is the point at which the node transitions from an inactive state to an active one.
- **Typical Use**: It's commonly used for initialization tasks. For example, setting up variables, initializing timers, or performing setup actions that are needed before the node starts its main operation.
### OnStop()
- **When It's Called**: `OnStop` is called when the node ends its execution. This happens when the node has completed its task, either successfully or unsuccessfully, or if it's interrupted by another process.
- **Typical Use**: This method is usually used for cleanup activities. It might involve resetting variables, stopping ongoing actions initiated in `OnStart` or `OnUpdate`, or performing any necessary finalization steps after the node has finished running.
### OnUpdate()
- **When It's Called**: `OnUpdate` is called repeatedly as long as the node is active and running. It's invoked in every frame or cycle of the game loop during the node's execution `Onupdate` must return a state.
- **Typical Use**: This is where the main behavior or logic of the node is implemented. In `OnUpdate`, you define what the node actually does — for instance, checking conditions, executing actions, updating status, or deciding whether the node should succeed, fail, or continue running.
## Step 5: Manage Node States

- Ensure your node correctly updates its state to `Running`, `Success`, or `Failure` based on its functionality.
## Example of a DebugLogNode 
```csharp
using UnityEngine;
using BehaviorTree;
    class DebugLogNode : ActionNode
    {
        public string message; // displayed in the inspector

        protected override void OnStart()
        {
            state = NodeState.Running;
        }

        protected override void OnStop()
        {
        }

        protected override NodeState OnUpdate()
        {
            Debug.Log(message);
            state = NodeState.Success;
            return state;
        }
    }
```

# How to create a Blackboard 
## Step 1: Create a New C# Script in Unity
- Create a new C# script. Name it appropriately to reflect its purpose (e.g., `NodeBlackboard`).
- Implement `using BehaviorTree;`
## Step 2: Define Your Custom Blackboard Class

- Open the script in a C# editor.
- Define a new class that inherits from `BlackBoard`.
## Step 3: Declare Variables

- Add any specific variables (fields) you need in your blackboard.
- Use the SetData to add variables to the blackboard
## Example of a Blackboard

```csharp
using UnityEngine;
using BehaviorTree;
public class EnemyBlackboard : BlackBoard
{
    [SerializeField] private Transform _origin;
    [SerializeField] private float _distance;
    [SerializeField] private Transform _target;

    void Start()
    {
        SetData("Origin", _origin);
        SetData("Target", _target);
        SetData("Distance", _distance);  
    }
}
```

# How to access Blackboard variables from nodes
If the GameObject posseses a Blackboard and its added to the Tree Runner component use blackBoard.GetData() to access a variable **see example:**

```csharp
using UnityEngine;
using BehaviorTree;

public class CheckIfEnemyInRange : ActionNode
{
    [SerializeField] private float _range = 2f;
    private Transform _target;
    private Transform _origin;
    
    protected override void OnStart()
    {
        _target = (Transform)blackBoard.GetData("Target"); // here you get the data
        _origin = (Transform)blackBoard.GetData("Origin");
    }

    protected override void OnStop()
    {
    }

    protected override NodeState OnUpdate()
    {
        if (Vector3.Distance(_origin.position, _target.position) <= _range)
        {
            state = NodeState.Success;
            return state;
        }
        else
        {
            state = NodeState.Failure;
            return state;
        }
    }
}
```

# Controls for the Behavior Tree Editor window

- **Right-Click:** to add a node from the existing nodes in the project
- **Mouse-Wheel:** hold mousewheel and move mouse to drag around the tree view
- **Scroll:** Scroll with your wheel up and down to zommin or zoom out from the tree view
- **Receneter:** press A to recenter your screen to your nodes 
- **Delete:** press the delete button to delete a node or connection.
