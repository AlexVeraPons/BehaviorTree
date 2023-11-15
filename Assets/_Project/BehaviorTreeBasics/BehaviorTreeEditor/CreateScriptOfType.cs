using UnityEngine;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using System.IO;

public abstract class CreateScriptOfType
{
    protected static void CreateScript(CreateScriptOfType instance)
    {
        var path = GetSelectedPathOrFallback();
        var tempAssetPath = Path.Combine(path, "NewScript.cs.temp"); // Temporary asset

        File.WriteAllText(tempAssetPath, "// Temporary file content");
        AssetDatabase.Refresh();

        var endNameEditAction = ScriptableObject.CreateInstance<CreateScriptOfTypeAsset>();
        endNameEditAction.defaultScriptContent = instance.GetDefaultScriptContent();

        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
            0,
            endNameEditAction,
            tempAssetPath,
            null,
            null);
    }


    private static string GetSelectedPathOrFallback()
    {
        string path = "Assets";

        foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
        {
            path = AssetDatabase.GetAssetPath(obj);
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
                break;
            }
        }
        return path;
    }

    protected abstract string GetDefaultScriptContent();

    private class CreateScriptOfTypeAsset : EndNameEditAction
    {
        public string defaultScriptContent;

        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            AssetDatabase.DeleteAsset(resourceFile); // Delete the temporary file

            File.WriteAllText(pathName, defaultScriptContent.Replace("#SCRIPTNAME#", Path.GetFileNameWithoutExtension(pathName)));

            AssetDatabase.ImportAsset(pathName);
            ProjectWindowUtil.ShowCreatedAsset(AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object)));
        }
    }
}



