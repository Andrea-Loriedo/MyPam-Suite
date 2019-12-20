using UnityEngine;
using UnityEditor;
 
// Simple tool to enable/disable custom defines (e.g. log enabler)
public class Definer : EditorWindow
{
    [MenuItem("Tools/Definer")]
    private static void OpenWindow()
    {
        const float wndWidth = 200.0f;
        const float wndHeight = 200.0f;
        var pos = new Vector2(0.5f * (Screen.currentResolution.width - wndWidth),
                              0.5f * (Screen.currentResolution.height - wndHeight));
        var window = GetWindow<Definer>();
        window.titleContent = new GUIContent("Definer");
        window.position = new Rect(pos, new Vector2(wndWidth, wndHeight));
    }
 
    private void OnGUI()
    {
        if (GUILayout.Button("Enable"))
        {
            EditorUtils.AddDefineIfNecessary("ENABLE_LOGS", BuildTargetGroup.Standalone);
        }
        if (GUILayout.Button("Disable"))
        {
            EditorUtils.RemoveDefineIfNecessary("ENABLE_LOGS", BuildTargetGroup.Standalone);
        }
    }
}
