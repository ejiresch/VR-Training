using UnityEditor;
using UnityEngine;
using UnityEditor.ShortcutManagement;

public static class FindReferencesShortcut
{
    [Shortcut("Custom/Find References In Scene", KeyCode.F, ShortcutModifiers.Control)]
    static void FindRefsInScene()
    {
        var obj = Selection.activeObject;
        if (obj == null)
        {
            Debug.LogWarning("No object selected.");
            return;
        }

        EditorApplication.ExecuteMenuItem("Assets/Find References In Scene");
    }
}
