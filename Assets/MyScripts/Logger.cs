#if ENABLE_LOGS
#define LOGGER
#endif

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngineInternal;
using System.Diagnostics;

// Enables Debug functions based on whether the "ENABLE_LOGS" macro is defined 

public static class Logger {
    
    [System.Diagnostics.Conditional("ENABLE_LOGS")]
    public static void Debug(object message, UnityEngine.Object context = null)
    {   
        UnityEngine.Debug.Log(message, context);
    }

    [System.Diagnostics.Conditional("ENABLE_LOGS")]
    public static void DebugError(object message, UnityEngine.Object context = null) 
    {
        UnityEngine.Debug.LogError(message, context);
    }

    [System.Diagnostics.Conditional("ENABLE_LOGS")] 
    public static void LogWarning (object message, UnityEngine.Object context = null)
    {   
        UnityEngine.Debug.LogWarning (message.ToString (), context);
    }

    [System.Diagnostics.Conditional("ENABLE_LOGS")]
    public static void LogFormat(string message, params object[] args) 
    {
	    UnityEngine.Debug.LogFormat(message, args);
    }

    [System.Diagnostics.Conditional("ENABLE_LOGS")]
    public static void LogErrorFormat(string message, params object[] args) 
    {
	    UnityEngine.Debug.LogErrorFormat(message, args);
    }

    [System.Diagnostics.Conditional("ENABLE_LOGS")] 
    public static void DrawLine(Vector3 start, Vector3 end, Color color = default(Color), float duration = 0.0f, bool depthTest = true)
    {
 	    UnityEngine.Debug.DrawLine(start, end, color, duration, depthTest);
    } 
	
    [System.Diagnostics.Conditional("ENABLE_LOGS")]
    public static void DrawRay(Vector3 start, Vector3 dir, Color color = default(Color), float duration = 0.0f, bool depthTest = true)
    {
	    UnityEngine.Debug.DrawRay(start, dir, color, duration, depthTest);
    }
}