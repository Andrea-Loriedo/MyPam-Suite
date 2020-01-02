using UnityEngine;
using UnityEditor;
 
// Simple tool to add/remove custom defines (e.g. log enabler), accessible from the Tools menu
public class TestModeDefiner : EditorWindow
{
    [MenuItem("Tools/Test Mode")]
    private static void OpenWindow()
    {
        const float wndWidth = 200.0f;
        const float wndHeight = 200.0f;
        var pos = new Vector2(0.5f * (Screen.currentResolution.width - wndWidth),
                              0.5f * (Screen.currentResolution.height - wndHeight));
        var window = GetWindow<TestModeDefiner>();
        window.titleContent = new GUIContent("Test Mode");
        window.position = new Rect(pos, new Vector2(wndWidth, wndHeight));
    }
 
    private void OnGUI()
    {
        if (GUILayout.Button("Enable"))
        {
            EditorUtils.AddDefineIfNecessary("ENABLE_TESTING", BuildTargetGroup.Standalone);
        }

        if (GUILayout.Button("Disable"))
        {
            EditorUtils.RemoveDefineIfNecessary("ENABLE_TESTING", BuildTargetGroup.Standalone);
        }
    }
}
