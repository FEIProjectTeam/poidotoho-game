using UnityEngine;
using UnityEditor;

// source: https://discussions.unity.com/t/how-to-get-to-the-editor-to-display-a-childs-world-position/12069
public static class DebugMenu
{
    [MenuItem("Debug/Print Global Position")]
    public static void PrintGlobalPosition()
    {
        if (Selection.activeGameObject != null)
        {
            Debug.Log(Selection.activeGameObject.name + " is at " + Selection.activeGameObject.transform.position);
        }
    }
}
